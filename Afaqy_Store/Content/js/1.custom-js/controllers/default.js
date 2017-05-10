angular.module('app.defaultControllers', [])

//Default Master Page controllers ========
//.controller('defaultCtrl', defaultCtrl)
.controller('accountCtrl', accountCtrl)
.controller('LastestNotificationCtrl', LastestNotificationCtrl)
.controller('NotificationCtrl', NotificationCtrl)



function accountCtrl($scope) {
    //$scope.signOut = function () {
    //    showLoading();
    //    var modalInstance = $uibModal.open({
    //        animation: true,
    //        templateUrl: loginActionUrl,
    //        controller: 'loginCtrl',
    //        scope: $scope,
    //        backdrop: false,
    //    });
    //    modalInstance.result.then(null, function () { });
    //}
}

function LastestNotificationCtrl($scope, $uibModal) {
    $scope.notifications = notifications
    $scope.openNotification = function (notification) {
        if (notification.PopupWindow) {
            showLoading();
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: notification.ReferenceLink,
                controller: 'DeliveryRequestDetailsCtrl',
                windowClass: notification.PopupWindowClass,
                scope: $scope,
                backdrop: false,
            });
            modalInstance.result.then(null, function () { });
        } else {
            window.location = notification.ReferenceLink;
        }
    }
}

function NotificationCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {
    ctrlService.initCtrl($scope);
    gridService.initGrid($scope);
    gridService.configureExport($scope);

    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            var selectedIds = [];
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                selectedIds.push(item.NotificationId)
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

//function loginCtrl($scope, $uibModalInstance, global) {
//    hideLoading();
//    $scope.login = function () {
//        global.post(loginActionUrl, $scope.user, function (resp) {
//            if (resp.data) {
//                $uibModalInstance.dismiss('cancel');
//                window.location('device')
//            }
//        }, function (resp) {
//        });
//    }
//    $scope.cancel = function () {
//        $uibModalInstance.dismiss('cancel');
//    };
//}