angular.module("app").factory("apiRequestFactory", ["$http", "Upload", apiRequestFactory]);

function apiRequestFactory($http, Upload) {
	const post = function (url, requestBody) {
		var request = {
			method: 'POST',
			url: url,
			headers: {
				'Content-Type': "application/json"
			},
			data: JSON.stringify(requestBody)
		};
		return $http(request);
	};
	const uploadFile = function (file) {
		return Upload.upload({
			url: "/fileUpload/uploadAsync",
			data: { files: [file] }
		});
	};

	//const apiUrl = "/api/execute";
	const factory = {
		post: post,
		uploadFile: uploadFile
	};
	return factory;
}