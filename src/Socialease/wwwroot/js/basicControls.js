(function() {
    'use strict';

    angular.module('basicControls', [])
        .directive('spinner', spinner);

    function spinner() {
        return {
            scope: {
                show: '=display'
            },
            restrict: 'E',
            templateUrl: '/views/spinner.html'
        };
    }

})();