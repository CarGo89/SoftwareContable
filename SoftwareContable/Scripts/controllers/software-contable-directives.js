(function ($) {
    "use strict";

    window.angularDirectives = {
        inlineEdit: function () {
            var calculateWidth = function (element) {
                var inputValue = (element.val() || "");

                if (inputValue.length === 0) {
                    return "100px";
                }

                return ((inputValue.length + 4) * 8).toString().concat("px");
            };

            return {
                restrict: "A",

                link: function (scope, element, attrs) {
                    element.off("click").on("click", function () {
                        var label = element.children(".inline-edit-label");
                        var input = element.children(".inline-edit-input");
                        var isSelect = (input.is("select") === true);
                        var isCheckbox = (input.is(":checkbox") === true);
                        var rowData = element.data("rowData");

                        if (input.hasClass("hidden") !== true) {
                            return;
                        }

                        label.addClass("hidden");

                        if (!isSelect) {
                            input.css("width", calculateWidth(input));
                        }

                        input.removeClass("hidden").focus().off("change").on("change", function () {
                            var oldValue = label.text();
                            var newValue = input.val();
                            var newValueId = 0;
                            var options = [];

                            input.blur();

                            if (isSelect) {
                                var optionsField = input.attr("inline-edit-options");

                                options = scope[optionsField];

                                newValueId = parseInt(newValue, 10);

                                newValue = options[newValueId].Value;
                            }

                            if (isCheckbox) {
                                newValue = (input.is(":checked") === true);
                            }

                            if (oldValue === newValue) {
                                return;
                            }

                            var field = label.attr("inline-edit-field");
                            var onInlineEdited = label.attr("on-inline-edited");

                            label.text(newValue);

                            if (isSelect) {
                                rowData[field] = newValueId;
                            }

                            if (input.is("input")) {
                                rowData[field] = newValue;
                            }

                            if (typeof scope[onInlineEdited] === "function") {
                                scope[onInlineEdited].call(this, field, oldValue, newValue, rowData,
                                    function (isValid) {
                                        if (isValid !== true) {
                                            label.text(oldValue);

                                            if (isSelect) {
                                                rowData[field] = oldValue;
                                            }

                                            if (input.is("input")) {
                                                rowData[field] = oldValue;
                                            }

                                            input.val(oldValue);
                                        }
                                    });
                            }
                        }).off("blur").on("blur", function () {
                            label.removeClass("hidden");

                            input.addClass("hidden");
                        }).off("keyup keydown").on("keyup keydown", function () {
                            if (!isSelect) {
                                input.css("width", calculateWidth(input));
                            }
                        });
                    });
                }
            };
        },

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
        },

        datePicker: function () {
            return {
                restrict: "A",

                require: "ngModel",

                link: function (scope, element, attrs, ngModel) {
                    element = $(element).addClass("datepicker");

                    element.off().datepicker({
                        format: $("body[date-format]").attr("date-format")
                    }).on("keydown", function () {
                        return false;
                    });

                    ngModel.$render = function () {
                        var dateValue = new Date(ngModel.$viewValue || "");

                        if (dateValue.valueOf() > 0) {
                            $element.datepicker("update", dateValue).datepicker("setValue", dateValue);
                        }
                    };

                    element.datepicker().on("changeDate", function (event) {
                        scope.$applyAsync(function () {
                            var formattedDate = event.formatDate(event.date);

                            ngModel.$setViewValue(formattedDate);
                        });
                    });
                }
            };
        }
    };
})(window.jQuery);