angular.module('app.defaultControllers', [])

//Default Master Page controllers ========
//.controller('defaultCtrl', defaultCtrl)
.controller('accountCtrl', accountCtrl)
.controller('loginCtrl', loginCtrl)


function accountCtrl($scope, $uibModal) {
    $scope.signOut = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: loginActionUrl,
            controller: 'loginCtrl',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }
}

function loginCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}