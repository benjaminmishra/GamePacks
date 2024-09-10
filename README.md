## SETUP INSTRUCTIONS

- Clone the repository or download the codebase in a folder.
- The project has a [docker-compose.yaml](./src/docker-compose.yaml) file in the [src](./src/) folder. 
- Run the following command in the command line in the [src](./src/) directory. 

```bash
docker compose up 
```
This should be enough to start the whole project and its dependencies. The Open Api documentation should be avaiable on [http://localhost:3000/swagger](http://localhost:3000/swagger).

> [!IMPORTANT]  
> The only pre-requisite here is to have docker engine up and running.


## USAGE INSTRUCTIONS

- After the project is running you can use the [Open API page](http://localhost:3000/swagger/index.html) to test the APIs.
- The [requests](./requests/) also contains three .http files that have sample request to all three apis. You can use an IDE like VSCode, Visual Studio or Rider to run these requests directly from the file.


## TEST SETUP

There are two kinds of tests in the [test](./tests/GamePacks.Service.Tests/GamePacks.Service.Tests.csproj) project.

1. ### Unit test 
   To run unit tests you can either use a IDE or you an run the following command in the [project root directory](./)
   ```bash
   dotnet test GamePacks.sln --filter "Type=Unit"    
   ```

2. ### Integration Tests
   To run unit tests you can either use a IDE or you an run the following command in the [project root directory](./)
   ```bash
   dotnet test GamePacks.sln --filter "Type=Integration"
   ```
   
1. > [!NOTE]  
   > To run integration tests you need to have docker engine up and running (not in powersaver mode) . This is because the integration tests use [TestContainer](https://dotnet.testcontainers.org/modules/postgres/) to create and run a real database to test against. We don't need to have docker compose running. The tests manage their own containers creation startup shutdown and data population.


## MIGRATIONS PROJECT

The database is launched using docker compose or testcontainers. The migration project is an executable that is responsible for creating the database schema and seeding the database with some basic data. The tests also use the same project to launch the database for integration tests and populate the db with test data. Hence, while running the project via docker compose you will see an addtional container which runs for a short while and then exists gracefully.


## TECH STACK

.NET 8
ASP.NET Core
PostgresSQL
TestContainers
IDE like VSCode, Visual Studio , Rider

# CHECKLIST

- [X] Includes all [expected endpoints](./ProblemStatement.md/#api).
- [X] Has [sufficient tests](./ProblemStatement.md/#tests) that are passing.
- [X] Has [sufficient documentation](./ProblemStatement.md/#documentation).
- [X] Can be [started with a single `docker compose -up`](./ProblemStatement.md/#environment-and-infrastructure) command.