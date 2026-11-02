XelaTech Soporte
Sistema de gestión de servicios técnicos | Aplicación de consola en C#
  
Descripción
XelaTech Soporte es una aplicación de consola desarrollada en C# para organizar el proceso de atención de un centro de servicio técnico de equipos informáticos. Centraliza el registro de clientes, técnicos, equipos, solicitudes y repuestos, y permite administrar el trabajo desde la recepción del equipo hasta la entrega del servicio.

El proyecto aplica programación orientada a objetos, estructuras de datos y algoritmos de búsqueda y ordenamiento implementados en el código del sistema. Fue desarrollado como proyecto académico del curso Programación Avanzada.
Funcionalidades principales
Módulo	Funciones
Clientes	Registrar, consultar, actualizar y revisar solicitudes asociadas a un cliente.
Técnicos	Registrar técnicos, consultar especialidades, cambiar disponibilidad y visualizar carga de trabajo.
Equipos	Registrar portátiles, equipos de escritorio e impresoras; consultar por serie e historial de servicios.
Solicitudes	Crear solicitudes, consultar detalles, registrar diagnósticos y costos, y gestionar estados.
Repuestos	Registrar inventario, actualizar existencias y establecer niveles mínimos con alertas.

Colas de atención	Administrar solicitudes urgentes y normales, priorizando urgentes y respetando el orden de llegada.
Ordenamientos	Ordenar solicitudes, técnicos y repuestos mediante algoritmos implementados manualmente.
Búsquedas	Localizar clientes, equipos y solicitudes con búsqueda secuencial y binaria.
Reportes	Consultar actividad, cargas de trabajo, inventario, servicios e ingresos.
Datos de prueba	Generar registros de ejemplo utilizando Random.

Tecnologías y conceptos aplicados
- C# y .NET 10: ejecución como aplicación de consola.
- Programación orientada a objetos: clases, objetos, encapsulamiento, herencia, polimorfismo y sobrecarga de constructores.
- Listas (List<T>): almacenamiento dinámico de los registros de clientes, técnicos, equipos, solicitudes y repuestos.
- Colas (Queue<T>): atención de solicitudes normales y urgentes mediante el principio FIFO.
- Pilas (Stack<T>): historial de estados anteriores de cada solicitud.
- Arreglos y matrices: estadísticas y representación de la carga semanal de técnicos.
- Random: generación de datos de prueba.
- Validación de entradas: control de duplicados, cantidades y relaciones entre registros, entre otras comprobaciones.


Algoritmos de ordenamiento
Algoritmo	Criterio	Orden
Bubble Sort	Costo total de solicitudes	Ascendente
Selection Sort	Servicios finalizados por técnico	Descendente
Shell Sort	Existencia de repuestos	Ascendente
Quick Sort	Fecha de recepción de solicitudes	Ascendente


Algoritmos de búsqueda
- Secuencial: clientes por nombre parcial, equipos por marca y solicitudes por estado.
- Binaria: solicitudes por código, con preparación de los datos para realizar las comparaciones correspondientes.
Estructura del proyecto
XelaTech/
├── Modelos/
│   └── Entidades.cs
├── Servicios/
│   ├── Algoritmos.cs
│   └── Datos.cs
├── Program.cs
├── XelaTech.csproj
└── README.md
Responsabilidad de cada archivo:
- Program.cs: punto de entrada, menús, interacción con el usuario y operaciones del sistema.
- Modelos/Entidades.cs: modelos de clientes, técnicos, equipos, repuestos y solicitudes.
- Servicios/Datos.cs: colecciones, colas, referencias a los registros y acceso a datos en memoria.
- Servicios/Algoritmos.cs: implementación de los métodos de ordenamiento y búsqueda utilizados.
- XelaTech.csproj: configuración del proyecto y versión de .NET.
Requisitos
- SDK de .NET 10 instalado.
- Visual Studio 2026 compatible con .NET 10, Visual Studio Code con herramientas de C#, o una terminal con dotnet.
- Windows, macOS o Linux con soporte para .NET 10.
No se necesita una base de datos ni una interfaz gráfica.
Instalación y ejecución

Opción 1: Visual Studio
1. Descarga o clona el repositorio.
2. Abre la carpeta del proyecto en Visual Studio, o abre el archivo XelaTech.csproj.
3. Comprueba que el SDK de .NET 10 esté instalado.
4. Compila el proyecto y ejecútalo desde Visual Studio.
Opción 2: Terminal
Ubícate en la carpeta que contiene XelaTech.csproj y ejecuta:
dotnet restore
dotnet build
dotnet run
Uso del sistema
Al iniciar, el programa presenta un menú principal con los módulos disponibles:

==== XELATECH SOPORTE ====
==========================

1. Clientes
2. Tecnicos
3. Equipos
4. Solicitudes
5. Repuestos
6. Atencion de colas
7. Ordenamientos
8. Busquedas
9. Reportes
10. Generar datos de prueba
0. Volver / salir
===========================


Flujo básico sugerido
2. Registrar un cliente con sus datos de contacto.
3. Registrar uno o más técnicos y sus especialidades.
4. Registrar el equipo vinculado a su propietario.
5. Crear una solicitud y elegir su prioridad.
6. Atender la cola para asignar el servicio a un técnico compatible y disponible.
7. Registrar el diagnóstico y la reparación, incluidos los repuestos y costos cuando correspondan.
8. Actualizar el estado de la solicitud hasta finalizar la atención.
9. Consultar reportes y búsquedas para revisar la información generada.
Para explorar las funciones con mayor rapidez, utiliza Generar datos de prueba desde el menú principal.
Navegación de la consola

La versión 2.4.1 incluye limpieza de pantalla mediante Console.Clear() y pausas con Console.ReadKey(true). Los menús se presentan de manera separada para facilitar la lectura y permitir revisar los resultados antes de continuar.
Reportes disponibles
El programa incluye consultas de:
- Colas de solicitudes por prioridad.
- Solicitudes agrupadas por estado.
- Disponibilidad y ocupación de técnicos.
- Técnico con más servicios finalizados.
- Equipos atendidos por tipo.
- Solicitudes ordenadas por costo y fecha.
- Repuestos ordenados por existencia y alertas de stock bajo.
- Ingresos asociados a servicios entregados.
- Calificaciones y carga semanal de trabajo.
- Clientes con solicitudes activas.
- Solicitudes asignadas a un técnico.
- Resumen general de la operación.


Los resultados se calculan con la información disponible durante la ejecución.
Alcance y limitaciones
- La aplicación funciona en memoria: los datos registrados no se conservan automáticamente después de cerrar el programa.
- No dispone de autenticación de usuarios, base de datos ni interfaz gráfica.
- Los datos generados automáticamente son de carácter demostrativo.
- El sistema está planteado con fines académicos, para aplicar y demostrar conceptos de programación en un caso de servicio técnico.
Estado del proyecto

Versión documentada: 2.4.1
Tipo: aplicación de consola
Framework objetivo: .NET 10
