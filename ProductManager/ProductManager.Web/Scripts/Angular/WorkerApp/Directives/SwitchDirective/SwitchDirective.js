'use strict';

WorkerApp.controller('addonSwitchController', ['$scope', '$filter', 'orderStorageService', function ($scope, $filter, orderStorageService) {

    $scope.addOnsFromStorage = [];
    $scope.addOnIds = [];
    //$scope.addOnStorage = function () {
    //    var currentStorage = orderStorageService.getOrderStorage();
    //    if (!angular.isUndefined(currentStorage.productTypeGroups) && currentStorage.productTypeGroups !== null) {
    //        var currentTypeGroup = $filter('filter')(currentStorage.productTypeGroups, function (x) { return x.indexId.toString() === $scope.groupId.toString(); })[0];
    //        if (currentTypeGroup.addOns.length > 0) {
    //            $scope.addOnsFromStorage = currentTypeGroup.addOns;
    //            for (var i = 0; i < $scope.addOnsFromStorage.length; i++ ) {
    //                $scope.addOnIds.push($scope.addOnsFromStorage[i].id);
    //            };
    //        }
    //    };
    //};
    //$scope.addOnStorage();
}]);


WorkerApp.directive('angularSwitch', ['$timeout', function ($timeout) {
    return {
        restrict: 'AE',
        controller: 'addonSwitchController',
        templateUrl: '/Scripts/Angular/WorkerApp/Directives/SwitchDirective/Template/AngularSwitch.html',
        require: '^productSelect',
        scope: {
            viewModel: '=viewModel',
            groupId:'@groupId'
        },
        link: function (scope, elem, attrs, parentCtrl) {
            $timeout(function () {
                $('.use-bootstrap-switch').bootstrapSwitch();
                var switchName = scope.viewModel.Id + "-" + scope.groupId;

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