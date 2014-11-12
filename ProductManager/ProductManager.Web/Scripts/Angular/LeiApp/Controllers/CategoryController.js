'use strict';

LeiApp.controller('CategoryController', ['$scope', function ($scope) {

    $scope.AllowButtonText = "Allow Delete";
    $scope.showEdit = false;

    $scope.AllowEdit = function () {
        if ($scope.showEdit) {
            $scope.showEdit = false;
            $scope.AllowButtonText = "Enable Delete";
        } else {
            $scope.showEdit = true;
            $scope.AllowButtonText = "Disable Delete";
        }
    };

}]);


LeiApp.controller('ProductListController', ['$scope','$filter', 'promiseService', 'ngTableParams', function ($scope, $filter, promiseService, ngTableParams) {
        var subCategoryId = $('input#subCategoryId').val();
        var productPromise = promiseService.callActionPromise('/Product/AllProducts?subcategoryId=' + subCategoryId);
        productPromise.then(function(data) {
            $scope.allProducts = data;

            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                filter: {
                    Name: ''
                },
                sorting: {
                    Name: 'asc'
                }
            }, {
                total: data.length,
                getData: function ($defer, params) {
                    var filteredData = params.filter() ?
                        $filter('filter')(data, params.filter()) :
                            data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            data;

                    params.total(orderedData.length); 
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });

        });

}]);


LeiApp.controller('CreateProductController', ['$scope', 'promiseService', function($scope, promiseService) {


    $scope.ProductTypeChange = function () {
        var pricePlanPromise = promiseService.callActionPromise('/Product/PricePlans?productTypeId=' + $scope.ngProductType);
        pricePlanPromise.then(function (data) {
            $scope.priceplans = data;

            console.log(data);
        });
    };

}]);
