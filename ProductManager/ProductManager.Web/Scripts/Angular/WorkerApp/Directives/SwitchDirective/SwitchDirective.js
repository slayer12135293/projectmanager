'use strict';

WorkerApp.directive('angularSwitch', ['$timeout', function ($timeout) {
    return {
        restrict: 'AE',
        templateUrl: '/Scripts/Angular/WorkerApp/Directives/SwitchDirective/Template/AngularSwitch.html',
        require: '^productSelect',
        scope: {
            viewModel: '=viewModel',
            groupId:'@groupId'
        },
        link: function (scope, elem, attrs, parentCtrl) {
            $timeout(function () {
                $('input[type="checkbox"]').bootstrapSwitch();

                var switchName = scope.viewModel.Name + "-" + scope.groupId;

                $('input[name=' + switchName.toString() + ']').on('switchChange.bootstrapSwitch', function (event, state) {
                    if (state === true) {
                        parentCtrl.setAddon(scope.viewModel.Id, scope.viewModel.Name, scope.viewModel.Price, state);
                    } else {
                        parentCtrl.removeAddon(scope.viewModel.Id);
                    }

                

                });
            });
           

        }
    };

}]);