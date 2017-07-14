angular.module('sample_store_app')
    .factory('productsAPI', function($http, config){

        var _getAll = function(){
            return $http.get(config.productsUrl);
        };

        var _getByID = function(id){
            return $http.get(config.productsUrl + id );
        };

        return {
            getAll: _getAll,
            getById: _getByID
        };
    });