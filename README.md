# Sistema Web: GestionFacultadesCarreras (Grupo 2)

##  Tema y Problema Asignado
- **Tema:** Una universidad necesita publicar su oferta académica.
- **Entidad Principal:** Facultad (`Nombre`, `Decano?`).
- **Entidad Dependiente:** Carrera (`Nombre`, `Codigo`, `DuracionSemestres`, `FacultadId`).
- **Roles:** Coordinador (Gestión) y Aspirante (Consulta).

##  Integrantes y Orden de Turnos
1. **Alfredo Lapo** (Fase 1: Proyecto base, Entidad Carrera, Scaffolding, Migración InitialCreate y Configuración del Repositorio).
2. **Nathaly Castillo** (Fase 2: Validaciones con DataAnnotations en español y migración).
3. **Robinson Romero** (Fase 3: Creación de la entidad principal Facultad y su CRUD).
4. **Daniel Quilca** (Fase 3: Relación uno a muchos 1:N y migración relacional).
5. **Nancy Defaz** (Fase 4 & 5: Listas desplegables, Identity, Roles, Usuarios de prueba y Seguridad con [Authorize]).

##  Pasos para ejecutar el proyecto
1. Clonar el repositorio desde GitHub.
2. Ajustar la cadena de conexión en `appsettings.json` apuntando a tu SQL Server local (`.\\SQLEXPRESS`).
3. Abrir la Consola del Administrador de paquetes NuGet y ejecutar:
   ```text
   Update-Database