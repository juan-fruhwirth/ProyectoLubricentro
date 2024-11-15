# Lubricentro 🚗🛠️

**Lubricentro** es una aplicación web diseñada para gestionar las operaciones diarias de un lubricentro, facilitando el registro y manejo de usuarios, vehículos, turnos y servicios de una manera eficiente y accesible.

## Índice
- [Características](#características)
- [Tecnologías](#tecnologías)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Instalación](#instalación)
- [Uso](#uso)
- [Contribuciones](#contribuciones)
- [Licencia](#licencia)

## Características ✨

- **Gestión de Usuarios**: Registro, inicio de sesión y manejo de permisos de usuario.
- **Control de Vehículos**: Registro de datos detallados del vehículo, incluyendo marca, modelo, tipo de combustible y observaciones.
- **Turnos Inteligentes**: Creación y administración de turnos, con estado actual y fecha asignada.
- **Servicios Personalizables**: Registro de servicios con descripción, precios y tiempo estimado.
- **Validación y Seguridad**: Protección de datos con cifrado de contraseñas y confirmación de correo electrónico.
- **Diseño Intuitivo**: Interfaz de usuario simplificada para facilitar el flujo de trabajo del personal del lubricentro.

## Tecnologías 💻

- **Back-end**: ASP.NET Framework (C#)
- **Base de Datos**: SQL Server
- **Front-end**: HTML5, CSS3, JavaScript, Bootstrap
- **IDE**: Visual Studio
- **Control de Versiones**: Git

## Estructura del Proyecto 📁

Este proyecto contiene dos carpetas principales:

- **Lubricentro**: Carpeta que contiene las páginas **.aspx** y sus archivos code-behind (por ejemplo, `vehiculos.aspx` y `vehiculos.aspx.cs`). Aquí se encuentra la lógica de presentación y la interacción directa con los usuarios.
  
- **biz**: Biblioteca de clases (class library) que define las entidades y lógica de negocio. Incluye clases como `Vehiculo.cs`, `Usuario.cs`, entre otras. Estas clases encapsulan las propiedades y métodos que representan los datos y acciones principales de la aplicación.

## Instalación 🚀

1. **Clonar el Repositorio**
   ```bash
   git clone https://github.com/Joaquin-Flores/Lubricentro.git
   ```
2. **Configurar la Base de Datos**
   - Crear una base de datos en SQL Server llamada Lubricentro.
   - Ejecutar los scripts SQL del proyecto en la carpeta Database/ para crear las tablas y relaciones necesarias.
3. **Configurar la Cadena de Conexión**
   - Actualizar el archivo web.config con la cadena de conexión de SQL Server.
4. **Iniciar el Proyecto**
   - Abrir el proyecto en Visual Studio y ejecutar en modo de desarrollo.

  ## Uso 📖

1. **Registro y Autenticación de Usuario**
   - Accede a la página de registro e ingresa la información solicitada para crear una cuenta de usuario.
   - Confirma el correo electrónico para activar la cuenta.

2. **Gestión de Turnos**
   - Los usuarios pueden crear turnos asociados a vehículos registrados, asignar servicios y establecer la fecha deseada.
   - Visualización del estado de cada turno y posibilidad de cancelarlo.

3. **Administración de Vehículos y Servicios**
   - Registra nuevos vehículos y asígnales un propietario.
   - Añade, edita y elimina servicios disponibles en el lubricentro.

## Contribuciones 🤝

¡Las contribuciones son bienvenidas! Si tienes ideas para mejorar el sistema o corregir errores, por favor sigue estos pasos:
1. Realiza un fork del proyecto.
2. Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y confirma los commits (`git commit -m 'Agrego nueva funcionalidad'`).
4. Sube tus cambios (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request.

## Licencia 📄

Este proyecto está bajo la Licencia MIT.

---

**Lubricentro** es un proyecto que busca practicar y demostrar buenas prácticas del lenguaje y marco de trabajo, proporcionando una experiencia fluida, intuitiva y eficiente. ¡Gracias por tu interés en el proyecto!
