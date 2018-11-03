
templatingApp.controller('DataController', ['$scope', '$http', function ($scope, $http) {
    $scope.title = "All User";
    $scope.ListUser = null;
    $scope.userModel = {};
    $scope.userModel.id = 0;
    $scope.searchText = "";
    $scope.enableCreate = false;
    $scope.selectedRow = -1;
    $scope.selectedId = 0;

    getallData();
    $scope.enableCreateNew=function() {
        if ($scope.enableCreate) {
            $scope.newUser();
            $scope.enableCreate = false;
        }
        else { 
            createEmptyTrainee();
            $scope.enableCreate = true;
        }
    }
    $scope.setClickedRow = function (index, id) {
        if ($scope.selectedRow == index) {
            $scope.selectedRow = -1;
            $scope.selectedId = 0;
        }
        else {
            //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
            $scope.selectedId = id;
        }
    }
    //******=========Get All User=========******
    function getallData() {
        $http({
            method: 'GET',
            url: '/api/Values/GetAll/'
        }).then(function (response) {
            $scope.ListUser = response.data;
        }, function (error) {
            console.log(error);
        });
    };
    function createEmptyTrainee() {
        $scope.userModel = {};
        $scope.userModel.id = 0;
        $scope.userModel.name = "New";
        
    };
    //******=========Get Single User=========******
    $scope.getUser = function (user) {
        $http({
            method: 'GET',
            url: '/api/Values/GetUserByID/' + parseInt(user.id)
        }).then(function (response) {
            $scope.userModel = response.data;
        }, function (error) {
            console.log(error);
        });
    };

    //******=========Save User=========******
    $scope.saveUser = function () {
        $http({
            method: 'POST',
            url: '/api/Values/PostUser/',
            data: $scope.userModel
        }).then(function (response) {
            $scope.reset();
            getallData();
        }, function (error) {
            console.log(error);
        });
    };
    $scope.newUser = function () {
        createEmptyTrainee();
        $scope.enableCreate = false;
        $http({
            method: 'POST',
            url: '/api/Values/PostUser/',
            data: $scope.userModel
        }).then(function (response) {
            $scope.userModel = response.data;
            getallData();
        }, function (error) {
            console.log(error);
        });
    };
    //******=========Update User=========******
    $scope.updateUser = function () {
        $http({
            method: 'PUT',
            url: '/api/Values/PutUser/' + parseInt($scope.userModel.id),
            data: $scope.userModel
        }).then(function (response) {
            $scope.userModel = response.data;
            getallData();
        }, function (error) {
            console.log(error);
        });
    };

    //******=========Delete User=========******
    $scope.deleteUser = function (user) {
        var IsConf = confirm('You are about to delete ' + user.name + '. Are you sure?');
        if (IsConf) {
            $http({
                method: 'DELETE',
                url: '/api/Values/DeleteUserByID/' + parseInt(user.id)
            }).then(function (response) {
                $scope.selectedId = 0;
                $scope.selectedRow = -1;
                $scope.reset();
                getallData();
            }, function (error) {
                console.log(error);
            });
        }
    };
    $scope.FindUserById = function (id) {
        var userfound = null;
        $scope.ListUser.forEach(function (usr) {
            if (usr.id == id)
                userfound = usr;
        });

        return userfound;
    };

        $scope.deleteSelected = function () {
            var user = null;
            if ($scope.selectedId > 0) {
                user = $scope.FindUserById($scope.selectedId);
                if (user == null) {
                    return;
                }
            }
            $scope.deleteUser(user);
        
    }
    //******=========Clear Form=========******
    $scope.reset = function () {
        var msg = "Form Cleared";
        $scope.userModel = {};
        $scope.userModel.id = 0;
    };
}]);
