angular.module('sample_store_app')
    .value('config', { 
        baseUrl: 'http://localhost:5000/v1/',
        productsUrl: 'http://localhost:5000/v1/' + 'products/',
        customerUrl: 'http://localhost:5000/v1/' + 'customers/'
    });