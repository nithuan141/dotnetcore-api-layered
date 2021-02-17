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

<strong>Validation :</strong> Fluent validation used for the validation. (<a href="https://docs.fluentvalidation.net/en/latest/aspnet.html">More about Fluent validation here</a>)

<strong>Documetation :</strong> The swagger documentation has been used and it is configured using Swashbuckle in the middleware. (<a href="https://www.c-sharpcorner.com/article/swagger-integration-with-webapi-2-in-mvc/#:~:text=Swashbuckle/Swagger%20is%20simple%20and%20powerful%20representation%20of%20any,other%20third%20party%20testing%20tool%20(Postman,%20Fiddler%20etc"> More here</a>)

<strong>ORM :</strong> EntityFrameworkCore 5.0 is used for ORM.
