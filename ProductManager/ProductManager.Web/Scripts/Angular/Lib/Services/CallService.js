'use strict';
angular.module('callService', []).service('promiseService', [
    '$http', '$q', function ($http, $q) {

        this.callActionPromise = function (url, param) {
            if (param === undefined) {
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

