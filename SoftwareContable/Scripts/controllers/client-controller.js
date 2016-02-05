(function ($, angular) {
    "use strict";

    var softwareContable = angular.module("softwareContable", []);

    softwareContable.directive("inlineEdit", angularDirectives.inlineEdit);
    softwareContable.directive("integer", angularDirectives.integer);
    softwareContable.directive("modalWindow", angularDirectives.modalWindow);

    softwareContable.controller("clientController", ["$scope", "$compile", function ($scope, $compile) {
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
                columns: columns,
                columnDefs: [
                   {
                       targets: [0, 1, 2, 3],
                       createdCell: function (td, cellData, rowData, row, col) {
                           var field = columns[col].data;
                           var integerInput = "";

                           if (typeof cellData === "number") {
                               integerInput = "integer";
                           }

                           cellData = (cellData || "");

                           $(td).text("")
                               .data("rowData", rowData)
                               .append(("<span class='inline-edit-label' inline-edit-field=").concat(field).concat(" on-inline-edited='update'>").concat(cellData).concat("</span>"))
                               .append(("<input type='text'' class='form-control hidden inline-edit-input' ").concat(integerInput).concat(" value=").concat(cellData).concat(" />"))
                               .attr("inline-edit", "");
                       }
                   }
                ]
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

        var saveClients = function () {
            $scope.validationMessages = [];

            ajax.post(urls.validate, $scope.newClient, function (data, textStatus, jqXhr) {
                if (data === true) {
                    ajax.post(urls.save, $scope.newClient, function (data, textStatus, jqXhr) {
                        setSuccessMessage("Cliente guardado correctamente.");

                        $scope.newClientModal.modal("hide");

                        $scope.get();
                    });
                }
                else if (data.length > 0) {
                    $scope.validationMessages = data;

                    $scope.$apply();
                }
            });
        };

        var updateClient = function (field, oldValue, newValue, rowData, onValidation) {
            $scope.updateValidationMessages = [];

            ajax.post(urls.validate, rowData, function (data, textStatus, jqXHR) {
                onValidation.call(this, data);

                if (data === true) {
                    ajax.put(urls.update, rowData, function (data, textStatus, jqXHR) {
                        setSuccessMessage("Cliente actualizado correctamente.");
                    });
                }
                else if (data.length > 0) {
                    $scope.updateValidationMessages = data;

                    $scope.$apply();
                }
            });
        };

        var onNewClientHidden = function (event) {
            $scope.newClient = {};

            $scope.validationMessages = [];

            $scope.$apply();
        };

        $scope.clients = [];
        $scope.newClient = {};
        $scope.validationMessages = [];
        $scope.updateValidationMessages = [];
        $scope.newClientModal = {};

        $scope.initUrls = initUrls;
        $scope.get = getClients;
        $scope.update = updateClient;
        $scope.save = saveClients;
        $scope.onNewClientHidden = onNewClientHidden;
    }]);
})(window.jQuery, window.angular);