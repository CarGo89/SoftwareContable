(function ($) {
    "use strict";

    var timeOutMessages = 10000;

    var init = function () {
        window.ajaxLoadingPanel = $("#loadingPanel");

        if ($("form").attr("hide-loading-panel") === "true") {
            window.ajaxLoadingPanel.removeClass("active");
        }

        window.errorPanel = $("#errorPanel");

        window.warningPanel = $("#warningPanel");

        window.successPanel = $("#successPanel");

        $(".alert-messages-section .alert").off("click").on("click", function () {
            $(this).addClass("hidden");
        });

        $(document).on("click", ".set-loading-panel", function () {
            window.ajaxLoadingPanel.addClass("active");
        });
    };

    window.formatDate = function (date) {
        var format = $("body").attr("date-format");

        var separator = format.match(/[.\/\-\s].*?/);
        var parts = format.split(/\W+/);

        if (!separator || !parts || parts.length === 0) {
            throw new Error("Invalid date format.");
        }
        format = { separator: separator, parts: parts };

        var val = {
            d: date.getDate(),
            m: date.getMonth() + 1,
            yy: date.getFullYear().toString().substring(2),
            yyyy: date.getFullYear()
        };

        val.dd = (val.d < 10 ? "0" : "") + val.d;

        val.mm = (val.m < 10 ? "0" : "") + val.m;

        var dateParts = [];

        for (var i = 0, cnt = format.parts.length; i < cnt; i++) {
            dateParts.push(val[format.parts[i]]);
        }

        return dateParts.join(format.separator);
    }

    window.AjaxProvider = function () {
        return {
            get: function (url, cache, onSuccess, onError) {
                $.ajax({
                    url: url,
                    method: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    cache: cache || false
                }).success(function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === "function") {
                        onSuccess.call(this, data, textStatus, jqXHR);
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === "function") {
                        onError.call(this, jqXHR, textStatus, errorThrown);
                    }
                    else {
                        setErrorMessage(errorThrown);
                    }
                });
            },

            put: function (url, data, onSuccess, onError) {
                var dataJson = JSON.stringify(data);

                $.ajax({
                    url: url,
                    method: "PUT",
                    data: dataJson,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8"
                }).success(function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === "function") {
                        onSuccess.call(this, data, textStatus, jqXHR);
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === "function") {
                        onError.call(this, jqXHR, textStatus, errorThrown);
                    }
                    else {
                        setErrorMessage(errorThrown);
                    }
                });
            },

            post: function (url, data, onSuccess, onError) {
                var dataJson = JSON.stringify(data);

                $.ajax({
                    url: url,
                    method: "POST",
                    data: dataJson,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8"
                }).success(function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === "function") {
                        onSuccess.call(this, data, textStatus, jqXHR);
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === "function") {
                        onError.call(this, jqXHR, textStatus, errorThrown);
                    }
                    else {
                        setErrorMessage(errorThrown);
                    }
                });
            },

            deletion: function (url, data, onSuccess, onError) {
                var dataJson = JSON.stringify(data);

                $.ajax({
                    url: url,
                    method: "DELETE",
                    data: dataJson,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8"
                }).success(function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === "function") {
                        onSuccess.call(this, data, textStatus, jqXHR);
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === "function") {
                        onError.call(this, jqXHR, textStatus, errorThrown);
                    }
                    else {
                        setErrorMessage(errorThrown);
                    }
                });
            }
        };
    };

    window.setErrorMessage = function (errorMessage) {
        window.successPanel.addClass("hidden");
        window.warningPanel.addClass("hidden");

        $("span.message", window.errorPanel.removeClass("hidden")).html(errorMessage);

        setTimeout(function () {
            window.errorPanel.addClass("hidden");
        }, timeOutMessages);
    };

    window.setWarningMessage = function (warningMessage) {
        window.errorPanel.addClass("hidden");
        window.successPanel.addClass("hidden");

        $("span.message", window.warningPanel.removeClass("hidden")).html(warningMessage);

        setTimeout(function () {
            window.warningPanel.addClass("hidden");
        }, timeOutMessages);
    };

    window.setSuccessMessage = function (successMessage) {
        window.errorPanel.addClass("hidden");
        window.warningPanel.addClass("hidden");

        $("span.message", window.successPanel.removeClass("hidden")).html(successMessage);

        setTimeout(function () {
            window.successPanel.addClass("hidden");
        }, timeOutMessages);
    };

    window.htmlTemplates = {
        editIconCssClass: "fa fa-pencil-square-o text-primary edit-icon",

        deleteIconCssClass: "fa fa-trash-o text-danger delete-icon"
    };

    $(document).off("ajaxSend ajaxComplete").ready(function () {
        init();
    }).ajaxSend(function (event, request, settings) {
        if (window.ajaxLoadingPanel) {
            window.ajaxLoadingPanel.addClass("active");
        }
    }).ajaxComplete(function (event, request, settings) {
        if (window.ajaxLoadingPanel) {
            window.ajaxLoadingPanel.removeClass("active");
        }
    });

})(window.jQuery);