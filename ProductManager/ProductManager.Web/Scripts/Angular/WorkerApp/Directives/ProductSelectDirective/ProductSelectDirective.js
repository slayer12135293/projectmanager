'use strict';

WorkerApp.controller('productSelectDirController', ['$scope', 'promiseService', 'orderStorageService', function ($scope, promiseService, orderStorageService) {

    var categoriesPromise = promiseService.callActionPromise('/Orders/AllCategories');
    $scope.selection = {};
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


    this.setOrderline = function (a) {
        $scope.orderline =a;
    }; 

    $scope.addFields = function () {
        if (typeof $scope.selection.orderlines == 'undefined') {
            $scope.selection.orderlines = [];
        }


        console.log($scope);

        var productPromise = promiseService.callActionPromise('/Orders/GetProductById?productId=' + $scope.selection.selectedProduct + '&productTypeId=' + $scope.productTypeId);

        productPromise.then(function (data) {
            var orderline = { name: data.Name, id: data.Id, width: $scope.orderline.width, height: $scope.orderline.height, amount: $scope.orderline.amount, price:232 };
            $scope.selection.orderlines.push(orderline);
        });
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