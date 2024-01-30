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


  

