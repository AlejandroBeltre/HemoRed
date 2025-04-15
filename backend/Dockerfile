FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj file first
COPY *.csproj ./

# Diagnose the project file content
RUN echo "============ PROJECT FILE CONTENT ============" && \
    cat *.csproj && \
    echo "=============================================="

# Fix the project file to prevent duplicate items
RUN cp *.csproj original.csproj && \
    echo "Adding EnableDefaultCompileItems=false to project file" && \
    sed -i '/<PropertyGroup>/a \    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>' *.csproj && \
    echo "Modified project file:" && \
    cat *.csproj

# Restore dependencies with fixed project file
RUN dotnet restore

# Copy all source files
COPY . ./

# Create proper global.json
RUN echo '{"sdk":{"version":"9.0.203","rollForward":"latestFeature"}}' > global.json

# Build project with fixed configuration
RUN dotnet publish -c Release -o out

# Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Ensure uploads directory exists
RUN mkdir -p /app/uploads

# Set environment variables from Railway configuration
ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "backend.dll"]