# Simple StackOverflow Project

## Description
This project is a hands-on exercise for learning GraphQL in .NET. It is a simple Q/A Forum backend where a user would be able to publish a new question, and in-turn other users on the platform would be able to answer the question as well as up/down vote questions. Each question is ranked based on the amount of interaction that takes place with the question based on the metrics(upvotes, downvotes, answers). No UI is included in this exercise.

### Functional Requirements
- **Authentication** : Simple authentication will be done by passing username and password to the login endpoint as listed below.
- Authentication returns a simple ID to be used for further calls. it can be as simple as a UUID, don't overcomplicate this process. This is not a best practice in terms of security, it is only acceptable for the purposes of this exercise, it is meant to simplify this task.
- **Up/Down Voting** : Answers/Question can be up/down voted by any logged in user. Each answer/question would have a final VoteScore which is {UpVotes} - {DownVotes}. 
- An Answer/Question is considered to be UpVoted if the number of up votes is higher than down votes i.e VoteScore > 0 and vice versa.

##  GraphQL

GraphQL is a query language and server-side runtime for application programming interfaces (APIs) that prioritizes giving clients exactly the data they request and build by Facebook(Meta) company.

Until now, with the traditional REST API approach we might have experienced over-fetching (where the API returns 10 fields but we need only 2) or under-fetching (API return only the IDs but we need more related data which forces us make a second API Resource call) of data.

GraphQL is like a middle layer between our data and our clients,  
and it can be considered as an alternative to REST API or maybe even an evolution.

There are four key concepts in GraphQL:

-   Schema
-   Resolver
-   Query
-   Mutation

A GraphQL  **schema**  consists of object types that define the type of objects that are possible to receive and the type of fields available.

The  **resolvers**  are the collaborators that we can associate with the fields of our scheme and which will take care of recovering data from these fields.

Considering a typical CRUD, we can define the concept of  **query**  and  **mutation**.  
The first one will deal with the information reading, while the creation, modification, and deletion are tasks managed by mutation.


