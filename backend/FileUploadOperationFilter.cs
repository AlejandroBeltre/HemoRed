using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace backend;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileUploadMime = "multipart/form-data";

        if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x => x.Key.Equals(fileUploadMime, StringComparison.InvariantCultureIgnoreCase)))
        {
            return;
        }

        var uploadMediaType = operation.RequestBody.Content[fileUploadMime];
        var formParameters = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile))
            .ToList();

        if (formParameters.Any())
        {
            foreach (var formParameter in formParameters)
            {
                AddSingleFileUploadProperty(uploadMediaType, formParameter.Name);
            }
        }
        else
        {
            // If no IFormFile parameters are found, add a default file upload property
            AddSingleFileUploadProperty(uploadMediaType, "file");
        }
    }

    private void AddSingleFileUploadProperty(OpenApiMediaType mediaType, string propertyName)
    {
        mediaType.Schema.Properties[propertyName] = new OpenApiSchema
        {
            Type = "string",
            Format = "binary"
        };
    }
}