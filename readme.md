# ASP .Net Core API project Sample

## Set up

1. Create a Database on Local db called *ContactsAPISample*
    1. View-> Sql Server Object Explorer in Visual Studio
    1. Add (localdb)\MSSQLLocalDB server if it does not exist
    1. Right click  databases node under (localdb)\MSSQLLocalDB
    1. Add New database called *ContactsAPISample*
1. Go to [.Net Core 2.1 downloads](https://dotnet.microsoft.com/download/dotnet-core/2.1) and install the latest sdk and Runtime & Hosting Bundle
1. Right click Solution and choose Restore Nuget Packages
1. Open Package manager console in visual studio Tools-> Nuget Package Manager-> Package Manager Console
1. Run *Update-Database*

## Running

Press <kbd>F5</kbd>.  You will see a swagger page show.  This will allow you to play with the different endpoints.  
Create a contact (POST) and then run the other api end points.  Try things like invalid input, duplicate email addresses, Birthdate in future.

## Things to be aware of

This is a quick and dirty and not how I would create a good api.  See below

- I did not use automapper and create API models to translate back and forth between enity framework and api models.
- I did not create a controller for email addresses. 
- I did not use odata to allow the client to control things like filtering and such

