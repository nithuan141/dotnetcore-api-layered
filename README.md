![.NET](https://github.com/nithuan141/dotnetcore-api-layered/workflows/.NET/badge.svg?branch=main)

# Dot net core 3.1 REST API - Layerd architecture

Ready to start boiler plates for .net core 3.1 REST API with layered architecture and repository pattern.

# Introduction

This repo is part of a mission to build ready to start boilerplate for dot net core REST API with different architecture and all essential goodies for production ready code.
This one is a version of layered architecture with Repository pattern and entity framework. I am following the pattern of micro service and creating individual micro-API’s which can be scalable independently.

I still beleive there are scenarios for building REST API using the layered architecture and this has relevance. 

# Architecture

![alt text](https://github.com/nithuan141/dotnetcore-api-layered/blob/main/architecture.PNG)

# Contents

<strong>Rest API :</strong> .net core 3. Web API Project.

<strong>ORM :</strong> EntityFrameworkCore 5.0 is used for ORM.

<strong>Unit Test :</strong> MS Test is used for unit testing the service/business layer classes and Moq frame work is used to mock the data layer.

<strong>Validation :</strong> Fluent validation is used for the validation. (<a href="https://docs.fluentvalidation.net/en/latest/aspnet.html">More about Fluent validation here</a>)

<strong>Documentation :</strong> The swagger documentation has been used and it is configured using Swashbuckle in the middleware. (<a href="https://www.c-sharpcorner.com/article/swagger-integration-with-webapi-2-in-mvc/#:~:text=Swashbuckle/Swagger%20is%20simple%20and%20powerful%20representation%20of%20any,other%20third%20party%20testing%20tool%20(Postman,%20Fiddler%20etc"> More here</a>)

<strong>Data Layer : </strong> Data layer is built using the repository pattern.

<strong>Authentication : </strong> AspNetCore.Authentication.JwtBearer Package is used to introduce JWT token based authentication and authorization. This can be easily replaced with AAD or any other oauth based identity provider by configuring the middleware in start-up and changing the AutheticationService in the service layer. As of now role-based authorization is also implemented using JWT  token.

# Run

Once cloned/downloaded you can run this API in IISExpres, IIS or Kestrel servers. Also please remeber to add the connection string in the appsetting.json
  ` "ConnectionString": {
      "KeyServiceDB": "<-- Your connection String Here -->;"
   }`
   
# Test Coverage

The test coverage is measuring using  <a href="https://github.com/coverlet-coverage/coverlet" taget="_blank">coverlet</a> and  <a href="https://github.com/danielpalme/ReportGenerator" taget="_blank">ReportGenerator</a>. Coverlet generates the coverage report xml and ReportGenerator converts it to human readble reports using html. Once you build the test project run `dotnet test` command from the command prompts from the UserServiceTest folder. The report can be viewed from the index.html file in Coverage/Report folder.

# Contribute

All contributions and corrections/fixes are welcome, please feel free to create a branch and make your changes and then create a pull request to merge that to main branch.
