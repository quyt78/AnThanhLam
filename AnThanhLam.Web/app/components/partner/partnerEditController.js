(function (app) {
    app.controller('partnerEditController', partnerEditController);

    partnerEditController.$inject = ['apiService','$http', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function partnerEditController(apiService,$http, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.partner = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.UpdatePartner = UpdatePartner;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.partner.Alias = commonService.getSeoTitle($scope.partner.Name);
        }

        function loadPartnerDetail() {           
            apiService.get('/api/partner/getbyid/' + $stateParams.id, null, function (result) {
                $scope.partner = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdatePartner() {
            apiService.put('/api/partner/update', $scope.partner,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('partner');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadParentPartner() {
            apiService.get('/api/partner/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentPartner();
        loadPartnerDetail();
    }

})(angular.module('anthanhlam.partner'));