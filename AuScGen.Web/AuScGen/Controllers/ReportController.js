// ***********************************************************************
// <copyright file="ReportsController.js" company="Epam">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ReportsController file</summary>
// ***********************************************************************

app.controller('ReportsController', ['$scope', '$http', function ($scope, $http) {

    $scope.Reports = [];

    /// Function to get report details list

    $scope.GetReportsDetails = function () {
        $http({
            method: 'GET',
            url: '/Home/DisplayReports',
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $scope.Reports = response.data;
        }, function () { });
    }

}]);