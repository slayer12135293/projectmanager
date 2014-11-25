'use strict';
angular.module('orderStorage', ['localStorage']).service('orderStorageService', ['localStorageService', function (localStorageService) {
    this.getOrderStorage = function () {
        return localStorageService.getStorage('orderStorage');
    };
    this.saveOrderStorage = function(value) {
        localStorageService.CreateStorage('orderStorage',value);
    };




}]);


