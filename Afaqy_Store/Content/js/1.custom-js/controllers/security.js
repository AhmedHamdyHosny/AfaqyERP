angular.module('app.securityControllers', [])

//SystemRole controllers ========
.controller('SystemRoleCtrl', SystemRoleCtrl)
.controller('SystemRoleCreateCtrl', SystemRoleCreateCtrl)
.controller('SystemRoleEditCtrl', SystemRoleEditCtrl)
.controller('SystemRoleDetailsCtrl', SystemRoleDetailsCtrl)

//SystemRole functions ========
function SystemRoleCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'SystemRoleCreateCtrl',
            windowClass: 'meduim-Modal',
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
            controller: 'SystemRoleEditCtrl',
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
            controller: 'SystemRoleDetailsCtrl',
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
                selectedIds.push(item.RoleId)
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

function SystemRoleCreateCtrl($scope, $uibModalInstance, uiGridConstants, $q, global) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    var gridOptions = {}

    //Pagination
    $scope.pagination = {
        paginationPageSizes: ["All"],
        ddlpageSize: "All",
        pageNumber: 1,
        pageSize: -1,
        totalItems: 0,

        getTotalPages: function () {
            return Math.ceil(this.totalItems / this.pageSize);
        },
        pageSizeChange: function () {
            if (this.ddlpageSize == "All")
                this.pageSize = -1;
            else
                this.pageSize = this.ddlpageSize

            this.pageNumber = 1
            $scope.GetItems();
        },
        firstPage: function () {
            if (this.pageNumber > 1) {
                this.pageNumber = 1
                $scope.GetItems();
            }
        },
        nextPage: function () {
            if (this.pageNumber < this.getTotalPages()) {
                this.pageNumber++;
                $scope.GetItems();
            }
        },
        previousPage: function () {
            if (this.pageNumber > 1) {
                this.pageNumber--;
                $scope.GetItems();
            }
        },
        lastPage: function () {
            if (this.pageNumber >= 1) {
                this.pageNumber = this.getTotalPages();
                $scope.GetItems();
            }
        }
    };

    //ui-Grid Call
    $scope.GetItems = function () {
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.gridOptions = {
            useExternalPagination: true,
            useExternalSorting: false,
            enableFiltering: true,
            enableRowSelection: false,
            enableSelectAll: true,
            columnDefs: serviceGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
                gridApi.core.on.filterChanged($scope,
                    function () {
                        var grid = this.grid;
                        //reset filters
                        gridOptions.Filters = [];
                        for (var i = 0; i < grid.columns.length; i++) {
                            term = grid.columns[i].filters[0].term;
                            field = grid.columns[i].field;
                            if (term != undefined && term != null && term != '') {
                                gridOptions.Filters.push({ Property: field, Operation: 'Like', Value: term, LogicalOperation: 'And' });
                            }
                        }
                        //console.log(JSON.stringify(gridOptions));
                        //rebind grid data
                        $scope.GetItems();
                    });
                gridApi.core.on.sortChanged($scope, function () {
                    var grid = this.grid;
                    //reset sort
                    gridOptions.Sorts = [];
                    for (var i = 0; i < grid.columns.length; i++) {
                        direction = grid.columns[i].sort.direction;
                        field = grid.columns[i].field;
                        priority = grid.columns[i].sort.priority
                        if (direction != undefined && direction != null && direction != '') {
                            gridOptions.Sorts.push({ Property: field, SortType: direction, Priority: priority });
                        }
                    }
                    //console.log(JSON.stringify(gridOptions));
                    //rebind grid data
                    $scope.GetItems();
                });
            },
            exporterAllDataFn: function () {
                return getPage(1, $scope.gridOptions.totalItems, paginationOptions.sort)
                .then(function () {
                    $scope.gridOptions.useExternalPagination = false;
                    $scope.gridOptions.useExternalSorting = false;
                    getPage = null;
                });
            },
        };
        var NextPage;
        if (isNaN($scope.pagination.pageNumber)) {
            NextPage = 1;
        } else {
            NextPage = $scope.pagination.pageNumber;
        }
        var NextPageSize = $scope.pagination.pageSize;
        if (NextPageSize != -1) {
            gridOptions.Paging = { PageNumber: NextPage, PageSize: NextPageSize };
        }
        else {
            gridOptions.Paging = null;
        }
        
        var url = getServiceAccessViewActionUrl;
        var formData = { options: gridOptions };
        global.post(url, formData, function (resp) {
            $scope.pagination.totalItems = resp.data.TotalItemsCount;
            $scope.gridOptions.data = resp.data.PageItems;
            $scope.gridOptions.selectedItems = [];
        }, function (resp) {
            console.log("Error: " + error);
        }, function () {
            $scope.loaderMore = true;
        }, function () {
            $scope.loaderMore = false;
        });
    }

    //Default Load
    $scope.GetItems();
    
    $scope.ToggoleGridFilter = function ($event) {
        $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
    }

    //$scope.ExportAllDataAsPdf = function () {
    //    var grid = $scope.gridApi.grid;
    //    var rowTypes = uiGridExporterConstants.ALL;
    //    var colTypes = uiGridExporterConstants.ALL;
    //    uiGridExporterService.pdfExport(grid, rowTypes, colTypes);
    //}

    //$scope.ExportVisibleDataAsPdf = function () {
    //    var grid = $scope.gridApi.grid;
    //    var rowTypes = uiGridExporterConstants.VISIBLE;
    //    var colTypes = uiGridExporterConstants.ALL;
    //    uiGridExporterService.pdfExport(grid, rowTypes, colTypes);
    //}

    //$scope.ExportAllDataAsCsv = function () {
    //    var grid = $scope.gridApi.grid;
    //    var rowTypes = uiGridExporterConstants.ALL;
    //    var colTypes = uiGridExporterConstants.ALL;
    //    uiGridExporterService.csvExport(grid, rowTypes, colTypes);
    //}

    //$scope.ExportVisibleDataAsCsv = function () {
    //    var grid = $scope.gridApi.grid;
    //    var rowTypes = uiGridExporterConstants.VISIBLE;
    //    var colTypes = uiGridExporterConstants.ALL;
    //    uiGridExporterService.csvExport(grid, rowTypes, colTypes);
    //}
}

function SystemRoleEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function SystemRoleDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}
