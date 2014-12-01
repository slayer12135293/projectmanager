'use strict';

WorkerApp.controller('priceCalculationTypeController', ['$scope', function ($scope) {
    $scope.orderline = { amount: 1, width: 0, height: 0, size:0 };
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



WorkerApp.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                // this next if is necessary for when using ng-required on your input. 
                // In such cases, when a letter is typed first, this parser will be called
                // again, and the 2nd time, the value will be undefined
                if (inputValue == undefined) return '';
                var transformedInput = inputValue.replace(/[^0-9+.]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});

function MyCtrl($scope) {
    $scope.number = '';
}