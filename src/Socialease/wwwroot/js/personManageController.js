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
        vm.newNote = {};
        vm.notes = [];

        $http.get('/api/people/' + $routeParams.id)
            .then(function(response) {
                angular.copy(response.data, vm.person);
            }, function(error) {
                vm.errorMessage = 'Failed to load person.';
            })
            .finally(function() {
                vm.isBusy = false;
            });

        $http.get('/api/people/' + $routeParams.id + '/notes')
	        .then(function (response) {
	            angular.copy(response.data, vm.notes);
	        }, function (error) {
	            vm.errorMessage = 'Failed to load data. ' + error;
	        })
	        .finally(function () {
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

        vm.addNote = function() {
            vm.isBusy = true;
            vm.errorMessage = '';

            $http.post('/api/people/' + $routeParams.id + '/notes', vm.newNote)
                .then(function(response) {
                    vm.notes.push(response.data);
                    vm.newNote = {};
                }), function() {
                vm.errorMessage = 'Failed to add new note.';
            }.finally(function() {
                vm.isBusy = false;
            });
        }

    }

})();