/// <reference path="../../../assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('anthanhlam.partner', ['anthanhlam.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('partner', {
            url: "/partner",
            templateUrl: "/app/components/partner/partnerListView.html",
            parent: 'base',
            controller: "partnerListController"
        }).state('add_partner', {
            url: "/add_partner",
            templateUrl: "/app/components/partner/partnerAddView.html",
            parent: 'base',
            controller: "partnerAddController"
        }).state('edit_partner', {
            url: "/edit_partner/:id",
            templateUrl: "/app/components/partner/partnerEditView.html",
            parent: 'base',
            controller: "partnerEditController"
        });
    }
})();