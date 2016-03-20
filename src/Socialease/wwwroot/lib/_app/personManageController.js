!function(){"use strict";function n(n,e){var o=this;o.id=n.id,o.errorMessage="",o.person={},o.isBusy=!0,o.isEditing=!1,o.newNote={},o.notes=[],o.pings=[],e.get("/api/people/"+n.id).then(function(n){angular.copy(n.data,o.person)},function(n){o.errorMessage="Failed to load person."})["finally"](function(){o.isBusy=!1}),e.get("/api/people/"+n.id+"/notes").then(function(n){angular.copy(n.data,o.notes)},function(n){o.errorMessage="Failed to load data. "+n})["finally"](function(){o.isBusy=!1}),e.get("/api/people/"+n.id+"/pings").then(function(n){angular.copy(n.data,o.pings)},function(n){o.errorMessage="Failed to load data. "+n})["finally"](function(){o.isBusy=!1}),o.editCommand=function(){o.isEditing=!0},o.cancelCommand=function(){o.isEditing=!1},o.saveCommand=function(){o.isBusy=!0,o.errorMessage="",e.post("/api/people/"+n.id,o.person).then(function(n){angular.copy(n.data,o.person)},function(){o.errorMessage="Failed to save changes to person."})["finally"](function(){o.isBusy=!1}),o.isEditing=!1},o.addNote=function(){o.isBusy=!0,o.errorMessage="",e.post("/api/people/"+n.id+"/notes",o.newNote).then(function(n){o.notes.push(n.data),o.newNote={}}),function(){o.errorMessage="Failed to add new note."}["finally"](function(){o.isBusy=!1})}}n.$inject=["$routeParams","$http"],angular.module("app").controller("personManageController",n)}();