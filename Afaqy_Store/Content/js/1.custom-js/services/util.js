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

function gridService(uiGridConstants, $interval, $q, uiGridExporterConstants, uiGridExporterService, global) {
    
    return {
        initGrid: function ($scope, postBind) {
            var fakeI18n = function (title) {
                var deferred = $q.defer();
                $interval(function () {
                    deferred.resolve('col: ' + title);
                }, 1000, 1);
                return deferred.promise;
            };

            // filter
            //$scope.filterOptions = {
            //    filterText: "",
            //    useExternalFilter: true
            //};

            var gridOptions = {}

            //Pagination
            $scope.pagination = {
                paginationPageSizes: [15, 25, 50, 75, 100], //, "All"
                ddlpageSize: 15,
                pageNumber: 1,
                pageSize: 15,
                totalItems: 0,

                getTotalPages: function () {
                    return Math.ceil(this.totalItems / this.pageSize);
                },
                pageSizeChange: function () {
                    if (this.ddlpageSize == "All")
                        this.pageSize = $scope.pagination.totalItems;
                    else
                        this.pageSize = this.ddlpageSize

                    this.pageNumber = 1
                    $scope.GetProducts();
                },
                firstPage: function () {
                    if (this.pageNumber > 1) {
                        this.pageNumber = 1
                        $scope.GetProducts();
                    }
                },
                nextPage: function () {
                    if (this.pageNumber < this.getTotalPages()) {
                        this.pageNumber++;
                        $scope.GetProducts();
                    }
                },
                previousPage: function () {
                    if (this.pageNumber > 1) {
                        this.pageNumber--;
                        $scope.GetProducts();
                    }
                },
                lastPage: function () {
                    if (this.pageNumber >= 1) {
                        this.pageNumber = this.getTotalPages();
                        $scope.GetProducts();
                    }
                }
            };

            //ui-Grid Call
            $scope.GetProducts = function () {
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
                    //filterOptions: $scope.filterOptions,
                    //keepLastSelected: true,
                    //showColumnMenu: true,
                    //showFilter: true,
                    //showFooter: true,

                    //enableSorting: true,
                    enableRowSelection: true,
                    enableSelectAll: true,
                    enableGridMenu: true,
                    gridMenuTitleFilter: fakeI18n,
                    exporterMenuCsv: false,
                    exporterMenuPdf: false,
                    columnDefs: gridColumnDefs,
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
                                $scope.GetProducts();
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
                            $scope.GetProducts();
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
                }else{
                    NextPage = $scope.pagination.pageNumber;
                }
                var NextPageSize = $scope.pagination.pageSize;
                gridOptions.Paging = { PageNumber: NextPage, PageSize: NextPageSize };
                var url = getViewActionUrl; //+ '/' + NextPage + '/' + NextPageSize;
                global.post(url, gridOptions, function (resp) {
                    //console.log(JSON.stringify("hi--------------:"+resp.data));
                    $scope.pagination.totalItems = resp.data.TotalItemsCount;
                    $scope.gridOptions.data = resp.data.PageItems;
                    //console.log(JSON.stringify(resp.data.PageItems));
                    $scope.gridOptions.selectedItems = [];
                    if (postBind != null) {
                        postBind();
                    }
                    
                }, function (resp) {
                    console.log("Error: " + error);
                }, function () {
                    $scope.loaderMore = true;
                }, function () {
                    $scope.loaderMore = false;
                });
            }

            //Default Load
            $scope.GetProducts();

            //Selected Call
            //$scope.GetByID = function (model) {
            //    $scope.SelectedRow = model;
            //};

            //$scope.gridOptions = {
            //    enableFiltering: true,
            //    exporterMenuCsv: false,
            //    exporterMenuPdf: false,
            //    enableGridMenu: true,
            //    gridMenuTitleFilter: fakeI18n,
            //    columnDefs: gridColumnDefs,
            //    onRegisterApi: function (gridApi) {
            //        $scope.gridApi = gridApi;
            //        gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
            //            $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
            //        });
            //    }
            //};

            //$scope.gridOptions.data = gridData;
            //$scope.gridOptions.selectedItems = [];

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
                