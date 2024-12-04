## Fase 1: Uso do Sistemas - BackEnd

### 1. Pré-requisitos

- Instalar o DotNet Core SDK 8
  - [.NET SDK Downdload](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Pacotes de Dependências instalados nos Projetos.
  ```bash
  dotnet add package AutoMapper
  dotnet add package FluentValidation
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.Abstractions
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  dotnet add Newtonsoft.Json
  dotnet add Serilog.AspNetCore
  dotnet add Dapper
  ```
- Instalar o Docker Desktop ou docker cli (Linux ou MacOs)
  - [Docker Downdload](https://www.docker.com/products/docker-desktop/)

- Criar a rede local do Docker

  ```bash
  docker network create -d bridge localdev

  ```

* Abra a pasta root do projeto no terminal e rode o comando bash abaixo para criação do Banco de Dados SQL Server do projeto.
  ```bash
  docker compose down && docker rm -f api sqlserver && docker compose up -d

  ```

## Fase 2: FrontEnd
1. Instalar o gerenciador de Node NVM 
   * [Link para Windows e MacOS](https://github.com/nvm-sh/nvm)
2. Com a pasta root do projeto aberto, execute o comando abaixo para instalar as dependências.
+ Instalar a última versão do NodeJs
  ```bash
  nvm install latest
  ```
+ Instalar a última versão do Angular CLI, nesse projeto é o Angular 19
  ```bash
  npm -g i @angular/cli
  ```
+ Instalar todas as dependências do projeto
  ```bash
  npm i
  ```
### 2. Arquitetura
Para esse projeto utilizei os príncipios de DDD e com arquitetura MVC, além de utilizar o Specification Pattern que resolve os problemas de delegar as buscas dos CRUD de forma organizada e separando das responsabilidades de negócio. Além de seguir 100% os princípios de S.O.L.I.D e Design Patterns.
Em conjunto utilizei o Repository Pattern para aplicar o CRUD de consultas communs, para todas os services que estão criados. Isso facilita realizar os testes unitários, para validar as coberturas de testes e facilitar o Mock dos integrações de API e Database.