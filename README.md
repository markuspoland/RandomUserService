# RandomUserService

RandomUserService is an application that fetches random users from an external API and stores them in a database. The project includes a scheduler that automatically retrieves users at configured intervals.

## Project Structure

- **RandomUserService.API** – Web API project, the entry point of the application.
- **RandomUserService.Application** – application logic, commands and queries.
- **RandomUserService.Domain** – entities, aggregates, value objects.
- **RandomUserService.Infrastructure** – repository implementations, scheduler, HTTP client.
- **RandomUserService.Tests** – unit and integration tests.

## Requirements

- .NET 8 SDK
- Visual Studio 2022 or later
- SQL Server

## Setup

1. Open the solution `RandomUserService.sln` in Visual Studio.
2. Make sure the startup project is **RandomUserService.API**.
3. Configure the schedule interval in appsettings.

## Running

- In Visual Studio, set **RandomUserService.API** as the startup project.
- Press **F5** or click **Start Debugging** to run the application.
- Swagger UI will be available at `https://localhost:<port>/swagger` for testing the API endpoints.

## Tests

- Tests are located in the **RandomUserService.Tests** project.
- Tests include:
  - Unit tests for the scheduler (status, pause, resume)
  - Integration tests for the scheduler with database (InMemory)
- To run tests:
  1. Open **Test Explorer** in Visual Studio.
  2. Click **Run All** or run selected tests.
- Tests use **xUnit**, **Moq**, and **InMemoryDatabase** for integration tests.

## Logging

- The application uses built-in `ILogger` for logging scheduler actions and errors.

## Notes

- The scheduler runs in the background and fetches users according to the interval configured in appsettings.
- Schedule interval is provided via SchedulerConfigurationProvider.
- Scheduler execution can be paused and resumed via API endpoints.
