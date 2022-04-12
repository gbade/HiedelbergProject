Domain Driven Design or DDD, is a software development approach coined by Eric Evans. Domain-Driven Design is based on
* Placing the main interest of the project into defining the core domain of the problem it wants to solve
* Explore collaboration of domain practitioners (business or users) and software developers.
* Speaking an ubiquitous language within an explicitly bounded context

There are several building blocks of DDD highlighted below - 

## Bounded Contexts
A bounded context is the logical boundary around the code that represents the solution for that domain.

## Layered Architecture
DDD recommends using a layered architecture. The idea is to keep the domain knowledge focused and not spread across different application components such as Ui, database, persistence layer. This would ensure the code is readable, boundable contexts are not mixed up and testable in isolation.

## Entities
Entities are domain objects that are uniquely defined by a unique identifier, and not by their attributes. 

## Services
These serve as operations to the model as a standalone interface declared as a service. For example there is a Schedule entity, the addition of a new schedule is something to implement in a service and not inside the entities themselves.

## Repositories
A repository is a service that uses a global interface to provide access to all entities and value objects that are within a particular aggregate collection.

# Merits
A key merit of Domain Driven Design is to create well-defined components with clear contracts between them. This helps in dividing operations and responsibilities. It also makes replacing and updating any of these components much easier with less impact on the overall system.

From the following projects in the solution, it can be seen that there is the component (microservice) for the actual API, the component for integration tests related to the API, an infrastructure component dealing with the database, migrations and other infra-related aspects, a component for business logic and models, and a component (common) where code is shared across the components. These makes the components highly maintanable.

# Demerits
A key drawback is that domain experts are needed for DDD design that have a fairly clear idea of the sort of solution that is required. When there's a team of experienced developers who have experience writing these sort of systems, the forward planning works well. 

Another drawback is, given the peculiar nature of the project (scheduled machine run times for a plant), DDD requires regular communication between the domain expert(s) and developers. These often results in a longer development and duration that ultimately translates to higher costs for the business.

Also, overengineering or overcomplication is anonther con, especially if the project is to start out as simple CRUD application and grow in complexity.

One way to solve this is to create a small prototype, possibly multiple iterations of prototypes until you have enough understanding to come up with the reasonable design. While this can delay the start of the project, it is likely to result in a much more maintainable solution.