using backend.DTO;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace backend;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.OperationId == "Register" || context.MethodInfo.Name == "Register")
        {
            var schema = context.SchemaGenerator.GenerateSchema(typeof(RegisterUserDTO), context.SchemaRepository);
            
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = 
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = schema.Properties,
                            Required = schema.Required
                        }
                    }
                }
            };

            operation.RequestBody.Content["multipart/form-data"].Schema.Properties.Add("image", new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            });
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