(function (app) {
    app.controller('sizeAddController', sizeAddController);

    sizeAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function sizeAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.size = {
            CreatedDate: new Date(),
            Status: true,
        }              

        $scope.AddSize = AddSize;

        function AddSize() {
            apiService.post('/api/size/create', $scope.size,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('size');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }        
       
        
    }

})(angular.module('anthanhlam.size'));