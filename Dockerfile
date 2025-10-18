# ----------------------------------------------------
# ETAPA 1: CONSTRUCCIÓN (BUILD)
# ----------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copia TODAS las carpetas de proyectos y el .sln
# Esta copia aplanada asegura que todos los archivos fuente (incluidos .csproj, Program.cs, etc.)
# necesarios para RESTORE y PUBLISH estén en el contenedor antes de cualquier comando dotnet.

# Copia la carpeta del API/Web (que contiene el .sln y el .Web.csproj)
COPY BankAppTestBack/ ./BankAppTestBack/ 

# Copia las carpetas de los demás proyectos
COPY BankAppTestBack.Application/ ./BankAppTestBack.Application/
COPY BankAppTestBack.Domain/ ./BankAppTestBack.Domain/
COPY BankAppTestBack.Infrastructure/ ./BankAppTestBack.Infrastructure/
COPY XUnitTestBankAppTestBack/ ./XUnitTestBankAppTestBack/

# 2. Mover el .sln para que esté en el mismo nivel que las otras carpetas de proyecto
# Si el .sln en tu disco dice "../BankAppTestBack.Domain...", entonces el .sln debe estar 
# DENTRO de la carpeta BankAppTestBack.
# WORKDIR al lugar donde debe estar el .sln para que las referencias funcionen.
WORKDIR /src/BankAppTestBack

# 3. Restaura las dependencias
# Como estamos en el directorio que contiene el .sln, esto debería funcionar.
RUN dotnet restore *.sln

# 4. Publica la aplicación
# Usamos el nombre del proyecto API.
RUN dotnet publish BankAppTestBack.Web.csproj -c Release -o /app/publish --no-restore

# ----------------------------------------------------
# ETAPA 2: EJECUCIÓN (RUNTIME)
# ----------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080 

# Copia los archivos publicados.
# La ruta de origen es /app/publish desde el WORKDIR /src/BankAppTestBack
COPY --from=build /app/publish .

# Define el punto de entrada
ENTRYPOINT ["dotnet", "BankAppTestBack.Web.dll"]