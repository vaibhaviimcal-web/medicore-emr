# MediCore EMR - Backend API

ASP.NET Core 8.0 Web API for MediCore EMR system.

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- PostgreSQL 14+
- Visual Studio 2022 or VS Code

### Installation

1. **Restore packages:**
```bash
dotnet restore
```

2. **Update database connection string:**
Edit `appsettings.json` and update the connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=medicore;Username=postgres;Password=yourpassword"
}
```

3. **Run migrations:**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. **Run the API:**
```bash
dotnet run
```

API will be available at: `https://localhost:5001`

### API Documentation

Once running, access Swagger UI at: `https://localhost:5001/swagger`

## API Endpoints

### Patients
- `GET /api/v1/patient` - List all patients
- `GET /api/v1/patient/search?q={query}` - Search patients
- `GET /api/v1/patient/{id}` - Get patient details
- `POST /api/v1/patient` - Create new patient
- `PUT /api/v1/patient/{id}` - Update patient
- `DELETE /api/v1/patient/{id}` - Delete patient

### Visits
- `GET /api/v1/visit/patient/{patientId}` - Get patient visit history
- `GET /api/v1/visit/{id}` - Get visit details
- `POST /api/v1/visit` - Create new visit
- `POST /api/v1/visit/{id}` - Update visit
- `GET /api/v1/visit/followups/today` - Get today's follow-ups

### Master Data
- `GET /api/v1/masterdata/chief-complaints` - Get chief complaints list
- `GET /api/v1/masterdata/diagnoses` - Get diagnoses list
- `GET /api/v1/masterdata/medicines` - Get medicines list

## Database Schema

The system uses PostgreSQL with the following main tables:
- `patients` - Patient demographics
- `visits` - OPD visit records
- `prescriptions` - Prescription data
- `billings` - Billing and payment records
- Master data tables for complaints, diagnoses, and medicines

## Development

### Adding new migrations:
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Running tests:
```bash
dotnet test
```

## Deployment

For production deployment, update `appsettings.Production.json` with production database credentials and deploy to your hosting platform (Azure, AWS, etc.).
