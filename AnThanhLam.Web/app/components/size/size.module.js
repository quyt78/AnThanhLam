/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('anthanhlam.size', ['anthanhlam.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('size', {
            url: "/size",
            templateUrl: "/app/components/size/sizeListView.html",
            parent: 'base',
            controller: "sizeListController"
        }).state('add_size', {
            url: "/add_size",
            templateUrl: "/app/components/size/sizeAddView.html",
            parent: 'base',
            controller: "sizeAddController"
        }).state('edit_size', {
            url: "/edit_size/:id",
            templateUrl: "/app/components/size/sizeEditView.html",
            parent: 'base',
            controller: "sizeEditController"
        });
    }
})();