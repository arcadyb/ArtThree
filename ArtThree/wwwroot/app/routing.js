templatingApp.config(['$locationProvider', '$stateProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider', '$compileProvider',
    function ($locationProvider, $stateProvider, $urlRouterProvider, $urlMatcherFactoryProvider, $compileProvider) {

        //console.log('Appt.Main is now running')
        if (window.history && window.history.pushState) {
            $locationProvider.html5Mode({
                enabled: true,
                requireBase: true
            }).hashPrefix('!');
        };
        $urlMatcherFactoryProvider.strictMode(false);
        $compileProvider.debugInfoEnabled(false);

        $stateProvider
            .state('Home', {
                url: '/',
                templateUrl: './views/data/datar.html' + fileVersion,
                controller: 'DataController'
            })
            .state('Analysis', {
                url: '/Analysis',
                templateUrl: './views/analysis/analysis.html' + fileVersion,
                controller: 'AnalysisController'
            })
            .state('Data', {
                url: '/data',
                templateUrl: './views/data/data.html' + fileVersion,
                controller: 'DataController'
            })
            .state('monitor', {
                url: '/monitor',
                templateUrl: './views/monitor/monitor.html' + fileVersion,
                controller: 'MonitorController'
            });

        $urlRouterProvider.otherwise('/data');
    }]); 
