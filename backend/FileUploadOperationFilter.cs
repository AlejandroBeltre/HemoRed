using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace backend;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileUploadMime = "multipart/form-data";
        var routeValues = context.ApiDescription.ActionDescriptor.RouteValues;

        // Verificar si la clave 'X-KEY' está presente en el diccionario
        if (routeValues.ContainsKey("X-KEY"))
        {
            var xKeyValue = routeValues["X-KEY"];
            // Procesar el valor de la clave 'X-KEY'
            operation.Summary = $"Operation with X-KEY: {xKeyValue}";
        }
        else
        {
            // Manejar el caso en que la clave 'X-KEY' no esté presente
            operation.Summary = "Operation without X-KEY";
        }

        if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x => x.Key.Equals(fileUploadMime, System.StringComparison.InvariantCultureIgnoreCase)))
        {
            return;
        }

        // Check if the key exists in the SchemaRepository before accessing it
        var parameterTypeName = context.MethodInfo.GetParameters()[0].ParameterType.Name;
        if (context.SchemaRepository.Schemas.TryGetValue(parameterTypeName, out var schema))
        {
            var uploadMediaType = operation.RequestBody.Content[fileUploadMime];
            uploadMediaType.Schema.Properties.Clear();
            foreach (var property in schema.Properties)
            {
                uploadMediaType.Schema.Properties.Add(property.Key, property.Value);
            }

            // Check if the "image" key already exists
            if (!uploadMediaType.Schema.Properties.ContainsKey("image"))
            {
                uploadMediaType.Schema.Properties.Add("image", new OpenApiSchema()
                {
                    Description = "Upload File",
                    Type = "string",
                    Format = "binary"
                });
            }

            // Check if the "image" key already exists in the Encoding dictionary
            if (!uploadMediaType.Encoding.ContainsKey("image"))
            {
                uploadMediaType.Encoding = new Dictionary<string, OpenApiEncoding>
                {
                    ["image"] = new OpenApiEncoding { Style = ParameterStyle.Form }
                };
            }
        }
        else
        {
            // Handle the case where the schema for the parameter type is not found
            // You might want to log this situation or handle it as appropriate for your use case
            operation.Summary += " (Schema not found for parameter type)";
        }
    }
}
