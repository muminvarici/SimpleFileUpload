﻿(function () {
	'use strict';
	function userList(apiRequestFactory, userElasticSearchFactory) {
		this.nameEnabled = false;
		this.filterText;
		this.list = [];
		this.dropDownValues = [];

		this.loadData = function () {
			const filterMethod = this.nameEnabled ? "/user/filterByName" : "/user/filterByPhone";
			apiRequestFactory.post(filterMethod, {
				Filter: this.filterText,
				PageNumber: 1,
				PageSize: 50
			}).then(response => {
				this.list.length = 0;
				if (!response.data.error && response.data.data) {
					Array.prototype.push.apply(this.list, response.data.data);
				}
			}).catch(error => {
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
			this.nameEnabled = !isPhone;
			this.filterBy = !isPhone ? "Name" : "Phone";
		};
		this.filterOnChange = function () {
			this.loadData();
		};

		this.setFilterByText();
		this.loadData(this.list);
	}

	angular.module('app').component('userList', {
		controller: ["apiRequestFactory", "userElasticSearchFactory", userList],
		templateUrl: 'pages/user/list.html',
		//bindings: {
		//	data: '='
		//}
	});
})();