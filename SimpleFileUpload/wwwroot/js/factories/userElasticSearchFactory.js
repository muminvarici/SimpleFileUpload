//angular.module("app").factory("userElasticSearchFactory", ["$http", "Upload", userElasticSearchFactory]);

//function userElasticSearchFactory($http, Upload) {
//	const serviceUrl = "http://localhost:9200/user_index/";

//	const filter = function (name, mobilePhone) {
//		const query = {
//			"query": {
//				"term": { "name": name }
//			}
//		};

//		const request = {
//			method: 'GET',
//			url: serviceUrl + "_search",
//			headers:
//			{
//				'Content-Type': "application/json",
//				"Access-Control-Allow-Origin": "http://localhost:9200"
//			},
//			data: JSON.stringify(query)
//		};
//		return $http(request);
//	};
//	const uploadFile = function (file) {
//		return Upload.upload({
//			url: "/fileUpload/uploadAsync",
//			data: { files: [file] }
//		});
//	};

//	//const apiUrl = "/api/execute";
//	const factory = {
//		filter: filter,
//		uploadFile: uploadFile
//	};
//	return factory;
//}