(function ($, angular) {
    "use strict";

    var softwareContable = angular.module("softwareContable", []);

    softwareContable.directive("datePicker", angularDirectives.datePicker);
    softwareContable.directive("integer", angularDirectives.integer);
    softwareContable.directive("modalWindow", angularDirectives.modalWindow);

    softwareContable.controller("createReportController", ["$scope", "$compile", function ($scope, $compile) {
        var clientResults = {};
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
                $scope.clientsById = data.clientsById;
                $scope.soldSystems = data.soldSystems;

                clientResults.fnClearTable();

                if ($scope.clients.length > 0) {
                    clientResults.fnAddData($scope.clients);
                    clientResults.fnAdjustColumnSizing();

                    $compile(clientResults)($scope);
                }

                $scope.$apply();
            });
        };

        var clearMessages = function () {
            $scope.validationMessages = [];
            $scope.successMessages = [];

            $scope.showErrors = false;
            $scope.showMessages = false;
        };

        var createReport = function () {
            clearMessages();

            $scope.newReport.ClientId = $scope.selectedClient.Id;
            $scope.newReport.SoldSystemId = $scope.selectedSoldSystem.Id;

            ajax.post(urls.save, $scope.newReport, function (data, textStatus, jqXhr) {
                if (data.Id > 0) {
                    $scope.successMessages = ["Reporte creado correctamente."];

                    $scope.showMessages = true;
                }
                else if (data.length > 0) {
                    $scope.validationMessages = data;

                    $scope.showErrors = true;
                }

                $scope.$applyAsync();
            });
        };

        var selectClient = function (clientId) {
            $scope.selectedClient = $scope.clientsById[clientId];
        };

        var columns = [
            { data: "Rfc", width: "auto" },
            { data: "CorporateName", width: "auto" }
        ];

        angular.element(document).ready(function () {
            clientResults = $("#clientResults").dataTable({
                responsive: true,
                autoWidth: true,
                deferRender: true,
                language: {
                    search: "Filtro:",
                    searchPlaceholder: "Filtrar Clientes",
                    emptyTable: "No se encontraron Clientes",
                    zeroRecords: "No se encontraron coincidencias",
                    lengthMenu: "Mostrar _MENU_ Clientes",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ Clientes",
                    infoEmpty: "Mostrando 0 a 0 de 0 Clientes",
                    infoFiltered: "(filtro de _MAX_ total Clientes)"
                },
                columns: columns,
                columnDefs: [
                   {
                       targets: [0],
                       createdCell: function (td, cellData, rowData, row, col) {
                           var htmlRfc = ("<a href='#' data-dismiss='modal' class='btn btn-link' ng-click='selectClient(")
                               .concat(rowData.Id).concat(")'>").concat(cellData).concat("</span>");

                           $(td).text("").append(htmlRfc);
                       }
                   }
                ]
            });

            getCatalogs();
        });

        $scope.showErrors = false;
        $scope.showMessages = false;
        $scope.validationMessages = [];
        $scope.successMessages = [];

        $scope.newReport = {};
        $scope.selectedClient = {};
        $scope.selectedSoldSystem = {};
        $scope.clients = [];
        $scope.clientsById = [];
        $scope.soldSystems = [];

        $scope.$watch("selectedClient.Rfc", function (newValue, oldValue) {
            if (newValue !== oldValue) {
                if (!newValue || newValue.length === 0) {
                    $scope.selectedClient = {};
                }
            }
        });

        $scope.initUrls = initUrls;
        $scope.selectClient = selectClient;
        $scope.create = createReport;
        $scope.clearMessages = clearMessages;
    }]);
})(window.jQuery, window.angular);