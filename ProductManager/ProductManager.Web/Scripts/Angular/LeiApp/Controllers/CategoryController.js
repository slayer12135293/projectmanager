'use strict';

LeiApp.controller('CategoryController', ['$scope', function ($scope) {

    $scope.test = "From Angular";
    $scope.AllowButtonText = "Allow Edit";
    $scope.showEdit = false;

    $scope.AllowEdit = function () {
        if ($scope.showEdit) {
            $scope.showEdit = false;
            $scope.AllowButtonText = "Enable Delete";
        } else {
            $scope.showEdit = true;
            $scope.AllowButtonText = "Disable Delete";
        }
    };

}]);



LeiApp.controller('SubController', ['$scope', function ($scope) {


}]);