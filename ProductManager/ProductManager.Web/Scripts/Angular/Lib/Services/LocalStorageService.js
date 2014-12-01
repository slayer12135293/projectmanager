'use strict';
angular.module('localStorage', []).service('localStorageService', [function () {
    this.getStorage = function (key) {
        var valueString = localStorage.getItem(key);
        return JSON.parse(valueString);
    };

    this.CreateStorage = function(key, value) {
        localStorage[key] = JSON.stringify(value);
    };

    this.DeletStorage = function(key) {
        localStorage.removeItem(key);
    };

}]);

