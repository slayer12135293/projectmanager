'use strict';

WorkerApp.controller('OrderController', ['$scope','$filter', 'promiseService', 'getProductTypesService', function ($scope,$filter, promiseService, getProductTypesService) {

    var productTypesPromise = getProductTypesService.getAllTypesPromise();
    $scope.typeGroup = {};
    $scope.selection = {};
    $scope.selection.hasProduct = true;
    productTypesPromise.then(function (data) {
        $scope.selection.allProductTypes = data;
    });


    $scope.addTypeGroup = function () {
        var selectedTypeId = $scope.selection.selectedProductType;

        if (typeof $scope.typeGroup.groups == 'undefined') {
            $scope.typeGroup.groups = [];
        };

        var selectedType = $filter('filter')($scope.selection.allProductTypes, function (x) { return x.Id === selectedTypeId; })[0];

        var productTypeGroup = { productTypeId: selectedTypeId, productTypeName: selectedType.Name };
        $scope.typeGroup.groups.push(productTypeGroup);
    };


    
    $scope.removeTypeGroup = function (i) {
        $scope.typeGroup.groups.splice(i, 1);
    };



}]);




