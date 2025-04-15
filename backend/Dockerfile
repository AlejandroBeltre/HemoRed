FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy entire project directory structure with all files 
COPY . ./

# Verify Enums directory and file exists (for debugging)
RUN ls -la Enums/

# Override global.json with SDK version available in container
RUN echo '{"sdk":{"version":"9.0.203","rollForward":"latestFeature"}}' > global.json

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