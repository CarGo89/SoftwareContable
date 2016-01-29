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
            { data: "UserName", width: "auto" },
            { data: "Email", width: "auto" },
            { data: "Role.Name", width: "auto" }
        ];

        angular.element(document).ready(function () {
            userResults = $("#userResults").dataTable({
                responsive: true,
                autoWidth: true,
                deferRender: true,
                language: {
                    search: "Filtro:",
                    searchPlaceholder: "Filtrar Users",
                    emptyTable: "No se encontraron Users",
                    zeroRecords: "No se encontraron coincidencias",
                    lengthMenu: "Mostrar _MENU_ Users",
                    info: "Mostrando _START_ a _END_ de _TOTAL_ Users",
                    infoEmpty: "Mostrando 0 a 0 de 0 Users",
                    infoFiltered: "(filtro de _MAX_ total Users)"
                },
                columns: columns
            });

            $scope.get();
        });

        var getUsers = function () {
            var getUrl = urls.get;

            ajax.get(getUrl, false, function (data, textStatus, jqXhr) {
                $scope.users = data.users;
                $scope.roles = data.roles;

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

        $scope.users = [];
        $scope.newUser = {};
        $scope.validationMessages = [];
        $scope.updateValidationMessages = [];
        $scope.newUserModal = {};

        $scope.initUrls = initUrls;
        $scope.get = getUsers;
        $scope.onNewUserHidden = onNewUserHidden;
    }]);
})(window.jQuery, window.angular);