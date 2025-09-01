# Build stage
FROM node:24 AS build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --configuration production

# Debug: list dist contents
RUN echo "DEBUG: contents of /app/dist" && ls -R /app/dist
RUN echo "DEBUG: contents of /app/dist/client" && ls -l /app/dist/client

	

# Runtime stage
FROM nginx:alpine
COPY --from=build /app/dist/client /usr/share/nginx/html
RUN echo "DEBUG: contents of /usr/share/nginx/html" && ls -R /usr/share/nginx/html
EXPOSE 80
