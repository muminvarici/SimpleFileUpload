const serverSideTable = function () {
	this.provider = { currentPage: 1 };

	this.changePage = function (pageNumber) {
		if (pageNumber) {
			this.provider.currentPage = pageNumber;
		}
		this.provider.getData(pageNumber, this.provider.pageSize);
	};
	this.changePageSize = function (pageSize) {
		this.provider.pageSize = pageSize;
		this.changePage(this.provider.currentPage);
	};
};

angular.module("app").component("serverSideTable", {
	controller: [serverSideTable],
	templateUrl: "js/directives/serverSideTable.html",
	bindings: {
		provider: "="
	}
});

