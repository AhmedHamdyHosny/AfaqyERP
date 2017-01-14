function DeviceCtrl($scope, $uibModal, confirmService, $http, $interval, $q, uiGridConstants) {
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
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.DeviceId)
            });

            //call delete confirm method and pass ids
            var config = {};
            var url = deleteActionUrl; 
            var data = { ids: selectedIds };
            $http.post(url, data, config).then(
                function (resp) {
                    if (resp) {
                        location.reload();
                    }
                },
                function (resp) {
                });

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