(function ($, angular) {
    "use strict";

    var softwareContable = angular.module("softwareContable", []);

    softwareContable.directive("datePicker", angularDirectives.datePicker);
    softwareContable.directive("integer", angularDirectives.integer);
    softwareContable.directive("modalWindow", angularDirectives.modalWindow);

    softwareContable.controller("createReportController", ["$scope", "$compile", function ($scope, $compile) {
        var ajax = new AjaxProvider();
        var urls = {
            catalogs: "",
            save: "",
            validate: ""
        };

        var initUrls = function (catalogs, save, validate) {
            urls.catalogs = catalogs;
            urls.save = save;
            urls.validate = validate;
        };

        var getCatalogs = function () {
            ajax.get(urls.catalogs, false, function (data, textStatus, jqXhr) {
                $scope.clients = data.clients;
                $scope.soldSystems = data.soldSystems;

                $scope.$apply();
            });
        };

        var selectClient = function (client) {
            $scope.selectedClient = client;
        };

        angular.element(document).ready(function () {
            getCatalogs();
        });

        $scope.selectedClient = {};

        $scope.initUrls = initUrls;
        $scope.selectClient = selectClient;
    }]);
})(window.jQuery, window.angular);