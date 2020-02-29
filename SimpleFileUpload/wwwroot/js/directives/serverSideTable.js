const serverSideTable = function () {

};

angular.module("app").component("serverSideTable", {
	controller: [serverSideTable],
	templateUrl: "js/directives/serverSideTable.html",
	bindings: {
		title: '=',
		recordCount: '@',
		items: '=',
		properties: '=',
		headers: '=',
		activePage: '=',
		pageChanged: "="
	}
});

