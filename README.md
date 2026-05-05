# 🏟️ Sistema de Gestin de Canchas - Proyecto Final (La Practica 3)

## 📝 1. Descripción del Proyecto
Este sistema es una solución integral desarrollada para la administración de escenarios deportivos. Permite gestionar el ciclo completo de una reserva: desde la disponibilidad de la cancha hasta el registro del cliente y la confirmación del pago, garantizando la integración total entre el backend (WebAPI) y el frontend (Blazor).

## 👥 2. Integrantes del Equipo
* Yeferxon Alejandro Márquez Araque
* Miguel Ángel Peña García
* Dylan Jeronimo Arias Ruiz
* [Nombre de Compañero 2]

## 🏗️ 3. Arquitectura y Tecnologías
Siguiendo los requerimientos técnicos de la práctica, el proyecto se divide en:
* **Backend:** ASP.NET Core .NET 10 WebAPI.
* **Frontend:** Blazor WebAssembly con Componentes Razor reutilizables.
* **Shared:** Proyecto de entidades y modelos compartidos.
* **Persistencia:** Entity Framework Core con Migraciones aplicadas.

## 📊 4. Entidades Principales y Relaciones
El dominio del negocio se basa en las siguientes entidades definidas en la capa Shared:
* **Canchas:** Representa los escenarios deportivos (Fútbol, Tenis, etc.).
* **Clientes:** Registro de los usuarios que realizan apartados.
* **Reservas:** Entidad central que coordina la fecha y horas.
* **Pagos:** Registro financiero vinculado a la reserva.

**Relaciones implementadas:**
* **Canchas (1:N) Reservas:** Una cancha puede tener múltiples reservas asociadas.
* **Clientes (1:N) Reservas:** Un cliente puede gestionar diversas reservas.
* **Reservas (1:1) Pagos:** Cada reserva tiene un registro único de pago para validación.

## 🔐 5. Implementación de SeedDb
Se implementó un proceso de carga inicial que cumple con los requerimientos de la Actividad 3:
* **Entidades cargadas:** Se inicializan datos reales para **Canchas** y **Clientes**.
* **Usuario Administrador Inicial:** Se crea automáticamente un usuario para pruebas.
  * **Correo:** `admin@yopmail.com`
  * **Contraseña:** `123456`
* **Validación:** El SeedDb evita la duplicación de registros en cada ejecución.

## 🔌 6. Endpoints Principales (WebAPI)
El API REST expone las siguientes operaciones CRUD probadas en Swagger:
* `GET /api/canchas` - Listar escenarios.
* `POST /api/reservas` - Crear nueva reserva con validaciones.
* `PUT /api/reservas/{id}` - Editar estado de reserva.
* `DELETE /api/reservas/{id}` - Cancelar/Eliminar registro.

## 🎥 7. Video Demostrativo
De acuerdo con el punto 8 de la práctica, aquí se encuentra la evidencia del funcionamiento:
* [Link al video de YouTube/Drive aquí][cite: 1]

> **Nota:** El video incluye la explicación de la arquitectura, evidencia de migraciones y la demostración del CRUD funcional desde Blazor junto al SeedDb.