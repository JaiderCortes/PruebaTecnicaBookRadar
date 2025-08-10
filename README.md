# 📖 Sistema de búsqueda de libros - API OpenLibrary 📖

Proyecto creado como parte del proceso de selección para el puesto de **Desarrollador Semi Senior**.

## Estructura de base de datos
Para la generación de la base de datos y la tabla necesaria para la ejecución del proyecto, dentro de la solución se encuentra un archivo llamado `SQLCreateDB.sql`. Dentro de este archivo se encuentran los siguientes scripts:
* Creación del login.
* Creación de la base de datos.
* Creación del usuario de la base de datos con el login y asignación de rol.
* Creación de la tabla de historial de búsquedas.

## Cadena de conexión a la base de datos
Dentro del proyecto, en el archivo llamado `appsettings.json`, en la linea N° 10 se encuentra la definición de la cadena de conexión a la base de datos con las credenciales configuradas según los scripts de creación de la misma. Esta línea se puede modificar según el servidor donde se ejecute el script, en este caso, queda por defecto con servidor _(local)_.

### ⚠️ Importante ⚠️
Antes de iniciar el proyecto, se debe ejecutar el script de la base de datos; ya que si no se realiza esto primero, se generarán errores de compilación al intentar conectar a una base de datos inexistente en el _ApplicationDbContext_.

Llegados a este punto, ya se puede ejecutar el proyecto y no debería generar ningún problema. Este ya viene con todos los paquetes y demás librerías necesarias para su correcta ejecución.

## Diseño de la interfaz
El proyecto está diseñado al 100% con Bootstrap para una interfaz más agradable a la vista. Al ejecutar el proyecto, se lanza en una vista inicial generada desde el controlador _Home_ que contiene la interfaz "Búsqueda de libros por autor". Cuenta con un menú superior con las opciones:
* Buscar libros (vista actual)
* Historial de búsquedas

### Buscar libros
Debajo del título principal, se encuentra el formulario de búsqueda de libros con un _input_ para ingresar el nombre de autor y un _button_ "Buscar" para ejecutar la llamada a la API y retornar los libros encontrados. Si se oprime el botón de "Buscar" sin haber ingresado un nombre de autor, se genera un mensaje de advertencia indicando que se debe ingresar un autor para la búsqueda, esto como parte de las validaciones de entrada de datos, como se evidencia en la imagen:

<img width="474" height="67" alt="image" src="https://github.com/user-attachments/assets/2023fa58-d233-4e1a-8a9b-dc47b6f4114f" />

A pesar de esto y para más seguridad, se aplicó una validación adicional en el controlador.

Esta alerta se aplica con una validación desde JavaScript utilizando una librería llamada [Flashy.js](https://flashyjs.pablomsj.com/), es de código abierto, por ende no se hace referencia a un paquete externo sino que se descargó el código fuente completo de la librería para implementarlo en el proyecto.

Al oprimir el botón "Buscar" con un nombre de autor se realiza el consumo del API en el controlador, donde se crea una lista de objetos tipo `LibroViewModel` que contiene todas las propiedades necesarias para el retorno de los datos, por cada libro encontrado. Inmediatamente se obtiene la lista, se almacenan los resultados en la base de datos como parte de la persistencia de datos en la tabla `HistorialBusquedas` complementando la información con las propiedades del objeto `HistorialBusqueda`. Después de esto, se retorna la lista de libros encontrados a la vista, en la que tenemos la implementación de modelo fuertemente tipado (`@model`). Esta se carga en una _table_ de HTML con código incrustado de _razor_ para recorrer y pintar la lista de libros devuelta. Así mismo, sobre la tabla generada, se escribe un texto con la cantidad de libros encontrados.

### Historial de búsquedas
En esta vista, encontramos una _table_ de HTML donde obtiene desde el controlador la lista de items del historial; consultando a la base de datos por medio del _ApplicationDbContext_ y creando un objeto de tipo `HistorialBusqueda` por cada registro de la base de datos. Al final se retorna a la vista la lista obtenida y con el modelo fuertemente tipado (`@model`) para pintar los resultados en una _table_ de HTML. En esta vista, implementé jQuery para aplicar funcionalidades interactivas a la tabla, específicamente:
* Opción de buscar dentro de la tabla.
* Paginado.
* Ordenamiento de los datos.

jQuery ya viene por defecto al crear un proyecto MVC con .NET Core, así que solo fue necesario hacer referencia a la funcionalidad de DataTables junto con sus respectivos scripts y estilos.

## Mejoras pendientes
En este punto el proyecto ya es funcional según lo solicitado, sin embargo, considero que se pueden aplicar las siguientes mejoras para conseguir una solución más óptima y escalable:
* Usar jQuery junto con AJAX para el tema de las validaciones de formularios, para controlar la entrada del usuario, implementaciones adicionales de UI/UX, entre otras opciones.
* Usar jQuery para pintar los datos en las tablas, para no usar tanto código Razor embebido y hacer las tablas más personalizables.
* Con jQuery DataTables se podrían implementar botones para exportar los datos de las tablas en formatos  `.xlsx` o `.pdf`.
* Usar _Font Awesome_ para agregar íconos, para un diseño más atractivo.
* Implementar control de errores y otros tipos de mensajes con _SweetAlert_.
* Implementar pantalla de carga o "rueda" de carga al oprimir los botones con funcionalidad, como el de "Buscar".

## ¡Eso es todo! ✔️
Espero que el proyecto sea de su agrado y que cumpla en su totalidad con las especificaciones indicadas, y que de esta forma, pueda continuar en el proceso de selección.
Muchas gracias.
