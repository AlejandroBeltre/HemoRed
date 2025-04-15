FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and fix it
COPY *.csproj ./
RUN cat *.csproj > csproj_backup.txt && \
    sed -i '/<PropertyGroup>/a \    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>' *.csproj

# Restore dependencies
RUN dotnet restore

# Copy all files
COPY . ./

# Override global.json
RUN echo '{"sdk":{"version":"9.0.203","rollForward":"latestFeature"}}' > global.json

# Build with explicit file inclusion
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Ensure uploads directory exists
RUN mkdir -p /app/uploads

ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "backend.dll"]