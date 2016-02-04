(function() {

    'use strict';

    angular.module('app')
        .controller('personManageController', personManageController);

    function personManageController($routeParams, $http) {

        var vm = this;

        vm.id = $routeParams.id;
        vm.errorMessage = '';
        vm.person = {};
        vm.isBusy = true;
        vm.isEditing = false;

        $http.get('/api/people/' + $routeParams.id)
            .then(function(response) {
                angular.copy(response.data, vm.person);
            }, function(error) {
                vm.errorMessage = 'Failed to load person.';
            })
            .finally(function() {
                vm.isBusy = false;
            });

        vm.editCommand = function() {
            vm.isEditing = true;
        }

        vm.cancelCommand = function() {
            vm.isEditing = false;
        }

        vm.saveCommand = function() {
            vm.isBusy = true;
            vm.errorMessage = '';

            $http.post('/api/people/' + $routeParams.id, vm.person)
                .then(function (response) {
                    angular.copy(response.data, vm.person);
                }, function () {
                    vm.errorMessage = 'Failed to save changes to person.';
                })
                .finally(function () {
                    vm.isBusy = false;
                });

            vm.isEditing = false;
        }

    }

})();