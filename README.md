# MongoDB training

## Installation requirements
* Docker (https://hub.docker.com/editions/community/docker-ce-desktop-windows)
* Install VSCode (https://code.visualstudio.com/docs/?dv=win)
* Install MongoDB for VS Code extension (https://marketplace.visualstudio.com/items?itemName=mongodb.mongodb-vscode)
* MongoDB Compass (https://downloads.mongodb.com/compass/mongodb-compass-1.27.1-win32-x64.zip)
* .NET5 SDK (https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.301-windows-x64-installer)

## Information
The repository contains the following files and folders:
 - **docker-compose.yml**: used to run a local MongoDB instance.
 - **1_MongoDB.QueryLanguage**: contains some files of exercises and their solutions that can be executed using the MongoDB for VS Code extension.
 - **2_MongoDB.Compass**: contains some text files with instructions to perform in MongoDB Compass to learn how to visually use Aggregations and the Explain Plan tabs.
 - **3_MongoDB.CSharpDriver**: contains a .NET5 test project with XUnit that has the Arrange and Assert part implemented and the Act part empty pending to be implemented.

## Let's code
MongoDB local instance is containerized with Docker. Open a terminal, go to the root folder of the repository and run the command `docker compose up` to run the MongoDB instance. MongoDB is listening on port `27018` and can be connected via connection string `mongodb://mongodb:training@localhost:27018`.

### [1_MongoDB.QueryLanguage](https://github.com/jcarloslr10/mongodb-training/tree/master/1_MongoDB.QueryLanguage)
The goal of these exercises is to learn how to make different types of queries (CRUD and aggregation) using the MongoDB Query Language (MQL). These exercises must be done with VSCode and the MongoDB extension for VSCode.
The script is responsible for cleaning up and inserting an initial dataset to set a context for the exercises.
### [2_MongoDB.Compass](https://github.com/jcarloslr10/mongodb-training/tree/master/2_MongoDB.Compass)
The goal of these files with instructions is to know and learn how to visually use the Aggregations and Explain Plan tabs.
### [3_MongoDB.CSharpDriver](https://github.com/jcarloslr10/mongodb-training/tree/master/3_MongoDB.CSharpDriver)
The goal of these exercises is to learn how to make different types of queries (CRUD and aggregation) using the MongoDB Driver for C#. These exercises must be done with Visual Studio 2019.
The test project is responsible for cleaning up and inserting an initial dataset to set a testing context.
