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

//DeliveryRequest controllers ========
.controller('DeliveryNoteCtrl', DeliveryNoteCtrl)
.controller('DeliveryNoteCreateCtrl', DeliveryNoteCreateCtrl)
.controller('DeliveryNoteEditCtrl', DeliveryNoteEditCtrl)
.controller('DeliveryNoteDetailsCtrl', DeliveryNoteDetailsCtrl)
.controller('DeliveryNoteReportCtrl', DeliveryNoteReportCtrl)
.controller('DeliveryNoteServerAddCtrl', DeliveryNoteServerAddCtrl)
.controller('DeliveryNoteTechnicalApprovalCtrl', DeliveryNoteTechnicalApprovalCtrl)
.controller('DeliveryNoteStoreDeviceNamingCtrl', DeliveryNoteStoreDeviceNamingCtrl)

//DeliveryReturn controllers ========
.controller('DeliveryReturnCtrl', DeliveryReturnCtrl)
.controller('DeliveryReturnCreateCtrl', DeliveryReturnCreateCtrl)
.controller('DeliveryReturnDetailsCtrl', DeliveryReturnDetailsCtrl)

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

    gridService.initGrid($scope, function () {
        for (var i = 0; i < $scope.gridOptions.data.length; i++) {
            $scope.gridOptions.data[i].showActions = $scope.gridOptions.data[i].EditPermission ||
                $scope.gridOptions.data[i].AssignPermission || 
                $scope.gridOptions.data[i].StoreReceivedPermission || 
                $scope.gridOptions.data[i].DeliveryPermission;
        }
    });

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

        //modalInstance.opened.then(function () {
        //    $scope.showSubmit = false;
        //});

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

    $scope.ReceivedDeliveryRequest = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: detailsActionUrl + '/' + id,
            controller: 'DeliveryRequestDetailsCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });

        //modalInstance.opened.then(function () {
        //    $scope.showSubmit = true;
        //});

        modalInstance.result.then(null, function () { });
    }

    $scope.GotoDelivery = function (id) {
        window.location.href = DeliveryNoteUrl + "/" + id;
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
        if (SelectedCustomer != null && SelectedCustomer.Value != null) {
            data = getcustomerContactsFilterData(SelectedCustomer.Value);
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
        //Default Load
        $scope.GetItems();
        //bind item families
        //for (var i = 0; i < $scope.gridOptions.data.length; i++) {
        //    $scope.gridOptions.data[i].ItemFamilies = itemFamilies
        //    if ($scope.gridOptions.data[i].ItemFamilies != null && $scope.gridOptions.data[i].ItemFamilies.length > 0) {
        //        $scope.gridOptions.data[i].ItemFamily = $scope.gridOptions.data[i].ItemFamilies[0].Value;
        //        $scope.gridOptions.data[i].ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String($scope.gridOptions.data[i].ItemFamily) } }, true);
        //    }
        //}
    }
    $scope.bindModel = function (SelectedFamily, rowIndex) {
        $scope.gridOptions.data[rowIndex].ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(SelectedFamily) } }, true);
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
        $scope.create();

    }

    $scope.create = function () {
        var gridRowData = { ItemFamilies: itemFamilies, ItemFamily: itemFamilies[0].Value.toString(), Quantity: 1, Note: '' }
        if (gridRowData.ItemFamily != null) {
            gridRowData.ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(gridRowData.ItemFamily) } }, true);
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

function DeliveryRequestEditCtrl($scope, $uibModalInstance, $uibModal, uiGridConstants, $q, $filter, global, confirmService) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.bindCustomers = function () {
        $scope.customers = customers;
        $scope.selectCustomer = $filter('filter')($scope.customers, { Selected: true })[0];
    }
    $scope.bindSystems = function () {
        $scope.systems = systems;
        $scope.selectSystem = $filter('filter')($scope.systems, { Selected: true })[0].Value;
    }
    $scope.bindCustomerContacts = function (SelectedCustomer, callBackFunc) {
        
        if (SelectedCustomer != null && SelectedCustomer.Value != null) {
            data = getcustomerContactsFilterData(SelectedCustomer.Value);
            //console.log(data);
            var url = ContactsGetInfoUrl;
            global.post(url, data, function (resp) {
                var result = resp.data
                //console.log(result);
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
            var gridRowData = { DeliveryRequestDetailsId: gridData[i].DeliveryRequestDetailsId, DeliveryRequestId: gridData[i].DeliveryRequestId, ItemFamilies: itemFamilies, ItemFamily: gridData[i].fa_code.toString(), Quantity: gridData[i].Quantity, Note: gridData[i].Note }
            if (gridRowData.ItemFamily != null) {
                gridRowData.ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(gridRowData.ItemFamily) } });
            }
            gridRowData.ModelType = gridData[i].ModelType_ia_item_id.toFixed(1).toString();
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
   
}

//DeliveryNote functions ========
function DeliveryNoteCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function (deliveryRequestId) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            //for test
            templateUrl: createActionUrl + '/' + deliveryRequestId,
            controller: 'DeliveryNoteCreateCtrl',
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
            controller: 'DeliveryNoteEditCtrl',
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
            controller: 'DeliveryNoteDetailsCtrl',
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
                selectedIds.push(item.TransactionId)
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

    if (deliveryRequestId != null && deliveryRequestId != ''){
        var requestId = Number(deliveryRequestId);
        if (!isNaN(requestId)) {
            $scope.create(requestId);
        }
    }
    
    $scope.report = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: reportActionUrl + '/' + id,
            controller: 'DeliveryNoteReportCtrl',
            windowClass: 'large-Modal',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.serverAdd = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: serverAddActionUrl + '/' + id,
            controller: 'DeliveryNoteServerAddCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });

        modalInstance.result.then(null, function () { });
    }

    $scope.technicalApproval = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: technicalApprovalActionUrl + '/' + id,
            controller: 'DeliveryNoteTechnicalApprovalCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }

    $scope.storeDeviceNaming = function (id) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: storeDeviceNamingActionUrl + '/' + id,
            controller: 'DeliveryNoteStoreDeviceNamingCtrl',
            windowClass: 'meduim-Modal',
            scope: $scope,
            backdrop: false,
        });
        modalInstance.result.then(null, function () { });
    }
}

function DeliveryNoteCreateCtrl($scope, $uibModalInstance, $uibModal, uiGridConstants, $q, $filter, global, confirmService) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.bindGirdData = function () {
        //Default Load
        $scope.GetItems();
        //store Delivery RequestId
        $scope.DeliveryRequestId = gridData[0].DeliveryRequestId;

        for (var i = 0; i < gridData.length; i++) {
            var gridRowData = { DeliveryRequestDetailsId: gridData[i].DeliveryRequestDetailsId, DeliveryRequestId: gridData[i].DeliveryRequestId, ItemFamilies: itemFamilies, ItemFamily: gridData[i].fa_code.toString(), Quantity: gridData[i].Quantity, Note: gridData[i].Note }

            if (gridRowData.ItemFamily != null) {
                gridRowData.ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(gridRowData.ItemFamily) } });
            }
            gridRowData.ModelType = gridData[i].ModelType_ia_item_id.toFixed(1).toString();
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
            columnDefs: deliveryDetailsGridColumnDefs,
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

    //Device Grid Region
    $scope.bindDeviceGirdData = function () {
        //Default Load
        $scope.GetDeviceItems();
        $scope.createDevice();
    }
    $scope.GetDeviceItems = function () {
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.deviceGridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: deliveryDeviceGridColumnDefs,
            onRegisterApi: function (deviceGridApi) {
                $scope.deviceGridApi = deviceGridApi;
                deviceGridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.deviceGridOptions.data = [];
        $scope.setDeviceRows();
    }
    $scope.createDevice = function () {
        var deviceGridRowData = { IMEI: '', DeviceId: 0, IMEI_LoadingClass:'' }
        $scope.deviceGridOptions.data.push(deviceGridRowData);
    }
    $scope.DeleteDevices = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            //get selected ids from grid
            var selectedItems = $scope.deviceGridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                $scope.deviceGridOptions.data.pop(item)
            });
            hideLoading();
        });
    }
    $scope.bindDeviceIMEI = function (index, imei) {
        //get device Id
        var data = getDeviceFilterData(imei);
        //reset device information
        $scope.deviceGridOptions.data[index].DeviceId = null;
        $scope.deviceGridOptions.data[index].IMEI_LoadingClass = "loading";
        var url = DeviceGetInfoUrl;
        global.post(url, data, function (resp) {
            var result = resp.data
            if (result != undefined && result != null) {
                if (result.length == 1) {
                    var device = result[0];
                    $scope.deviceGridOptions.data[index].DeviceId = device.DeviceId;
                    $scope.deviceGridOptions.data[index].ModelType_ia_item_id = device.ModelType_ia_item_id;
                }
            }
        }, function (resp) {
            console.log("Error: " + error);
        }, function () {

        }, function () {

        });
        $scope.deviceGridOptions.data[index].IMEI_LoadingClass = "";
    }
    $scope.setDeviceRows = function () {
        //get total device count
        var totalDeviceCount = 0;
        //filter by device only
        var deviceRows = $filter('filter')($scope.gridOptions.data, { ItemFamily: "1" });
        for (var i = 0; i < deviceRows.length ; i++) {
            totalDeviceCount += deviceRows[i].Quantity
        }
        
        var deviceCount = $scope.deviceGridOptions.data.length;
        if (deviceCount == 0) {
            deviceCount = 1;
        }
        if (totalDeviceCount > deviceCount) {
            for (var i = deviceCount; i < totalDeviceCount ; i++) {
                $scope.createDevice();
            }
        }
        else if (totalDeviceCount < deviceCount) {
            for (var i = deviceCount; i > totalDeviceCount ; i--) {
                $scope.deviceGridOptions.data.pop()
            }
        }
    }
    
}

function DeliveryNoteEditCtrl($scope, $uibModalInstance, $uibModal, uiGridConstants, $q, $filter, global, confirmService) {
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
    $scope.bindCustomerContacts = function (SelectedCustomer, callBackFunc) {
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
        //store Delivery Note Id
        $scope.TransactionId = gridData[0].TransactionId;

        for (var i = 0; i < gridData.length; i++) {
            var gridRowData = { DeliveryDetailsId: gridData[i].DeliveryDetailsId, TransactionId: gridData[i].DeliveryNoteId, ItemFamilies: itemFamilies, ItemFamily: gridData[i].DeviceModelType.ItemFamilyId.toString(), Quantity: gridData[i].Quantity, Note: gridData[i].Note }

            if (gridRowData.ItemFamily != null) {
                gridRowData.ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(gridRowData.ItemFamily) } });
            }
            gridRowData.ModelType = gridData[i].ModelType_ia_item_id.toString()
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
        var gridRowData = { DeliveryDetailsId: 0, TransactionId: $scope.TransactionId, ItemFamilies: itemFamilies, ItemFamily: itemFamilies[0].Value.toString(), Quantity: 1, Note: '' }

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

function DeliveryNoteDetailsCtrl($scope, $uibModalInstance) {
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

function DeliveryNoteReportCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

}

function DeliveryNoteServerAddCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //var gridOptions = {}
    //ui-Grid Call
    $scope.bindDeliveryDetailsAndDevicesGirdData = function () {
        $scope.bindDeliveryDevicesGirdData();
        $scope.bindDeliveryDetailsGirdData();
    }

    $scope.bindDeliveryDetailsGirdData = function () {
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.deliveryDetailsGridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: deliveryDetailsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.deliveryDetailsGridOptions.data = deliveryDetailsData;
    }

    $scope.bindDeliveryDevicesGirdData = function () {
        
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.deliveryDevicesGridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: false,
            enableSelectAll: false,
            columnDefs: deliveryDeviceGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };
        $scope.deliveryDevicesGridOptions.data = deliveryDevicesData;
    }
}

function DeliveryNoteTechnicalApprovalCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //var gridOptions = {}
    //ui-Grid Call
    $scope.bindDeliveryDetailsAndDevicesGirdData = function () {
        $scope.bindDeliveryDevicesGirdData();
        $scope.bindDeliveryDetailsGirdData();
    }

    $scope.bindDeliveryDetailsGirdData = function () {
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.deliveryDetailsGridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: deliveryDetailsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.deliveryDetailsGridOptions.data = deliveryDetailsData;
    }

    $scope.bindDeliveryDevicesGirdData = function () {

        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.deliveryDevicesGridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: false,
            enableSelectAll: false,
            columnDefs: deliveryDeviceGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };
        $scope.deliveryDevicesGridOptions.data = deliveryDevicesData;
    }

    $scope.selectAll = function (checked) {
        for (var i = 0; i < $scope.deliveryDevicesGridOptions.data.length; i++) {
            $scope.deliveryDevicesGridOptions.data[i].TechnicalApproval = checked;
        }
    }
}

function DeliveryNoteStoreDeviceNamingCtrl($scope, $uibModalInstance) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //var gridOptions = {}
    //ui-Grid Call
    $scope.bindDeliveryDetailsAndDevicesGirdData = function () {
        $scope.bindDeliveryDevicesGirdData();
        $scope.bindDeliveryDetailsGirdData();
    }

    $scope.bindDeliveryDetailsGirdData = function () {
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.deliveryDetailsGridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: deliveryDetailsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.deliveryDetailsGridOptions.data = deliveryDetailsData;
    }

    $scope.bindDeliveryDevicesGirdData = function () {

        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.deliveryDevicesGridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: false,
            enableSelectAll: false,
            columnDefs: deliveryDeviceGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };
        $scope.deliveryDevicesGridOptions.data = deliveryDevicesData;

        for (var i = 0; i < $scope.deliveryDevicesGridOptions.data.length; i++) {
            $scope.deliveryDevicesGridOptions.data[i].NamingTypes = namingTypes;
            $scope.deliveryDevicesGridOptions.data[i].DeviceNamingType = namingTypes[0].Value.toString();
        }
    }

}


//DeliveryReturn functions ========
function DeliveryReturnCtrl($scope, $uibModal, confirmService, global, gridService, ctrlService) {

    ctrlService.initCtrl($scope);

    gridService.initGrid($scope);

    gridService.configureExport($scope);

    $scope.create = function (deliveryNoteId) {
        showLoading();
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: createActionUrl + '/' + deliveryNoteId,
            controller: 'DeliveryReturnCreateCtrl',
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
            controller: 'DeliveryReturnEditCtrl',
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
            controller: 'DeliveryReturnDetailsCtrl',
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
                selectedIds.push(item.TransactionId)
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

    if (deliveryNoteId != null && deliveryNoteId != '') {
        var transactionId = Number(deliveryNoteId);
        if (!isNaN(transactionId)) {
            $scope.create(transactionId);
        }
    }

}

function DeliveryReturnCreateCtrl($scope, $uibModalInstance, $uibModal, uiGridConstants, $q, $filter, global, confirmService) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.bindDetailsGirdData = function () {
        //Default Load
        $scope.GetDetailsItems();
        //store Delivery RequestId
        //$scope.DeliveryRequestId = gridData[0].DeliveryRequestId;
        //for (var i = 0; i < gridData.length; i++) {
        //    var gridRowData = { DeliveryRequestDetailsId: gridData[i].DeliveryRequestDetailsId, DeliveryRequestId: gridData[i].DeliveryRequestId, ItemFamilies: itemFamilies, ItemFamily: gridData[i].fa_code.toString(), Quantity: gridData[i].Quantity, Note: gridData[i].Note }
        //    if (gridRowData.ItemFamily != null) {
        //        gridRowData.ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(gridRowData.ItemFamily) } });
        //    }
        //    gridRowData.ModelType = gridData[i].ModelType_ia_item_id.toFixed(1).toString();
        //    $scope.gridOptions.data.push(gridRowData);
        //}

    }
    //ui-Grid Call
    $scope.GetDetailsItems = function () {
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
            columnDefs: deliveryDetailsGridColumnDefs,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.gridOptions.data = deliveryDetailsData;
    }
    //Device Grid Region
    $scope.bindDeviceGirdData = function () {
        //Default Load
        $scope.GetDeviceItems();
        $scope.createDevice();
    }
    $scope.GetDeviceItems = function () {
        $scope.result = "color-green";
        $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
            if (col.filters[0].term) {
                return 'header-filtered';
            } else {
                return '';
            }
        };
        $scope.deviceGridOptions = {
            useExternalPagination: false,
            useExternalSorting: false,
            enableFiltering: false,
            enableRowSelection: true,
            enableSelectAll: true,
            columnDefs: deliveryDeviceGridColumnDefs,
            onRegisterApi: function (deviceGridApi) {
                $scope.deviceGridApi = deviceGridApi;
                deviceGridApi.core.on.columnVisibilityChanged($scope, function (changedColumn) {
                    $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                });
            },
        };

        $scope.deviceGridOptions.data = [];
        $scope.setDeviceRows();
    }
    $scope.createDevice = function () {
        var deviceGridRowData = { IMEI: '', DeviceId: 0, IMEI_LoadingClass: '' }
        $scope.deviceGridOptions.data.push(deviceGridRowData);
    }
    $scope.DeleteDevices = function (ev) {
        var modalOptions = deleteModalOptions;
        confirmService.showModal({}, modalOptions).then(function (result) {
            showLoading();
            //get selected ids from grid
            var selectedItems = $scope.deviceGridApi.selection.getSelectedRows();
            selectedItems.forEach(function (item) {
                $scope.deviceGridOptions.data.pop(item)
            });
            hideLoading();
        });
    }
    $scope.bindDeviceIMEI = function (index, imei) {
        //get device Id
        var data = getDeviceFilterData(imei);
        //reset device information
        $scope.deviceGridOptions.data[index].DeviceId = null;
        $scope.deviceGridOptions.data[index].IMEI_LoadingClass = "loading";
        var url = DeviceGetInfoUrl;
        global.post(url, data, function (resp) {
            var result = resp.data
            if (result != undefined && result != null) {
                if (result.length == 1) {
                    var device = result[0];
                    $scope.deviceGridOptions.data[index].DeviceId = device.DeviceId;
                    $scope.deviceGridOptions.data[index].ModelType_ia_item_id = device.ModelType_ia_item_id;
                }
            }
        }, function (resp) {
            console.log("Error: " + error);
        }, function () {

        }, function () {

        });
        $scope.deviceGridOptions.data[index].IMEI_LoadingClass = "";
    }
    $scope.setDeviceRows = function () {
        //get total device count
        var totalDeviceCount = 0;
        //filter by device only
        var deviceRows = $filter('filter')($scope.gridOptions.data, { fa_code: "001001" });
        for (var i = 0; i < deviceRows.length ; i++) {
            totalDeviceCount += deviceRows[i].Quantity
        }
        if (totalDeviceCount > 0) {
            var deviceCount = $scope.deviceGridOptions.data.length;
            if (deviceCount == 0) {
                deviceCount = 1;
            }
            if (totalDeviceCount > deviceCount) {
                for (var i = deviceCount; i < totalDeviceCount ; i++) {
                    $scope.createDevice();
                }
            }
            else if (totalDeviceCount < deviceCount) {
                for (var i = deviceCount; i > totalDeviceCount ; i--) {
                    $scope.deviceGridOptions.data.pop()
                }
            }
        }
        
    }
}

function DeliveryReturnEditCtrl($scope, $uibModalInstance, $uibModal, uiGridConstants, $q, $filter, global, confirmService) {
    hideLoading();
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.bindGirdData = function () {
        //Default Load
        $scope.GetItems();
        //store Delivery Note Id
        $scope.TransactionId = gridData[0].TransactionId;

        for (var i = 0; i < gridData.length; i++) {
            var gridRowData = { DeliveryDetailsId: gridData[i].DeliveryDetailsId, TransactionId: gridData[i].DeliveryNoteId, ItemFamilies: itemFamilies, ItemFamily: gridData[i].DeviceModelType.ItemFamilyId.toString(), Quantity: gridData[i].Quantity, Note: gridData[i].Note }

            if (gridRowData.ItemFamily != null) {
                gridRowData.ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(gridRowData.ItemFamily) } });
            }
            gridRowData.ModelType = gridData[i].ModelType_ia_item_id.toString()
            $scope.gridOptions.data.push(gridRowData);
        }

    }
    $scope.bindModel = function (SelectedFamily, rowIndex) {
        $scope.gridOptions.data[rowIndex].ModelType = null;
        $scope.gridOptions.data[rowIndex].ModelTypes = $filter('filter')(modelTypes, { Group: { Name: String(SelectedFamily) } });
    }
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
        var gridRowData = { DeliveryDetailsId: 0, TransactionId: $scope.TransactionId, ItemFamilies: itemFamilies, ItemFamily: itemFamilies[0].Value.toString(), Quantity: 1, Note: '' }

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

function DeliveryReturnDetailsCtrl($scope, $uibModalInstance) {
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