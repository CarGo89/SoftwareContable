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

        var initUrls = function (getUrl, saveUrl, authorizeUrl, validateUrl) {
            urls.get = getUrl;
            urls.save = saveUrl;
            urls.authorize = authorizeUrl;
            urls.validate = validateUrl;
        };

        var columns = [
            { data: "UserName", width: "auto" },
            { data: "Email", width: "auto" },
            { data: "Role.Name", width: "auto" },
            { data: "IsAuthorized", width: "auto" }
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
                columns: columns,
                columnDefs: [
                {
                    targets: [3],
                    createdCell: function (td, cellData, rowData, row, col) {
                        var jqTd = $(td).addClass("text-center").text("");

                        if (cellData === true) {
                            jqTd.text("Autorizado");
                        }
                        else if ($scope.isLoggedInUserAdmin) {
                            var htmlBtnAuthorize = "<button class='btn btn-sm btn-success' ng-click='authorizeUser(";

                            htmlBtnAuthorize = htmlBtnAuthorize.concat(rowData.Id).concat(")'>Autorizar</button>");

                            jqTd.append(htmlBtnAuthorize);
                        } else {

                            jqTd.text("No Autorizado");
                        }
                    }
                }]
            });

            $scope.get();
        });

        var getUsers = function () {
            var getUrl = urls.get;

            ajax.get(getUrl, false, function (data, textStatus, jqXhr) {
                $scope.users = data.users;
                $scope.isLoggedInUserAdmin = data.isLoggedInUserAdmin;

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

        var authorizeUser = function (userId) {
            var url = urls.authorize.concat("/").concat(userId);

            ajax.put(url, null, function (data, textStatus, jqXhr) {
                if (data === true) {
                    $scope.get();
                }
                else {
                    window.setWarningMessage(data);
                }
            });
        };

        $scope.users = [];
        $scope.newUser = {};
        $scope.validationMessages = [];
        $scope.updateValidationMessages = [];
        $scope.newUserModal = {};

        $scope.initUrls = initUrls;
        $scope.get = getUsers;
        $scope.authorizeUser = authorizeUser;
    }]);
})(window.jQuery, window.angular);