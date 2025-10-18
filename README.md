# Backend dev technical test solution

Bank app test application

Postman: https://lively-shuttle-827504.postman.co/workspace/Tracking~8b8479cf-b785-422d-80d1-e085cc19ec9e/collection/349031-5fee2b71-7c3e-4d33-8ac6-b4d8dd6e9548?action=share&creator=349031

Postman json file: bank-app-test-back.postman_collection.json (ubicado en la raiz de este proyecto)

## Features

- .Net Core 8
- DDD Architecture
- MediatR pattern
- Open Api
- SQL Server
- xUnit

## Instructions

Este documento detalla los pasos para levantar la aplicación completa (Frontend, Backend y Base de Datos) utilizando Docker Compose

---

## 1. Requisitos Previos

Se debe tener instalados los siguientes componentes:

- Git  
- Docker Desktop (o Docker Engine)

---

## 2. Configuración de la Estructura de Repositorios

Para que el archivo `docker-compose.yml` (ubicado en este repositorio: `BankAppTestBack`) pueda acceder a ambos proyectos, hay que clonarlos y colocarlos a la misma altura en una carpeta padre.

**Pasos:**

1. Crea una carpeta de trabajo (ej: `ProyectoBancario`).
2. Clonar ambos repositorios dentro de esa carpeta.
---

## 3. Puesta en Marcha de la Aplicación

El proceso de inicio debe ejecutarse desde la carpeta que contiene el archivo `docker-compose.yml` (el repositorio del Backend).

```bash
cd /path/a/ProyectoBancario/BankAppTestBack
docker compose up --build -d```

## 4. Acceso y Verificación de Servicios

Una vez que Docker Compose termine de inicializar todos los servicios:

| Servicio              | URL de Acceso Local                                 | Puerto Mapeado | Notas                                                  |
|-----------------------|-----------------------------------------------------|----------------|--------------------------------------------------------|
| Aplicación Frontend   | [http://localhost:3000](http://localhost:3000)      | 3000:80        | La página de inicio                                    |

| API Backend (Swagger) | [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html) | 8080:8080      | Documentación para probar los endpoints de la API     |

