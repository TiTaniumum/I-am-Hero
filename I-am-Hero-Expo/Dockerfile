# Use a lightweight Node.js image
FROM node:18-alpine

# Set working directory
WORKDIR /app

# Copy package files and install dependencies
COPY package*.json ./
RUN npm install --legacy-peer-deps

# Copy the rest of the project files
COPY . .

# Build the project for web
RUN npm run export

# Use a simple web server to serve the static files
RUN npm install -g serve

# Expose port 3000 for the server
EXPOSE 3000

# Command to serve the app
CMD ["serve", "-s", "dist", "-l", "3000"]