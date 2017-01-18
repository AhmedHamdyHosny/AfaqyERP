//angular.module('app.services', [])
//.service('confirmService', confirmService)

function confirmService($uibModal)
{
    var modalDefaults = {
        backdrop: true,
        keyboard: true,
        modalFade: true,
        templateUrl: '/Templates/Confirm.html'
    };

    var modalOptions = {
        closeButtonText: 'Close',
        actionButtonText: 'OK',
        headerText: 'Proceed?',
        bodyText: 'Perform this action?'
    };

    this.showModal = function (customModalDefaults, customModalOptions) {
        if (!customModalDefaults) customModalDefaults = {};
        customModalDefaults.backdrop = 'static';
        return this.show(customModalDefaults, customModalOptions);
    };

    this.show = function (customModalDefaults, customModalOptions) {
        //Create temp objects to work with since we're in a singleton service
        var tempModalDefaults = {};
        var tempModalOptions = {};

        //Map angular-ui modal custom defaults to modal defaults defined in service
        angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

        //Map modal.html $scope custom properties to defaults defined in service
        angular.extend(tempModalOptions, modalOptions, customModalOptions);

        if (!tempModalDefaults.controller) {
            tempModalDefaults.controller = function ($scope, $uibModalInstance) {
                $scope.modalOptions = tempModalOptions;
                $scope.modalOptions.ok = function (result) {
                    $uibModalInstance.close(result);
                };
                $scope.modalOptions.close = function (result) {
                    $uibModalInstance.dismiss('cancel');
                };
            }
        }

        return $uibModal.open(tempModalDefaults).result;
    }
}

function gridService(uiGridConstants, $interval, $q, uiGridExporterConstants, uiGridExporterService) {
    
    return {
        initGrid: function($scope){
            var fakeI18n = function (title) {
                var deferred = $q.defer();
                $interval(function () {
                    deferred.resolve('col: ' + title);
                }, 1000, 1);
                return deferred.promise;
            };

            $scope.gridOptions = {
                enableFiltering: true,
                exporterMenuCsv: false,
                exporterMenuPdf: false,
                enableGridMenu: true,
                gridMenuTitleFilter: fakeI18n,
                columnDefs: gridColumnDefs,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                        $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                    });
                }
            };

            $scope.gridOptions.data = gridData;
            $scope.gridOptions.selectedItems = [];

            $scope.ToggoleGridFilter = function ($event) {
                $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
                $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
            }
        },
        configureExport: function ($scope) {
            $scope.ExportAllDataAsPdf = function () {
                var grid = $scope.gridApi.grid;
                var rowTypes = uiGridExporterConstants.ALL;
                var colTypes = uiGridExporterConstants.ALL;
                uiGridExporterService.pdfExport(grid, rowTypes, colTypes);
            }

            $scope.ExportVisibleDataAsPdf = function () {
                var grid = $scope.gridApi.grid;
                var rowTypes = uiGridExporterConstants.VISIBLE;
                var colTypes = uiGridExporterConstants.ALL;
                uiGridExporterService.pdfExport(grid, rowTypes, colTypes);
            }

            $scope.ExportAllDataAsCsv = function () {
                var grid = $scope.gridApi.grid;
                var rowTypes = uiGridExporterConstants.ALL;
                var colTypes = uiGridExporterConstants.ALL;
                uiGridExporterService.csvExport(grid, rowTypes, colTypes);
            }

            $scope.ExportVisibleDataAsCsv = function () {
                var grid = $scope.gridApi.grid;
                var rowTypes = uiGridExporterConstants.VISIBLE;
                var colTypes = uiGridExporterConstants.ALL;
                uiGridExporterService.csvExport(grid, rowTypes, colTypes);
            }
        }
    }

    
}

function ctrlService() {
    return {
        initCtrl: function ($scope) {
            showAlert();
        }
    }


}
                