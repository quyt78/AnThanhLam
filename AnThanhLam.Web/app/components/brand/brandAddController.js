(function (app) {
    app.controller('brandAddController', brandAddController);

    brandAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function brandAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.brand = {
            CreatedDate: new Date(),
            Status: true,
        }
        $scope.flatFolders = [];
        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.brand.Alias = commonService.getSeoTitle($scope.brand.Name);
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.brand.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.AddBrand = AddBrand;

        function AddBrand() {
            apiService.post('/api/brand/create', $scope.brand,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('brand');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
        function loadParentBrand() {
            apiService.get('/api/brand/getallparents', null, function (result) {
                console.log(result);
                $scope.parentBrand = commonService.getTree(result.data, "ID", "ParentID");
                $scope.parentBrand.forEach(function (item) {
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
        loadParentBrand();
    }

})(angular.module('anthanhlam.brand'));