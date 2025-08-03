using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Panda.Api.Configurations;

public class EnumSchemaFilter : ISchemaFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema == null)
        {
            throw new ArgumentNullException(nameof(schema));
        }

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Type.IsEnum && schema.Enum.Any())
        {
            var enumNames = new OpenApiArray();
            enumNames.AddRange(Enum.GetNames(context.Type).Select(name => new OpenApiString(name) as IOpenApiAny));
            schema.Extensions.Add(nameof(enumNames), enumNames);
        }
    }
}