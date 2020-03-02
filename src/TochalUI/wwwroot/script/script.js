function removeit(e) {
    document.querySelector(e).remove();
}

function optionChange() {
    let itemCount = document.querySelector("#itemCount");
    var icValue = itemCount.options[itemCount.selectedIndex].value;
    document.querySelector("#pricetag").innerHTML = icValue * pricetag;
    document.querySelector("#purchase").removeAttribute("disabled");
    document.querySelector("#countoption").setAttribute("disabled", true);
}

$(function () {
    $('[ticket-id]').hide();
    $('[ticket-id="1"]').show();
})

let viewBoxSelect = function (item) {

    // اضافه کردن استایل به دکمه بعد از کلیک
    let selected = document.querySelector(".sc-selected");
    if (selected.classList.contains("sc-selected")) {
        selected.classList.remove("sc-selected");
    }
    item.classList.add("sc-selected");

    $('[ticket-id]').hide();
    let ticketId = item.getAttribute("ticket-btn");

    $(`[ticket-id=${ticketId}]`).show();
    $(`[ticket-id=${ticketId}] .viewbox img`).hide();
    $(`[ticket-id=${ticketId}] .viewbox img`).show();
    $(`[ticket-id=${ticketId}] .sc-pname,[ticket-id=${ticketId}] .sc-p`).hide();
    $(`[ticket-id=${ticketId}] .sc-pname,[ticket-id=${ticketId}] .sc-p`).fadeIn(500);
};

let toggleNav = function (item) {
    setTimeout(() => {
        document
            .querySelector("#backtoggle")
            .setAttribute("style", "display:unset !important");
    }, 500);
    item.setAttribute("style", "visibility:hidden !important");
    $(".fullscreennav")
        .css("display", "flex")
        .animate({ "margin-right": "+=40vw" });
    document
        .querySelector("body")
        .setAttribute("style", "height: 100%; overflow:hidden;");
    $("#fsblur")
        .addClass("fsblur")
        .fadeTo(1000, 1);
};

let toggleNavClose = function (item) {
    setTimeout(() => {
        document
            .querySelector("#toggler")
            .setAttribute("style", "visibility:visible");
        $(".fullscreennav").css("display", "none");
        document
            .querySelector("body")
            .setAttribute("style", "height: unset; overflow-y:unset");
    }, 1000);
    item.setAttribute("style", "visibility:hidden !important");
    $(".fullscreennav").animate({ "margin-right": "-=40vw" });
    $("#fsblur").fadeOut(1000);
};

$(function () {
    let announce = 0;
    announceSlides();
    function announceSlides() {
        let announcement = document.querySelectorAll(".announcement");
        for (let i = 0; i < announcement.length; i++) {
            announcement[i].style.display = "none";
        }
        announce++;
        if (announce > announcement.length) {
            announce = 1;
        }
        announcement[announce - 1].style.display = "grid";
        setTimeout(announceSlides, 5000);
    }
})

let seasonIndex = 0;
let seasonSlider = function (num) {
    let seasonimg = $(".s-img");
    let seasontext = $(".s-text");
    seasonimg.hide();
    seasontext.hide();
    $(seasonimg[num]).show("fade");
    $(seasontext[num]).show("fade");
    switch (num) {
        case 0:
            $(".seasons").css("box-shadow", "#20bf6b 0px 0px 0px 4px inset");
            break;

        case 1:
            $(".seasons").css("box-shadow", "#ffd900 0px 0px 0px 4px inset");
            break;

        case 2:
            $(".seasons").css("box-shadow", "#e67e42 0px 0px 0px 4px inset");
            break;

        case 3:
            $(".seasons").css("box-shadow", "#3498db 0px 0px 0px 4px inset");
            break;

        default:
            break;
    }
};
$(function () {
    seasonSlider(0);
})

let barshow = function (item) {
    document.querySelector(".bars").childNodes.forEach(item => {
        item.style = "display:none";
    });
    let bar = document.querySelector(item);
    bar.style = "display:grid";
    bar.children[bar.children.length - 1].addEventListener("click", () => {
        bar.style = "display:none";
    });
};

let signup = function (item) {
    document.querySelector(item).style = "display:none";
    document.querySelector(".signup").style = "display:grid !important";

    if (item == ".signup") {
        document.querySelector(item).style = "display:none";
        document.querySelector(".signin").style = "display:grid !important";
    }
};

let bgindex = 0;
$(function () {
    let imgs = document.querySelectorAll(".backgroundImage img");
    for (let i = 0; i < imgs.length; i++) {
        $(imgs[i]).hide();
    }
    $(imgs[bgindex - 1]).fadeIn(1000, Function());
})
bgSlider();
function bgSlider() {
    let imgs = document.querySelectorAll(".backgroundImage img");
    for (let i = 0; i < imgs.length; i++) {
        $(imgs[i]).hide();
    }
    bgindex++;
    if (bgindex > imgs.length) {
        bgindex = 1;
    }
    $(imgs[bgindex-1]).fadeIn(1000, Function());
    setTimeout(bgSlider, 20000);
}

let newsindex = 1;
$(function () {
    $(".newspost").hide();
    $(".newspost:first").show()
    $("#nextbtn").on("click", () => {
        let news = document.querySelectorAll(".newspost");
        news.forEach(item => {
            $(item).hide("slide");
        });
        newsindex++;
        if (newsindex >= news.length) {
            newsindex = 0;
        }
        setTimeout(() => {
            $(news[newsindex]).fadeIn();
        }, 395);
    });
})

let innavshow = function (item) {
    $(".selectedNav").slideUp();
    $(".selectediNav").slideUp();
    $(item)
        .next()
        .addClass("selectedNav")
        .slideDown();
};

let iinavshow = function (item) {
    // item.nextSibling.nextSibling.style = "display:unset";
    $(".selectediNav").slideUp();
    $(item)
        .next()
        .addClass("selectediNav")
        .slideDown();
};

window.addEventListener("click", function (e) {
    if (e.target.contains(document.querySelector(".innerlist"))) {
        $(".innerlist").fadeOut(200);
    }
});

window.onscroll = function () {
    $(".innerlist").fadeOut(200);
    let navbar = document.querySelector("header");
    let sticky = (navbar.offsetTop = 550);
    if (window.pageYOffset <= sticky) {
        $(navbar).css("display", "none");
        $(".logo img").attr("src", "/images/logo3.png");
        // $('.stickynav').css('background-color','#da251e');
    }
    if (window.pageYOffset >= sticky) {
        $(navbar)
            .addClass("stickynav")
            .slideDown();
        // $('.stickynav').css('background-color','white');
        $(".logo img").attr("src", "/images/logo5.png");
    } else {
        $(navbar).removeClass("stickynav");
        $(navbar).css("display", "unset");
    }
};

$(".innerlist").fadeOut(200);

let listhandler = function (item) {
    $(item)
        .children()
        .fadeIn();
    $(item)
        .children()
        .css("display", "flex");
    $(".innerlist")
        .not($(item).children())
        .fadeOut();
};
let listhandlerutil = function (item) {
    $(item)
        .next()
        .fadeIn();
    $(item)
        .next()
        .css("display", "flex");
    $(".innerlist")
        .not($(item).next())
        .fadeOut();
};

let listhandlermob = function (item) {
    // $(item).children().next().toggle('fade');
    $(".usericon").show();
    $(".filterdiv").show("fade");
    $(".mobilenav").hide("slide");
    $(item).css("display", "flex");
    $(item).fadeIn();
};

$(".innerlist")
    .not($(".profilelist"))
    .on("mouseleave", () => {
        $(".innerlist").fadeOut(200);
    });

let userformhandler = function (event, event2) {
    $(event2).fadeOut();
    setTimeout(() => {
        $(event).fadeIn();
    }, 400);
};

$(window).on("load", () => {
    $(".preloader").fadeOut(1000);
});

$(function () {
    $(".mobilenav").hide();
})

let hideout = function (item) {
    $(item)
        .parent()
        .fadeOut();
    $(".filterdiv").fadeOut();
    $(".usericon").hide();
};
let hideoutmob = function (item) {
    $(item)
        .parent()
        .fadeOut();
};

let slidein = function (item) {
    $(item).show("slide");
};

let showlist = function (item) {
    $(item)
        .next()
        .slideToggle();
    $(item)
        .parent()
        .parent()
        .children()
        .children()
        .next()
        .not($(item).next())
        .slideUp();
};
let showfinallist = function (item) {
    $(item)
        .next()
        .show("slide");
};

let hidefinallist = function (item) {
    $(item)
        .parent()
        .hide("slide");
};

function myFunction(x) {
    if (x.matches) {
        // If media query matches
        $(".logo img").attr("src", "/images/logo5.png");
    } else {
        $(".logo img").attr("src", "/images/logo3.png");
    }
}

var x = window.matchMedia("(max-width: 1136px)");
myFunction(x);
x.addListener(myFunction);

//if (x.matches) {
//    $(".logo img").attr("src", "/images/logo5.png");
//} else {
//    $(".logo img").attr("src", "/images/logo3.png");
//}
