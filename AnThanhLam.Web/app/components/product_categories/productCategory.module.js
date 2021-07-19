/// <reference path="../assets/admin/libs/bower_components/angular/angular.js" />
(function () {
    angular.module('anthanhlam.product_categories', ['anthanhlam.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories', {
            url: "/product_categories",
            templateUrl: "/app/components/product_categories/productCategoryListView.html",
            controller: "productCategoryListController"
        });
    }
})();