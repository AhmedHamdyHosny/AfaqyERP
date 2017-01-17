angular.module('app.storeControllers', [])

//Device controllers ========
.controller('DeviceCtrl', DeviceCtrl)
.controller('DeviceCreateCtrl', DeviceCreateCtrl)
.controller('DeviceEditCtrl', DeviceEditCtrl)
.controller('DeviceDetailsCtrl', DeviceDetailsCtrl)

//SIMCard controllers ========
.controller('SIMCardCtrl', SIMCardCtrl)
.controller('SIMCardCreateCtrl', SIMCardCreateCtrl)
.controller('SIMCardEditCtrl', SIMCardEditCtrl)
.controller('SIMCardDetailsCtrl', SIMCardDetailsCtrl)

//SIMCardStatus controllers ========
.controller('SIMCardStatusCtrl', SIMCardStatusCtrl)
.controller('SIMCardStatusEditCtrl', SIMCardStatusEditCtrl)
.controller('SIMCardStatusDetailsCtrl', SIMCardStatusDetailsCtrl)

//DeviceStatus controllers ========
.controller('DeviceStatusCtrl', DeviceStatusCtrl)
.controller('DeviceStatusEditCtrl', DeviceStatusEditCtrl)
.controller('DeviceStatusDetailsCtrl', DeviceStatusDetailsCtrl)

//Device functions ========
function DeviceCtrl($scope, $uibModal, confirmService, global, $interval, $q, uiGridConstants) {
    showAlert();
    var fakeI18n = function (title) {
        var deferred = $q.defer();
        $interval(function () {
            deferred.resolve('col: ' + title);
        }, 1000, 1);
        return deferred.promise;
    };

    $scope.gridOptions = {
        enableFiltering: true,
        exporterMenuCsv: true,
        enableGridMenu: true,
        gridMenuTitleFilter: fakeI18n,
        columnDefs: gridColumnDefs,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;

            // interval of zero just to allow the directive to have initialized
            //$interval(function () {
            //    gridApi.core.addToGridMenu(gridApi.grid, [{ title: 'Dynamic item', order: 100 }]);
            //}, 0, 1);
            gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
            });
        }
    };

    $scope.gridOptions.data = gridData; //'@Html.Raw(JsonConvert.SerializeObject(this.Model))'
    $scope.gridOptions.selectedItems = [];

    $scope.ToggoleGridFilter = function ($event) {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
    }

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'DeviceCreateCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'DeviceEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'DeviceDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {

        var modalOptions = deleteModalOptions;

        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.DeviceId)
            });

            //call delete confirm method and pass ids
            //var config = {};
            var url = deleteActionUrl; 
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });

            hideLoading();
            //$http.post(url, data, config).then(
            //    function (resp) {
            //        if (resp) {
            //            location.reload();
            //        }
            //    },
            //    function (resp) {
            //    });

        });
    }
}

function DeviceCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//SIMCard functions ========
function SIMCardCtrl($scope, $uibModal, confirmService, global, $interval, $q, uiGridConstants) {
    showAlert();
    var fakeI18n = function (title) {
        var deferred = $q.defer();
        $interval(function () {
            deferred.resolve('col: ' + title);
        }, 1000, 1);
        return deferred.promise;
    };

    $scope.gridOptions = {
        enableFiltering: true,
        exporterMenuCsv: true,
        enableGridMenu: true,
        gridMenuTitleFilter: fakeI18n,
        columnDefs: gridColumnDefs,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;

            // interval of zero just to allow the directive to have initialized
            //$interval(function () {
            //    gridApi.core.addToGridMenu(gridApi.grid, [{ title: 'Dynamic item', order: 100 }]);
            //}, 0, 1);
            gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
            });
        }
    };

    $scope.gridOptions.data = gridData; //'@Html.Raw(JsonConvert.SerializeObject(this.Model))'
    $scope.gridOptions.selectedItems = [];

    $scope.ToggoleGridFilter = function ($event) {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
    }

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SIMCardCreateCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'SIMCardEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'SIMCardDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.DeleteItems = function (ev) {

        var modalOptions = deleteModalOptions;

        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.SIMCardId)
            });

            //call delete confirm method and pass ids
            var url = deleteActionUrl;
            var data = { ids: selectedIds };
            global.post(url, data, function (resp) {
                if (resp) {
                    location.reload();
                }
            }, function (resp) { });

            hideLoading();
        });
    }
}

function SIMCardCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}


//SIMCardStatus functions ========
function SIMCardStatusCtrl($scope, $uibModal, $interval, $q, uiGridConstants) {
    showAlert();
    var fakeI18n = function (title) {
        var deferred = $q.defer();
        $interval(function () {
            deferred.resolve('col: ' + title);
        }, 1000, 1);
        return deferred.promise;
    };

    $scope.gridOptions = {
        enableFiltering: true,
        exporterMenuCsv: true,
        enableGridMenu: true,
        gridMenuTitleFilter: fakeI18n,
        columnDefs: gridColumnDefs,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;

            // interval of zero just to allow the directive to have initialized
            //$interval(function () {
            //    gridApi.core.addToGridMenu(gridApi.grid, [{ title: 'Dynamic item', order: 100 }]);
            //}, 0, 1);
            gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
            });
        }
    };

    $scope.gridOptions.data = gridData; //'@Html.Raw(JsonConvert.SerializeObject(this.Model))'
    $scope.gridOptions.selectedItems = [];

    $scope.ToggoleGridFilter = function ($event) {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'SIMCardStatusEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'SIMCardStatusDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}

function SIMCardStatusEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardStatusDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//DeviceStatus functions ========
function DeviceStatusCtrl($scope, $uibModal, $interval, $q, uiGridConstants) {
    showAlert();
    var fakeI18n = function (title) {
        var deferred = $q.defer();
        $interval(function () {
            deferred.resolve('col: ' + title);
        }, 1000, 1);
        return deferred.promise;
    };

    $scope.gridOptions = {
        enableFiltering: true,
        exporterMenuCsv: true,
        enableGridMenu: true,
        gridMenuTitleFilter: fakeI18n,
        columnDefs: gridColumnDefs,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;

            // interval of zero just to allow the directive to have initialized
            //$interval(function () {
            //    gridApi.core.addToGridMenu(gridApi.grid, [{ title: 'Dynamic item', order: 100 }]);
            //}, 0, 1);
            gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
            });
        }
    };

    $scope.gridOptions.data = gridData; //'@Html.Raw(JsonConvert.SerializeObject(this.Model))'
    $scope.gridOptions.selectedItems = [];

    $scope.ToggoleGridFilter = function ($event) {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'DeviceStatusEditCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.details = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'DeviceStatusDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}

function DeviceStatusEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceStatusDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}