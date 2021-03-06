﻿(function() {

	'use strict';

    angular.module('app')
        .controller('peopleController', peopleController);

	function peopleController($http) {

		var vm = this;

	    vm.people = [];
	    vm.newPerson = {};
	    vm.errorMessage = '';
	    vm.nameFilter = '';
	    vm.isBusy = true;
	    vm.isFilterSeen = false;
	    vm.selectedPerson = { value: 0 };

	    $http.get('/api/people')
	        .then(function(response) {
	            angular.copy(response.data, vm.people);
	        }, function(error) {
	            vm.errorMessage = 'Failed to load data. ' + error;
	        })
	        .finally(function() {
	            vm.isBusy = false;
	        });

	    vm.toggleFilter = function() {
	        vm.isFilterSeen = !vm.isFilterSeen;
	    };

		vm.addPerson = function() {
		    vm.isBusy = true;
		    vm.errorMessage = '';

		    $http.post('/api/people', vm.newPerson)
		        .then(function(response) {
		            vm.people.push(response.data);
		            vm.newPerson = {};
		        }, function() {
		            vm.errorMessage = 'Failed to add new person.';
		        })
		        .finally(function() {
		            vm.isBusy = false;
		        });
		}

	}

})();