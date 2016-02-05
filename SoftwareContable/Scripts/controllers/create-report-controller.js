(function ($, angular) {
    "use strict";

    var softwareContable = angular.module("softwareContable", []);

    softwareContable.directive("integer", angularDirectives.integer);
    softwareContable.directive("modalWindow", angularDirectives.modalWindow);

    softwareContable.controller("createReportController", ["$scope", "$compile", function ($scope, $compile) {
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