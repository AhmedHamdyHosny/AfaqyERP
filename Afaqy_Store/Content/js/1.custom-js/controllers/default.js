angular.module('app.defaultControllers', [])

//Default Master Page controllers ========
//.controller('defaultCtrl', defaultCtrl)
.controller('accountCtrl', accountCtrl)
.controller('NotificationCtrl',NotificationCtrl)


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

function NotificationCtrl($scope, $uibModal) {
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