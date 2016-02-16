(function ($, angular) {
    "use strict";

    var softwareContable = angular.module("softwareContable", []);

    softwareContable.controller("userRegistrationController", ["$scope", "$compile", function ($scope, $compile) {
        var ajax = new AjaxProvider();

        var urls = {
            loginUrl: "",
            registerUrl: ""
        };

        var initRegistrationUrls = function (registerUrl) {
            urls.registerUrl = registerUrl;
        };

        var initLoginUrls = function (loginUrl) {
            urls.loginUrl = loginUrl;
        };

        var clearMessages = function () {
            $scope.errorMessages = [];
            $scope.successMessages = [];

            $scope.showErrors = false;
            $scope.showMessages = false;
        };

        var login = function () {
            clearMessages();

            ajax.post(urls.registerUrl, $scope.loginUser, function (data, textStatus, jqXhr) {
                if (data.returnUrl && data.returnUrl.length > 0) {
                    window.location.href = data.returnUrl;

                    window.ajaxLoadingPanel.addClass("active");
                }
                else {
                    if (typeof data === "string") {
                        $scope.errorMessages = [data];
                    }
                    else {
                        $scope.errorMessages = data;
                    }

                    $scope.showErrors = true;
                }

                $scope.$apply();
            });
        };

        var register = function () {
            clearMessages();

            ajax.post(urls.registerUrl, $scope.newUser, function (data, textStatus, jqXhr) {
                if (data.Id > 0) {
                    var successMessage = data.UserName.concat(", gracias por registrarte!");

                    $scope.showMessages = true;

                    $scope.successMessages = [successMessage];

                    $scope.newUser = {};
                }
                else {
                    if (typeof data === "string") {
                        $scope.errorMessages = [data];
                    }
                    else {
                        $scope.errorMessages = data;
                    }

                    $scope.showErrors = true;
                }

                $scope.$apply();
            });
        };

        $scope.newUser = {};
        $scope.loginUser = {};
        $scope.errorMessages = [];
        $scope.successMessages = [];

        $scope.showErrors = false;
        $scope.showMessages = false;

        $scope.initRegistrationUrls = initRegistrationUrls;
        $scope.initLoginUrls = initLoginUrls;

        $scope.login = login;
        $scope.register = register;
        $scope.clearMessages = clearMessages;
    }]);
})(window.jQuery, window.angular);