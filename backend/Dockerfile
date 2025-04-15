FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project file first (better for layer caching)
COPY *.csproj *.sln ./

# Fix the project file
RUN sed -i '/<PropertyGroup>/a \    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>' backend.csproj

# Restore dependencies with specific project file
RUN dotnet restore backend.csproj

# Copy all source files after restore
COPY . ./

# Create proper global.json
RUN echo '{"sdk":{"version":"9.0.203","rollForward":"latestFeature"}}' > global.json

# Build project with specific project file
RUN dotnet publish backend.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Ensure uploads directory exists
RUN mkdir -p /app/uploads

ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "backend.dll"]