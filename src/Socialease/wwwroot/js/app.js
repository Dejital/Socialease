(function() {

    'use strict';

    angular.module('app', ['basicControls', 'ngRoute'])
    .config(function($routeProvider) {

            $routeProvider.when('/', {
                controller: 'peopleController',
                controllerAs: 'vm',
                templateUrl: '/views/peopleView.html'
            });

            $routeProvider.when('/:id', {
                controller: 'personManageController',
                controllerAs: 'vm',
                templateUrl: '/views/personManageView.html'
            });

            $routeProvider.otherwise({ redirectTo: '/' });

        });

})();