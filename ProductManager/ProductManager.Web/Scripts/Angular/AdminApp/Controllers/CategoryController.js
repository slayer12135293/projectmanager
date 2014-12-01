'use strict';

AdminApp.controller('CategoryController', ['$scope', function ($scope) {

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

    $('#myCollapsible').collapse({
        toggle: false
    });

}]);


AdminApp.controller('ProductListController', ['$scope','$filter', 'promiseService', 'ngTableParams', function ($scope, $filter, promiseService, ngTableParams) {
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
                    ProductType: 'asc'
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


AdminApp.controller('CreateProductController', ['$scope', 'promiseService', function($scope, promiseService) {
    $scope.usePricePlan = false;
    $scope.ProductTypeChange = function () {
        var userPricePlanPromise = promiseService.callActionPromise('/ProductTypes/UsePricePlan?productTypeId=' + $scope.ngProductType);
        
        userPricePlanPromise.then(function (data) {
            $scope.usePricePlan = data.Result;
        });


        var pricePlanPromise = promiseService.callActionPromise('/Product/PricePlans?productTypeId=' + $scope.ngProductType);
        pricePlanPromise.then(function (data) {
            $scope.priceplans = data;
        });

        
    };

}]);


AdminApp.controller('PricePlanController', ['$scope', 'promiseService', 'ngTableParams', '$filter', function ($scope, $promiseService, ngTableParams, $filter) {
    var pricePlansPromise = $promiseService.callActionPromise('/Priceplan/GetPriceplans');
    pricePlansPromise.then(function(data) {
        $scope.tableParams = new ngTableParams({
            page: 1,
            count: 10,
            filter: {
                ProductTypeName: ''
            },
            sorting: {
                ProductTypeName: 'asc'
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