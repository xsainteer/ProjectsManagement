
# Projects Management

Data - data access, context, configuration, migrations

API - controllers, mapping (DTO -> Domain Entity)

Business - business logic

Domain - interfaces, entities, enums

Frontend - presentation, API calls


## Frontend Deployment

### Running frontend

run this in the solution folder

```bash
  cd frontend
  npm install
  npm run dev
```

Note:

`/wizard` route - wizard, step by step project creation, uploads files and dtos only when the final button `Save` is clicked.

`/task-creation` route - task creation, it takes entities from API, so you should first create a project with other entities via `/wizard` and only then proceed to `/task-creation`

### Configurations

change apiUrl in `.env` if needed



## Backend Deployment

### Run Docker Compose

in the root folder, run:

```bash
docker-compose up -d
```

### Apply database migrations

run this in the solution folder:

```bash
dotnet ef database update UpdateModels --project Data --startup-project API
```

### Configure File Storage Path


In the API project, update the `appsettings.json` file if needed:
```javascript
"FileStorageSettings": {
    "FilePath": "/uploads"
  }
```

 Note:
 
The file path must be an absolute path. If you set `"FilePath": "/uploads"`, it will be interpreted as C:/uploads on Windows.
## Used:

What libraries/frameworks were used:

### Frontend

1. Vue 3

2. Element Plus

3. Axios

4. Router

### Backend

1. ASP.Net Core Web API

2. Ef Core

3. AutoMapper

4. Swagger

5. PostgreSQL
