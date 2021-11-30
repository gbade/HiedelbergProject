# HeidelbergCement - Tech Lead Case Study
Der applicant, thank you for your interest in technical leadership at HeidelbergCement!

We want to make the best use of your and our time and give you a sense of the kind of work that is currently done in HProduce to support our cement plants' operational excellence.

The following case study describes an interaction of a complex technical space and its corresponding business requirements. It is your task to understand the requirements, how they are not currently being met, and to create a fix to the issue.

We would like to see your approach to the code base, your knowledge of microservices, concurrency issues, domain driven design.  Later on we would like you to present your solution to help us get a better sense of your skills and the way you solve problems.

Please note that this case study is HeidelbergCement IP, consequently you are not allowed to share or post the description, the data or your results publicly!

Please complete the case study and send it as an attachment (no download link)
to Sebastian.Walter@heidelbergcement.com. 

If you have any questions, do not hesitate to reach out!

# The Case Study

# Business Case
We have machines that run at cement plants that produce certain asset types. These machines can be scheduled to produce an asset.

Here is an example schedule in the form of a gantt chart (Imagine every `:` is a 15 minute interval)
```
ZM1 -- [::CEM-I:::]  [::::::::::::::CEM-I:::::::::::::::][:CEM-I:]
ZM2 --       [::::::CEM-52,5R:::::::::::]   [:::::::::::]
RM1 --                      [::::::CEM-II:::::::::::]         [::]
```

Schedule Status
* Schedules can have a `Saved`, `Submitted`, or `Draft` status. At any given time there can only be one `Draft` schedule for a plant.
* Schedules in `Draft` or `Saved` status can add, modify or delete items.
* Once a schedule is `Submitted` it cannot be modified.
Schedule Items
* Schedules can have schedule items that correspond to machine production times for a specific asset. Schedule items for the same asset cannot overlap for the same time period 
* When items are added they should never conflict with another schedule item.


# The Problem
This project is a microservice that manages the schedules for the different plants.

It currently only has two api endpoints: 
* GET   `schedule/draft` allows getting the latest draft schedule for a plant
* POST  `schedule/items` allows adding an item to a schedule.

Currently if you add an item then the service validates that the item does not conflict. Unfortunately if you make two requests to add an item in quick succession we have a concurrency issue where both items are saved to the database, immediately making the entire schedule invalid.


# Deliverables
Your task is to adjust the project so that this race condition no longer is possible and the business rules around item creation are met.  You should feel free to adjust any aspect of the project except for the test itself.

Also include in your email your analysis of the problem, its causes, possible solutions, and why you selected your currently implemented solution.


# Project Setup Basics

Your purpose is to get all of the tests working.

You will find the following projects in the solution

* **Api** - The actual microservice scaffolding.
* **Api.Tests.Integration** - Integration tests related to the Api
* **Infrastructure** - Dealing with the database and other infra-related aspects.
* **Domain** - All of the business models and business logic relating to these models are contained here. 
* **Common** - Any code which could be shared across all other projects (i.e. common extension methods or the like)

# Getting Started
1. Install .NET 6
1. Clone the repo.
1. Run the postgres service in `docker-compose.yml`
1. Run the tests (`dotnet test or the like`)
