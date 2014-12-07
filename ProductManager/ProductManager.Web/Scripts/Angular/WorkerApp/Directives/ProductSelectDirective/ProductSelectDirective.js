'use strict';

WorkerApp.controller('productSelectDirController', ['$scope', '$filter', 'promiseService', 'orderStorageService', function ($scope,$filter,promiseService, orderStorageService) {

    this.setOrderline = function (a) {
        $scope.orderline = a;
    };

    $scope.selection = {};
    $scope.selection.orderlines = [];
    $scope.addOns = [];
    

    $scope.checkOrderLineStorage = function () {
        var currentStorage = orderStorageService.getOrderStorage();
        if (!angular.isUndefined(currentStorage.productTypeGroups) && currentStorage.productTypeGroups !== null) {
            var currentTypeGroup = $filter('filter')(currentStorage.productTypeGroups, function (x) { return x.indexId.toString() === $scope.groupIndexId.toString(); })[0];
            if (currentTypeGroup.orderlines.length > 0) {
                $scope.selection.orderlines = currentTypeGroup.orderlines;
            }

        };
    };

    $scope.checkOrderLineStorage();


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
    this.setAddon = function (id, name, price) {
        var addOn = new AddOn(id, name, price);
        $scope.addOns.push(addOn);

    };
    this.removeAddon = function (id) {
        var targetAddon = $filter('filter')($scope.addOns, function (x) { return x.id.toString() === id.toString(); })[0];
        var index = $scope.addOns.indexOf(targetAddon);
        $scope.addOns.splice(index, 1);

    };





    $scope.addFields = function () {
        if (typeof $scope.selection.orderlines === 'undefined') {
            $scope.selection.orderlines = [];
        }
        var productPromise = promiseService.callActionPromise('/Orders/GetProductById?productId=' + $scope.selection.selectedProduct + '&productTypeId=' + $scope.productTypeId + '&width=' + $scope.orderline.width + '&height=' + $scope.orderline.height);

        productPromise.then(function (data) {
            var currentAddons = [];
            angular.copy($scope.addOns, currentAddons);
            var orderline = new OrderLine(data.Id, data.Name, $scope.orderline.width, $scope.orderline.height, $scope.orderline.amount, 0, data.UnitPrice, currentAddons, $scope.selection.additionalInfo);
            
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