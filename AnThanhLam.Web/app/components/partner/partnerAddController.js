(function (app) {
    app.controller('partnerAddController', partnerAddController);

    partnerAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function partnerAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.partner = {
            CreatedDate: new Date(),
            Status: true,
        }
        $scope.flatFolders = [];
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.partner.Alias = commonService.getSeoTitle($scope.partner.Name);
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.partner.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.AddPartner = AddPartner;

        function AddPartner() {
            apiService.post('/api/partner/create', $scope.partner,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('partner');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        function loadParentPartner() {
            apiService.get('/api/partner/getallparents', null, function (result) {
                console.log(result);
                $scope.parentpartner = commonService.getTree(result.data, "ID", "ParentID");
                $scope.parentpartner.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Cannot get list parent');
            });
        }
        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };
        loadParentPartner();
    }

})(angular.module('anthanhlam.partner'));