'use strict';
angular.module('callService', []).service('promiseService', [
    '$http', '$q', function ($http, $q) {

        this.callActionPromise = function (url, param) {
            if (param == undefined) {
                param = "";
            }
            var defer = new $q.defer();
            $http.get(url + param, { cache: true }).success(function (data, status) {
                defer.resolve(data);
            }).error(function (data, status) {
                defer.reject(data);
            });
            return defer.promise;
        };

    }]);




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
        }
        );
    };


}]);




