(function (app) {
    app.controller('brandEditController', brandEditController);

    brandEditController.$inject = ['apiService','$http', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function brandEditController(apiService,$http, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.brand = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.UpdateBrand = UpdateBrand;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.brand.Alias = commonService.getSeoTitle($scope.brand.Name);
        }

        function loadBrandDetail() {           
            apiService.get('/api/brand/getbyid/' + $stateParams.id, null, function (result) {
                $scope.brand = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateBrand() {
            apiService.put('/api/brand/update', $scope.brand,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('brand');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        function loadParentBrand() {
            apiService.get('/api/brand/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadParentBrand();
        loadBrandDetail();
    }

})(angular.module('anthanhlam.brand'));