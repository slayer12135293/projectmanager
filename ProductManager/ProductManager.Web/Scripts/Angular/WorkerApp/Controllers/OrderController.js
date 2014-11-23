'use strict';

WorkerApp.controller('OrderController', ['$scope', '$filter', 'promiseService', 'getProductTypesService', 'orderStorageService', function ($scope, $filter, promiseService, getProductTypesService, orderStorageService) {

    var productTypesPromise = getProductTypesService.getAllTypesPromise();
    $scope.typeGroup = {};
    $scope.selection = {};
    productTypesPromise.then(function (data) {
        $scope.selection.allProductTypes = data;
    });


    $scope.addTypeGroup = function () {
        var selectedTypeId = $scope.selection.selectedProductType;
        if (typeof $scope.typeGroup.groups === 'undefined') {
            $scope.typeGroup.groups = [];
        };

        var selectedType = $filter('filter')($scope.selection.allProductTypes, function (x) { return x.Id === selectedTypeId; })[0];

        var productTypeGroup = new ProductTypeGroup($scope.typeGroup.groups.length, selectedTypeId, selectedType.Name, selectedType.PriceCalculationType);

        $scope.typeGroup.groups.push(productTypeGroup);

        var currentOrderStorage = orderStorageService.getOrderStorage();
        if (currentOrderStorage === null || currentOrderStorage === undefined) {
            currentOrderStorage = new LocalStorageOrder();
            orderStorageService.saveOrderStorage(currentOrderStorage);
        }
        currentOrderStorage.productTypeGroups.push(productTypeGroup);
        orderStorageService.saveOrderStorage(currentOrderStorage);
   
    };


    
    $scope.removeTypeGroup = function (i) {
        $scope.typeGroup.groups.splice(i, 1);
        var currentOrderStorage = orderStorageService.getOrderStorage();
        currentOrderStorage.productTypeGroups.splice(i, 1);
        orderStorageService.saveOrderStorage(currentOrderStorage);
    };



}]);




