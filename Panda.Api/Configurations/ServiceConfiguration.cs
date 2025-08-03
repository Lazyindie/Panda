using Panda.Services.Appointments.AddAppointment;
using Panda.Services.Appointments.GetAppointment;
using Panda.Services.Appointments.UpdateAppointmentStatus;
using Panda.Services.Members.Clinicians.AddClinician;
using Panda.Services.Members.Clinicians.GetClinicians;
using Panda.Services.Members.Patients.AddPatient;
using Panda.Services.Members.Patients.EditPatient;
using Panda.Services.Members.Patients.GetPatient;
using System.Reflection;

namespace Panda.Api.Configurations;

/// <summary>
/// Provides a static method for configuring services in the API.
/// This also provides the ability to implement the same dependency injection in unit tests.
/// </summary>
public static class ServiceConfiguration
{
    /// <summary>
    /// Add services to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static void Configure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen((options) =>
            {
                // Include documentation from generated XML comments.
                string[] xmlCommentFiles = [$"{Assembly.GetExecutingAssembly().GetName().Name}.xml", "Public.xml"];
                foreach (var fileName in xmlCommentFiles)
                {
                    string xmlFilePath = Path.Combine(AppContext.BaseDirectory, fileName);
                    if (File.Exists(xmlFilePath))
                    {
                        options.IncludeXmlComments(xmlFilePath, includeControllerXmlComments: true);
                    }
                }
                // Display enums as names instead of numbers.
                options.SchemaFilter<EnumSchemaFilter>();
            })
            .ConfigureServices()
            .ConfigureEntityFramework(configuration)
            .AddControllers();
    }

    /// <summary>
    /// Add services for dependency injection.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        return services
            // Patient Services
            .AddScoped<IAddPatientService, AddPatientService>()
            .AddScoped<IGetPatientService, GetPatientService>()
            .AddScoped<IEditPatientService, EditPatientService>()

            // Appointment Services
            .AddScoped<IAddAppointmentService, AddAppointmentService>()
            .AddScoped<IFindAppointmentService, FindAppointmentService>()
            .AddScoped<IEditAppointmentService, EditAppointmentService>()

            // Clinician Services
            .AddScoped<IAddClinicianService, AddClinicianService>()
            .AddScoped<IGetCliniciansService, GetCliniciansService>()
        ;
    }
}
