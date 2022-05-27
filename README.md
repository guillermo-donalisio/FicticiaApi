# FicticiaApi - Code First approach
> Web Api from Ficticia S.A.

### Project Versions:
- IDE: Visual Studio Code: 1.67.2
- Net 6.0

**Packages NuGet installed:**
### To Authentications:
- Microsoft.AspNetCore.Authentication.JwtBearer (6.0.5)
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (6.0.5)
- Microsoft.Extensions.Identity.Core (6.0.5)
### To Database Connections & CRUD operations:
- Microsoft.EntityFrameworkCore (6.0.5)
- Microsoft.EntityFrameworkCore.Design (6.0.5)
- Microsoft.EntityFrameworkCore.SqlServer (6.0.5)
- Microsoft.EntityFrameworkCore.Tools (6.0.5)
### To consume the app:
- Swashbuckle.AspNetCore (6.2.3)

### Configurations

- Inside *appsetings.json* place your connection string and JWT secret and ports:
````
{
  "ConnectionStrings": {
    "DisneyConnection": "--- WRITE YOUR CONNECTION STRING HERE ---",
    "UserConnection": "--- WRITE YOUR CONNECTION STRING HERE ---"
  },  
  "JWT": {
    "ValidAudience": "--- WRITE YOUR PORT HERE ---",
    "ValidIssuer": "--- WRITE YOUR PORT HERE ---",
    "Secret": "--- SECRET KEY ---"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
````
### Create Database with these commands

````
dotnet ef migrations add <nameMigration> 
dotnet ef database update
````
### Test your endpoints using Swagger at local host

````
https://localhost:5001/swagger/index.html
````



