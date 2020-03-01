(function () {
	'use strict';
	function userList(apiRequestFactory/*, dataTableProvider*/) {
		this.dropDownValues = [];

		this.totalRecords = "0";

		this.listProvider = {};
		this.listProvider.filterText;
		this.listProvider.nameEnabled = false;
		this.listProvider.headers = ["Id", "Name", "Surname", "Mobile No", "Birth Date", "Last Location"];
		this.listProvider.properties = ["id", "name", "surname", "mobileNo", "birthDate", "lastLocation"];
		this.listProvider.list = [];
		this.listProvider.title = "User List";
		this.listProvider.getData = function (pageNumber, pageSize) {
			pageNumber = pageNumber || 1;
			pageSize = pageSize || 10;
			const filterMethod = this.nameEnabled ? "/user/filterByName" : "/user/filterByPhone";
			apiRequestFactory.post(filterMethod, {
				Filter: this.filterText,
				PageNumber: pageNumber,
				PageSize: pageSize
			}).then(response => {
				this.list.length = 0;
				if (!response.data.error && response.data.data) {
					Array.prototype.push.apply(this.list, response.data.data.items);
					this.totalRecords = response.data.data.totalRecords;
					this.currentPage = pageNumber;
					this.pageSize = pageSize;
				}
			}).catch(error => {
				console.log(error);
				debugger;
			});
			//userElasticSearchFactory.filter("Kaley")
			//	.then(response => {
			//		debugger;
			//	})
			//	.catch(reason => {
			//		debugger;
			//	});
		};
		this.setFilterByText = function (isPhone) {
			this.listProvider.nameEnabled = !isPhone;
			this.filterBy = !isPhone ? "Name" : "Phone";
		};
		this.filterOnChange = function () {
			this.listProvider.getData(this.listProvider.currentPage, this.listProvider.pageSize);
		};

		this.setFilterByText();
		this.listProvider.getData();
	}

	angular.module('app').component('userList', {
		controller: ["apiRequestFactory", /*"dataTableProvider",*/ userList],
		templateUrl: 'pages/user/list.html',
		//bindings: {
		//	data: '='
		//}
	});
})();
