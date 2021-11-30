# Concurrency Case Study
This repository is a simple case study involving different concurrency issues.  

Your purpose is to get all of the tests working.

# Project Design
The overall structure of the code is influenced by Domain Driven Design (DDD).  As such there are 4 projects.
## Basic Projects
* **Api** - The actual microservice scaffolding.
    * No other project do or should include this as a reference
* **Infrastructure** - Dealing with the database and other infra-related aspects.
    * Api includes this project as a reference
* **Domain** - All of the business models and business logic relating to these models are contained here. 
    * All projects include this as a reference
* **Common** - Any code which could be shared across all other projects (i.e. common extension methods or the like)
    * Any project could include this as a reference

## Test Projects
The standard would be to have a `Test` project next to each source project.  e.g. `Api.Test` or `Domain.Test`. 

In the case of integration tests there is an `Api.Test.Integration` project. These tests require postgres to be run by the docker-compose.yml file in the repository root.

# Steps to Run
1. Install .NET 6
1. Clone the repo.
1. Run the postgres service in `docker-compose.yml`
1. Run the tests (`dotnet test or the like`)
