# A simple BLOG

![blog](screen.PNG)

## Architecture

- Domain-driven design (DDD)
- Clean Architecture
- CQRS

## Technologies
- .NET 6.0
- C# 10
- Blazor WebAssembly App
- ASP.NET Core Web API
- Entity Framework
- Dapper ORM
- AutoMapper
- NLog
- MediatR
- FluentValidation


## Main layers

| Layer | Description |
| ------ | ------ |
| Blog.Client | Blazor application |
| Blog.Server | API |
| Blog.Application | Communication with Domain Layer |
| Blog.Infrastructure | Persistence |
| Blog.Domain | Core business logic |


## How to run the application
Create database - in Package Manager Console for Blog.Infrastructure project write:
```sh
update-database
```
Launch the application!


## If you want to add a post or manage comments in the application use the following data to log in
`login: test@test.com`
`hasło: Test123*`


## About the Author
Michał Misiukanis
[Linkedin](https://www.linkedin.com/in/micha%C5%82-misiukanis-875129119/)

