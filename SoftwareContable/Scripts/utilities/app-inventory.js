(function ($) {
    'use strict';

    var timeOutMessages = 10000;

    var init = function () {
        window.ajaxLoadingPanel = $('#loadingPanel');

        window.errorPanel = $('#errorPanel');

        window.warningPanel = $('#warningPanel');

        window.successPanel = $('#successPanel');

        $('.alert-messages-section .alert').off('click').on('click', function () {
            $(this).addClass('hidden');
        });

        //$('.datepicker').off().datepicker({
        //    format: $('body[date-format]').attr('date-format')
        //}).on('keydown', function () {
        //    return false;
        //});

        $(document).on('click', 'a.set-loading-panel', function () {
            window.ajaxLoadingPanel.addClass('active');
        });
    };

    window.AjaxProvider = function () {
        return {
            get: function (url, cache, onSuccess, onError) {
                $.ajax({
                    url: url,
                    method: 'GET',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    cache: cache || false
                }).success(function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === 'function') {
                        onSuccess.call(this, data, textStatus, jqXHR);
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === 'function') {
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
                    method: 'PUT',
                    data: dataJson,
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8"
                }).success(function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === 'function') {
                        onSuccess.call(this, data, textStatus, jqXHR);
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === 'function') {
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
                    method: 'POST',
                    data: dataJson,
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8"
                }).success(function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === 'function') {
                        onSuccess.call(this, data, textStatus, jqXHR);
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === 'function') {
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
                    method: 'DELETE',
                    data: dataJson,
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8"
                }).success(function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === 'function') {
                        onSuccess.call(this, data, textStatus, jqXHR);
                    }
                }).error(function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === 'function') {
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
        window.successPanel.addClass('hidden');
        window.warningPanel.addClass('hidden');

        $('span.message', window.errorPanel.removeClass('hidden')).html(errorMessage);

        setTimeout(function () {
            window.errorPanel.addClass('hidden');
        }, timeOutMessages);
    };

    window.setWarningMessage = function (warningMessage) {
        window.errorPanel.addClass('hidden');
        window.successPanel.addClass('hidden');

        $('span.message', window.warningPanel.removeClass('hidden')).html(warningMessage);

        setTimeout(function () {
            window.warningPanel.addClass('hidden');
        }, timeOutMessages);
    };

    window.setSuccessMessage = function (successMessage) {
        window.errorPanel.addClass('hidden');
        window.warningPanel.addClass('hidden');

        $('span.message', window.successPanel.removeClass('hidden')).html(successMessage);

        setTimeout(function () {
            window.successPanel.addClass('hidden');
        }, timeOutMessages);
    };

    window.htmlTemplates = {
        editIconCssClass: 'fa fa-pencil-square-o text-primary edit-icon',

        deleteIconCssClass: 'fa fa-trash-o text-danger delete-icon'
    };

    $(document).ready(function () {
        init();
    }).ajaxSend(function (event, request, settings) {
        if (window.ajaxLoadingPanel) {
            window.ajaxLoadingPanel.addClass('active');
        }
    }).ajaxComplete(function (event, request, settings) {
        if (window.ajaxLoadingPanel) {
            window.ajaxLoadingPanel.removeClass('active');
        }
    });

})(window.jQuery);