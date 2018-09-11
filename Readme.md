# Running Background Services in .NET Core Web API

This project is a proof of concept for running multiple background services on a Web API that manages parallel tasks in the background. It also provides synchronous and asynchronous controller methods.

## Sync Controller

### `GET /sync`
Fetches all `Sync` objects from the repository

### `GET /sync/{id}`
Fetches specific `Sync` object from the repository

### `POST /sync`
Creates a new `Sync` object synchronously, that is, it simulates a time consuming operation during the lifecycle of the request.

### `POST /sync/alt`
Creates a new `Sync` object asynchronously, that is, it delegates the task of creating the new object and "instantly" returns a response to the user.

## Status Background Service
This service runs every 10 seconds, retrieves all pending `Sync` objects and updates their status to `Status.Completed`.

## Data Process Background Service
This service runs every 10 seconds, retrieves all completed `Sync` objects and changes their `Data` property to lowercase. Pretty useless, I know, but it simulates a basic task.