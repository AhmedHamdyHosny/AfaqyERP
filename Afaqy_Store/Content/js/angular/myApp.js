var app = angular.module('app', ['ui.bootstrap']);
app.controller('SIMCardCtrl', ['$scope', '$uibModal', function ($scope, $uibModal) {

    $scope.create = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCard/Create",
            controller: 'SIMCardCreateCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () {});
    }

    

    $scope.edit = function (id) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCard/edit/"+id,
            controller: 'SIMCardEditCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }


    $scope.details = function (id) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCard/details/" + id,
            controller: 'SIMCardDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}]);

app.controller('SIMCardCreateCtrl', ['$scope', '$uibModalInstance', function ($scope, $uibModalInstance) {
    $scope.cancel = function () {
        //it dismiss the modal
        $uibModalInstance.dismiss('cancel');
        //$uibModalInstance.close();
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



//----------------------- Device ---------------------------------

app.controller('DeviceCtrl', ['$scope', '$uibModal', function ($scope, $uibModal) {

    $scope.create = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/Device/Create",
            controller: 'DeviceCreateCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }



    $scope.edit = function (id) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/Device/edit/" + id,
            controller: 'DeviceEditCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }


    $scope.details = function (id) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/Device/details/" + id,
            controller: 'DeviceDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}]);