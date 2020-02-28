FROM nginx:latest

COPY nginx.conf /etc/nginx/nginx.conf

COPY MyMovies.crt /etc/ssl/certs/MyMovies.crt
COPY MyMovies.key /etc/ssl/private/MyMovies.key