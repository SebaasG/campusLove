# CampusLove 💘

CampusLove es una aplicación de consola pensada para conectar estudiantes universitarios mediante perfiles, intereses comunes, interacciones y matches.

---

## 🧪 Cómo Probar la Base de Datos

1. Abre tu cliente de base de datos MySQL (como MySQL Workbench o DBeaver).
2. Ejecuta el script `Docs/Sql/bd.sql` para crear todas las tablas, procedimientos y triggers.
3. Luego, ejecuta el script `Docs/Sql/insertsPueba.sql` para insertar datos de ejemplo.

> ✅ **Recomendado**: Asegúrate de tener una base de datos vacía llamada `CSL` creada antes de ejecutar los scripts.

---

## 📁 Estructura del Proyecto

- `application/` – Lógica de presentación y controladores de UI.
- `domain/` – Entidades y lógica de negocio.
- `infrastructure/` – Implementación de persistencia y conexiones externas.
- `Docs/Diagrams/` – Diagramas de diseño del sistema.
- `Docs/Sql/` – Scripts SQL para la creación e inserción de datos.
- `Program.cs` – Punto de entrada de la aplicación.
- `README.md` – Este archivo.

---

## 🧰 Tecnologías y Librerías Usadas

- `.NET` (C#)
- `Spectre.Console` – Para una interfaz de consola visual y estilizada.
- `MailKit` – Para el envío de correos electrónicos.
- `MySQL` – Sistema de gestión de base de datos relacional.

---

## ⚙️ Funcionalidades Clave

- Registro y login de usuarios
- Creación y edición de perfiles
- Asociación de intereses por usuario
- Interacciones tipo *Like* y *Mensaje*
- Generación de *Matches* automáticos con triggers
- Estadísticas individuales de usuarios
- Créditos diarios y sistema de límite por día

---

## 💡 Notas Finales

Este proyecto está desarrollado bajo una arquitectura limpia, separando responsabilidades para facilitar mantenimiento y escalabilidad.

