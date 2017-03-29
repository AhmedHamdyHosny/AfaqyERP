angular.module('app.storeControllers', [])

//Device controllers ========
.controller('DeviceCtrl', DeviceCtrl)
.controller('DeviceCreateCtrl', DeviceCreateCtrl)
.controller('DeviceEditCtrl', DeviceEditCtrl)
.controller('DeviceDetailsCtrl', DeviceDetailsCtrl)

//DeviceStatus controllers ========
.controller('DeviceStatusCtrl', DeviceStatusCtrl)
.controller('DeviceStatusEditCtrl', DeviceStatusEditCtrl)
.controller('DeviceStatusDetailsCtrl', DeviceStatusDetailsCtrl)

//DeviceModelType controllers ========
.controller('DeviceModelTypeCtrl', DeviceModelTypeCtrl)
.controller('DeviceModelTypeCreateCtrl', DeviceModelTypeCreateCtrl)
.controller('DeviceModelTypeEditCtrl', DeviceModelTypeEditCtrl)
.controller('DeviceModelTypeDetailsCtrl', DeviceModelTypeDetailsCtrl)

//DeliveryRequest controllers ========
.controller('DeliveryRequestCtrl', DeliveryRequestCtrl)
.controller('DeliveryRequestCreateCtrl', DeliveryRequestCreateCtrl)
.controller('DeliveryRequestEditCtrl', DeliveryRequestEditCtrl)
.controller('DeliveryRequestDetailsCtrl', DeliveryRequestDetailsCtrl)
.controller('DeliveryRequestAssignCtrl', DeliveryRequestAssignCtrl)

//Device functions ========
function DeviceCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'DeviceCreateCtrl',
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
            controller: 'DeviceEditCtrl',
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
            controller: 'DeviceDetailsCtrl',
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
                selectedIds.push(item.DeviceId)
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

function DeviceCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//DeviceStatus functions ========
function DeviceStatusCtrl($scope, $uibModal, gridService, ctrlService) {
    
    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'DeviceStatusEditCtrl',
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
            controller: 'DeviceStatusDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}

function DeviceStatusEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceStatusDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//DeviceModelType functions ========
function DeviceModelTypeCtrl($scope, $uibModal, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'DeviceModelTypeCreateCtrl',
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
            controller: 'DeviceModelTypeEditCtrl',
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
            controller: 'DeviceModelTypeDetailsCtrl',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

}

function DeviceModelTypeCreateCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceModelTypeEditCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

function DeviceModelTypeDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}

//DeliveryRequest functions ========
function DeliveryRequestCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function () {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl,
            controller: 'DeliveryRequestCreateCtrl',
            scope: $scope,
            backdrop: false,
        });

        //modalInstance.opened.then(function () {
            
        //});
        modalInstance.result.then(null, function () { });
    }

    $scope.edit = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: editActionUrl + '/' + id,
            controller: 'DeliveryRequestEditCtrl',
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
            controller: 'DeliveryRequestDetailsCtrl',
            windowClass: 'meduim-Modal',
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
                selectedIds.push(item.DeliveryRequestId)
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

    $scope.assign = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: assignActionUrl + '/' + id,
            controller: 'DeliveryRequestAssignCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }
}

function DeliveryRequestCreateCtrl($scope, $uibModalInstance, $uibModal, uiGridConstants, $q, $filter, global, confirmService) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.bindCustomers = function () {
        $scope.customers = customers;
    }
    $scope.bindSystems = function () {
        $scope.systems = systems;
    }
    $scope.bindCustomerContacts = function (SelectedCustomer) {
        if (SelectedCustomer != null) {
            data = getcustomerContactsFilterData(SelectedCustomer);
            var url = ContactsGetInfoUrl;
            global.post(url, data, function (resp) {
                var result = resp.data
                if (result != null && result.length > 0) {
                    $scope.customerContacts = result;
                }
            }, function (resp) {
                console.log("Error: " + error);
            }, function () {
            }, function () {
            });
        }
        
    }
    $scope.bindGirdData = function () {
        //bind item families
        for (var i = 0; i < $scope.gridOptions.data.length; i++) {
            $scope.gridOptions.data[i].ItemFamilies = itemFamilies
            if ($scope.gridOptions.data[i].ItemFamilies != null && $scope.gridOptions.data[i].ItemFamilies.length > 0) {
                $scope.gridOptions.data[i].ItemFamily = $scope.gridOptions.data[i].ItemFamilies[0].Value;
                $scope.gridOptions.data[i].ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String($scope.gridOptions.data[i].ItemFamily) } });
            }

        }
    }
    $scope.bindModel = function (SelectedFamily, rowIndex) {
        $scope.gridOptions.data[rowIndex].ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(SelectedFamily) } });
    }

    var gridOptions = {}
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
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: requestDetailsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };
        
        $scope.gridOptions.data = [];
        //var requestDetails = { ModelTypes: $scope.ModelTypes, Quantity: 1 };
        //if (requestDetails.ItemFamilies != null && requestDetails.ItemFamilies.length > 0) {
        //    requestDetails.ItemFamily = requestDetails.ItemFamilies[0];
        //}
        $scope.gridOptions.data.push({Quantity: 1 });

    }
    //Default Load
    $scope.GetItems();

    $scope.create = function () {
        $scope.gridOptions.data.push({ ItemFamilies: itemFamilies, ModelTypes: modelTypes, Quantity: 1 });
    }
    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                $scope.gridOptions.data.pop(item)
            });
            hideLoading();
        });
    }
}

function DeliveryRequestEditCtrl($scope, $uibModalInstance, $uibModal, uiGridConstants, $q, $filter, global, confirmService) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.bindCustomers = function () {
        $scope.customers = customers;
        $scope.selectCustomer = $filter('filter')($scope.customers, { Selected: true })[0].Value;
    }
    $scope.bindSystems = function () {
        $scope.systems = systems;
        $scope.selectSystem = $filter('filter')($scope.systems, { Selected: true })[0].Value;
    }
    $scope.bindCustomerContacts = function (SelectedCustomer,callBackFunc) {
        if (SelectedCustomer != null) {
            data = getcustomerContactsFilterData(SelectedCustomer);
            var url = ContactsGetInfoUrl;
            global.post(url, data, function (resp) {
                var result = resp.data
                $scope.selectCustomerContact = null;
                $scope.customerContacts = result;
                if (callBackFunc != null) {
                    callBackFunc();
                }
            }, function (resp) {
                console.log("Error: " + error);
            }, function () {
            }, function () {
            });
        }
        else {
            if ($scope.selectCustomer != null) {
                $scope.bindCustomerContacts($scope.selectCustomer, function () { $scope.selectCustomerContact = selectCustomerContact });
            }
            $scope.selectCustomerContact = selectCustomerContact;
        }

    }
    $scope.bindGirdData = function () {
        //Default Load
        $scope.GetItems();
        //store Delivery RequestId
        $scope.DeliveryRequestId = gridData[0].DeliveryRequestId;

        for (var i = 0; i < gridData.length; i++) {
            var gridRowData = { DeliveryRequestDetailsId: gridData[i].DeliveryRequestDetailsId, DeliveryRequestId: gridData[i].DeliveryRequestId, ItemFamilies: itemFamilies, ItemFamily: gridData[i].DeviceModelType.ItemFamilyId.toString(), Quantity: gridData[i].Quantity, Note: gridData[i].Note }
            
            if (gridRowData.ItemFamily != null) {
                gridRowData.ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(gridRowData.ItemFamily) } });
            }
            gridRowData.ModelType = gridData[i].DeviceModelTypeId.toString()
            $scope.gridOptions.data.push(gridRowData);
        }

    }
    $scope.bindModel = function (SelectedFamily, rowIndex) {
        $scope.gridOptions.data[rowIndex].ModelType = null;
        $scope.gridOptions.data[rowIndex].ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(SelectedFamily) } });
    }

    var gridOptions = {}
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
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: editRequestDetailsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.gridOptions.data = [];
    }

    $scope.create = function () {
        var gridRowData = { DeliveryRequestDetailsId: 0, DeliveryRequestId: $scope.DeliveryRequestId, ItemFamilies: itemFamilies, ItemFamily: itemFamilies[0].Value.toString(), Quantity: 1, Note: '' }

        if (gridRowData.ItemFamily != null) {
            gridRowData.ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(gridRowData.ItemFamily) } });
        }
        $scope.gridOptions.data.push(gridRowData);
    }
    $scope.DeleteItems = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            //get selected ids from grid
            var selectedItems = $scope.gridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                $scope.gridOptions.data.pop(item)
            });
            hideLoading();
        });
    }
}

function DeliveryRequestDetailsCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    var gridOptions = {}
    //ui-Grid Call
    $scope.bindGirdData = function () {
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.gridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: detailsRequestDetailsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.gridOptions.data = gridData;
    }
}

function DeliveryRequestAssignCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
    var gridOptions = {}
    //ui-Grid Call
    $scope.bindGirdData = function () {
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.gridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: detailsRequestDetailsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.gridOptions.data = gridData;
    }
    //set default show
    //$scope.showDetails = true;
    //$scope.showAssign = false;

    //$scope.next = function () {
    //    $scope.showDetails = false;
    //    $scope.showAssign = true;
    //}
    //$scope.back = function () {
    //    $scope.showDetails = true;
    //    $scope.showAssign = false;
    //}

}