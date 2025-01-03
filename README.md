# MovieXprt
A movie database builder & backend for movie xprts
The movie database is accessable through the Rest api project.
This application uses the Tvmaze api's backend to pull show data.


# Current endpoints
GET /show/aired?airDate=yyyy-mm-dd
- Returns a list of shows that have aired on the given date.

# Project structure

## api
The exposed web api of the moviexprt api. 
This project directly refers to the usecases or repositories from the application project.

it is in no way intended to directly call the infrastructure code.
This project exposes the application project as a Rest api.

It is intended to handle the (future) authentication and authorization of users on the controller.
It's responsibility is to preform initial request validation and map the usecase/repository response to a valid http response.

## Application
This is where the application logic resides.
it will hold:
- usecases
- repositories 
- etc.

Usecases can be directly translated to functional requests of an application or system. It should be treated as logical bundles of code that belongs together to perform a single action.
A usecase will be responsible to validate the input according to it's business rules.

Repositories are the functional part where mutations to (multiple) collections or gateways can be combined. For example  
- adding a show and a cast item to the database through the MovieXprt database gateway
- Deleting a show and it's dependencies

## Infrastructure
The infrastructure project is responsible for the communication to all of the backend services.
Usually this communication will proceed through gateway classes. These gateway classes hold the specific implementation of how to communicate with an 'end resource'.

and 'end resource' can be defined as a 
- (SQL) database 
- API (and specific endpoints) 

## Domain
The domain project is the core of the application. It holds the models and contracts that are shared accross the application.
This project should not have any dependencies on other projects.

## Indexer 
The indexer is a console application with which the tvmaze api can be crawled in order to build the database of shows.
This console application can be configured to run periodically as a webjob in an Azure WebService.

The indexer will use the infrastructure project to communicate with the tvmaze api and the MovieXprt database.
The last inserted movie id will correspond to the last movie id in the tvmaze api, this will be used to determine the page to start crawling from.

## creating the database
```
CREATE DATABASE moviexprt;

USE moviexprt;

CREATE TABLE Shows (
    Id AS CAST(JSON_VALUE(Data, '$.Id') AS INT) PERSISTED PRIMARY KEY,
    Premiered AS CAST(JSON_VALUE(Data, '$.Premiered') AS DATETIME), 
    Data NVARCHAR(MAX) NOT NULL CHECK (ISJSON(Data) = 1)
);
```