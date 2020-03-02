function drowBPMN(target , state,minlength,minbreadth,jsonList){
    // These parameters need to be set before defining the templates.
    var MINLENGTH = minlength; // this controls the minimum length of any swimlane
    var MINBREADTH = minbreadth; // this controls the minimum breadth of any non-collapsed swimlane
    var chackVis = false;
    var nodeId = '';
    var angle =''


    if (state == "Horizontal"){
        angle  =  270
    }else{
        angle  =  0
    }



  //////////////////////////
  ///////// helper /////////
  //////////////////////////
  function colorizedHover(element, colorBox1, colorBox2, colorText, fontStyle, zIndex) {
    element.fromNode.findObject("Shape").fill = colorBox2;
    element.fromNode.findObject("Shape").stroke = colorText;
    element.fromNode.findObject("shape-lable").stroke = colorText;
    element.toNode.findObject("Shape").fill = colorBox2;
    element.toNode.findObject("Shape").stroke = colorText;
    element.toNode.findObject("shape-lable").stroke = colorText;
    element.findObject("SHAPE").stroke = colorBox1;
    element.findObject("SHAPE-lable-text").font = fontStyle;
    element.findObject("SHAPE-lable").fill = colorBox1;
    element.findObject("SHAPE-lable").stroke = colorBox1;
    element.findObject("SHAPE-arrow").stroke = colorBox1;
    element.findObject("SHAPE-arrow").fill = colorBox1;
    element.findObject("link").zOrder = zIndex
  }

  // this may be called to force the lanes to be laid out again
  function relayoutLanes() {
      myDiagram.nodes.each(function(lane) {
        if (!(lane instanceof go.Group)) return;
        if (lane.category === "Pool") return;
        lane.layout.isValidLayout = false;  // force it to be invalid
      });
      myDiagram.layoutDiagram();
    }

  // this is called after nodes have been moved or lanes resized, to layout all of the Pool Groups again
  function relayoutDiagram() {
      myDiagram.layout.invalidateLayout();
      myDiagram.findTopLevelGroups().each(function(g) {
         if (g.category === "Pool") g.layout.invalidateLayout(); 
        });
      myDiagram.layoutDiagram();
    }

  // compute the minimum size of a Pool Group needed to hold all of the Lane Groups
  function computeMinPoolSize(pool) {
      // assert(pool instanceof go.Group && pool.category === "Pool");
      
      if (state == "Horizontal"){
          var len = MINLENGTH;
          pool.memberParts.each(function(lane) {
            // pools ought to only contain lanes, not plain Nodes
            if (!(lane instanceof go.Group)) return;
            var holder = lane.placeholder;
            if (holder !== null) {
              var sz = holder.actualBounds;
              len = Math.max(len, sz.width);
            }
          });
          return new go.Size(len, NaN);
        }else{          
          var len = MINLENGTH;
          pool.memberParts.each(function (lane) {
            // pools ought to only contain lanes, not plain Nodes
            if (!(lane instanceof go.Group)) return;
            var holder = lane.placeholder;
            if (holder !== null) {
              var sz = holder.actualBounds;
              len = Math.max(len, sz.height);
            }
          });
          return new go.Size(NaN, len);
        }
    }
    // compute the minimum size for a particular Lane Group
  function computeLaneSize(lane) {
      // assert(lane instanceof go.Group && lane.category !== "Pool");
      var sz = computeMinLaneSize(lane);
      if (lane.isSubGraphExpanded) {
        var holder = lane.placeholder;
        if (holder !== null) {
          var hsz = holder.actualBounds;
          sz.height = Math.max(sz.height, hsz.height);
        }
      }
      // minimum breadth needs to be big enough to hold the header
      var hdr = lane.findObject("HEADER");
      if (hdr !== null) sz.height = Math.max(sz.height, hdr.actualBounds.height);
      return sz;
    }
  // determine the minimum size of a Lane Group, even if collapsed
  function computeMinLaneSize(lane) {
      if (!lane.isSubGraphExpanded) return new go.Size(MINLENGTH, 1);
      return new go.Size(MINLENGTH, MINBREADTH);
    }
    // define a custom ResizingTool to limit how far one can shrink a lane Group
  function LaneResizingTool() {
      go.ResizingTool.call(this);
    }
  go.Diagram.inherit(LaneResizingTool, go.ResizingTool); 
  

    LaneResizingTool.prototype.isLengthening = function () {
      if (state == "Horizontal" ){
        return (this.handle.alignment === go.Spot.Center);
      }else{
        return (this.handle.alignment === go.Spot.Right);
      }
    };
  
    /** @override */
    LaneResizingTool.prototype.computeMinSize = function() {
      var lane = this.adornedObject.part;
      // assert(lane instanceof go.Group && lane.category !== "Pool");
      var msz = computeMinLaneSize(lane);  // get the absolute minimum size
      if (this.isLengthening()) {  // compute the minimum length of all lanes
        var sz = computeMinPoolSize(lane.containingGroup);
        msz.width = Math.max(msz.width, sz.width);
      } else {  // find the minimum size of this single lane
        var sz = computeLaneSize(lane);
        msz.width = Math.max(msz.width, sz.width);
        msz.height = Math.max(msz.height, sz.height);
      }
      return msz;
    };

    /** @override */
    LaneResizingTool.prototype.resize = function(newr) {
      var lane = this.adornedObject.part;
      if (this.isLengthening()) {  // changing the length of all of the lanes
        lane.containingGroup.memberParts.each(function(lane) {
          if (!(lane instanceof go.Group)) return;
          var shape = lane.resizeObject;
          if (shape !== null) {  // set its desiredSize length, but leave each breadth alone
            shape.width = newr.width;
          }
        });
      } else {  // changing the breadth of a single lane
        go.ResizingTool.prototype.resize.call(this, newr);
      }
      relayoutDiagram();  // now that the lane has changed size, layout the pool again
    };
  // end LaneResizingTool class

  function PoolLayout() {
    if(state == "Horizontal"){
      go.GridLayout.call(this);
      this.cellSize = new go.Size(1, 1);
      this.wrappingColumn = 1;
      this.wrappingWidth = Infinity;
      this.isRealtime = false;  // don't continuously layout while dragging
      this.alignment = go.GridLayout.Position;
      // This sorts based on the location of each Group.
      // This is useful when Groups can be moved up and down in order to change their order.
      this.comparer = function(a, b) {
        var ay = a.location.y;
        var by = b.location.y;
        if (isNaN(ay) || isNaN(by)) return 0;
        if (ay < by) return -1;
        if (ay > by) return 1;
        return 0;
      };
    }else{
      go.GridLayout.call(this);
      this.cellSize = new go.Size(50, 1);
      this.wrappingColumn = Infinity;
      this.wrappingWidth = Infinity;
      this.isRealtime = false; // don't continuously layout while dragging
      this.alignment = go.GridLayout.Position;
      // This sorts based on the location of each Group.
      // This is useful when Groups can be moved up and down in order to change their order.
      this.comparer = function (a, b) {
        var ax = a.location.x;
        var bx = b.location.x;
        if (isNaN(ax) || isNaN(bx)) return 0;
        if (ax < bx) return -1;
        if (ax > bx) return 1;
        return 0;
      };
    }
    }
    go.Diagram.inherit(PoolLayout, go.GridLayout);


    /** @override */
    PoolLayout.prototype.doLayout = function(coll) {
      var diagram = this.diagram;
      if (diagram === null) return;
      diagram.startTransaction("PoolLayout");
      var pool = this.group;
      if (pool !== null && pool.category === "Pool") {
        // make sure all of the Group Shapes are big enough
        var minsize = computeMinPoolSize(pool);
        if (state == "Horizontal"){
          pool.memberParts.each(function(lane) {
            if (!(lane instanceof go.Group)) return;
            if (lane.category !== "Pool") {
              var shape = lane.resizeObject;
              if (shape !== null) {  // change the desiredSize to be big enough in both directions
                var sz = computeLaneSize(lane);
                shape.width = (isNaN(shape.width) ? minsize.width : Math.max(shape.width, minsize.width));
                shape.height = (!isNaN(shape.height)) ? Math.max(shape.height, sz.height) : sz.height;
                var cell = lane.resizeCellSize;
                if (!isNaN(shape.width) && !isNaN(cell.width) && cell.width > 0) shape.width = Math.ceil(shape.width / cell.width) * cell.width;
                if (!isNaN(shape.height) && !isNaN(cell.height) && cell.height > 0) shape.height = Math.ceil(shape.height / cell.height) * cell.height;
              }
            }
          });

        }else{
          pool.memberParts.each(function (lane) {
            if (!(lane instanceof go.Group)) return;
            if (lane.category !== "Pool") {
              var shape = lane.resizeObject;
              if (shape !== null) { // change the desiredSize to be big enough in both directions
                var sz = computeLaneSize(lane);
                shape.width = (!isNaN(shape.width)) ? Math.max(shape.width, sz.width) : sz.width;
                shape.height = (isNaN(shape.height) ? minsize.height : Math.max(shape.height, minsize.height));
                var cell = lane.resizeCellSize;
                if (!isNaN(shape.width) && !isNaN(cell.width) && cell.width > 0) shape.width = Math.ceil(shape.width / cell.width) * cell.width;
                if (!isNaN(shape.height) && !isNaN(cell.height) && cell.height > 0) shape.height = Math.ceil(shape.height / cell.height) * cell.height;
              }
            }
          });
        }
      }
      // now do all of the usual stuff, according to whatever properties have been set on this GridLayout
      go.GridLayout.prototype.doLayout.call(this, coll);
      diagram.commitTransaction("PoolLayout");
    };
    // end PoolL

  //////////////////////////
  ///////// helper /////////
  //////////////////////////

  function init() {
    if (window.goSamples) goSamples(); // init for these samples -- you don't need to call this
    var $ = go.GraphObject.make;
 
 
 
    myDiagram =
      $(go.Diagram, target, {
        // start everything in the middle of the viewport
        initialContentAlignment: go.Spot.Center,
        // use a custom ResizingTool (along with a custom ResizeAdornment on each Group)
        resizingTool: new LaneResizingTool(),
        // use a simple layout that ignores links to stack the top-level Pool Groups next to each other
        layout: $(PoolLayout),
        // don't allow dropping onto the diagram's background unless they are all Groups (lanes or pools)
        mouseDragOver: function (e) {
          if (!e.diagram.selection.all(function (n) {
              return n instanceof go.Group;
            })) {
            e.diagram.currentCursor = 'not-allowed';
          }
        },
        mouseDrop: function (e) {
          if (!e.diagram.selection.all(function (n) {
              return n instanceof go.Group;
            })) {
            e.diagram.currentTool.doCancel();
          }
        },
        // a clipboard copied node is pasted into the original node's group (i.e. lane).
        "commandHandler.copiesGroupKey": true,
        // automatically re-layout the swim lanes after dragging the selection
        "SelectionMoved": relayoutDiagram, // this DiagramEvent listener is
        "SelectionCopied": relayoutDiagram, // defined above
        "animationManager.isEnabled": false,
        // enable undo & redo
        "undoManager.isEnabled": true
      });
    myDiagram.toolManager.mouseMoveTools.insertAt(0, new LinkLabelOnPathDraggingTool());
    // this is a Part.dragComputation function for limiting where a Node may be dragged
    function stayInGroup(part, pt, gridpt) {
      // don't constrain top-level nodes
      var grp = part.containingGroup;
      if (grp === null) return pt;
      // try to stay within the background Shape of the Group
      var back = grp.resizeObject;
      if (back === null) return pt;
      // allow dragging a Node out of a Group if the Shift key is down
      if (part.diagram.lastInput.shift) return pt;
      var p1 = back.getDocumentPoint(go.Spot.TopLeft);
      var p2 = back.getDocumentPoint(go.Spot.BottomRight);
      var b = part.actualBounds;
      var loc = part.location;
      // find the padding inside the group's placeholder that is around the member parts
      var m = grp.placeholder.padding;
       // now limit the location appropriately
       var x = Math.max(p1.x + m.left, Math.min(pt.x, p2.x - m.right - b.width - 1)) + (loc.x-b.x);
       var y = Math.max(p1.y + m.top, Math.min(pt.y, p2.y - m.bottom - b.height - 1)) + (loc.y-b.y);
       return new go.Point(x, y);
    }
 
    myDiagram.nodeTemplate =
      $(go.Node, "Auto", {
       click: function (e, node) {
         if (nodeId == "") {
           myDiagram.nodes.each(function (node) {
             if (node.findObject("Shape") != null) {
               node.findObject("Shape").opacity = 0;
               node.findObject("shape-lable").opacity = 0;
             }
           })
           myDiagram.links.each(function (link) {
             if (link.findObject("SHAPE") != null) {
               link.findObject("SHAPE").opacity = 0;
               link.findObject("SHAPE-lable").opacity = 0;
               link.findObject("SHAPE-arrow").opacity = 0;
               link.findObject("SHAPE-lable-text").opacity = 0;
             }
           })
           node.findObject("Shape").opacity = 1;
           node.findObject("shape-lable").opacity = 1;
           node.findLinksOutOf().each(function (link) {
             link.findObject("SHAPE").opacity = 1;
             link.findObject("SHAPE-lable").opacity = 1;
             link.findObject("SHAPE-arrow").opacity = 1;
             link.findObject("SHAPE-lable-text").opacity = 1;
             link.toNode.findObject("Shape").opacity = 1;
             link.toNode.findObject("shape-lable").opacity = 1;
           });
           node.findLinksOutOf().each(function (link) {
             link.findObject("SHAPE").stroke = "#00cc00";
             link.findObject("SHAPE-lable").fill = "#00cc00";
             link.findObject("SHAPE-lable").stroke = "#00cc00";
             link.findObject("SHAPE-arrow").stroke = "#00cc00";
             link.findObject("SHAPE-arrow").fill = "#00cc00";
             link.findObject("link").zOrder = 20
             link.toNode.findObject("Shape").fill = "#00e6e6";
           });
           node.findObject("Shape").fill = "#00cc00";
           node.findObject("Shape").stroke = "#00cc00";
           node.findObject("shape-lable").stroke = "#fff";
           chackVis = true
           nodeId = node.key
         } else if (nodeId == node.key) {
           node.findLinksOutOf().each(function (link) {
             link.findObject("SHAPE").stroke = "#0066ff";
             link.findObject("SHAPE-lable").fill = "#0066ff";
             link.findObject("SHAPE-lable").stroke = "#0066ff";
             link.findObject("SHAPE-arrow").stroke = "#0066ff";
             link.findObject("SHAPE-arrow").fill = "#0066ff";
             link.findObject("link").zOrder = 10
             link.toNode.findObject("Shape").fill = "white";
           });
           node.findObject("Shape").fill = "white";
           node.findObject("Shape").stroke = "#444";
           node.findObject("shape-lable").stroke = "#444";
           myDiagram.nodes.each(function (node) {
             if (node.findObject("Shape") != null) {
               node.findObject("Shape").opacity = 1;
               node.findObject("shape-lable").opacity = 1;
             }
           })
           myDiagram.links.each(function (link) {
             if (link.findObject("SHAPE") != null) {
               link.findObject("SHAPE").opacity = 1;
               link.findObject("SHAPE-lable").opacity = 1;
               link.findObject("SHAPE-arrow").opacity = 1;
               link.findObject("SHAPE-lable-text").opacity = 1;
             }
           })
           chackVis = false;
           nodeId = "";
         } else {
          myDiagram.nodes.each(function (node) {
            if (node.findObject("Shape") != null) {
              node.findObject("Shape").opacity = 0;
              node.findObject("shape-lable").opacity = 0;
              node.findObject("Shape").fill = "#fff";
              node.findObject("Shape").stroke = "#444";
              node.findObject("shape-lable").stroke = "#444";
            }
          })
          myDiagram.links.each(function (link) {
            if (link.findObject("SHAPE") != null) {
              link.findObject("SHAPE").stroke = "#0066ff";
              link.findObject("SHAPE-lable").fill = "#0066ff";
              link.findObject("SHAPE-lable").stroke = "#0066ff";
              link.findObject("SHAPE-arrow").stroke = "#0066ff";
              link.findObject("SHAPE-arrow").fill = "#0066ff";
              link.findObject("SHAPE").opacity = 0;
              link.findObject("SHAPE-lable").opacity = 0;
              link.findObject("SHAPE-arrow").opacity = 0;
              link.findObject("SHAPE-lable-text").opacity = 0;
            }
          })
 
 
 
 
          node.findObject("Shape").opacity = 1;
          node.findObject("shape-lable").opacity = 1;
          node.findLinksOutOf().each(function (link) {
            link.findObject("SHAPE").opacity = 1;
            link.findObject("SHAPE-lable").opacity = 1;
            link.findObject("SHAPE-arrow").opacity = 1;
            link.findObject("SHAPE-lable-text").opacity = 1;
            link.toNode.findObject("Shape").opacity = 1;
            link.toNode.findObject("shape-lable").opacity = 1;
          });
          node.findLinksOutOf().each(function (link) {
            link.findObject("SHAPE").stroke = "#00cc00";
            link.findObject("SHAPE-lable").fill = "#00cc00";
            link.findObject("SHAPE-lable").stroke = "#00cc00";
            link.findObject("SHAPE-arrow").stroke = "#00cc00";
            link.findObject("SHAPE-arrow").fill = "#00cc00";
            link.findObject("link").zOrder = 20
            link.toNode.findObject("Shape").fill = "#00e6e6";
          });
          node.findObject("Shape").fill = "#00cc00";
          node.findObject("Shape").stroke = "#00cc00";
          node.findObject("shape-lable").stroke = "#fff";
          nodeId = node.key
         }
       },
       mouseEnter: function (e, node) {
 
       },
       mouseLeave: function (e, node) {
 
       },
       name: "Node",
       selectionAdorned: false,
     },
        new go.Binding("location", "loc", go.Point.parse).makeTwoWay(go.Point.stringify),
        $(go.Shape, "RoundedRectangle", {
          name: "Shape",
          fill: "white",
          stroke: "#444",
          //portId: "",
          cursor: "pointer",
          fromLinkable: true,
          toLinkable: true,
        }),
        $(go.TextBlock, {
            name: "shape-lable",
            //editable: true,
            column: 1,
            row: 1,
            stroke: "#444",
            width: 100,
            margin: 2,
            textAlign: "center"
          }, {
            font: "normal 12pt tahoma",
            margin: 10
          },
          new go.Binding("text", "text")), {
          dragComputation: stayInGroup
        } // limit dragging of Nodes to stay within the containing Group, defined above
      );
    go.Shape.defineFigureGenerator("Hex", function (shape, w, h) {
      var geo = new go.Geometry();
      geo.add(new go.PathFigure(10, 0)
        .add(new go.PathSegment(go.PathSegment.Line, w - 10, 0))
        .add(new go.PathSegment(go.PathSegment.Line, w, h / 2))
        .add(new go.PathSegment(go.PathSegment.Line, w - 10, h))
        .add(new go.PathSegment(go.PathSegment.Line, 10, h))
        .add(new go.PathSegment(go.PathSegment.Line, 0, h / 2).close()));
      // available space within this figure does not include the triangular portions on either side
      geo.spot1 = new go.Spot(0, 0, 10, 0);
      geo.spot2 = new go.Spot(1, 1, -10, 0);
      return geo;
    });
 
    function groupStyle() { // common settings for both Lane and Pool Groups
      return [{
          layerName: "Background", // all pools and lanes are always behind all nodes and links
          background: "transparent", // can grab anywhere in bounds
          movable: true, // allows users to re-order by dragging
          copyable: false, // can't copy lanes or pools
          avoidable: false, // don't impede AvoidsNodes routed Links
          minLocation: new go.Point(-Infinity, NaN), // only allow horizontal movement
          maxLocation: new go.Point(Infinity, NaN)
        },
        new go.Binding("location", "loc", go.Point.parse).makeTwoWay(go.Point.stringify)
      ];
    }
 
    // hide links between lanes when either lane is collapsed
    function updateCrossLaneLinks(group) {
      group.findExternalLinksConnected().each(function (l) {
        l.visible = (l.fromNode.isVisible() && l.toNode.isVisible());
      });
    }
 

      //////////////////////////
      ///////// Horizontal or Vertical /////////
      //////////////////////////
    if (state == "Horizontal" ){
      console.log("Horizontal")
    // each Group is a "swimlane" with a header on the left and a resizable lane on the right
        myDiagram.groupTemplate =
        $(go.Group, "Horizontal", groupStyle(),
            {
            selectionObjectName: "SHAPE",  // selecting a lane causes the body of the lane to be highlit, not the label
            resizable: true, resizeObjectName: "SHAPE",  // the custom resizeAdornmentTemplate only permits two kinds of resizing
            layout: $(go.GridLayout ,  // automatically lay out the lane's subgraph
                        {
                        isInitial: false,  // don't even do initial layout
                        isOngoing: false,  // don't invalidate layout when nodes or links are added or removed
                
                        cellSize: new go.Size(1, 1),
                        spacing: new go.Size(50, 50),
                        alignment: go.GridLayout.Position,
                        comparer: function(a, b) {  // can re-order tasks within a lane
                            var ay = a.location.y;
                            var by = b.location.y;
                            if (isNaN(ay) || isNaN(by)) return 0;
                            if (ay < by) return -1;
                            if (ay > by) return 1;
                            return 0;
                        }
                        
                        //layeringOption: go.LayeredDigraphLayout.LayerLongestPathSource
                        }),
            computesBoundsAfterDrag: true,  // needed to prevent recomputing Group.placeholder bounds too soon
            computesBoundsIncludingLinks: false,  // to reduce occurrences of links going briefly outside the lane
            computesBoundsIncludingLocation: true,  // to support empty space at top-left corner of lane
            handlesDragDropForMembers: true,  // don't need to define handlers on member Nodes and Links
            mouseDrop: function(e, grp) {  // dropping a copy of some Nodes and Links onto this Group adds them to this Group
                if (!e.shift) return;  // cannot change groups with an unmodified drag-and-drop
                // don't allow drag-and-dropping a mix of regular Nodes and Groups
                if (!e.diagram.selection.any(function(n) { return n instanceof go.Group; })) {
                var ok = grp.addMembers(grp.diagram.selection, true);
                if (ok) {
                    updateCrossLaneLinks(grp);
                } else {
                    grp.diagram.currentTool.doCancel();
                }
                } else {
                e.diagram.currentTool.doCancel();
                }
            },
            subGraphExpandedChanged: function(grp) {
                var shp = grp.resizeObject;
                if (grp.diagram.undoManager.isUndoingRedoing) return;
                if (grp.isSubGraphExpanded) {
                shp.height = grp._savedBreadth;
                } else {
                grp._savedBreadth = shp.height;
                shp.height = NaN;
                }
                updateCrossLaneLinks(grp);
            }
            },
            new go.Binding("isSubGraphExpanded", "expanded").makeTwoWay(),
            // the lane header consisting of a Shape and a TextBlock
            $(go.Panel, "Horizontal",
            { name: "HEADER",
                angle: 0,  // maybe rotate the header to read sideways going up
                alignment: go.Spot.Center,
                width: 200 },
            $(go.Panel, "Horizontal",  // this is hidden when the swimlane is collapsed
                new go.Binding("visible", "isSubGraphExpanded").ofObject(),
                $(go.Shape, "Diamond",
                { width: 0, height: 0, fill: "white" },
                new go.Binding("fill", "color")),
                $(go.TextBlock,  // the lane label
                { font: "bold 13pt tahoma",
                // editable: true,
                    margin: new go.Margin(20, 0, 20, 0) },
                new go.Binding("text", "text").makeTwoWay())
            ),
            $("SubGraphExpanderButton", { margin: 5 })  // but this remains always visible!
            ),  // end Horizontal Panel
            $(go.Panel, "Auto",  // the lane consisting of a background Shape and a Placeholder representing the subgraph
            $(go.Shape, "Rectangle",  // this is the resized object
                { name: "SHAPE", fill: "white" },
                new go.Binding("fill", "color"),
                new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify)),
            $(go.Placeholder,
                { padding: 12, alignment: go.Spot.Center }),
            $(go.TextBlock,  // this TextBlock is only seen when the swimlane is collapsed
                { name: "LABEL",
                font: "bold 13pt tahoma", 
                //editable: true,
                angle: 0, alignment: go.Spot.Center, margin: new go.Margin(2, 0, 0, 4) },
                new go.Binding("visible", "isSubGraphExpanded", function(e) { return !e; }).ofObject(),
                new go.Binding("text", "text").makeTwoWay())
            )  // end Auto Panel
        );  // end Group
        myDiagram.groupTemplate.resizeAdornmentTemplate =
        $(go.Adornment, "Spot",
            $(go.Placeholder),
            $(go.Shape,  // for changing the length of a lane
            {
                alignment: go.Spot.Center,
                desiredSize: new go.Size(7, 50),
                fill: "lightblue", stroke: "dodgerblue",
                cursor: "col-resize"
            },
            new go.Binding("visible", "", function(ad) {
                if (ad.adornedPart === null) return false;
                return ad.adornedPart.isSubGraphExpanded;
            }).ofObject()),
            $(go.Shape,  // for changing the breadth of a lane
            {
                alignment: go.Spot.Center,
                desiredSize: new go.Size(50, 7),
                fill: "lightblue", stroke: "dodgerblue",
                cursor: "row-resize"
            },
            new go.Binding("visible", "", function(ad) {
                if (ad.adornedPart === null) return false;
                return ad.adornedPart.isSubGraphExpanded;
            }).ofObject())
        );

        myDiagram.groupTemplateMap.add("Pool",
       $(go.Group, "Auto", groupStyle(),
         { // use a simple layout that ignores links to stack the "lane" Groups on top of each other
           layout: $(PoolLayout, { spacing: new go.Size(0, 0) })  // no space between lanes
         },
         $(go.Shape,
           { fill: "white" },
           new go.Binding("fill", "color")),
         $(go.Panel, "Table",
           { defaultColumnSeparatorStroke: "black" },
           $(go.Panel, "Horizontal",
             { column: 0, angle: angle },
             $(go.TextBlock,
               { font: "bold 26pt tahoma", 
               //editable: true, 
               margin: new go.Margin(20, 0, 20, 0) },
               new go.Binding("text").makeTwoWay())
           ),
           $(go.Placeholder,
             { column: 1 })
         )
       ));
    }else{
      console.log("Vertical")
        myDiagram.groupTemplate =
        $(go.Group, "Vertical", groupStyle(), {
            selectionObjectName: "SHAPE", // selecting a lane causes the body of the lane to be highlit, not the label
            resizable: true,
            resizeObjectName: "SHAPE", // the custom resizeAdornmentTemplate only permits two kinds of resizing
            layout: $(go.LayeredDigraphLayout, // automatically lay out the lane's subgraph
            {
                isInitial: false, // don't even do initial layout
                isOngoing: false, // don't invalidate layout when nodes or links are added or removed
                direction: 0,
                columnSpacing: 50,
                layeringOption: go.LayeredDigraphLayout.LayerLongestPathSource
            }),
            computesBoundsAfterDrag: true, // needed to prevent recomputing Group.placeholder bounds too soon
            computesBoundsIncludingLinks: false, // to reduce occurrences of links going briefly outside the lane
            computesBoundsIncludingLocation: true, // to support empty space at top-left corner of lane
            handlesDragDropForMembers: true, // don't need to define handlers on member Nodes and Links
            mouseDrop: function (e, grp) { // dropping a copy of some Nodes and Links onto this Group adds them to this Group
            if (!e.shift) return; // cannot change groups with an unmodified drag-and-drop
            // don't allow drag-and-dropping a mix of regular Nodes and Groups
            if (!e.diagram.selection.any(function (n) {
                return n instanceof go.Group;
                })) {
                var ok = grp.addMembers(grp.diagram.selection, true);
                if (ok) {
                updateCrossLaneLinks(grp);
                } else {
                grp.diagram.currentTool.doCancel();
                }
            } else {
                e.diagram.currentTool.doCancel();
            }
            },
            subGraphExpandedChanged: function (grp) {
            var shp = grp.resizeObject;
            if (grp.diagram.undoManager.isUndoingRedoing) return;
            if (grp.isSubGraphExpanded) {
                shp.height = grp._savedBreadth;
            } else {
                grp._savedBreadth = shp.height;
                shp.height = NaN;
            }
            updateCrossLaneLinks(grp);
            }
        },
        new go.Binding("isSubGraphExpanded", "expanded").makeTwoWay(),
        // the lane header consisting of a Shape and a TextBlock
        $(go.Panel, "Vertical", {
            name: "HEADER",
            angle: 0, // maybe rotate the header to read sideways going up
            alignment: go.Spot.Center
            },
            $(go.Panel, "Vertical", // this is hidden when the swimlane is collapsed
            new go.Binding("visible", "isSubGraphExpanded"),
            $(go.Shape, "Diamond", {
                width: 0,
                height: 0,
                fill: "white"
                },
                new go.Binding("fill", "color")),
            $(go.TextBlock, // the lane label
                {
                font: "bold 13pt tahoma",
                editable: true,
                width: 250,
                margin: new go.Margin(20, 0, 20, 0)
                },
                new go.Binding("text", "text").makeTwoWay())
            ),
            $("SubGraphExpanderButton", {
            margin: 5
            }) // but this remains always visible!
        ), // end Vertical Panel
        $(go.Panel, "Auto", // the lane consisting of a background Shape and a Placeholder representing the subgraph
            $(go.Shape, "Rectangle", // this is the resized object
            {
                name: "SHAPE",
                fill: "white"
            },
            new go.Binding("fill", "color"),
            new go.Binding("desiredSize", "size", go.Size.parse).makeTwoWay(go.Size.stringify)),
            $(go.Placeholder, {
            padding: 20,
            alignment: go.Spot.Center
            }),
            $(go.TextBlock, // this TextBlock is only seen when the swimlane is collapsed
            {
                name: "LABEL",
                font: "bold 13pt tahoma",
                editable: true,
                angle: 0,
                alignment: go.Spot.Center,
                margin: new go.Margin(20, 0, 0, 20)
            },
            new go.Binding("visible", "isSubGraphExpanded", function (e) {
                return !e;
            }).ofObject(),
            new go.Binding("text", "text").makeTwoWay())
        ) // end Auto Panel
        ); // end Group

        myDiagram.groupTemplate.resizeAdornmentTemplate =
        $(go.Adornment, "Spot",
        $(go.Placeholder),
        $(go.Shape, // for changing the length of a lane
            {
            alignment: go.Spot.Right,
            desiredSize: new go.Size(7, 50),
            fill: "lightblue",
            stroke: "dodgerblue",
            cursor: "col-resize"
            },
            new go.Binding("visible", "", function (ad) {
            if (ad.adornedPart === null) return false;
            return ad.adornedPart.isSubGraphExpanded;
            }).ofObject()),
        $(go.Shape, // for changing the breadth of a lane
            {
            alignment: go.Spot.Bottom,
            desiredSize: new go.Size(50, 7),
            fill: "lightblue",
            stroke: "dodgerblue",
            cursor: "row-resize"
            },
            new go.Binding("visible", "", function (ad) {
            if (ad.adornedPart === null) return false;
            return ad.adornedPart.isSubGraphExpanded;
            }).ofObject())
        );


        myDiagram.groupTemplateMap.add("Pool",
        $(go.Group, "Auto", groupStyle(), { // use a simple layout that ignores links to stack the "lane" Groups next to each other
            layout: $(PoolLayout, {
              spacing: new go.Size(0, 0)
            }) // no space between lanes
          },
          $(go.Shape, {
              fill: "white"
            },
            new go.Binding("fill", "color")),
          $(go.Panel, "Table", {
              defaultRowSeparatorStroke: "black"
            },
            $(go.Panel, "Horizontal", {
                row: 0,
                angle: 0
              },
              $(go.TextBlock, {
                  font: "bold 16pt tahoma",
                  editable: true,
                  margin: new go.Margin(20, 0, 20, 0)
                },
                new go.Binding("text").makeTwoWay())
            ),
            $(go.Placeholder, {
              row: 1
            })
          )
        ));
   
    }
 
     
 
    myDiagram.linkTemplate =
      $(go.Link, {
          name: "link",
          routing: go.Link.AvoidsNodes, // may be either Orthogonal or AvoidsNodes
          curve: go.Link.JumpGap,
          corner: 10,
          selectionAdorned: false,
          zOrder: 1,
          layerName: "Foreground",
          mouseEnter: function (e, link) {
            if (!chackVis) {
              colorizedHover(link, "#ff0000", "#ff0000", "#fff", "bold 13pt tahoma", 20)
            }
          },
          mouseLeave: function (e, link) {
            if (!chackVis) {
              colorizedHover(link, "#0066ff", "#fff", "#444", "normal 11pt tahoma", 10)
            }
          }
        },
        new go.Binding("zOrder"),
        $("Shape", {
          name: "SHAPE",
          stroke: "#0066ff",
          strokeWidth: 4
        }),
        $(go.Shape, {
          name: "SHAPE-arrow",
          toArrow: "Standard",
          stroke: "#0066ff",
          fill: "#0066ff",
          strokeWidth: 8,
        }),
        $(go.Panel, "Auto", // this whole Panel is a link label
          {
            _isLinkLabel: true
          }, // marks this Panel as being a draggable label
          $(go.Shape, "RoundedRectangle", {
            name: "SHAPE-lable",
            fill: "#0066ff",
            stroke: "#0066ff"
          }),
          $(go.TextBlock, {
              name: "SHAPE-lable-text",
              font: "normal 11pt tahoma",
              stroke: "#fff",
              margin: 3,
              //editable: true
            },
            new go.Binding("text", "text")),
          new go.Binding("segmentIndex").makeTwoWay(),
          new go.Binding("segmentFraction").makeTwoWay()
        )
      );
 
 
 
    ////////////////////////////////////////////////////////////////////////
    var linkDataArray = []
    var nodeDataArray = [];
 
 
    var json = jsonList
    var roleOrder = [];
 
    function setRoleOrder() {
      jQuery(json.Roles).each(function (i, el) {
        roleOrder.push((json.Roles).find(function (element) {
          return element.SortOrder === i + 1
        }))
      })
    }
 
    function makeElement(id, lable) {
      linkDataArray.push({
        key: id,
        text: lable,
        group: addChildToeRoles(id)
      })
    }
 
    function makeLink(el1, el2, text) {
      nodeDataArray.push({
        from: el1,
        to: el2,
        text: text
      })
    }
    linkDataArray.push({
      key: "MainPool",
      text: json.Title,
      isGroup: true,
      category: "Pool"
    })
 
    function makeGrp(string, i) {
      linkDataArray.push({
        key: i + string,
        text: string,
        isGroup: true,
        group: "MainPool",
        color: "lightblue"
      })
    }
 
    function makeElementList() {
      jQuery(json.Steps).each(function (i, el) {
        makeElement(el.Step, "مرحله" + el.Step +  ' : ' +el.Title   );
      })
    }
 
    function makeLinkList() {
      jQuery(json.Steps).each(function (i, el) {
        var actionTitle = ''
        if (el.F1 != null) {
          actionTitle = getNameOfAction(el.Step, "F1")
          createNode(el.Step, el.F1, actionTitle + "-F1")
        }
        if (el.F2 != null) {
          actionTitle = getNameOfAction(el.Step, "F2")
          createNode(el.Step, el.F2, actionTitle + "-F2")
        }
        if (el.F3 != null) {
          actionTitle = getNameOfAction(el.Step, "F3")
          createNode(el.Step, el.F3, actionTitle + "-F3")
        }
        if (el.F4 != null) {
          actionTitle = getNameOfAction(el.Step, "F4")
          createNode(el.Step, el.F4, actionTitle + "-F4")
        }
        if (el.F5 != null) {
          actionTitle = getNameOfAction(el.Step, "F5")
          createNode(el.Step, el.F5, actionTitle + "-F5")
        }
        if (el.F6 != null) {
          actionTitle = getNameOfAction(el.Step, "F6")
          createNode(el.Step, el.F6, actionTitle + "-F6")
        }
        if (el.F7 != null) {
          actionTitle = getNameOfAction(el.Step, "F7")
          createNode(el.Step, el.F7, actionTitle + "-F7")
        }
 
      })
    }
 
    function createNode(sourceID, targetID, string) {
      makeLink(sourceID, targetID, string);
    }
 
    function getNameOfAction(step, action) {
      var Actions
      jQuery(json.Actions).each(function (i, el) {
        if (step == el.Step && action == el.Action) {
          Actions = el.Title
        }
      })
      return Actions
    }
 
    function makeRolesList() {
      jQuery(roleOrder).each(function (i, el) {
        makeGrp(el.Title, i)
      })
    }
 
    function addChildToeRoles(id) {
      var lane;
      jQuery(roleOrder).each(function (i, el) {
        var listChilde = JSON.parse(el.StepsJson)
        if (listChilde.includes(id.toString())) {
          lane = i + el.Title
        }
      })
      return lane
    }
 
    setRoleOrder()
    makeRolesList()
    makeElementList()
    makeLinkList()
    ////////////////////////////////////////////////////////////////////////
    // define some sample graphs in some of the lanes
    myDiagram.model = new go.GraphLinksModel(
      linkDataArray,
      nodeDataArray
    );
 
    // force all lanes' layouts to be performed
    relayoutLanes();
  } // end init
  
  
  init()
  function save() {
    document.getElementById("mySavedModel").value = myDiagram.model.toJson();
    myDiagram.isModified = false;
  }
 
  function load() {
    myDiagram.model = go.Model.fromJson(document.getElementById("mySavedModel").value);
    myDiagram.delayInitialization(relayoutDiagram);
  }


}