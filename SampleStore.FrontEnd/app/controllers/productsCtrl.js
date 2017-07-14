angular.module("sample_store_app")
    .controller("productsCtrl", function($scope, $filter, productsAPI){
        $scope.app = "Sample Store";
        $scope.products = [];
        $scope.product = null;
        


        $scope.getAll = function(){
            productsAPI.getAll()
                .then(                      
                    function(data, status){ 
                        $scope.products = data.data;
                })
                .catch(function(data, status){
                    $scope.message = "Deu erro negada!!!!" + data;
                });
        };

        $scope.getid = function(id){
            productsAPI.getById(id)
                .then(function(data, status){
                    $scope.product = data;
                })
                .catch(function(data, status){
                    $scope.message = "Deu erro" + data;
                })
        };

        
        // loadProducts();

    });