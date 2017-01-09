﻿var app = angular.module('app', ['ui.bootstrap']);
app.controller('SIMCardCtrl', ['$scope', '$uibModal', function ($scope, $uibModal) {

    $scope.create = function () {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCards/Create",
            controller: 'SIMCardCreateCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () {});
    }

    

    $scope.edit = function (id) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCards/edit/"+id,
            controller: 'SIMCardEditCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }


    $scope.details = function (id) {
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: "/SIMCards/details/" + id,
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