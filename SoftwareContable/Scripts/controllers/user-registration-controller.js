(function ($, angular) {
    "use strict";

    var userRegistrationController = angular.module("softwareContable", []);

    userRegistrationController.controller("userRegistrationController", ["$scope", "$compile", function ($scope, $compile) {
        var ajax = new AjaxProvider();

        var urls = {
            registerUrl: ""
        };

        var initUrls = function (registerUrl) {
            urls.registerUrl = registerUrl;
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

                $scope.$applyAsync(function () {
                    window.ajaxLoadingPanel.css("display", "none");
                });
            });
        };

        $scope.newUser = {};
        $scope.errorMessages = [];
        $scope.successMessages = [];

        $scope.initUrls = initUrls;
        $scope.register = register;
    }]);
})(window.jQuery, window.angular);