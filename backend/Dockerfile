FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything EXCEPT global.json first
COPY *.csproj ./
COPY [!g]* ./
COPY Controllers/ ./Controllers/
COPY Models/ ./Models/
COPY DTO/ ./DTO/
COPY Context/ ./Context/
COPY classes/ ./classes/

# Create a new global.json that matches the available SDK
RUN echo '{"sdk":{"version":"9.0.203","rollForward":"latestFeature"}}' > global.json

# Restore and build
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Create uploads directory
RUN mkdir -p /app/uploads

ENV PORT=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "backend.dll"]