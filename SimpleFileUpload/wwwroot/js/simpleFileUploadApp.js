var app = angular.module('app', ['ui.router', "ngFileUpload"]);
app.config(function ($stateProvider, $urlRouterProvider) {
	// For any unmatched URL redirect to dashboard URL
	$urlRouterProvider.otherwise("User");
	$stateProvider
		.state('User', {
			url: "/User",
			views: {
				'container-view': {
					templateUrl: "User/Index"
				},
				'right-view@User': {
					templateUrl: "User/List"
				}
			}
		})
		.state('User.Upload', {
			url: "/Upload",
			views: {
				'container-view': {
					templateUrl: "User/Index"
				},
				'right-view@User': {
					templateUrl: "User/Upload"
				}
			}
		});
});