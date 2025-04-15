FROM node:18-alpine

WORKDIR /app

# Copy package files
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy all files
COPY . .

# Set environment variables to prevent warnings from becoming errors
ENV CI=false
ENV NODE_ENV=production

# Build the app
RUN npm run build:railway

# Install serve to run the application
RUN npm install -g serve

# Set the command to serve the built application
# Use a shell form to access environment variables
CMD serve -s build -l ${PORT:-80}

# Expose port 80
EXPOSE 80