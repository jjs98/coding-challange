FROM node:latest as build
WORKDIR /src/client
COPY ./ /src/client/
RUN npm install
RUN npm run build

FROM build as final
# Serve app with nginx server
FROM nginx:latest
COPY --from=build /src/client/dist/articlestore /usr/share/nginx/html
EXPOSE 80