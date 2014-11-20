'use strict';

WorkerApp.directive('angularSwitch', function () {
    return {
        restrict: 'AE',
        templateUrl: '/Scripts/Angular/WorkerApp/Directives/SwitchDirective/Template/AngularSwitch.html',
        scope: {
            viewModel: '=viewModel'
        },
        link: function (scope) {
            scope.$watch('viewModel', function (viewModel) {
                if (angular.isDefined(viewModel)) {
                    $('input[type="checkbox"]').bootstrapSwitch();
                }
            }, true);
            scope.$apply();
        }
       
    };

});