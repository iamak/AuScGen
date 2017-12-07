// ***********************************************************************
// <copyright file="ExecutionController.js" company="Epam">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>ExecutionController file</summary>
// ***********************************************************************

app.controller('ExecutionController', ['$scope', '$http', '$timeout', function ($scope, $http, $timeout) {
    /// Input Dll path intilaisation
    $scope.InputTestDllPath = "";

    $scope.TestMethods = [];

    /// function that gets the test method details.

    $scope.GetMethodsFromTheAssembly = function () {
        $http({
            method: 'POST',
            url: '/Home/GetMethodsFromTheAssembly',
            headers: { 'Content-Type': 'application/json' },
            data: { 'filePath': $scope.InputTestDllPath }
        }).then(function success(response) {
            $scope.TestMethods = response.data;
        }, function () { });
    }

    /// Selection gives the information about selcted test cases.
    $scope.Selection = [];
    $scope.MethodSelection = function (method) {

        var index = $scope.Selection.indexOf(method.MethodName);
        if (index > -1) { $scope.Selection.splice(index, 1); } else {
            $scope.Selection.push(method.MethodName);
        }
    }

    $scope.IsClassChecked = false;

    /// function for the checbox functionality.

    $scope.ClassSelected = function (className, classChecked) {
        if (classChecked) {
            angular.forEach($scope.TestMethods, function (value) {
                if (value.ClassName === className) {
                    angular.forEach(value.ClassMethods, function (method) {
                        method.IsChecked = true;
                        $scope.MethodSelection(method);
                    });
                }
            });
        } else {
            angular.forEach($scope.TestMethods, function (value) {
                if (value.ClassName === className) {
                    angular.forEach(value.ClassMethods, function (method) {
                        method.IsChecked = false;
                        $scope.MethodSelection(method);
                    });
                }
            });
        }
    }

    $scope.TestResults = [];

    /// Function to post selcted test cases and get the executed results.

    $scope.ExecuteTestmethods = function () {
        $http({
            method: 'POST',
            url: '/Test/RunTests',
            headers: { 'Content-Type': 'application/json' },
            data: {
                'filePath': $scope.InputTestDllPath,
                'selectedTestCases': $scope.Selection
            }
        }).then(function (response) {
            $scope.TestResults = response.data.Results[0].Results[0].Results[0].Results[0].Results;
            $scope.Message = "Tests Executed...!!"
            $timeout(function () { $scope.Message = ""; }, 3000);
        }, function () { });
    }

    /// Function to generate the report.

    $scope.GenerateReport = function () {
        $http({
            method: 'POST',
            url: '/Test/GenerateReport',
            headers: { 'Content-Type': 'application/json' },
            data: { 'testdata': $scope.TestResults }
        }).then(function () {
            $scope.Message = "Report Generated...!!"
            $timeout(function () { $scope.Message = ""; }, 3000);
        }, function () { })
    }

}]);
