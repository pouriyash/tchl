(function() {
    if (typeof window.MutationObserver === typeof undefined) {
        return;
    }

    // var MutationObserver = window.MutationObserver || window.WebKitMutationObserver;

    var observer = new MutationObserver(function (mutations, observer) {
        // fired when a mutation occurs
        // console.log(mutations, observer);
        // if ($.fn.matchHeight) $.fn.matchHeight._update();
    });

    // define what element should be observed by the observer
    // and what types of mutations trigger the callback
    observer.observe(document, {
        subtree: true,
        childList: true,
        attributes: true
    });
}());