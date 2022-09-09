# GraphQL Authentication and Authorization in DotNet 6


## **What are the main features of project?**

-   Use PostgreSQL as a data storage
-   Use Hot Chocolate library for creating GraphQL
-   Use JWT for Authentication
-   GraphQL Authorization capability
-   Add Refresh Token capability in project

##  GraphQL & Hot Chocolate:

GraphQL is a query language and server-side runtime for application programming interfaces (APIs) that prioritizes giving clients exactly the data they request and build by Facebook(Meta) company.

Until now, with the traditional REST API approach we might have experienced over-fetching (where the API returns 10 fields but we need only 2) or under-fetching (API return only the IDs but we need more related data which forces us make a second API Resource call) of data.

GraphQL is like a middle layer between our data and our clients,  
and it can be considered as an alternative to REST API or maybe even an evolution.

Hot Chocolate is an open-source GraphQL server for the Microsoft .NET platforms and easy to use library that support most of GraphQL features and functionalities.

There are four key concepts in GraphQL:

-   Schema
-   Resolver
-   Query
-   Mutation

A GraphQL  **schema**  consists of object types that define the type of objects that are possible to receive and the type of fields available.

The  **resolvers**  are the collaborators that we can associate with the fields of our scheme and which will take care of recovering data from these fields.

Considering a typical CRUD, we can define the concept of  **query**  and  **mutation**.  
The first one will deal with the information reading, while the creation, modification, and deletion are tasks managed by mutation.

## Access Token:

When a user logins in, the authorization server issues an access token, which is an artifact that client applications can use to make secure calls to an API server. When a client application needs to access protected resources on a server on behalf of a user, the access token lets the client signal to the server that it has received authorization by the user to perform certain tasks or access certain resources

## Refresh Token:

As we know using token base Authentication methods like JWT (access token) cause short user login time, because expiration of access token should be short(5 min ~ 1 hour) to prevent hacked token.

To solve above problem of short token time, we should have another token called “RefreshToken” than it can be a GUID number and expiration time is longer(1 week ~ several month) that this token lead to lets a client application get new access tokens without having to ask the user to log in again.

