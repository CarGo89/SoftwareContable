(function ($, angular) {
    "use strict";

    var softwareContable = angular.module("softwareContable", []);

    softwareContable.directive("integer", angularDirectives.integer);
    softwareContable.directive("modalWindow", angularDirectives.modalWindow);

    softwareContable.controller("viewReportController", ["$scope", "$compile", function ($scope, $compile) {
        var ajax = new AjaxProvider();
        var reportResults = {};

        var urls = {
            get: "",
            search: ""
        };

        var initUrls = function (getUrl, searchUrl) {
            urls.get = getUrl;
            urls.search = searchUrl;
        };

        var columns = [
            { data: "Id", width: "auto" },
            { data: "Client.CorporateName", width: "auto" },
            { data: "CreationDate", width: "auto" },
            { data: "InvoiceDate", width: "auto" },
            { data: "InvoiceSerial", width: "auto" }
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
                columns: columns,
                columnDefs: [
                     {
                         targets: [0, 4],
                         createdCell: function (td, cellData, rowData, row, col) {
                             $(td).addClass("text-center");
                         }
                     },
                   {
                       targets: [2, 3],
                       createdCell: function (td, cellData, rowData, row, col) {
                           var dateValue = eval(("new ").concat(cellData).replace(/\//g, ""));
                           var formattedDate = "";

                           if (dateValue.valueOf() > 0) {
                               formattedDate = formatDate(dateValue);
                           }

                           $(td).addClass("text-center").text(formattedDate);
                       }
                   }
                ]
            });

            $scope.get();
        });

        var bindReports = function () {
            reportResults.fnClearTable();

            if ($scope.reports.length > 0) {
                reportResults.fnAddData($scope.reports);
                reportResults.fnAdjustColumnSizing();

                $compile(reportResults)($scope);
            }

            $scope.$apply();
        }

        var getReports = function () {
            var getUrl = urls.get;

            ajax.get(getUrl, false, function (data, textStatus, jqXhr) {
                $scope.reports = data.reports;

                bindReports();
            });
        };

        var searchReports = function () {
            var searchUrl = urls.search;
            var initialFolio = ($scope.initialFolio > 0 ? $scope.initialFolio : 0);
            var finalFolio = ($scope.finalFolio > 0 ? $scope.finalFolio : 0);

            if (!(initialFolio + finalFolio > 0)) {
                return;
            }

            searchUrl = searchUrl.concat("/").concat(initialFolio).concat("/").concat(finalFolio);

            ajax.get(searchUrl, false, function (data, textStatus, jqXhr) {
                $scope.reports = data.reports;

                bindReports();
            });
        };

        $scope.initialFolio = "";
        $scope.finalFolio = "";

        $scope.initUrls = initUrls;
        $scope.get = getReports;
        $scope.search = searchReports;
    }]);
})(window.jQuery, window.angular);