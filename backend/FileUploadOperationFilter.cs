using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileUploadMime = "multipart/form-data";

        if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x => x.Key.Equals(fileUploadMime, System.StringComparison.InvariantCultureIgnoreCase)))
        {
            return;
        }

        var uploadMediaType = operation.RequestBody.Content[fileUploadMime];
        var schema = (OpenApiSchema)context.SchemaRepository.Schemas[context.MethodInfo.GetParameters()[0].ParameterType.Name];
        
        uploadMediaType.Schema.Properties.Clear();
        foreach (var property in schema.Properties)
        {
            uploadMediaType.Schema.Properties.Add(property.Key, property.Value);
        }

        uploadMediaType.Schema.Properties.Add("image", new OpenApiSchema()
        {
            Description = "Upload File",
            Type = "string",
            Format = "binary"
        });

        uploadMediaType.Encoding = new Dictionary<string, OpenApiEncoding>
        {
            ["image"] = new OpenApiEncoding { Style = ParameterStyle.Form }
        };
    }
}