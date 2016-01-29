(function ($) {
    "use strict";

    window.angularDirectives = {
        integer: function () {
            return {
                restrict: "A",

                link: function (scope, element, attrs) {
                    element = $(element);

                    if (element.is("input") !== true) {
                        return;
                    }

                    element.keypress(function () {
                        return (event.which >= 48 && event.which <= 57);
                    });
                }
            };
        },

        modalWindow: function () {
            return {
                restrict: "A",

                link: function (scope, element, attrs) {
                    element = $(element);

                    var elementId = element.attr("id");
                    var onHiddenAttr = (element.attr("onHidden") || "");
                    var onHidden = scope[onHiddenAttr];
                    var onShownAttr = (element.attr("onShown") || "");
                    var onShown = scope[onShownAttr];

                    scope[elementId] = element.modal({
                        show: false
                    });

                    if (typeof onHidden === "function") {
                        element.off("hidden.bs.modal").on("hidden.bs.modal", function (e) {
                            onHidden.call(this, e);
                        });
                    }

                    if (typeof onShown === "function") {
                        element.off("shown.bs.modal").on("shown.bs.modal", function (e) {
                            onShown.call(this, e);
                        });
                    }
                }
            };
        }
    };
})(window.jQuery);