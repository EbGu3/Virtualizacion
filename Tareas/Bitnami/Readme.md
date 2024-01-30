# Ejercicio usando bitami


## Descargar imagen con base de datos

  > docker run -it --name supabase-postgres bitnami/supabase-postgres                                                                            
  
## Configurar Postgres en Docker

  > docker run -d -it --name supabase-postgres -p 5432:5432 -e POSTGRES_PASSWORD=sa bitnami/supabase-postgres


## Entrar Postgres en docker

  > docker exec -it supabase-postgres bash

## Acceder a la base de datos de postgres

  > psql -U postgres

## Crear el Scaffold de la base de datos

  > Ejecutar en la terminal
    
    - dotnet tool install --global mlnet-win-x64
    - dotnet tool install --global dotnet-ef

  > Ejecutar comando en la terminal
    
    * dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=chat_plus;User Id=postgres;Password=sa" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models


-----------------------------------------------
Consumir Bitnami Wordpress

  1. Descargar imagen de wordpres
   
    > docker pull bitnami/wordpress:latest

  2. Crear una red para el wordpres

    > docker network create wordpress-network

  3. Crear volumne con MariaDB

    > docker volume create --name mariadb_data

    > docker run -d --name mariadb `
      --env ALLOW_EMPTY_PASSWORD=yes `
      --env MARIADB_USER=bn_wordpress `
      --env MARIADB_PASSWORD=bitnami `
      --env MARIADB_DATABASE=bitnami_wordpress `
      --network wordpress-network `
      --volume mariadb_data:/bitnami/mariadb `
      bitnami/mariadb:latest


  4. Ejecutar el contedor

    > docker volume create --name wordpress_data
    > docker run -d -it --name wordpress `
      -p 8080:8080 -p 8443:8443 `
      --env ALLOW_EMPTY_PASSWORD=yes `
      --env WORDPRESS_DATABASE_USER=bn_wordpress `
      --env WORDPRESS_DATABASE_PASSWORD=bitnami `
      --env WORDPRESS_DATABASE_NAME=bitnami_wordpress `
      --network wordpress-network `
      --volume wordpress_data:/bitnami/wordpress `
      bitnami/wordpress:latest

  5. Ver navegador

    > http://localhost:8080/
    
Consumir API C#

  1. Ir al directorio donde esta el proyecto y ejecutar el siguiente comando

    > docker build -t imagen-api-chat-plus .

    > docker run -d -it --network wordpress-network --name api-chat-plus -p 5202:5202 imagen-api-chat-plus



docker run -d -p 5202:5202 --name api-chat-plus imagen-api-chat-plus


