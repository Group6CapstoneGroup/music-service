# Music Service

An API service for music and selected song as a part of our Tune Tip project

## Prerequisites

### .NET 5

.NET is the framework under the Web API application. For more information and to download .NET please click [here](https://dotnet.microsoft.com/download/dotnet/5.0).

### Docker

Docker is leveraged for automation and assures boundary separation. For more information and to download please click this [link](https://www.docker.com/products/docker-desktop).

### Postman

API integration tests are written in Postman for more information please refer to this [link](https://github.com/Group6CapstoneGroup/capstone-documents/tree/main/Test%20Plan%20and%20Test%20Results) for a detailed overview of our testing plan.

### Newman

Postman tests are run via newman for information on running collections on the command line with Newman please refer [here](https://learning.postman.com/docs/running-collections/using-newman-cli/command-line-integration-with-newman/).

### Visual Studio 2019

We currently are utilizing Visual Studio 2019 as our IDE for music-service. For information and download instructions click [here](https://visualstudio.microsoft.com/vs/older-downloads/).

### PostgreSQL

We are currently leveraging PostgreSQL for our database, please refer [here](https://www.postgresql.org/download/) for download instructions and information.

### Git

To successfully run Git commands and clone down repositories in this organization make sure you have Git installed on your computer. For more information and to download click [here](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git).

## Setup Instructions

Once you have successfully installed all the above prerequisites on your machine you can now begin the process to run Music Service on your machine.

The first step is to clone down the music-service repository to your computer. Open a command prompt window and navigate to a directory where you would like to clone the repository. You can clone the repository to your machine by entering this command:

`git clone https://github.com/Group6CapstoneGroup/music-service.git`

Once you have successfully cloned down the repository navigate to the project directory to set your user secrets, the user-secret command **cannot** be run against the class library. The project directory is located here: `./src/MusicService`.

Once you successfully navigated to the project directory enter this command into your command prompt to set the user secrets:

```sh
dotnet user-secrets init

dotnet user-secrets set ConnectionStrings:MusicDb "Server=localhost;Database=musicdb;Port=8080;User Id=docker;Password=docker;" 
```

You should get an output saying the user secrets have successfully been set! To validate the secrets have been set you can open up the MusicService.sln file under the `src/` directory. Once the solution is pulled up, right click on MusicService in the solution explorer and click Manage User-Secrets in the menu. This should take you to the secrets.json file of the sln and you should see you have successfully set your ConnectionString.

Now navigate back up to the `music-service/` directory where your docker-compose file lives. You can spin up an instance of the postgres database via docker-compose. Run `docker-compose up` from the root directory and wait for Flyway to complete it's migration. For more information on Flyway Migrations see [here](https://flywaydb.org/documentation/concepts/migrations). To stop and remove your docker container press CTRL + C and enter `docker-compose down`.

Once you're successfully running your database instance in docker you can open up the MusicService.sln under the `src/` folder to run the MusicService web application.

For video instructions on setup and install please click [here](https://www.youtube.com/watch?v=W_VX_slroMk).

## Project Layout

**`src/MusicService`**

This directory contains the .NET Web API.

**`sql/`**

This directory contains the schema definitions for the musicdb database.

**`test/`**

This directory contains the Postman Collection and Postman Environment used for running the automated integration tests via newman.
