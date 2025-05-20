
---

```markdown
# CampusLove 💘

CampusLove es una aplicación de consola pensada para conectar estudiantes universitarios mediante perfiles, intereses comunes, interacciones y matches.


## 🧪 Cómo Probar la Base de Datos

1. Abre tu cliente de base de datos MySQL (como MySQL Workbench o DBeaver).
2. Ejecuta el script `Docs/Sql/bd.sql` para crear todas las tablas, procedimientos y triggers.
3. Luego, ejecuta el script `Docs/Sql/insertsPueba.sql` para insertar datos de ejemplo.

> ✅ Recomendado: Asegúrate de tener una base de datos vacía llamada `CSL` creada antes de ejecutar los scripts.

## 📌 Notas Adicionales

- El proyecto está basado en arquitectura limpia y está organizado por capas: `application`, `domain`, `infrastructure`.
- Utiliza `Spectre.Console` para mejorar la experiencia en consola (interfaz más visual).
- Utiliza `MailKit` para el envio de correos electronicos.
- El sistema soporta funcionalidades como registro, login, gestión de perfiles, interacciones tipo *like* y *mensaje*, y generación automática de *matches* mediante triggers.



---


