console.log('HEllo!')

function handleElement(elem) {
  if ((elem.a).is(':checked')) {
    elem.b.show();
    elem.c.show().prop('required', true);
    elem.d.prop('required', true);
    elem.f.show();
    setTimeout(() => {
      elem.g.prop('required', true);
      console.log(elem.g)
    }, 500);
    elem.h.prop('required', true);
    elem.i.prop('required', true);
    elem.j.prop('required', true);
  } else {
    elem.b.hide();
    elem.c.hide().prop('required', false);
    elem.d.prop('required', false);
    elem.f.hide();
    elem.g.prop('required', false);
    elem.h.prop('required', false);
    elem.i.prop('required', false);
    elem.j.prop('required', false);
  }
};

$('form[data-role="question"] .ios-toggle').on('change',
  function () {
    var $checkbox = $(this);
    var $parent = $checkbox.closest('[data-role="question"]');
    var $inputRow = $parent.find('.inputRow');
    var $inputRequired = $inputRow.find('input');
    var $textarea = $parent.find('textarea');
    var $showHide = $parent.find('.showHide');
    var $regatKeuwordInput = $parent.find('.regateKeywords [data-dynalist="origin-container"] input');
    var $regatKeuwordInputAdded = $parent.find('.regateKeywords [data-dynalist="origin-section"] input');
    var $regatJsonInput = $parent.find($('.regateJson .container-fullw table .input-group input'));
    var $regatJsonSelect = $parent.find($('.regateJson .container-fullw table .input-group select'));

    handleElement({
      a: $checkbox,
      b: $inputRow,
      c: $inputRequired,
      d: $textarea,
      f: $showHide,
      g: $regatKeuwordInput,
      h: $regatKeuwordInputAdded,
      i: $regatJsonInput,
      j: $regatJsonSelect
    });

    $('[data-dynalist="origin-section"] + [data-dynalist="origin-container"] [data-dynalist="origin"]').attr('required', false);
  }
);

$('form[data-role="question"] button[type="submit"]').on('submit',
  function () {
    console.log('hi')
    var $parent = $(this)
    var $inputRow = $parent.find('.inputRow');
    var $inputRequired = $inputRow.find('input');
    var $textarea = $parent.find('textarea');
    var $checkbox = $parent.find('.ios-toggle');
    var $showHide = $parent.find('.showHide');
    var $regatKeuwordInput = $parent.find('.regateKeywords [data-dynalist="origin-container"] input');
    var $regatKeuwordInputAdded = $parent.find('.regateKeywords [data-dynalist="origin-section"] input');
    var $regatJsonInput = $parent.find($('.regateJson .container-fullw table .input-group input'));
    var $regatJsonSelect = $parent.find($('.regateJson .container-fullw table .input-group select'));

    handleElement({
      a: $checkbox,
      b: $inputRow,
      c: $inputRequired,
      d: $textarea,
      f: $showHide,
      g: $regatKeuwordInput,
      h: $regatKeuwordInputAdded,
      i: $regatJsonInput,
      j: $regatJsonSelect
    });

    $('[data-dynalist="origin-section"] + [data-dynalist="origin-container"] [data-dynalist="origin"]').attr('required', false);
  }
);

$(document).ready(function () {
  var ancestor = $('form[data-role="question"]');
  $(ancestor).each(function () {
    var $parent = $(this)
    var $inputRow = $parent.find('.inputRow');
    var $inputRequired = $inputRow.find('input');
    var $textarea = $parent.find('textarea');
    var $checkbox = $parent.find('.ios-toggle');
    var $showHide = $parent.find('.showHide');
    var $regatKeuwordInput = $parent.find('.regateKeywords [data-dynalist="origin-container"] input');
    var $regatKeuwordInputAdded = $parent.find('.regateKeywords [data-dynalist="origin-section"] input');
    var $regatJsonInput = $parent.find($('.regateJson .container-fullw table .input-group input'));
    var $regatJsonSelect = $parent.find($('.regateJson .container-fullw table .input-group select'));

    handleElement({
      a: $checkbox,
      b: $inputRow,
      c: $inputRequired,
      d: $textarea,
      f: $showHide,
      g: $regatKeuwordInput,
      h: $regatKeuwordInputAdded,
      i: $regatJsonInput,
      j: $regatJsonSelect
    });

    $('[data-dynalist="origin-section"] + [data-dynalist="origin-container"] [data-dynalist="origin"]').attr('required', false);
  });
  console.log("ready!");
});