# Music Service

An API service for music and selected song as apart of our Tune Tip project

## Prerequisites

### .NET 5

.NET is the framework under the WebAPI application. For more information and to download .NET please click [here](https://dotnet.microsoft.com/download/dotnet/5.0).

### Docker

Docker is leveraged for automation and assures boundry seperation. For more information and to download please click this [link](https://www.docker.com/products/docker-desktop).

### Postman

API integration tests are written in Postman for more information please refer to this link for a detaild overview of our testing plan.

### Newman

Postman tests are run via newman

### Visual Studio 2019

We currently are utilizing Visual Studio 2019 as our IDE for music-service. For information and download instructions clicke [here](https://visualstudio.microsoft.com/vs/older-downloads/).

### PostgreSQL

We are currently leveraging PostgreSQL for our database, please refer [here](https://www.postgresql.org/download/) for download instructions and information.

## Configuration

Where configuration overlaps with secrets we must work to avoid manually adjusting the appsettings.json. For local development, please use the dotnet user-secrets feature.

Run the following from the application project directory. This cannot be run against a class library, the project directory is here: ./src/MusicService

```sh
dotnet user-secrets init

dotnet user-secrets set ConnectionStrings:MusicDb "Server=localhost;Database=musicdb;Port=8080;User Id=docker;Password=docker;" 
```

## Local Development

You can spin up an instance of postgres via docker-compose. Run docker-compose up from the root directory and wait for Flyway to complete it's migration. For more information on Flyway Migrations see [here](https://flywaydb.org/documentation/concepts/migrations).

## Project Layout

**`src/MusicService`**

This directory contains the .NET Web API.

**`sql/`**

This directory contains the schema definitions for the musicdb database.

**`test/`**

This directory contains the Postman Collection and Postman Enviornment used for running the automated integration tests via newman.
