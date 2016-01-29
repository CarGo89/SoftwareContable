(function ($, angular) {
    "use strict";

    var clientController = angular.module("softwareContable", []);

    clientController.directive("integer", angularDirectives.integer);
    clientController.directive("modalWindow", angularDirectives.modalWindow);

    clientController.controller("clientController", ["$scope", "$compile", function ($scope, $compile) {
        var ajax = new AjaxProvider();
        var clientResults = {};

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
            { data: "Rfc", width: "auto" },
            { data: "CorporateName", width: "auto" },
            { data: "PhoneNumber", width: "auto" },
            { data: "Email", width: "auto" }
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
                columns: columns
            });

            $scope.get();
        });

        var getClients = function () {
            var getUrl = urls.get;

            ajax.get(getUrl, false, function (data, textStatus, jqXhr) {
                $scope.clients = data.clients;

                clientResults.fnClearTable();

                if ($scope.clients.length > 0) {
                    clientResults.fnAddData($scope.clients);
                    clientResults.fnAdjustColumnSizing();

                    $compile(clientResults)($scope);
                }

                $scope.$applyAsync(function () {
                    window.ajaxLoadingPanel.css("display", "none");
                });
            });
        };

        $scope.initUrls = initUrls;
        $scope.get = getClients;
    }]);
})(window.jQuery, window.angular);