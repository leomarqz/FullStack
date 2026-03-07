# 📝 TechNotes

Una aplicación web moderna y profesional para la creación y gestión de notas. Desarrollada con **.NET** y **Blazor**, y estructurada rigurosamente bajo los principios de **Clean Architecture** para garantizar escalabilidad, mantenibilidad y un código limpio.

> 🚧 **Estado del Proyecto:** En desarrollo (Work in Progress). Se están construyendo e iterando las vistas principales y la lógica de negocio.

## 🏗️ Arquitectura del Proyecto

La solución (`FullStack.slnx`) sigue una separación de responsabilidades estricta, dividida en los siguientes proyectos:

* **`TechNotes` (Presentation):** La interfaz de usuario desarrollada con Blazor. Cuenta con un diseño responsivo, limpio y estilo SaaS, utilizando Bootstrap 5 personalizado.
* **`TechNotes.Application`:** Contiene los casos de uso del sistema, las interfaces de los repositorios y la lógica de negocio principal.
* **`TechNotes.Domain`:** El núcleo del sistema. Aquí residen las entidades de dominio (como `Note`), los objetos de valor y las excepciones propias del negocio, sin dependencias externas.
* **`TechNotes.Infrastructure`:** La capa de implementación técnica. Se encarga del acceso a datos (Entity Framework), la comunicación con la base de datos y otras integraciones externas.

## 🚀 Tecnologías y Herramientas

* **Backend:** C# / .NET
* **Frontend:** Blazor Web App (UI UI/UX con Bootstrap 5)
* **Arquitectura:** Clean Architecture
* **Base de Datos:** SQL (Estructura inicial en `queries.sql`)
* **Infraestructura:** Docker (Configurado vía `docker-compose.yaml`)

## 🛠️ Requisitos Previos

Para ejecutar este proyecto en tu entorno local, necesitarás tener instalado:

* [.NET SDK](https://dotnet.microsoft.com/download) (Versión .Net 8)
* [Docker Desktop](https://www.docker.com/products/docker-desktop) (Para levantar los servicios de infraestructura)
* Visual Studio, JetBrains Rider o VS Code.

## ⚙️ Instalación y Ejecución

1. **Clonar el repositorio:**
   ```bash
   git clone [https://github.com/leomarqz/FullStack.git](https://github.com/leomarqz/FullStack.git)
   cd FullStack
