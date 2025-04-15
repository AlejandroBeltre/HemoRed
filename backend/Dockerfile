FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project file first and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything except the global.json
COPY . ./

# Create Enums directory if it doesn't exist
RUN mkdir -p Enums

# Create the allEnums.cs file with all enum definitions
RUN cat > Enums/allEnums.cs << 'EOL'
using System;
using System.ComponentModel;

namespace backend.Enums
{
    public enum BloodType
    {
        A_Positive,
        A_Negative,
        B_Positive,
        B_Negative,
        AB_Positive,
        AB_Negative,
        O_Positive,
        O_Negative
    }

    public static class BloodTypeExtensions
    {
        public static string ToDatabaseString(this BloodType bloodType)
        {
            return bloodType switch
            {
                BloodType.A_Positive => "A+",
                BloodType.A_Negative => "A-",
                BloodType.B_Positive => "B+",
                BloodType.B_Negative => "B-",
                BloodType.AB_Positive => "AB+",
                BloodType.AB_Negative => "AB-",
                BloodType.O_Positive => "O+",
                BloodType.O_Negative => "O-",
                _ => throw new ArgumentOutOfRangeException(nameof(bloodType), bloodType, null)
            };
        }

        public static BloodType FromDatabaseString(string bloodType)
        {
            return bloodType switch
            {
                "A+" => BloodType.A_Positive,
                "A-" => BloodType.A_Negative,
                "B+" => BloodType.B_Positive,
                "B-" => BloodType.B_Negative,
                "AB+" => BloodType.AB_Positive,
                "AB-" => BloodType.AB_Negative,
                "O+" => BloodType.O_Positive,
                "O-" => BloodType.O_Negative,
                _ => throw new ArgumentException("Invalid blood type", nameof(bloodType))
            };
        }
    }

    public enum Status
    {
        D,
        P,
    }

    public enum Gender
    {
        M,
        F
    }

    public enum UserRole
    {
        A,
        U
    }

    public enum DocumentType
    {
        P,
        C
    }
}
EOL

# Create a fixed global.json
RUN echo '{"sdk":{"version":"9.0.203","rollForward":"latestFeature"}}' > global.json

# Create endpoints directory (if referenced)
RUN mkdir -p endpoints

# Build the application
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Ensure the uploads directory exists
RUN mkdir -p /app/uploads

# Set environment variables
ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "backend.dll"]