# Panda Healthcare Management System

A comprehensive healthcare management API built with .NET 8 for managing patients, clinicians, appointments, and departments in a healthcare environment.

## 🏥 Overview

PANDA is a healthcare management system that provides a RESTful API.
Users can manage patients, clinicians and appointments.

## 🛠️ Project Structure

```
Panda/
├── Panda.Api/              # Web API controllers and configuration
├── Panda.Domain/           # Domain entities (Patient, Clinician, etc.)
├── Panda.EntityFramework/  # Database context and migrations
├── Panda.Library.Class/    # Shared library classes
├── Panda.Services/         # Business logic services
└── Panda.Test/             # Unit and integration tests
```

## 🔧 Getting Started

### 1. Clone the Repository
```bash
git clone <repository-url>
cd Panda
```

### 2. Setup Database Connection
Update the connection string in `Panda.Api/appsettings.json` and `Panda.EntityFrameworkd.appsettings.json`:

An example has been provided below:
```json
{
  "ConnectionStrings": {
    "DatabaseContext": "Server=(localdb)\\mssqllocaldb;Database=PandaDb;Trusted_Connection=true;MultipleActiveResultSets=true;"
  }
}
```

### 3. Run Database Migrations
```bash
dotnet ef database update --project Panda.EntityFramework --startup-project Panda.Api
```
Included in the migrations are some example data.

### 4. Build and Run
```bash
dotnet build
dotnet run --project Panda.Api
```

The API will be available at:
- **HTTPS**: `https://localhost:7213`
- **Swagger UI**: `https://localhost:7213/swagger/index.html`

## 📚 API Endpoints

### Patients
- `GET /patient/{id}` - Get patient by ID
- `POST /patient` - Create new patient
- `PUT /patient/{id}` - Update patient

### Clinicians
- `POST /clinician` - Create new clinician

### Appointments
- `GET /appointment/{id}` - Get appointment by ID
- `POST /appointment` - Create new appointment
- `PUT /appointment/{id}` - Update appointment

## Domain Models

### IAuditable
All the domain models inherit from `IAuditable`, to track
    - CreatedAt
    - ModifiedAt
    - DeletedAt

If the entities `DeleteAt` is not null, the item will be filtered out when searched.
This is, for all purposes, "Soft Deleted". This is useful for auditing purposes.

### Patient
- **Name (Required)**: Patient's full name
- **DateOfBirth (Required)**: Patient's date of birth
- **Postcode (Required)**: Patient's postal code
- **Appointments**: Associated appointments

### Clinician
- **DepartmentId (Required)**: Associated department
- **Name (Required)**: Clinician's full name
- **DateOfBirth (Required)**: Clinician's date of birth
- **Department**: Associated department
- **Appointments**: Assigned appointments

### Appointment
- **ClinicianId (Required)**: Assigned clinician
- **PatientId (Required)**: Associated patient
- **DepartmentId (Required)**: Department where appointment takes place
- **Status (Required)**: Appointment status (Active, Cancelled, etc.)
- **Time (Required)**: Appointment date and time
- **Duration (Required)**: Appointment duration (default: 15 minutes)

### Department
- **Name (Required)**: Department name
- **Description**: Department description
- **Clinicians**: Assigned clinicians
- **Appointments**: Department appointments

## Testing

Run the test suite:
```bash
dotnet test
```

# Future Development
- Add users to auditing
    - Using ICurrentUseProvider, track users Id. This required authentication, however, which is out of scope for this MVP.
- Add base classes
    - There is some repeated code which would benifit from it's own class (i.e., Clinicien & Patient)
- Complete CRUD
    - Some endpoints have been skipped for MVP, such as managing departments, as this was out of scope.
- Authorization & CORS
    - A basic authorization has been added, some more secure policies would be benificial.
- Monitoring
    - There is currently no logging or health checks implemented, adding some would be useful for continuous support.