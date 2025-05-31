# HemoRed

HemoRed is a web platform designed to optimize blood donation processes and improve blood bank management. This project aims to connect donors with blood banks, facilitate blood acquisition, and manage donation campaigns.

## Table of Contents
- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Docker](#docker)
- [Contributing](#contributing)

## Introduction

This project was created using [Create React App](https://github.com/facebook/create-react-app) for the frontend and [.NET Web API](https://dotnet.microsoft.com/en-us/apps/aspnet/apis) for the backend.

## Prerequisites

- [Node.js](https://nodejs.org/)
- [npm](https://www.npmjs.com/)
- [.NET SDK](https://dotnet.microsoft.com/download)
- [dotnet CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- [MySQL](https://dev.mysql.com/doc/)
- [Docker](https://www.docker.com/products/docker-desktop/)

## Installation

1. **Clone the repository:**
```bash
git clone https://github.com/AlejandroBeltre/HemoRed.git
cd HemoRed
```

2. **Install frontend dependencies:**
```bash
cd frontend 
npm install
```

3. **Install backend dependencies:**
```bash
cd backend
dotnet restore
```

4. **MySQL Server Setup**

   a. Have MySQL Workbench installed and launch it.
   
   b. In the MySQL Connections section, click the plus button to add a new connection.
   
   ![MySQL Connection Setup](https://github.com/AlejandroBeltre/HemoRed/assets/127040596/d41280dc-adcc-466e-8cdd-1842b1c2654a)
   
   c. Add the connection name of your choice, set the IP address to: `159.203.103.67` in the Hostname field, add Port: `3307`, keep username as `root`, and set password to `AsuntadorSupremo1234`.
   
   ![MySQL Connection Configuration](https://github.com/AlejandroBeltre/HemoRed/assets/127040596/ae25ee1c-4a28-4078-908c-1528c2371e8a)
   
   d. After this, validate that the connection was successful using the "Test Connection" button.
   
   e. If this step was successful, you can access the connection where the project database called "HemoRedDB" is located and verify that it contains the data.

## Running the Application

1. **Start the backend:**
```bash
cd backend
dotnet run
# or
dotnet watch
```

2. **Start the frontend:**
```bash
cd ../frontend
npm start
```

## Docker

The project's Docker configuration is already set up. You must have a Docker Hub account logged into Docker Desktop to run without issues. This will be used for application deployment or running in environments that don't support our application natively.

1. **Run docker compose:**
```bash
cd HemoRed # Navigate back to project root
docker-compose up --build
```

2. Go to Docker Desktop to validate the app container running correctly. Here you can validate the images and paths where the container is executing. From this same UI, you can access the application.

3. **Application and database ports:**
```bash
backend-1: 8000:80
frontend-1: 80:80
mysql-1: 3308:3306
```

## Contributing

Due to the nature of the project, when contributing you must follow the Issues assigned to your user. When developing these issues, you must create a new branch with the name of the issue you are solving. After solving the issue and pushing your code, you must generate a pull request to the "Develop" branch, which will be used by the main developer, in this case [Alejandro Beltre](https://github.com/AlejandroBeltre). After this, you must wait for pull request approval to resolve your issue. The issue should not be marked as solved until it has been approved.
