﻿angular.module('app.adminControllers', [])

//Branch controllers ========
.controller('BranchCtrl', BranchCtrl)
//.controller('BranchCreateCtrl', BranchCreateCtrl)
.controller('BranchEditCtrl', BranchEditCtrl)
.controller('BranchDetailsCtrl', BranchDetailsCtrl)

//SIMCardProvider controllers ========
.controller('SIMCardProviderCtrl', SIMCardProviderCtrl)
.controller('SIMCardProviderCreateCtrl', SIMCardProviderCreateCtrl)
.controller('SIMCardProviderEditCtrl', SIMCardProviderEditCtrl)
.controller('SIMCardProviderDetailsCtrl', SIMCardProviderDetailsCtrl)


//Branch functions ========
function BranchCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    //$scope.create = function () {
    //    showLoading();
    //    var modalInstance = $uibModal.open({
    //        animation: true,
    //        templateUrl: createActionUrl,
    //        controller: 'BranchCreateCtrl',
    //        scope: $scope,
    //        backdrop: false,
    //    });

    //    modalInstance.result.then(null, function () { });
    //}

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'BranchEditCtrl',
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
            controller: 'BranchDetailsCtrl',
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
                selectedIds.push(item.BranchId)
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

//function BranchCreateCtrl($scope, $uibModalInstance) {
//    hideLoading();
//    $scope.cancel = function () {
//        $uibModalInstance.dismiss('cancel');
//    };
//}

function BranchEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function BranchDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}


//SIMCardProvider functions ========
function SIMCardProviderCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SIMCardProviderCreateCtrl',
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
            controller: 'SIMCardProviderEditCtrl',
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
            controller: 'SIMCardProviderDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    //$scope.DeleteItems = function (ev) {
    //    var modalOptions = deleteModalOptions;
    //    confirmService.showModal({}, modalOptions).then(function (result) {
    //        showLoading();
    //        var selectedIds = [];
    //        //get selected ids from grid
    //        var selectedItems = $scope.gridApi.selection.getSelectedRows();
    //        selectedItems.forEach(function (item) {
    //            selectedIds.push(item.BranchId)
    //        });
    //        //call delete confirm method and pass ids
    //        var url = deleteActionUrl;
    //        var data = { ids: selectedIds };
    //        global.post(url, data, function (resp) {
    //            if (resp) {
    //                location.reload();
    //            }
    //        }, function (resp) { });

    //        hideLoading();
    //    });
    //}

}

function SIMCardProviderCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardProviderEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SIMCardProviderDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}