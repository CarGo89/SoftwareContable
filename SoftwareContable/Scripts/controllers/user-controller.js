(function ($, angular) {
    "use strict";

    var userController = angular.module("softwareContable", []);

    userController.directive("integer", angularDirectives.integer);
    userController.directive("modalWindow", angularDirectives.modalWindow);

    userController.controller("userController", ["$scope", "$compile", function ($scope, $compile) {
        var ajax = new AjaxProvider();
        var userResults = {};

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
            { data: "Name", width: "auto" },
            { data: "Email", width: "auto" },
            { data: "Role", width: "auto" }
        ];

        angular.element(document).ready(function() {
            userResults = $("#userResults").dataTable({
                responsive: true,
                autoWidth: true,
                deferRender: true,
                language: {
                    search: "Filtro:",
                    searchPlaceholder: "Filtrar Usuarios",
                    emptyTable: "No se encontraron Usuarios",
                    zeroRecords: "No se encontraron coincidencias",
                    lengthMenu: "Mostrar _MENU_ Usuarios",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ Usuarios",
                    infoEmpty: "Mostrando 0 a 0 de 0 Usuarios",
                    infoFiltered: "(filtro de _MAX_ total Usuarios)"
                },
                columns: columns
            });

            $scope.get();
        });

        var getUsers = function () {
            var getUrl = urls.get;

            ajax.get(getUrl, false, function (data, textStatus, jqXhr) {
                $scope.users = data.users;

                userResults.fnClearTable();

                if ($scope.users.length > 0) {
                    userResults.fnAddData($scope.users);
                    userResults.fnAdjustColumnSizing();

                    $compile(userResults)($scope);
                }

                $scope.$applyAsync(function () {
                    window.ajaxLoadingPanel.css("display", "none");
                });
            });
        };

        $scope.initUrls = initUrls;
        $scope.get = getUsers;
    }]);
})(window.jQuery, window.angular);