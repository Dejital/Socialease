(function() {

	'use strict';

    angular.module('app')
        .controller('peopleController', peopleController);

	function peopleController() {

		var vm = this;

	    vm.people = [
	        {
	        	name: 'John Fante',
				lastPing: new Date()
	        }, {
	            name: 'Arturo Bandini',
				lastPing: new Date()
	        }
	    ];

		vm.addPerson = function() {
		    vm.people.push({ name: vm.newPerson.name, lastPing: new Date() });
		}

	}

})();