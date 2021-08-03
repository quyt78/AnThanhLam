(function (app) {
    app.controller('sizeEditController', sizeEditController);

    sizeEditController.$inject = ['apiService','$http', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function sizeEditController(apiService,$http, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.size = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.UpdateSize = UpdateSize;       

        function loadSizeDetail() {           
            apiService.get('/api/size/getbyid/' + $stateParams.id, null, function (result) {
                $scope.size = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateSize() {
            apiService.put('/api/size/update', $scope.size,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('size');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
       
        
        loadSizeDetail();
    }

})(angular.module('anthanhlam.size'));