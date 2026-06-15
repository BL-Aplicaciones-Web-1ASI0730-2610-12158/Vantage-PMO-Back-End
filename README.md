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

- **[.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)** (no basta con .NET 11 u otra versión)
- **Docker** (recomendado) o una instancia de **MySQL 8** accesible en `localhost:3306`
- (Opcional) Herramienta EF: `dotnet tool install --global dotnet-ef`

### Instalar .NET SDK 10

En Windows, con winget:

```powershell
winget install Microsoft.DotNet.SDK.10
```

Cierra y vuelve a abrir la terminal después de instalar. Comprueba que aparece la versión 10:

```powershell
dotnet --list-sdks
dotnet --list-runtimes
```

Debes ver entradas `10.0.xxx`. Si solo tienes .NET 11, `dotnet run` compilará pero fallará al ejecutar con:

```
You must install or update .NET to run this application.
Framework: 'Microsoft.NETCore.App', version '10.0.0' (x64)
```

## Levantar el proyecto

Sigue estos pasos en orden:

### 1. Base de datos MySQL

La cadena de conexión en `vantagePMO-platform/appsettings.Development.json` es:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;user=root;password=root1234;database=vantage-pmo-db"
}
```

**Primera vez** — crear el contenedor:

```bash
docker run --name vantage-mysql -e MYSQL_ROOT_PASSWORD=root1234 -e MYSQL_DATABASE=vantage-pmo-db -p 3306:3306 -d mysql:8
```

**Si el contenedor ya existe** (error `container name is already in use`):

```bash
docker start vantage-mysql
```

Espera 10–30 segundos a que MySQL termine de iniciar antes de arrancar la API.

### 2. Compilar y ejecutar

```bash
cd vantagePMO-platform
dotnet restore
dotnet build
dotnet run --launch-profile http
```

Las migraciones de EF Core se aplican automáticamente al iniciar (`Database.Migrate()`),
así que MySQL debe estar arriba antes de ejecutar `dotnet run`.

Si todo va bien verás:

```
Now listening on: http://localhost:5278
Hosting environment: Development
```

### 3. Abrir Swagger

Con la aplicación corriendo (solo en entorno `Development`), abre en el navegador:

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

## Problemas frecuentes

| Síntoma | Causa | Solución |
|---------|-------|----------|
| Swagger no carga / conexión rechazada en `localhost:5278` | La API no arrancó | Revisa la terminal: debe aparecer `Now listening on: http://localhost:5278` |
| `You must install or update .NET... version '10.0.0'` | Falta el SDK/runtime de .NET 10 | Instala con `winget install Microsoft.DotNet.SDK.10` y reinicia la terminal |
| `Unable to connect to any of the specified MySQL hosts` | MySQL no está corriendo o aún no está listo | `docker start vantage-mysql` y espera unos segundos |
| `container name "/vantage-mysql" is already in use` | El contenedor ya fue creado antes | Usa `docker start vantage-mysql` en lugar de `docker run` |
| Swagger devuelve 404 | Entorno distinto de `Development` | Ejecuta con `dotnet run --launch-profile http` |

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
