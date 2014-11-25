'use strict';

WorkerApp.controller('priceCalculationTypeController',['$scope', function($scope) {
    $scope.orderline = { amount: 1, width: 0, height: 0 };
}]);


WorkerApp.directive('priceCalculationType', [ function () {
    return {
        restrict: 'AE',
        controller: 'priceCalculationTypeController',
        require: '^productSelect',
        scope: {
            calculationType: '@calculationType'
        },
        templateUrl: '/Scripts/Angular/WorkerApp/Directives/PriceCalculationTypeDirective/Template/UniversalTemplate.html',
        link: function(scope, elem, attrs, parentCtrl) {
            scope.$watch('orderline', function () {
                parentCtrl.setOrderline(scope.orderline);
            },true);

        }

    };

}]);