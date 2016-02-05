(function ($, angular) {
    "use strict";

    var userRegistrationController = angular.module("softwareContable", []);

    userRegistrationController.controller("userRegistrationController", ["$scope", "$compile", function ($scope, $compile) {
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

        var login = function () {
            ajax.post(urls.registerUrl, $scope.loginUser, function (data, textStatus, jqXhr) {
                if (data.returnUrl && data.returnUrl.length > 0) {
                    window.location.href = data.returnUrl;
                }
                else {
                    if (typeof data === "string") {
                        $scope.errorMessages = [data];
                    }
                    else {
                        $scope.errorMessages = data;
                    }
                }

                $scope.$apply();
            });
        };

        var register = function () {
            window.ajaxLoadingPanel.css("display", "table");

            ajax.post(urls.registerUrl, $scope.newUser, function (data, textStatus, jqXhr) {
                if (data.Id > 0) {
                    var successMessage = data.UserName.concat(", gracias por registrarte!");

                    $scope.successMessages = [successMessage];
                    $scope.newUser = {};
                    $scope.errorMessages = [];
                }
                else {
                    $scope.successMessages = [];

                    if (typeof data === "string") {
                        $scope.errorMessages = [data];
                    }
                    else {
                        $scope.errorMessages = data;
                    }
                }

                $scope.$apply(function () {
                    window.ajaxLoadingPanel.css("display", "none");
                });
            });
        };

        $scope.newUser = {};
        $scope.loginUser = {};
        $scope.errorMessages = [];
        $scope.successMessages = [];

        $scope.initRegistrationUrls = initRegistrationUrls;
        $scope.initLoginUrls = initLoginUrls;

        $scope.login = login;
        $scope.register = register;
    }]);
})(window.jQuery, window.angular);