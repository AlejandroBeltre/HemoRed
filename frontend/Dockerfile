FROM node:18-alpine as build

WORKDIR /app

# Copy only package files first for better caching
COPY package*.json ./
RUN npm ci --quiet

# Copy the rest of the app
COPY . .

# Set environment variables
ENV CI=false
ENV NODE_ENV=production

# Build the app
RUN npm run build:railway

# Production stage
FROM nginx:alpine

# Copy built files
COPY --from=build /app/build /usr/share/nginx/html

# Configure nginx
RUN echo 'server { \
  listen 80; \
  location / { \
    root /usr/share/nginx/html; \
    try_files $uri $uri/ /index.html; \
  } \
}' > /etc/nginx/conf.d/default.conf

# Expose port
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]