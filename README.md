# Microondas-Benner

## Descrição
Aplicação web (MVC) e API em ASP.NET Core para simular um micro-ondas com cadastro de pré-aquecimentos e autenticação via JWT.

## Tecnologias
- C#
- .NET 8
- ASP.NET Core MVC (Controllers + Razor Views)
- Entity Framework Core
- SQL Server
- Autenticação JWT (JwtBearer)
- Swagger (Swashbuckle)
- DotNetEnv
- Bootstrap
- jQuery

## Instalação e Uso
1. Instale o **.NET SDK 8**.
2. Na pasta do projeto, restaure as dependências:
   - `dotnet restore`
3. Garanta que existe o arquivo `MicroondasMVC-Benner/Config/.env` com as chaves necessárias (ex.: `ChaveConnection`, `DefaultConnection` e/ou `ConnectionExterna`).
4. Rode a aplicação localmente:
   - `dotnet run --project MicroondasMVC-Benner/MicroondasMVC_Benner.csproj`
5. Acesse no navegador:
   - `http://localhost:5219` (ou `https://localhost:7065`), conforme `MicroondasMVC-Benner/Properties/launchSettings.json`.
6. Em ambiente `Development`, o Swagger fica disponível na UI padrão do projeto (Swagger UI).

## .gitignore
O projeto já possui um `.gitignore` adequado para .NET/Visual Studio; vou ajustá-lo para ignorar arquivos `.env` (incluindo `MicroondasMVC-Benner/Config/.env`).

## Créditos
>  This is a challenge by [Coodesh](https://coodesh.com/)
