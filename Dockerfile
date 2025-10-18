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
WORKDIR /src/BankAppTestBack

RUN dotnet restore *.sln

RUN dotnet publish BankAppTestBack.Web.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080 

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BankAppTestBack.Web.dll"]