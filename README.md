# ArtThree
Database name can be changed in appsettings.json
run add-migration/update-database to create database from model
IOC Container implemented in startup.cs
IServiceCollection services keeps interfaces to numbere of application services
IATRepository interface injected to the ValueController responsible for CRUD operations
Client side implemented by Angular framework
Angular application script found under ArtThree\wwwroot\app folder
template files under ArtThree\wwwroot\views path.
Navigation through the tabs perfomed by routing implemented in ArtThree\wwwroot\app\routing.js
Data tab managed by DataController (ArtThree\wwwroot\app\data.controller.js)
CRUD operations implemented in ValuesController

not fully solved issue is binding angular model DateTime property to the html input control of type "date"
(see https://github.com/angular-ui/ui-date/issues/148).problem solved partially using two html input conrols one of type text to view value (dateJoined)
and second to pick date.
User activity variables Search Filter number of page and page size saved in localStorage
