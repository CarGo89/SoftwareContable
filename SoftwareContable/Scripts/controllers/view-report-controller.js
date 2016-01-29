(function ($, angular) {
    "use strict";

    var reportController = angular.module("softwareContable", []);

    reportController.directive("integer", angularDirectives.integer);
    reportController.directive("modalWindow", angularDirectives.modalWindow);

    reportController.controller("viewReportController", ["$scope", "$compile", function ($scope, $compile) {
        var ajax = new AjaxProvider();
        var reportResults = {};

        var urls = {
            get: "",
            save: "",
            update: "",
            validate: ""
        };

        var initUrls = function (getUrl, saveUrl, updateUrl, validateUrl) {
            urls.get = getUrl;
            urls.save = saveUrl;
            urls.update = updateUrl;
            urls.validate = validateUrl;
        };

        var columns = [
            { data: "Id", width: "auto" },
            { data: "Client.CorporateName", width: "auto" },
            { data: "CreationDate", width: "auto" },
            { data: "InvoiceDate", width: "auto" },
            { data: "InvoiceId", width: "auto" }
        ];

        angular.element(document).ready(function () {
            reportResults = $("#reportResults").dataTable({
                responsive: true,
                autoWidth: true,
                deferRender: true,
                language: {
                    search: "Filtro:",
                    searchPlaceholder: "Filtrar Reportes",
                    emptyTable: "No se encontraron Reportes",
                    zeroRecords: "No se encontraron coincidencias",
                    lengthMenu: "Mostrar _MENU_ Reportes",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ Reportes",
                    infoEmpty: "Mostrando 0 a 0 de 0 Reportes",
                    infoFiltered: "(filtro de _MAX_ total Reportes)"
                },
                columns: columns
            });

            $scope.get();
        });

        var getReports = function () {
            var getUrl = urls.get;

            ajax.get(getUrl, false, function (data, textStatus, jqXhr) {
                $scope.reports = data.reports;

                reportResults.fnClearTable();

                if ($scope.reports.length > 0) {
                    reportResults.fnAddData($scope.reports);
                    reportResults.fnAdjustColumnSizing();

                    $compile(reportResults)($scope);
                }

                $scope.$applyAsync(function () {
                    window.ajaxLoadingPanel.css("display", "none");
                });
            });
        };

        $scope.initUrls = initUrls;
        $scope.get = getReports;
    }]);
})(window.jQuery, window.angular);