using XelaTech.Servicios;
using XelaTech.Utilidades;

namespace XelaTech;

internal class Program
{
    static void Main()
    {
        GestorClientes clientes = new GestorClientes();
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("         XELATECH SOPORTE         ");
            Console.WriteLine("==================================");
            Console.WriteLine("1. Gestión de clientes");
            Console.WriteLine("2. Salir");
            Console.WriteLine("\nVersión 0.1 - Estructura inicial");

            int opcion = Entrada.LeerEntero("Seleccione una opción: ", 1, 2);
            switch (opcion)
            {
                case 1:
                    MenuClientes(clientes);
                    break;
                case 2:
                    salir = true;
                    break;
            }
        }

        Console.WriteLine("Gracias por utilizar XelaTech Soporte.");
    }

    static void MenuClientes(GestorClientes gestor)
    {
        bool regresar = false;
        while (!regresar)
        {
            Console.Clear();
            Console.WriteLine("===== GESTIÓN DE CLIENTES ===== \n1. Registrar cliente \n2. Consultar clientes \n3. Volver al menú principal");
            int opcion = Entrada.LeerEntero("Seleccione una opción: ", 1, 3);
            switch (opcion)
            {
                case 1:
                    gestor.Registrar();
                    Entrada.Pausar();
                    break;
                case 2:
                    gestor.MostrarTodos();
                    Entrada.Pausar();
                    break;
                case 3:
                    regresar = true;
                    break;
            }
        }
    }
}
