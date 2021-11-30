# Concurrency Case Study
This repository is a simple case study involving different concurrency issues.  

Your purpose is to get all of the tests working.

# Project Design
The overall structure of the code is influenced by Domain Driven Design (DDD).  As such there are three major projects
* Api - The actual microservice scaffolding.
    * No other project do or should include this as a reference
* Infrastructure - Dealing with the database and other infra-related aspects.
    * Api includes this project as a reference
* Domain - All of the business models and business logic relating to these models are contained here. 
    * All projects include this as a reference


# Steps to Run
1. Install .NET 6
1. Clone the repo.
1. Run the postgres service in `docker-compose.yml`
1. Run the tests (`dotnet test or the like`)
