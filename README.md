# Onion Architecture With .net core webapi, EF Core and Angular UI(Angular Material), MySQL
This is a project that will help you easily kickstart your .net core project with Onion architecture. The project containes .net core 2.2 as 
server side
with Angular UI using angular material and MySQL as backend. EF Core code first approach is used with Generic Repository And Unit Of Work 
Pattern.
This sample project has all the UI related validation along with Server side logging, exception handling, authentication and athorization 
using JWT token handled.
Anybody using this project can skip all these areas in your project and will save lot of time.

## Prequisite
* Have a general idea about onion architecture. There are lot of materials in the internet regarding the same
* EF core code first approach
* Angular  6, Angular Material
* Have dotnet core sdk installed in your machine. dotnet core 2.2 or higher

## Getting started
To get started with the project follow the below steps
### Server
On the main folder which has the solution file (Server/Demo.sln) file open the command prompt using CMD. Run the command **dotnet restore**. 
This will restore all the dependent nuget packages
#### Database
To create the database and necessary tables in mysql go to **Migrations** folder open the command prompt using CMD. Run the command **dotnet ef database update**.
This will create the database and tables in your MySQL server. In case you are using dotnet core 3.1 version the dotnet ef command will not be available by default.
Incase dotnet ef is not available run the the command  **dotnet tool install --global dotnet-ef --version 3.1.4** to invoke it.
If you are using mysql as a docker container run the command docker run  --name ms -p 3306:3306 -e MYSQL_ROOT_PASSWORD=password mysql 
###Run The Application
Once the above two steps are done to run the application run **dotnet run** from Demo.API project or if you are in Server folder run
**dotnet run --project Demo.API**
 
### UI
To run the UI application go into UI/DemoUI folder run the command **npm install** from command prompt which will download all necessary node_modules folder.
Run the command **ng serve** to run the angular UI application
Run the url : http://localhost:4200/ to open the UI application.
 
