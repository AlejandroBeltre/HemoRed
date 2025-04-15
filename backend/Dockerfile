FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj first
COPY *.csproj ./

# Force target framework to match available runtime
RUN sed -i 's/net8.0/net9.0/g' *.csproj

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Ensure the uploads directory exists
RUN mkdir -p /app/uploads

# Set environment variables
ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "backend.dll"]