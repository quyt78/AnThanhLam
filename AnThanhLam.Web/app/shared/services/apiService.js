/// <reference path="../assets/admin/libs/bower_components/angular/angular.js" />
(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http'];

    function apiService($http) {
        return {
            get : get
        }

        function get(url, params, successed, failure) {
            $http.get(url, params).then(function (result) {
                successed(result);
            }, function (error) {
                failure(error);
            });
        }
    }
}) (angular.module('anthanhlam.common'));