using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Panda.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SeedSomeInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                -- Seed initial data for Patients, Departments, Clinicians, and Appointments
                DECLARE @JohnDoe NVARCHAR(100) = NEWID(), 
                        @JaneSmith NVARCHAR(100) = NEWID(),
                        @CardiologyId NVARCHAR(100) = NEWID(),
                        @NeurologyId NVARCHAR(100) = NEWID(),
                        @DrAliceJohnsonId NVARCHAR(100) = NEWID(),
                        @DrBobBrownId NVARCHAR(100) = NEWID();

                INSERT INTO Panda.Patients (Id, Name, DateOfBirth, Postcode)
                VALUES 
                    (@JohnDoe, 'John Doe',   '1980-01-01', 'AB12 3CD'),
                    (@JaneSmith, 'Jane Smith', '1990-02-02', 'EF45 6GH')
                ;

                INSERT INTO Panda.Departments (Id, Name, Description)
                VALUES 
                    (@CardiologyId, 'Cardiology', 'Heart and blood vessel health'),
                    (@NeurologyId, 'Neurology',  'Nervous system and brain health')
                ;

                INSERT INTO Panda.Clinicians (Id, DepartmentId, Name, DateOfBirth)
                VALUES
                    (@DrAliceJohnsonId, @CardiologyId, 'Dr. Alice Johnson', '1975-03-03'),
                    (@DrBobBrownId, @NeurologyId,  'Dr. Bob Brown',     '1985-04-04')
                ;   

                INSERT INTO Panda.Appointments (Id, PatientId, ClinicianId, DepartmentId, Time, Duration, Status)
                VALUES
                    (NEWID(), @JohnDoe,   @DrAliceJohnsonId, @CardiologyId, '2026-01-01 12:00:00 +01:00', '00:15:00', '0'),
                    (NEWID(), @JaneSmith, @DrBobBrownId,     @CardiologyId, '2023-01-01 12:00:00 +01:00', '00:30:00', '100'),
                    (NEWID(), @JaneSmith, @DrAliceJohnsonId, @NeurologyId,  '2023-01-01 12:00:00 +01:00', '01:00:00', '200'),
                    (NEWID(), @JohnDoe,   @DrBobBrownId,     @NeurologyId,  '2027-01-01 12:00:00 +01:00', '01:15:00', '300')
                ;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
