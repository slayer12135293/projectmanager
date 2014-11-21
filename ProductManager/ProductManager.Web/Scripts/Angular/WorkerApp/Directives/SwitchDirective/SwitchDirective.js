'use strict';

WorkerApp.directive('angularSwitch', ['$timeout', function ($timeout) {
    return {
        restrict: 'AE',
        templateUrl: '/Scripts/Angular/WorkerApp/Directives/SwitchDirective/Template/AngularSwitch.html',
        scope: {
            viewModel: '=viewModel'
        },
        link: function () {
            $timeout(function () {
                $('input[type="checkbox"]').bootstrapSwitch();
            });
      
        }
    };

}]);