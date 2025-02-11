<h1>Safeguard</h1>

##  Overview

The Safeguard API is the backend core of the Safeguard ecosystem, enabling secure storage, organization, and access to passwords, PINs, and sensitive data. Built for reliability and scalability, it supports user authentication, data synchronization, and seamless integration with frontend interfaces. The API ensures robust functionality while maintaining secure storage practices, powering the Safeguard app's user-friendly experience.

##  Table of Contents

- [ Overview](#-overview)
- [ Project Structure](#-project-structure)
- [ Getting Started](#-getting-started)
  - [ Prerequisites](#-prerequisites)
  - [ Installation](#-installation)
  - [ Usage](#-usage)
  - [ Testing](#-testing)


##  Project Structure

```sh
┌── App
│   ├── App.csproj
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   ├── Configuration
│   ├── Controllers
│   ├── Db
│   ├── Extensions
│   ├── Ioc
│   ├── Program.cs
│   └── Properties
├── Application
│   ├── Application.csproj
│   ├── Configuration
│   ├── Dtos
│   └── Services
├── docker-compose.yml
├── Dockerfile
├── Domain
│   ├── Domain.csproj
│   ├── Interfaces
│   └── Entities
├── Infrastructure
│   ├── Context
│   ├── Infrastructure.csproj
│   ├── Migrations
│   └── Repositories
├── Safeguard.sln
└── Test
├── Test.csproj
└── Usings.cs
```

##  Getting Started

###  Prerequisites

Before getting started with , ensure your runtime environment meets the following requirements:

- **.NET SDK 6.0:** https://dotnet.microsoft.com/en-us/download/dotnet/6.0

- **Docker:** https://www.docker.com/products/docker-desktop/

###  Installation

Install  using one of the following methods:

**Build from source:**

Clone the  repository:
```sh
 git clone https://github.com/VictorAlvesFarias/Safeguard-Backend
```

### Using `nuget` &nbsp; 

Install the project dependencies:

```sh
 dotnet restore
```

Go to the app folder:


```sh
 cd app
```

Run  using the following command:


```sh
 dotnet run
```

### Using `docker` &nbsp; 

Build application image:

```sh
 docker build -t safeguard-backend .
```

Run image:

```sh
 docker run -it safeguard-backend
```

###  Testing
Run the test suite using the following command:
**Using `nuget`** &nbsp; 

```sh
 dotnet test
```
