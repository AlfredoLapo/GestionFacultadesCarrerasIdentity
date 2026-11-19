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

   ## Seguridad y control de acceso (Commit 4)

El sistema utiliza ASP.NET Core Identity para gestionar la autenticación de usuarios y la autorización mediante roles.

### Roles implementados

**Coordinador:** puede consultar, crear, editar y eliminar carreras.

**Aspirante:** puede consultar el listado de carreras y visualizar sus detalles, pero no puede crear, editar ni eliminar registros.

Los visitantes sin iniciar sesión pueden consultar el listado y los detalles de las carreras.

### Usuarios de prueba

| Usuario | Rol |
|---|---|
| coordinador@example.com | Coordinador |
| aspirante@example.com | Aspirante |

**Contraseña:** Ambos usuarios se crean utilizando la contraseña configurada localmente en `SeedUsers:Password` mediante User Secrets.

Por seguridad, la contraseña no se incluye en el repositorio de GitHub.

### Configuración de la contraseña de prueba

En Visual Studio:

1. Hacer clic derecho sobre el proyecto.
2. Seleccionar **Administrar secretos de usuario**.
3. Configurar el archivo `secrets.json` con la siguiente estructura:

```json
{
  "SeedUsers": {
    "Password": "CONFIGURAR_CONTRASEÑA_LOCAL"
  }
}
```

Reemplazar `CONFIGURAR_CONTRASEÑA_LOCAL` por una contraseña que cumpla con los requisitos de ASP.NET Core Identity antes de ejecutar el proyecto por primera vez.

### Restricciones de seguridad

En `CarrerasController.cs` se implementaron los atributos:

- `[AllowAnonymous]` para las acciones `Index` y `Details`.
- `[Authorize(Roles = "Coordinador")]` para las acciones GET y POST de `Create`, `Edit` y `Delete`.
- `[ValidateAntiForgeryToken]` en las acciones POST para proteger los formularios.

En las vistas `Index.cshtml` y `Details.cshtml` se utiliza `User.IsInRole("Coordinador")` para mostrar los botones de modificación únicamente a los usuarios autorizados.

### Pruebas realizadas

- Inicio de sesión correcto con el usuario Coordinador.
- Visualización de los botones Crear, Editar y Eliminar para Coordinador.
- Inicio de sesión correcto con el usuario Aspirante.
- Ocultamiento de los botones de modificación para Aspirante.
- Acceso denegado al intentar abrir `/Carreras/Create` con el rol Aspirante.
- Compilación del proyecto sin errores.

Estas pruebas verifican el funcionamiento de la autenticación, la visibilidad de los botones según el rol y la restricción de acceso a la creación de carreras.

### Seguridad de Facultades

También se implementó autorización basada en roles en `FacultadesController.cs`.

- **Coordinador:** puede consultar, crear, editar y eliminar facultades.
- **Aspirante:** puede consultar el listado y los detalles de las facultades.
- **Visitantes:** pueden consultar el listado y los detalles sin iniciar sesión.

Se aplicó `[AllowAnonymous]` en `Index` y `Details`, y `[Authorize(Roles = "Coordinador")]` en las acciones GET y POST de `Create`, `Edit` y `Delete`.

En las vistas `Views/Facultades/Index.cshtml` y `Details.cshtml`, los botones de modificación se muestran únicamente cuando `User.IsInRole("Coordinador")` es verdadero.

**Pruebas realizadas:**
- Compilación del proyecto sin errores.
- El Aspirante puede visualizar el listado de facultades.
- Los botones Crear, Editar y Eliminar permanecen ocultos para el Aspirante.
- El acceso directo a `/Facultades/Create` con el rol Aspirante muestra `Access denied`.