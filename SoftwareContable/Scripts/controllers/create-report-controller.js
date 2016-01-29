(function ($, angular) {
    "use strict";

    var reportController = angular.module("softwareContable", []);

    reportController.directive("integer", angularDirectives.integer);
    reportController.directive("modalWindow", angularDirectives.modalWindow);

    reportController.controller("createReportController", ["$scope", "$compile", function ($scope, $compile) {
        var ajax = new AjaxProvider();
        var urls = {
            get: "",
            save: "",
            update: "",
            validate: ""
        };
       
        $scope.initUrls = initUrls;
    }]);
})(window.jQuery, window.angular);