var app = angular.module('app', ['ui.bootstrap']);
app.controller('SIMCardCtrl', ['$scope', '$uibModal', function ($scope, $uibModal) {

    $scope.create = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCards/Create",
            controller: 'SIMCardCreateCtrl',
            backdrop: false,
        });
    }

    $scope.edit = function (id) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCards/edit/"+id,
            controller: 'SIMCardEditCtrl',
            backdrop: false,
        });
    }


    $scope.details = function (id) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCards/details/" + id,
            controller: 'SIMCardDetailsCtrl',
            backdrop: false,
        });
    }

}]);

app.controller('SIMCardCreateCtrl', ['$scope', '$uibModalInstance', function ($scope, $uibModalInstance) {
    $scope.cancel = function () {
        //it dismiss the modal
        $uibModalInstance.dismiss('cancel');
    };
}]);

app.controller('SIMCardEditCtrl', ['$scope', '$uibModalInstance', function ($scope, $uibModalInstance) {
    $scope.cancel = function () {
        //it dismiss the modal
        $uibModalInstance.dismiss('cancel');
    };
}]);


app.controller('SIMCardDetailsCtrl', ['$scope', '$uibModalInstance', function ($scope, $uibModalInstance) {
    $scope.cancel = function () {
        //it dismiss the modal
        $uibModalInstance.dismiss('cancel');
    };
}]);