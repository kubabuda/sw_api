# Star Wars API [![Build Status](https://api.travis-ci.org/kubabuda/sw_api.svg?branch=master&status=passed)](https://travis-ci.org/kubabuda/sw_api)


Simple API to try out few features in NET Core, oh and for interview

## Objective
Create "Star Wars" RESTful API service. Implement CRUD (Create, Read, Update, Delete) operations for managing 'Star Wars' characters. 
Persistence layer should be implemented using some ORM and relational database.

Start with something simple (e.g. create enpoint that will return hardcoded list of characters) and progressively enhance the service. 
Try to use best practices and architecture patterns (e.g. SOLID principle, clean architecture).

## Extra tasks:
a) example unit and/or integration and/or functional tests
b) swagger support
c) pagination

## Example data structure in JSON:
```javascript
{
"characters": [
	{
		"name": "Luke Skywalker",
		"episodes": ["NEWHOPE", "EMPIRE", "JEDI"],
		"friends": ["Han Solo", "Leia Organa", "C-3PO", "R2-D2"]
	},
	{
		"name": "Darth Vader",
		"episodes": ["NEWHOPE", "EMPIRE", "JEDI"],
		"friends": ["Wilhuff Tarkin"]
	},
	{
		"name": "Han Solo",
		"episodes": ["NEWHOPE", "EMPIRE", "JEDI"],
		"friends": ["Luke Skywalker", "Leia Organa", "R2-D2"]
	},
	{
		"name": "Leia Organa",
		"episodes": ["NEWHOPE", "EMPIRE", "JEDI"],
		"planet": "Alderaan",
		"friends": ["Luke Skywalker", "Han Solo", "C-3PO", "R2-D2"]
	},
	{
		"name": "Wilhuff Tarkin",
		"episodes": ["NEWHOPE"],
		"friends": ["Darth Vader"]
	},
	{
		"name": "C-3PO",
		"episodes": ["NEWHOPE", "EMPIRE", "JEDI"],
		"friends": ["Luke Skywalker", "Han Solo", "Leia Organa", "R2-D2"]
	},
	{
		"name": "R2-D2",
		"episodes": ["NEWHOPE", "EMPIRE", "JEDI"],
		"friends": ["Luke Skywalker", "Han Solo", "Leia Organa"]
	}
]
}
```

## Design remarks

### Hexagonal architecture

Application is using hexagonal architecture. Business logic by design has no external dependencies. 
Keep it that way to prevent pollution of bussines logic with models independent from it. 
If functions provided by external libraries (like DB access) are needed: 
- create interface of what is needed in BusinessLogic 
- implement interface in respective project 
- connect these two in dependency injection bootstrapper in API configuration

For CRUD demo repository directly in controller would suffice, but for app lasting any longer its lifesaver.
For now API model == domain model == DB Entity. After demo it would be better to separate these and connect using AutoMapper, for demo it seems overkill.

### Authorization

For now no authorization on this API is required for clients. This is obviously necessary to be added after demo.


## TODO list

- database (SQLite) + ORM (EF Core)
- DB in container
- example integration tests
- [SpecFlow for functional tests](https://docs.specflow.org/projects/specflow/en/latest/Installation/Installation.html)
- [Coverlet Collector](https://github.com/coverlet-coverage/coverlet)[ test coverage tool](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows)
- [make Travis use Docker](https://docs.travis-ci.com/user/docker/)
### To discuss with product owner
- select authorization mechanizm, preferably OAuth2 identity SaaS like Auth0
- why numeric key is not part of character model, and if could it be added to be used (instead of name as primary key)