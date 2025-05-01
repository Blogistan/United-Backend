# United-Backend
United - Web API

# Description
In this project , You can do blogs side operations like create blogs, report users , make comments on blogs like and etc. API, EF,Mediator,Serilog,ElasticSearch,AutoMapper,Moq,Otp.Net,xUnit libs and frameworks used.

# Requirements
- [.NET CORE SDK 8.8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Sql Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [PostgreSql](https://www.postgresql.org/download/)
- [Ollama](https://ollama.com)

#  Setup

- ## Clone Repo
  `git clone https://github.com/Blogistan/United-Backend.git`
- ## Download Dependencies
  `dotnet restore`
- ## Configure Database Connection
  ` "MsSqlConfiguration": {
    "ConnectionString": "Server=.;Database=United;Trusted_Connection=True;",
    "TableName": "Logs",
    "AutoCreateSqlTable": true
    },
  "PostgreSqlConfiguration": {
    "ConnectionString": "Server=.;Database=United;Trusted_Connection=True;",
    "TableName": "Logs",
    "NeedAutoCreateTable": false
  },
`
- ## Update Database
  ` dotnet ef database update`
- ## Run Project
  `dotnet run`

### API endpoint will be added.
