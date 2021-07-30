/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('anthanhlam.brand', ['anthanhlam.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('brand', {
            url: "/brand",
            templateUrl: "/app/components/brand/brandListView.html",
            parent: 'base',
            controller: "brandListController"
        }).state('add_brand', {
            url: "/add_brand",
            templateUrl: "/app/components/brand/brandAddView.html",
            parent: 'base',
            controller: "brandAddController"
        }).state('edit_brand', {
            url: "/edit_brand/:id",
            templateUrl: "/app/components/brand/brandEditView.html",
            parent: 'base',
            controller: "brandEditController"
        });
    }
})();