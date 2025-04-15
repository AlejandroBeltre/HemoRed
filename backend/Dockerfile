FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else
COPY . ./

# IMPORTANT: Override the global.json file to use available SDK
RUN rm -f global.json && \
    echo '{"sdk":{"version":"9.0.203","rollForward":"latestFeature"}}' > global.json

# Now build
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Add debugging/diagnostic output
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

# Ensure the uploads directory exists
RUN mkdir -p /app/uploads

# Set the PORT environment variable for Railway
ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "backend.dll"]