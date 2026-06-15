# Vantage-PMO-Back-End

Backend de la plataforma **Vantage PMO**, construido con **ASP.NET Core (.NET 10)** siguiendo
una arquitectura de **monolito modular** con **Bounded Contexts**, capas **DDD** y **CQRS ligero**.

## Stack

- .NET 10 / ASP.NET Core
- Entity Framework Core 10 + MySQL (`MySql.EntityFrameworkCore`)
- Swagger / OpenAPI (Swashbuckle + Annotations)
- Localización (en / es) con `IStringLocalizer`

## Bounded Contexts

- **Profiles** — gestión de perfiles de usuario (implementado).

Cada contexto se organiza en capas: `Domain`, `Application`, `Infrastructure`, `Interfaces/REST`.
La infraestructura compartida (un único `AppDbContext`, repositorios base, Unit of Work,
interceptor de auditoría, convención kebab-case y manejo global de excepciones) vive en `Shared/`.

## Requisitos previos

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- Una base de datos **MySQL 8** accesible
- (Opcional) Herramienta EF: `dotnet tool install --global dotnet-ef`

## Configuración

La cadena de conexión se define en `vantagePMO-platform/appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;user=root;password=root1234;database=vantage-pmo-db"
}
```

### Levantar MySQL con Docker

```bash
docker run --name vantage-mysql -e MYSQL_ROOT_PASSWORD=root1234 -e MYSQL_DATABASE=vantage-pmo-db -p 3306:3306 -d mysql:8
```

## Compilar

```bash
cd vantagePMO-platform
dotnet restore
dotnet build
```

## Ejecutar

Las migraciones de EF Core se aplican automáticamente al iniciar (`Database.Migrate()`),
por lo que basta con tener MySQL arriba:

```bash
cd vantagePMO-platform
dotnet run
```

La API queda disponible en `http://localhost:5278`.

## Ver Swagger en el navegador

Con la aplicación corriendo (en entorno `Development`), abre:

```
http://localhost:5278/swagger
```

El documento OpenAPI en crudo está en:

```
http://localhost:5278/swagger/v1/swagger.json
```

### Verlo desde la terminal

```bash
# Comprobar que el documento OpenAPI responde y listar las rutas expuestas
curl -s http://localhost:5278/swagger/v1/swagger.json

# (con jq) ver solo los paths
curl -s http://localhost:5278/swagger/v1/swagger.json | jq ".paths | keys"
```

## Endpoints de Profiles

Base: `http://localhost:5278/api/v1/profiles`

| Método | Ruta                         | Descripción                  | Respuestas                  |
|--------|------------------------------|------------------------------|-----------------------------|
| POST   | `/api/v1/profiles`           | Crear perfil                 | 201 / 400 / 409             |
| GET    | `/api/v1/profiles/{id}`      | Obtener por id               | 200 / 404                   |
| GET    | `/api/v1/profiles?email=`    | Obtener por email            | 200 / 400 / 404             |
| PATCH  | `/api/v1/profiles/{id}`      | Actualización parcial        | 200 / 400 / 404 / 409       |

> Los errores se devuelven como `ProblemDetails` (RFC 7807) y se localizan según el header
> `Accept-Language` (`en` o `es`).

### Ejemplos con curl

```bash
# Crear
curl -s -X POST http://localhost:5278/api/v1/profiles \
  -H "Content-Type: application/json" \
  -d '{"name":"Alex Sterling","email":"alex.sterling@vantagepmo.io","role":"Precision Lead","department":"Executive","joined":"January 2022","avatarSeed":"Alex","bio":["Linea 1"],"certifications":["PMP","SAFe"]}'

# Obtener por id
curl -s http://localhost:5278/api/v1/profiles/1

# Obtener por email
curl -s "http://localhost:5278/api/v1/profiles?email=alex.sterling@vantagepmo.io"

# Actualizar (parcial)
curl -s -X PATCH http://localhost:5278/api/v1/profiles/1 \
  -H "Content-Type: application/json" \
  -d '{"role":"Senior Precision Lead","availabilityLabel":"Fully Booked"}'
```

> También puedes usar el archivo `vantagePMO-platform/vantagePMO-platform.http` desde tu IDE.

## Migraciones de base de datos

```bash
cd vantagePMO-platform

# Crear una nueva migración
dotnet ef migrations add NombreMigracion \
  --context AppDbContext \
  --output-dir Shared/Infrastructure/Persistence/EntityFrameworkCore/Migrations

# Aplicar migraciones manualmente (también se aplican al iniciar la app)
dotnet ef database update --context AppDbContext
```
