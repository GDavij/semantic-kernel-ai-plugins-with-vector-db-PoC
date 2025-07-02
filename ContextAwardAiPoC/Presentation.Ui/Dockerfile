# Build stage
FROM node:22-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

# Production stage
FROM nginxinc/nginx-unprivileged:1.27.5-alpine AS runtime
COPY --from=build /app/dist /usr/share/nginx/html

RUN rm /etc/nginx/conf.d/default.conf
COPY ./nginx/default.conf /etc/nginx/conf.d/default.conf

USER nginx

# Expose Port to expose from container
EXPOSE 3000

# Define Entrypoint command
ENTRYPOINT [ "nginx", "-g", "daemon off;" ]
