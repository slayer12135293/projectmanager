'use strict';

WorkerApp.controller('productSelectDirController', ['$scope', '$filter', 'promiseService', 'orderStorageService', function ($scope,$filter,promiseService, orderStorageService) {

    this.setOrderline = function (a) {
        $scope.orderline = a;
    };

    $scope.addOns = [];
    $scope.selection = {};
    $scope.selection.orderlines = [];

    $scope.checkOrderLineAndAddOnStorage = function() {
        var currentStorage = orderStorageService.getOrderStorage();
        if (!angular.isUndefined(currentStorage.productTypeGroups) && currentStorage.productTypeGroups !== null) {
            var currentTypeGroup = $filter('filter')(currentStorage.productTypeGroups, function (x) { return x.indexId.toString() === $scope.groupIndexId.toString(); })[0];
            if (currentTypeGroup.orderlines.length > 0) {
                $scope.selection.orderlines = currentTypeGroup.orderlines;
            }
            if (currentTypeGroup.addOns.length > 0) {
                $scope.addOns = currentTypeGroup.addOns;
            }

        };
    };

    $scope.checkOrderLineAndAddOnStorage();


    






    this.setAddon = function(id, name, price) {
        var addOn = new AddOn(id, name, price);
        $scope.addOns.push(addOn);
        $scope.saveAddOnsToStorage();

    };
    this.removeAddon = function (id) {
        var targetAddon = $filter('filter')($scope.addOns, function (x) { return x.id.toString() === id.toString(); })[0];
        var index = $scope.addOns.indexOf(targetAddon);
        $scope.addOns.splice(index, 1);
        $scope.saveAddOnsToStorage();


    };

    $scope.saveAddOnsToStorage = function() {
        var currentOrderStorage = orderStorageService.getOrderStorage();
        var productTypeGroupsInStorage = currentOrderStorage.productTypeGroups;
        var currentProductTypeGroup = $filter('filter')(productTypeGroupsInStorage, function (x) { return x.indexId.toString() === $scope.groupIndexId.toString(); })[0];
        currentProductTypeGroup.addOns = $scope.addOns;
        orderStorageService.saveOrderStorage(currentOrderStorage);

        console.log($scope.groupIndexId);
    };



    var categoriesPromise = promiseService.callActionPromise('/Orders/AllCategories');
    $scope.selection.hasProduct = true;
    categoriesPromise.then(function (data) {
        $scope.selection.categories = data;
    });

    $scope.UpdateSubCatagories = function () {
        if ($scope.selection.selectedCategory != null) {
            var subCategoryPromise = promiseService.callActionPromise('/Orders/SubCategories?categoryId=' + $scope.selection.selectedCategory);
            subCategoryPromise.then(function (data) {
                $scope.selection.subcategories = data;
                $scope.selection.products = null;
            });
        } else {
            $scope.selection.subcategories = null;
            $scope.selection.products = null;
        }
    };

    $scope.UpdateProducts = function () {
        if ($scope.selection.selectedSubCategory != null) {
            var subCategoryPromise = promiseService.callActionPromise('/Orders/Products?subcategoryId=' + $scope.selection.selectedSubCategory + '&productTypeId=' + $scope.productTypeId);
            subCategoryPromise.then(function (data) {
                $scope.selection.products = data;
            });
        } else {
            $scope.selection.products = null;
        }

    };

    $scope.addFields = function () {
        if (typeof $scope.selection.orderlines === 'undefined') {
            $scope.selection.orderlines = [];
        }
        var productPromise = promiseService.callActionPromise('/Orders/GetProductById?productId=' + $scope.selection.selectedProduct + '&productTypeId=' + $scope.productTypeId);

        productPromise.then(function (data) {
            var orderline = { name: data.Name, id: data.Id, width: $scope.orderline.width, height: $scope.orderline.height, amount: $scope.orderline.amount, price:232 };
            $scope.selection.orderlines.push(orderline);
            $scope.saveOrderLineToStorage();
        });
    };

    $scope.saveOrderLineToStorage = function () {
        var currentOrderStorage = orderStorageService.getOrderStorage();
        var productTypeGroupsInStorage = currentOrderStorage.productTypeGroups;
        var currentProductTypeGroup = $filter('filter')(productTypeGroupsInStorage, function (x) { return x.indexId.toString() === $scope.groupIndexId.toString(); })[0];
        currentProductTypeGroup.orderlines = $scope.selection.orderlines;
        orderStorageService.saveOrderStorage(currentOrderStorage);
    };


    $scope.removeAField = function(i) {
        $scope.selection.orderlines.splice(i, 1);
        $scope.saveOrderLineToStorage();
    };




    var addOnsPromise = promiseService.callActionPromise('/Orders/GetAddOnsByProductType?productTypeId=' + $scope.productTypeId);
    addOnsPromise.then(function (data) {
        $scope.productAddOns = data;
    });


   


}]);
WorkerApp.directive('productSelect', function () {
    return {
        restrict: 'AE',
        controller: 'productSelectDirController',
        templateUrl: '/Scripts/Angular/WorkerApp/Directives/ProductSelectDirective/Template/ProductSelect.html',
        scope: {
            productTypeId: '@productTypeId',
            calculationType: '@calculationType',
            groupIndexId: '@groupIndexId'
        }
    };

});