'use strict';
angular.module('productTypesService', []).service('getProductTypesService', [
    'promiseService', function (promiseService) {
        this.getAllTypesPromise = function () {
           return promiseService.callActionPromise('/producttypes/getalltypes');
        };
    }]);

