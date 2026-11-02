using XelaTech.Modelos;
using XelaTech.Servicios;

class Program
{
    static readonly Datos db = new();
    static readonly Random azar = new();
    static int Entero(string mensaje, int minimo = 0)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (int.TryParse(Console.ReadLine(), out int n) && n >= minimo) return n;
            Console.WriteLine($"Ingrese un numero entero mayor o igual a {minimo}.");
        }
    }
    static string Correo(string mensaje)
    {
        while (true)
        {
            string correo = Texto(mensaje);
            int arroba = correo.IndexOf('@');
            if (arroba > 0 && arroba < correo.Length - 3 && correo.IndexOf('.', arroba) > arroba + 1) return correo;
            Console.WriteLine("Correo no valido.");
        }
    }
    static decimal Dinero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            if (decimal.TryParse(Console.ReadLine(), out decimal n) && n >= 0) return n;
            Console.WriteLine("Ingrese un monto valido no negativo.");
        }
    }
    static string Texto(string mensaje)
    {
        string? entrada;
        do { Console.Write(mensaje); entrada = Console.ReadLine()?.Trim(); }
        while (string.IsNullOrWhiteSpace(entrada));
        return entrada;
    }
    static int Opcion(string titulo, params string[] opciones)
    {
        Console.WriteLine($"\n=== {titulo} ===");
        for (int i = 0; i < opciones.Length; i++) Console.WriteLine($"{i + 1}. {opciones[i]}");
        Console.WriteLine("0. Volver / salir");
        int o = Entero("Opcion: ");
        return o <= opciones.Length ? o : -1;
    }
    static bool ExisteCodigo<T>(List<T> lista, int codigo, Func<T, int> leer)
    {
        foreach (var item in lista) if (leer(item) == codigo) return true;
        return false;
    }
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            int o = Opcion("XELATECH SOPORTE", "Clientes", "Tecnicos", "Equipos", "Solicitudes", "Repuestos", "Atencion de colas", "Ordenamientos", "Busquedas", "Reportes", "Generar datos de prueba");
            switch (o)
            {
                case 0: return;
                case 1: MenuClientes(); break;
                case 2: MenuTecnicos(); break;
                case 3: MenuEquipos(); break;
                case 4: MenuSolicitudes(); break;
                case 5: MenuRepuestos(); break;
                case 6: AtenderColas(); break;
                case 7: MenuOrdenar(); break;
                case 8: MenuBuscar(); break;
                case 9: MenuReportes(); break;
                case 10: GenerarPruebas(); break;
                default: Console.WriteLine("Opcion incorrecta."); break;
            }
        }
    }
    static void MenuClientes()
    {
        while (true)
        {
            int o = Opcion("CLIENTES", "Registrar", "Consultar todos", "Actualizar", "Ver solicitudes de un cliente");
            if (o == 0) return;
            if (o == 1)
            {
                int id = Entero("Codigo: ", 1);
                if (db.Cliente(id) != null) { Console.WriteLine("Codigo duplicado."); continue; }
                db.Clientes.Add(new Cliente { Codigo = id, Nombre = Texto("Nombre: "), Telefono = Texto("Telefono: "), Correo = Correo("Correo: "), Tipo = Texto("Tipo: ") });
                Console.WriteLine("Cliente registrado.");
            }
            else if (o == 2) foreach (var c in db.Clientes) Console.WriteLine($"{c.Codigo} | {c.Nombre} | {c.Telefono} | {c.Correo} | {c.Tipo} | Solicitudes: {c.CantidadSolicitudes}");
            else if (o == 3)
            {
                var c = db.Cliente(Entero("Codigo: ", 1));
                if (c == null) { Console.WriteLine("No existe."); continue; }
                c.Nombre = Texto("Nuevo nombre: "); c.Telefono = Texto("Nuevo telefono: "); c.Correo = Correo("Nuevo correo: "); c.Tipo = Texto("Nuevo tipo: ");
                Console.WriteLine("Actualizado.");
            }
            else if (o == 4)
            {
                int codigo = Entero("Codigo del cliente: ", 1);
                var cliente = db.Cliente(codigo);
                if (cliente == null) { Console.WriteLine("Cliente inexistente."); continue; }
                Console.WriteLine($"Solicitudes de {cliente.Nombre}:");
                int cantidad = 0;
                foreach (var solicitud in db.Solicitudes)
                    if (solicitud.ClienteCodigo == codigo)
                    {
                        Mostrar(solicitud);
                        cantidad++;
                    }
                if (cantidad == 0) Console.WriteLine("El cliente aun no tiene solicitudes registradas.");
                else Console.WriteLine($"Total de solicitudes: {cantidad}");
            }
        }
    }
    static void MenuTecnicos()
    {
        while (true)
        {
            int o = Opcion("TECNICOS", "Registrar", "Consultar", "Cambiar disponibilidad");
            if (o == 0) return;
            if (o == 1)
            {
                int id = Entero("Codigo: ", 1);
                if (db.Tecnico(id) != null) { Console.WriteLine("Codigo duplicado."); continue; }
                db.Tecnicos.Add(new Tecnico { Codigo = id, Nombre = Texto("Nombre: "), Especialidad = Especialidad() });
            }
            else if (o == 2) foreach (var t in db.Tecnicos)
                Console.WriteLine($"{t.Codigo} | {t.Nombre} | {t.Especialidad} | {(t.Disponible ? "Disponible" : "No disponible")} | Activos: {t.TrabajosActivos}/{Datos.LimiteTrabajos} | Finalizados: {t.Finalizados} | Promedio: {t.Promedio:F1}");
            else if (o == 3)
            {
                var t = db.Tecnico(Entero("Codigo del tecnico: ", 1));
                if (t == null) { Console.WriteLine("Tecnico inexistente."); continue; }
                t.Disponible = !t.Disponible;
                Console.WriteLine(t.Disponible ? "Tecnico habilitado." : "Tecnico marcado como no disponible.");
            }
        }
    }
    static string Especialidad()
    {
        while (true)
        {
            int o = Entero("Especialidad 1.Portatil 2.Escritorio 3.Impresora: ", 1);
            if (o >= 1 && o <= 3) return new[] { "Portatil", "Escritorio", "Impresora" }[o - 1];
        }
    }
    static void MenuEquipos()
    {
        while (true)
        {
            int o = Opcion("EQUIPOS", "Registrar", "Consultar", "Buscar por numero de serie", "Historial de servicios por equipo");
            if (o == 0) return;
            if (o == 1)
            {
                int id = Entero("Codigo interno: ", 1);
                if (db.Equipo(id) != null) { Console.WriteLine("Codigo duplicado."); continue; }
                int cliente = Entero("Codigo del propietario: ", 1);
                if (db.Cliente(cliente) == null) { Console.WriteLine("No existe el cliente."); continue; }
                string serie = Texto("Numero de serie: ");
                if (db.Equipos.Exists(e => e.Serie.Equals(serie, StringComparison.OrdinalIgnoreCase))) { Console.WriteLine("Serie duplicada."); continue; }
                int tipo = Entero("Tipo 1.Portatil 2.Escritorio 3.Impresora: ", 1);
                Equipo? equipo = tipo switch { 1 => new Portatil(), 2 => new Escritorio(), 3 => new Impresora(), _ => null };
                if (equipo == null) { Console.WriteLine("Tipo invalido."); continue; }
                equipo.Codigo = id; equipo.ClienteCodigo = cliente; equipo.Serie = serie;
                equipo.Marca = Texto("Marca: "); equipo.Modelo = Texto("Modelo: ");
                equipo.EstadoFisico = Texto("Estado fisico: "); equipo.Accesorios = Texto("Accesorios: ");
                db.Equipos.Add(equipo);
            }
            else if (o == 2) foreach (var e in db.Equipos)
                Console.WriteLine($"{e.Codigo} | {e.Tipo} | {e.Marca} {e.Modelo} | Serie: {e.Serie} | Propietario: {e.ClienteCodigo} | Revision Q{e.CostoRevision():F2}");
            else if (o == 3)
            {
                string serie = Texto("Numero de serie: ");
                bool encontrado = false;
                foreach (var e in db.Equipos)
                    if (e.Serie.Equals(serie, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"{e.Codigo} | {e.Tipo} | {e.Marca} {e.Modelo} | Propietario: {e.ClienteCodigo} | Estado: {e.EstadoFisico} | Accesorios: {e.Accesorios}");
                        encontrado = true;
                    }
                if (!encontrado) Console.WriteLine("No se encontro un equipo con ese numero de serie.");
            }
            else if (o == 4)
            {
                int codigo = Entero("Codigo del equipo: ", 1);
                var equipo = db.Equipo(codigo);
                if (equipo == null) { Console.WriteLine("Equipo inexistente."); continue; }
                Console.WriteLine($"Historial de {equipo.Tipo} | {equipo.Marca} {equipo.Modelo} | Serie: {equipo.Serie}");
                int cantidad = 0;
                foreach (var solicitud in db.Solicitudes)
                    if (solicitud.EquipoCodigo == codigo)
                    {
                        Mostrar(solicitud);
                        cantidad++;
                    }
                if (cantidad == 0) Console.WriteLine("Este equipo aun no tiene servicios registrados.");
                else Console.WriteLine($"Servicios encontrados: {cantidad}");
            }
        }
    }
    static void MenuRepuestos()
    {
        while (true)
        {
            int o = Opcion("REPUESTOS", "Registrar", "Consultar", "Agregar existencia", "Actualizar existencia minima");
            if (o == 0) return;
            if (o == 1)
            {
                int id = Entero("Codigo: ", 1);
                if (db.Repuesto(id) != null) { Console.WriteLine("Codigo duplicado."); continue; }
                var r = new Repuesto { Codigo = id, Nombre = Texto("Nombre: "), Categoria = Texto("Categoria: "), Existencia = Entero("Existencia: "), Minima = Entero("Existencia minima: "), CostoUnitario = Dinero("Costo unitario: "), PrecioVenta = Dinero("Precio venta: ") };
                if (r.PrecioVenta < r.CostoUnitario) Console.WriteLine("Aviso: precio de venta menor al costo.");
                db.Repuestos.Add(r); Alerta(r);
            }
            else if (o == 2) foreach (var r in db.Repuestos) Console.WriteLine($"{r.Codigo} | {r.Nombre} | {r.Categoria} | Stock {r.Existencia} | Min {r.Minima} | Q{r.PrecioVenta:F2} | Usados {r.Utilizados}");
            else if (o == 3)
            {
                var r = db.Repuesto(Entero("Codigo: ", 1));
                if (r == null) { Console.WriteLine("No existe."); continue; }
                int cantidad = Entero("Cantidad a agregar: ", 1);
                if (cantidad > int.MaxValue - r.Existencia) { Console.WriteLine("La cantidad supera el limite permitido."); continue; }
                r.Existencia += cantidad;
                Console.WriteLine($"Nueva existencia: {r.Existencia}");
                Alerta(r);
            }
            else if (o == 4)
            {
                var r = db.Repuesto(Entero("Codigo: ", 1));
                if (r == null) { Console.WriteLine("No existe."); continue; }
                r.Minima = Entero("Nueva existencia minima: ");
                Console.WriteLine("Existencia minima actualizada.");
                Alerta(r);
            }
        }
    }
    static void Alerta(Repuesto r)
    {
        if (r.Existencia <= r.Minima) Console.WriteLine($"ALERTA: {r.Nombre} tiene existencia baja ({r.Existencia}).");
    }
    static void MenuSolicitudes()
    {
        while (true)
        {
            int o = Opcion("SOLICITUDES", "Crear", "Consultar", "Registrar diagnostico", "Agregar repuesto", "Cambiar estado", "Ver historial", "Deshacer cambio", "Calificar servicio", "Consultar por codigo");
            if (o == 0) return;
            if (o == 1) CrearSolicitud();
            else if (o == 2)
            {
                Console.WriteLine("ACTIVAS");
                foreach (var s in db.Solicitudes) if (db.Activa(s)) Mostrar(s);
                Console.WriteLine("FINALIZADAS");
                foreach (var s in db.Solicitudes) if (s.Estado == "Entregada") Mostrar(s);
                Console.WriteLine("CANCELADAS");
                foreach (var s in db.Solicitudes) if (s.Estado == "Cancelada") Mostrar(s);
            }
            else if (o == 3)
            {
                var s = PedirSolicitud(); if (s == null) continue;
                if (s.Estado != "En diagnostico") { Console.WriteLine("Debe estar en diagnostico."); continue; }
                s.Diagnostico = Texto("Diagnostico: "); s.TiempoEstimado = Entero("Horas estimadas: ", 1);
                s.ManoObra = Dinero("Costo mano de obra: ");
                Console.WriteLine("Diagnostico guardado.");
            }
            else if (o == 4)
            {
                var s = PedirSolicitud(); if (s == null) continue;
                if (s.Estado != "En reparacion") { Console.WriteLine("Debe estar en reparacion."); continue; }
                var r = db.Repuesto(Entero("Codigo de repuesto: ", 1));
                if (r == null) { Console.WriteLine("No existe."); continue; }
                int cantidad = Entero("Cantidad: ", 1);
                if (cantidad > r.Existencia) { Console.WriteLine("Existencia insuficiente."); continue; }
                r.Existencia -= cantidad; r.Utilizados += cantidad;
                s.CostoRepuestos += cantidad * r.PrecioVenta;
                s.RepuestosUsados.Add($"{r.Nombre} x{cantidad}"); Alerta(r);
            }
            else if (o == 5)
            {
                var s = PedirSolicitud(); if (s == null) continue;
                CambiarEstado(s);
            }
            else if (o == 6)
            {
                var s = PedirSolicitud(); if (s == null) continue;
                Console.WriteLine("Estado actual: " + s.Estado);
                foreach (var anterior in s.Historial) Console.WriteLine("Anterior: " + anterior);
            }
            else if (o == 7)
            {
                var s = PedirSolicitud(); if (s == null) continue;
                if (!db.Activa(s) || s.Historial.Count == 0 || s.Estado == "Asignada" || s.Estado == "En espera")
                { Console.WriteLine("No se puede deshacer en este estado."); continue; }
                string anterior = s.Historial.Peek();
                if (s.RepuestosUsados.Count > 0 && (anterior == "En diagnostico" || anterior == "En reparacion"))
                { Console.WriteLine("No se puede regresar al diagnostico despues de consumir repuestos."); continue; }
                s.Estado = s.Historial.Pop(); Console.WriteLine("Restaurado a: " + s.Estado);
            }
            else if (o == 8)
            {
                var s = PedirSolicitud(); if (s == null) continue;
                if (s.Estado != "Entregada" || s.TecnicoCodigo == null) { Console.WriteLine("Solo puede calificar servicios entregados."); continue; }
                var t = db.Tecnico(s.TecnicoCodigo.Value)!;
                if (s.Calificacion != null) { Console.WriteLine("Ya fue calificado."); continue; }
                int nota = Entero("Calificacion de 1 a 5: ", 1);
                if (nota > 5) { Console.WriteLine("Fuera de rango."); continue; }
                t.SumaCalificaciones += nota; t.NumeroCalificaciones++; db.Calificaciones[nota - 1]++;
                s.Calificacion = nota;
            }
            else if (o == 9)
            {
                var s = PedirSolicitud();
                if (s != null) Mostrar(s);
            }
        }
    }
    static Solicitud? PedirSolicitud()
    {
        var s = db.Solicitud(Entero("Codigo de solicitud: ", 1));
        if (s == null) Console.WriteLine("Solicitud inexistente.");
        return s;
    }
    static void CrearSolicitud()
    {
        int id = Entero("Codigo: ", 1);
        if (db.Solicitud(id) != null) { Console.WriteLine("Codigo duplicado."); return; }
        int cliente = Entero("Codigo cliente: ", 1);
        if (db.Cliente(cliente) == null) { Console.WriteLine("Cliente inexistente."); return; }
        int equipo = Entero("Codigo equipo: ", 1);
        var e = db.Equipo(equipo);
        if (e == null || e.ClienteCodigo != cliente) { Console.WriteLine("Equipo inexistente o no pertenece al cliente."); return; }
        if (db.Solicitudes.Exists(s => s.EquipoCodigo == equipo && db.Activa(s))) { Console.WriteLine("Este equipo ya tiene una solicitud activa."); return; }
        string problema = Texto("Descripcion del problema: ");
        int p = Entero("Prioridad 1.Normal 2.Urgente: ", 1);
        if (p != 1 && p != 2) { Console.WriteLine("Prioridad incorrecta."); return; }
        var nueva = new Solicitud { Codigo = id, ClienteCodigo = cliente, EquipoCodigo = equipo, Problema = problema, Urgente = p == 2 };
        db.Solicitudes.Add(nueva); db.Cliente(cliente)!.CantidadSolicitudes++;
        Actualizar(nueva, "En espera");
        if (nueva.Urgente) db.Urgentes.Enqueue(id); else db.Normales.Enqueue(id);
        Console.WriteLine("Solicitud encolada.");
    }
    static void Mostrar(Solicitud s)
    {
        Console.WriteLine($"{s.Codigo} | Cliente {s.ClienteCodigo} | Equipo {s.EquipoCodigo} | {(s.Urgente ? "URGENTE" : "NORMAL")} | {s.Estado} | Tecnico {s.TecnicoCodigo?.ToString() ?? "Sin asignar"} | {s.Fecha:g} | Total Q{s.Total:F2}");
        if (s.Diagnostico != "") Console.WriteLine("  Diagnostico: " + s.Diagnostico);
        if (s.Calificacion != null) Console.WriteLine("  Calificacion: " + s.Calificacion);
        if (s.TiempoEstimado > 0) Console.WriteLine($"  Tiempo estimado: {s.TiempoEstimado} horas");
        foreach (string repuesto in s.RepuestosUsados) Console.WriteLine("  Repuesto: " + repuesto);
        Console.WriteLine($"  Mano de obra: Q{s.ManoObra:F2} | Repuestos: Q{s.CostoRepuestos:F2} | Total: Q{s.Total:F2}");
    }
    static void Actualizar(Solicitud s, string nuevo)
    {
        s.Historial.Push(s.Estado); s.Estado = nuevo;
    }
    static void AtenderColas()
    {

        Queue<int>? cola = db.Urgentes.Count > 0 ? db.Urgentes : db.Normales.Count > 0 ? db.Normales : null;
        if (cola == null) { Console.WriteLine("No hay solicitudes pendientes."); return; }
        var s = db.Solicitud(cola.Peek())!;
        var equipo = db.Equipo(s.EquipoCodigo)!;
        Console.WriteLine($"Siguiente: {s.Codigo}, {equipo.Tipo}, {(s.Urgente ? "URGENTE" : "NORMAL")}");
        Console.WriteLine("Tecnicos compatibles con capacidad:");
        Tecnico? recomendado = null;
        foreach (var t in db.Tecnicos)
            if (t.Disponible && t.Especialidad == equipo.Tipo && t.TrabajosActivos < Datos.LimiteTrabajos)
            {
                Console.WriteLine($"{t.Codigo} | {t.Nombre} | {t.TrabajosActivos} trabajos");
                if (recomendado == null || t.TrabajosActivos < recomendado.TrabajosActivos)
                    recomendado = t;
            }
        if (recomendado == null)
        {
            Console.WriteLine("No hay tecnicos disponibles para este tipo de equipo. La solicitud permanece en cola.");
            return;
        }
        Console.WriteLine($"Tecnico sugerido: {recomendado.Codigo} | {recomendado.Nombre}");
        int id = Entero("Codigo del tecnico (0 para cancelar): ");
        if (id == 0) return;
        var tecnico = db.Tecnico(id);
        if (tecnico == null || !tecnico.Disponible || tecnico.Especialidad != equipo.Tipo || tecnico.TrabajosActivos >= Datos.LimiteTrabajos)
        { Console.WriteLine("Tecnico no disponible o especialidad incorrecta."); return; }
        cola.Dequeue(); s.TecnicoCodigo = id; s.FechaAsignacion = DateTime.Now; tecnico.TrabajosActivos++;
        Actualizar(s, "Asignada"); Console.WriteLine("Solicitud asignada.");
    }
    static void CambiarEstado(Solicitud s)
    {
        if (!db.Activa(s)) { Console.WriteLine("Solicitud cerrada: no se modifica."); return; }
        string? siguiente = s.Estado switch
        {
            "Asignada" => "En diagnostico",
            "En diagnostico" when s.Diagnostico != "" => "En reparacion",
            "En reparacion" => "Lista para entregar",
            "Lista para entregar" when s.Diagnostico != "" => "Entregada",
            _ => null
        };
        Console.WriteLine("Estado actual: " + s.Estado);
        Console.WriteLine("1. Siguiente estado" + (siguiente == null ? " (no disponible)" : ": " + siguiente));
        Console.WriteLine("2. Cancelar solicitud (solo si aun no se ha reparado)");
        int op = Entero("Opcion: ");
        if (op == 2)
        {
            if (s.Estado != "En espera" && s.Estado != "Asignada" && s.Estado != "En diagnostico")
            { Console.WriteLine("No puede cancelar en este punto."); return; }
            if (s.TecnicoCodigo != null) db.Tecnico(s.TecnicoCodigo.Value)!.TrabajosActivos--;

            QuitarDeCola(db.Urgentes, s.Codigo); QuitarDeCola(db.Normales, s.Codigo);
            Actualizar(s, "Cancelada");
        }
        else if (op == 1 && siguiente != null)
        {
            Actualizar(s, siguiente);
            if (siguiente == "Entregada" && s.TecnicoCodigo != null)
            {
                var t = db.Tecnico(s.TecnicoCodigo.Value)!;
                t.TrabajosActivos--; t.Finalizados++;
            }
        }
        else Console.WriteLine("Cambio no permitido.");
    }
    static void QuitarDeCola(Queue<int> cola, int codigo)
    {
        int n = cola.Count;
        for (int i = 0; i < n; i++) { int actual = cola.Dequeue(); if (actual != codigo) cola.Enqueue(actual); }
    }
    static void MenuOrdenar()
    {
        int o = Opcion("ORDENAMIENTOS", "Solicitudes por costo (Burbuja ascendente)", "Tecnicos por finalizados (Seleccion descendente)", "Repuestos por existencia (Shell ascendente)", "Solicitudes por fecha (Quick ascendente)");
        if (o == 0) return;
        if (o == 1 && db.Solicitudes.Count >= 2) { Console.WriteLine("Burbuja | Costo total | Ascendente"); foreach (var s in Algoritmos.BurbujaCosto(db.Solicitudes)) Mostrar(s); }
        else if (o == 2 && db.Tecnicos.Count >= 2) { Console.WriteLine("Seleccion | Servicios finalizados | Descendente"); foreach (var t in Algoritmos.SeleccionFinalizados(db.Tecnicos)) Console.WriteLine($"{t.Nombre}: {t.Finalizados}"); }
        else if (o == 3 && db.Repuestos.Count >= 2) { Console.WriteLine("Shell | Existencia | Ascendente"); foreach (var r in Algoritmos.ShellExistencia(db.Repuestos)) Console.WriteLine($"{r.Nombre}: {r.Existencia}"); }
        else if (o == 4 && db.Solicitudes.Count >= 2) { Console.WriteLine("Quick Sort | Fecha de recepcion | Ascendente"); foreach (var s in Algoritmos.RapidoFecha(db.Solicitudes)) Mostrar(s); }
        else Console.WriteLine("Se requieren al menos dos registros.");
    }
    static void MenuBuscar()
    {
        int o = Opcion("BUSQUEDAS", "Clientes por nombre parcial (Secuencial)", "Equipos por marca (Secuencial)", "Solicitudes por estado (Secuencial)", "Solicitud por codigo (Binaria)");
        if (o == 0) return;
        int encontrados = 0;
        if (o == 1)
        {
            string palabra = Texto("Parte del nombre: ");
            foreach (var c in db.Clientes) if (c.Nombre.Contains(palabra, StringComparison.OrdinalIgnoreCase)) { Console.WriteLine($"{c.Codigo} - {c.Nombre}"); encontrados++; }
        }
        else if (o == 2)
        {
            string palabra = Texto("Marca: ");
            foreach (var e in db.Equipos) if (e.Marca.Contains(palabra, StringComparison.OrdinalIgnoreCase)) { Console.WriteLine($"{e.Codigo} - {e.Marca} {e.Modelo}"); encontrados++; }
        }
        else if (o == 3)
        {
            string palabra = Texto("Estado (o parte): ");
            foreach (var s in db.Solicitudes) if (s.Estado.Contains(palabra, StringComparison.OrdinalIgnoreCase)) { Mostrar(s); encontrados++; }
        }
        else if (o == 4)
        {
            var s = Algoritmos.BinariaPorCodigo(db.Solicitudes, Entero("Codigo: ", 1));
            if (s != null) { Mostrar(s); encontrados++; }
        }
        Console.WriteLine($"Coincidencias: {encontrados}");
    }
    static void MenuReportes()
    {
        int o = Opcion("REPORTES", "Colas por prioridad", "Solicitudes por estado", "Tecnicos disponibles/llenos", "Tecnico con mas finalizados", "Equipos por tipo", "Solicitudes por costo", "Solicitudes por fecha", "Repuestos por existencia", "Repuestos bajos", "Ingresos por entregados", "Calificaciones", "Matriz semanal", "Clientes con solicitudes activas", "Solicitudes por tecnico");
        if (o == 0) return;
        if (o == 1)
        {
            Console.WriteLine("Urgentes:"); foreach (int id in db.Urgentes) Console.WriteLine(id);
            Console.WriteLine("Normales:"); foreach (int id in db.Normales) Console.WriteLine(id);
        }
        else if (o == 2)
        {
            Console.WriteLine("ACTIVAS");
            foreach (var s in db.Solicitudes) if (db.Activa(s)) Mostrar(s);
            Console.WriteLine("FINALIZADAS");
            foreach (var s in db.Solicitudes) if (s.Estado == "Entregada") Mostrar(s);
            Console.WriteLine("CANCELADAS");
            foreach (var s in db.Solicitudes) if (s.Estado == "Cancelada") Mostrar(s);
        }
        else if (o == 3) foreach (var t in db.Tecnicos) Console.WriteLine($"{t.Nombre}: {(!t.Disponible ? "No disponible" : t.TrabajosActivos >= Datos.LimiteTrabajos ? "Carga completa" : "Disponible")}");
        else if (o == 4)
        {
            var lista = Algoritmos.SeleccionFinalizados(db.Tecnicos);
            if (lista.Count > 0) Console.WriteLine($"{lista[0].Nombre}: {lista[0].Finalizados}");
        }
        else if (o == 5) foreach (string tipo in new[] { "Portatil", "Escritorio", "Impresora" })
        {
            int n = 0; foreach (var s in db.Solicitudes) if (db.Equipo(s.EquipoCodigo)?.Tipo == tipo) n++;
            Console.WriteLine($"{tipo}: {n} solicitudes");
        }
        else if (o == 6 || o == 7)
        {
            if (db.Solicitudes.Count < 2) { Console.WriteLine("Faltan datos."); return; }
            Console.WriteLine(o == 6 ? "Burbuja | Costo total | Ascendente" : "Quick Sort | Fecha de recepcion | Ascendente");
            foreach (var s in o == 6 ? Algoritmos.BurbujaCosto(db.Solicitudes) : Algoritmos.RapidoFecha(db.Solicitudes)) Mostrar(s);
        }
        else if (o == 8)
        {
            if (db.Repuestos.Count < 2) { Console.WriteLine("Faltan datos."); return; }
            Console.WriteLine("Shell | Existencia | Ascendente");
            foreach (var r in Algoritmos.ShellExistencia(db.Repuestos)) Console.WriteLine($"{r.Nombre}: {r.Existencia}");
        }
        else if (o == 9) foreach (var r in db.Repuestos) if (r.Existencia <= r.Minima) Console.WriteLine($"{r.Nombre}: {r.Existencia} unidades");
        else if (o == 10)
        {
            decimal total = 0; foreach (var s in db.Solicitudes) if (s.Estado == "Entregada") total += s.Total;
            Console.WriteLine($"Ingresos por servicios entregados: Q{total:F2}");
        }
        else if (o == 11) for (int i = 0; i < db.Calificaciones.Length; i++) Console.WriteLine($"{i + 1} estrellas: {db.Calificaciones[i]}");
        else if (o == 12) MostrarMatriz();
        else if (o == 13)
        {
            int encontrados = 0;
            foreach (var cliente in db.Clientes)
            {
                int activas = 0;
                foreach (var solicitud in db.Solicitudes)
                    if (solicitud.ClienteCodigo == cliente.Codigo && db.Activa(solicitud)) activas++;
                if (activas > 0)
                {
                    Console.WriteLine($"{cliente.Codigo} | {cliente.Nombre} | Solicitudes activas: {activas}");
                    encontrados++;
                }
            }
            if (encontrados == 0) Console.WriteLine("No hay clientes con solicitudes activas.");
        }
        else if (o == 14)
        {
            int codigo = Entero("Codigo del tecnico: ", 1);
            var tecnico = db.Tecnico(codigo);
            if (tecnico == null) { Console.WriteLine("Tecnico inexistente."); return; }
            Console.WriteLine($"Solicitudes asignadas a {tecnico.Nombre}:");
            int activas = 0;
            int entregadas = 0;
            int canceladas = 0;
            foreach (var solicitud in db.Solicitudes)
            {
                if (solicitud.TecnicoCodigo != codigo) continue;
                Mostrar(solicitud);
                if (solicitud.Estado == "Entregada") entregadas++;
                else if (solicitud.Estado == "Cancelada") canceladas++;
                else activas++;
            }
            Console.WriteLine($"Activas: {activas} | Entregadas: {entregadas} | Canceladas: {canceladas}");
            if (activas + entregadas + canceladas == 0)
                Console.WriteLine("El tecnico aun no tiene solicitudes asignadas.");
        }
    }
    static void MostrarMatriz()
    {
        int[,] matriz = db.MatrizSemanal();
        Console.WriteLine($"Semana del {DateTime.Today.AddDays(-((int)DateTime.Today.DayOfWeek + 6) % 7):dd/MM/yyyy}");
        Console.WriteLine("Tecnico | Lun Mar Mie Jue Vie Sab Dom");
        int max = 0, tecnicoMax = -1, diaMax = -1;
        for (int i = 0; i < db.Tecnicos.Count; i++)
        {
            Console.Write($"{db.Tecnicos[i].Nombre} | ");
            for (int j = 0; j < 7; j++)
            {
                Console.Write(matriz[i, j] + " ");
                if (matriz[i, j] > max) { max = matriz[i, j]; tecnicoMax = i; diaMax = j; }
            }
            Console.WriteLine();
        }
        if (tecnicoMax >= 0) Console.WriteLine($"Mayor carga: {db.Tecnicos[tecnicoMax].Nombre}, dia {new[] { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" }[diaMax]}, {max} asignaciones.");
    }
    static int Siguiente<T>(List<T> lista, Func<T, int> codigo)
    {
        int mayor = 0; foreach (var item in lista) if (codigo(item) > mayor) mayor = codigo(item);
        return mayor + 1;
    }
    static void GenerarPruebas()
    {
        int cuantos = Entero("Cantidad de juegos de prueba (1 a 20): ", 1);
        if (cuantos > 20) { Console.WriteLine("Maximo 20."); return; }
        for (int i = 0; i < cuantos; i++)
        {
            int c = Siguiente(db.Clientes, x => x.Codigo);
            db.Clientes.Add(new Cliente { Codigo = c, Nombre = "Cliente prueba " + c, Telefono = "55550000", Correo = $"cliente{c}@ejemplo.com", Tipo = "Particular" });
            int t = Siguiente(db.Tecnicos, x => x.Codigo);
            string especialidad = new[] { "Portatil", "Escritorio", "Impresora" }[i % 3];
            db.Tecnicos.Add(new Tecnico { Codigo = t, Nombre = "Tecnico prueba " + t, Especialidad = especialidad });
            int e = Siguiente(db.Equipos, x => x.Codigo);
            Equipo equipo = especialidad switch { "Portatil" => new Portatil(), "Escritorio" => new Escritorio(), _ => new Impresora() };
            string serie = "TEST-" + e;
            int numero = 1;
            while (db.Equipos.Exists(x => x.Serie.Equals(serie, StringComparison.OrdinalIgnoreCase)))
            {
                serie = "TEST-" + e + "-" + numero;
                numero++;
            }
            equipo.Codigo = e; equipo.ClienteCodigo = c; equipo.Serie = serie;
            equipo.Marca = "Marca prueba"; equipo.Modelo = "Modelo prueba"; equipo.EstadoFisico = "Usado"; equipo.Accesorios = "Ninguno";
            db.Equipos.Add(equipo);
            int r = Siguiente(db.Repuestos, x => x.Codigo);
            db.Repuestos.Add(new Repuesto { Codigo = r, Nombre = "Repuesto prueba " + r, Categoria = "General", Existencia = azar.Next(0, 15), Minima = 3, CostoUnitario = azar.Next(10, 60), PrecioVenta = azar.Next(60, 150) });
            int s = Siguiente(db.Solicitudes, x => x.Codigo);
            var solicitud = new Solicitud { Codigo = s, ClienteCodigo = c, EquipoCodigo = e, Problema = "Falla de prueba", Urgente = azar.Next(2) == 1, TiempoEstimado = azar.Next(1, 9), ManoObra = azar.Next(40, 201) };
            db.Solicitudes.Add(solicitud); db.Cliente(c)!.CantidadSolicitudes++;
            Actualizar(solicitud, "En espera");
            if (solicitud.Urgente) db.Urgentes.Enqueue(s); else db.Normales.Enqueue(s);
        }
        Console.WriteLine("Datos de prueba generados con codigos unicos.");
    }
}