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
            ajax.post(urls.registerUrl, $scope.newUser, function (data, textStatus, jqXhr) {
                data;

                $scope.$applyAsync(function () {
                    window.ajaxLoadingPanel.css("display", "none");
                });
            });
        };

        $scope.newUser = {};
        $scope.validationMessages = [];

        $scope.initUrls = initUrls;
        $scope.register = register;
    }]);
})(window.jQuery, window.angular);