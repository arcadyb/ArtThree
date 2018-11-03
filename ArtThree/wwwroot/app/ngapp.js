var fileVersion = '?tmplv=' + Date.now();
var templatingApp;
(
    function () {
        'use strict';
        templatingApp = angular.module('templating_app', ['ui.router', 'moment-picker']);
    }
)();
