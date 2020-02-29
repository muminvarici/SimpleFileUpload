(function () {
	'use strict';
	function userUpload(apiRequestFactory) {
		this.uploading = false;

		this.uploadFile = function (file) {
			this.uploading = true;

			apiRequestFactory.uploadFile(file)
				.then(response => {
					file.completed = true;
					this.uploading = false;
				}).catch(result => {
					debugger;
					this.errorMsg = result;
					this.uploading = false;
				});
		};
	}

	angular.module('app').component('userUpload', {
		controller: ["apiRequestFactory", userUpload],
		templateUrl: 'pages/user/upload.html',
		//bindings: {
		//	data: '='
		//}
	});
})();
