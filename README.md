# HemoRed
HemoRed es una plataforma web diseñada para optimizar los procesos de donación de sangre y mejorar la gestión de los bancos de sangre. Este proyecto tiene como objetivo conectar donantes con bancos de sangre, facilitar la adquisición de sangre y administrar campañas de donación.

## Tabla de contenidos

- [Introducción](#introducción)
- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Ejecutar la Aplicación](#ejecutar-la-aplicación)
- [Docker](#docker)
- [Contribuir](#contribuir)

## Introducción

Este proyecto fue creado con [Create React App](https://github.com/facebook/create-react-app) para el frontend y [.NET Web API](https://dotnet.microsoft.com/en-us/apps/aspnet/apis) para el backend.

## Requisitos Previos

- [Node.js](https://nodejs.org/)
- [npm](https://www.npmjs.com/)
- [.NET SDK](https://dotnet.microsoft.com/download)
- [dotnet](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- [MySQL](https://dev.mysql.com/doc/)
- [Docker](https://www.docker.com/products/docker-desktop/)

## Instalación
1. **Clonar el repositorio:**

```bash
git clone https://github.com/AlejandroBeltre/HemoRed.git

cd HemoRed
```

2. **Instalar dependencias del frontend:**

```bash
cd frontend 

npm install
```

3. **Instalar dependencias del backend:**

```bash
cd backend

dotnet restore
```

4. **SetUp MySQL con Server**

  a. Tener MySQL Workbench e iniciarlo.
  b. En la sección de MySQL Connections dar clic en el botón de más para agregar una conección.
    
  ![image](https://github.com/AlejandroBeltre/HemoRed/assets/127040596/d41280dc-adcc-466e-8cdd-1842b1c2654a)

  c. Agregar el nombre de la conexión que desee, agregar la IP: 159.203.103.67 en el espacio de Hostname, agregar el Puerto: 3307, el username se queda en root y la contraseña es AsuntadorSupremo1234.

  ![image](https://github.com/AlejandroBeltre/HemoRed/assets/127040596/ae25ee1c-4a28-4078-908c-1528c2371e8a)

  d. Luego de esto validar que la conexión fue correcta en el botón de "Probar conexión" o "Test connection".
  
  e. Si este paso fue exitoso puede entrar a la conexión en donde se encuentra la base de datos del proyecto llamada "HemoRedDB" y verificar que posee los datos.


## Ejecutar la Aplicación

1. **Iniciar el backend:**

```bash
cd backend

dotnet run o dotnet watch
```

2. **Iniciar el frontend:**

```bash
cd ../frontend

npm start
```

## Docker

El docker del proyecto ya se encuentra confirgurado, se debe de tener una cuenta en Docker Hub logeada al Docker Desktop para poder ejecutar sin problemas. Este se utilizara para el deploy de la aplicación o correr en ambientes que no soporten nuestra aplicación.

1. **Ejecutar el docker compose:**

```bash
cd HemoRed --volver al root del proyecto.

docker-compose up --build
```

2. Ir a Docker Desktop para validar el contenedor de la app corriendo correctamente en el se pueden validar las imagenes y las rutas de donde se esta ejecutando el contenedor, desde este mismo UI podemos acceder a esta.

## Contribuir

Por la naturaleza del proyecto a la hora de contribuir al proyecto se debe de seguir los Issues asignado a su User. A la hora de desarrollar dichos issues debe de crear una Branch nueva con el nombre del issue que esta solucionando, tras solucionar el issue y pushear su codigo debe de generar un pull request a la branch "Develop", la misma branch la usará el main developer en este caso [Alejandro Beltre](https://github.com/AlejandroBeltre). Luego de esto debe de esperar la aprobación del pull request para solucionar su issue, el mismo no debe de ser marcado solucionado hasta que sea aprobado.
