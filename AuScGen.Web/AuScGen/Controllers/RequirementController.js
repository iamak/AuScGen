// ***********************************************************************
// <copyright file="RequirementController.js" company="Epam">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>RequirementController file</summary>
// ***********************************************************************

app.controller('RequirementController', ['$scope', '$window', '$http', function ($scope, $window, $http) {
    
    /// function to open link provide in web browser.

    $scope.Redirect = function () {
        window.open($scope.InputUrl, '_blank');
    }

    $scope.InputExePath = "";

    /// Method to generate page class.

    $scope.PageClassGeneration = function () {
        $http({
            method: 'POST',
            url: '/Home/GeneratePageClass',
            headers: { 'Content-Type': 'application/json' },
            data: { 'fileName': $scope.InputExePath }
        }).then(function() {
            $scope.Message = "Page Class Generated...!!"
            $timeout(function () { $scope.Message = ""; }, 3000);
        }, function() { });
    }
}]);
