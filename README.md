# Tienda Online — Parcial 1

Sistema de gestión de una tienda online desarrollado en **ASP.NET Web Forms** con
**SQL Server**, según la consigna del Parcial 1 de la materia.

---

## Requisitos

| Componente | Versión |
|---|---|
| Visual Studio | 2022 o superior (Community) |
| .NET Framework | 4.7.2 (Developer Pack) |
| SQL Server | 2019 o superior (Developer/Express) |
| Windows Authentication | habilitada en la instancia |

---

## Cómo ejecutarlo

### 1. Crear la base de datos

Abrir **SQL Server Management Studio** y ejecutar el script completo
[`database/creacion.sql`](database/creacion.sql):

```sql
CREATE DATABASE TiendaOnline;
USE TiendaOnline;
-- ... crea las tablas y carga los datos de ejemplo
```

El script crea:

- `categorias` — 5 registros
- `productos` — 6 registros (cada uno asociado a una categoría)
- la clave foránea `FK_productos_categorias` (sin `ON DELETE CASCADE`)

### 2. Abrir la solución

Abrir `Tienda Online.slnx` en Visual Studio. NuGet restaura las dependencias
automáticamente en la primera compilación.

### 3. Compilar y ejecutar

`F5` — IIS Express levanta el proyecto en `http://localhost:<puerto>`.

La cadena de conexión apunta a `localhost` con **Windows Authentication**.
Si tu instancia tiene otro nombre, modificá el atributo
`connectionString` de `TiendaOnlineDB` en
[`Tienda Online/Web.config`](Tienda%20Online/Web.config).

---

## Estructura

```
Tienda Online.slnx                  Solución
Tienda Online/
├── Site.Master                     Maestra: navegación y estilos
├── Default.aspx                    Portada con accesos a los 4 formularios
├── Alta.aspx                        Alta de producto (INSERT)
├── Consulta.aspx                    Listado con JOIN (SELECT)
├── Modificacion.aspx                Modificación de producto (UPDATE)
├── Baja.aspx                        Baja de producto (DELETE)
├── Utilidades.cs                    Parseo de precios y conexión ADO.NET
├── Content/Site.css                 Hoja de estilos externa
├── database/creacion.sql            Script de creación de la BD
└── Web.config                       Cadena de conexión y configuración
```

---

## Consignas cubiertas

- **Tablas relacionadas** — `categorias` (1) ↔ (N) `productos`, con clave foránea.
- **4 formularios Web Forms** — alta, consulta, modificación y baja, cada uno en
  su propio `.aspx` con su *code-behind*.
- **Navegación con `HyperLink`** — la portada enlaza a los cuatro formularios.
- **`SqlConnection` y `SqlCommand`** — alta, modificación y baja ejecutan sus
  sentencias con ADO.NET explícito (`Utilidades.AbrirConexion()`), parámetros
  `@nombre`, `@precio`, `@idCategoria` y `@idProducto`, y `using` para el
  cierre de la conexión. Ninguna concatenación de datos del usuario en el SQL.
- **`SqlDataSource` con `INNER JOIN`** — la consulta muestra la descripción de
  la categoría junto a cada producto.
- **Más de 5 registros** — 5 categorías y 6 productos de ejemplo.
- **Hoja de estilos externa** — `Content/Site.css`, cargada después de
  Bootstrap.
- **Validación** — `RequiredFieldValidator` y `RegularExpressionValidator` en
  todos los formularios, con mensajes en español.
- **Verificación de cambios** — la modificación no ejecuta el `UPDATE` si el
  usuario no modificó ningún campo.

---

## Notas técnicas

- Los precios usan `DECIMAL(10,2)` y se parsean aceptando coma **o** punto como
  separador decimal (`Utilidades.TryParsePrecio`).
- El parámetro de precio se declara con `SqlDbType.Decimal`, `Precision = 10` y
  `Scale = 2`, y recibe el `decimal` de C# directamente — así no interviene la
  cultura del servidor y no se truncan los decimales.
- Las páginas `.aspx` se guardan con **BOM UTF-8** para que los acentos se
  rendericen correctamente.
- El modo de redirección de FriendlyUrls está en `RedirectMode.Off` para que
  los `POST` de los formularios no se conviertan en `GET`.
- La FK `FK_productos_categorias` **no** tiene `ON DELETE CASCADE`, de modo que
  no se puede borrar una categoría que tenga productos asociados.
