'use strict';

WorkerApp.controller('productSelectDirController', ['$scope','promiseService', function($scope, promiseService) {

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
            var subCategoryPromise = promiseService.callActionPromise('/Orders/Products?subcategoryId=' + $scope.selection.selectedSubCategory);
            subCategoryPromise.then(function (data) {
                $scope.selection.products = data;
            });
        } else {
            $scope.selection.products = null;
        }

    };

    $scope.addFields = function () {
        if (typeof $scope.selection.orderlines == 'undefined') {
            $scope.selection.orderlines = [];
        }

        var productPromise = promiseService.callActionPromise('/Orders/GetProductById?productId=' + $scope.selection.selectedProduct);

        productPromise.then(function (data) {
            var orderline = { name: data.Name, id: data.Id };
            $scope.selection.orderlines.push(orderline);
        });
    };


    var addOnsPromise = promiseService.callActionPromise('/Orders/GetAddOnsByProductType?productId=' + $scope.productTypeId);
    addOnsPromise.then(function (data) {
        $scope.productAddOns = data;


       
    });

    $scope.$on('$viewContentLoaded', function () {
        alert('fired');
        $('input[type="checkbox"]').bootstrapSwitch();
    });
    

}]);
WorkerApp.directive('productSelect', function () {
    return {
        restrict: 'AE',
        controller: 'productSelectDirController',
        templateUrl: '/Scripts/Angular/WorkerApp/Directives/ProductSelectDirective/Template/ProductSelect.html',
        scope: {
            productTypeId: '@productTypeId'
        }
    };

});