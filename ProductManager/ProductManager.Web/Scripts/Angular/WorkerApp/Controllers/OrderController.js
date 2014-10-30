'use strict';

WorkerApp.controller('OrderController', ['$scope', 'promiseService', function ($scope, promiseService) {

    var categoriesPromise = promiseService.callActionPromise('/Orders/AllCategories');


   
    categoriesPromise.then(function(data) {
        $scope.categories = data;
    });

    $scope.UpdateSubCatagories = function() {
        var subCategoryPromise = promiseService.callActionPromise('/Orders/SubCategories?categoryId=' + $scope.selectedCategory);
        subCategoryPromise.then(function(data) {
                $scope.subcategories = data;
            }
        );
    };

    $scope.UpdateProducts = function () {
        var subCategoryPromise = promiseService.callActionPromise('/Orders/Products?subcategoryId=' + $scope.selectedSubCategory);
        subCategoryPromise.then(function (data) {
            $scope.products = data;
        });
    };


}]);




