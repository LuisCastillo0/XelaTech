using XelaTech.Modelos;
using XelaTech.Utilidades;

namespace XelaTech.Servicios;

public class GestorClientes
{
    private readonly List<Cliente> clientes = new List<Cliente>();

    public void Registrar()
    {
        Console.WriteLine("--- REGISTRAR CLIENTE ---");
        int codigo = Entrada.LeerEntero("Código de cliente: ", 1);

        if (BuscarPorCodigo(codigo) != null)
        {
            Console.WriteLine("Ya existe un cliente con ese código.");
            return;
        }

        string nombre = Entrada.LeerTexto("Nombre completo: ");
        string telefono = Entrada.LeerTexto("Teléfono: ");
        string correo = Entrada.LeerTexto("Correo electrónico: ");
        string tipo = Entrada.LeerTexto("Tipo de cliente: ");

        Cliente nuevoCliente = new Cliente(codigo, nombre, telefono, correo, tipo);
        clientes.Add(nuevoCliente);
        Console.WriteLine("Cliente registrado correctamente.");
    }

    public Cliente? BuscarPorCodigo(int codigo)
    {

        for (int i = 0; i < clientes.Count; i++)
        {
            if (clientes[i].Codigo == codigo)
                return clientes[i];
        }
        return null;
    }

    public void MostrarTodos()
    {
        Console.WriteLine("\n--- CLIENTES REGISTRADOS ---");
        if (clientes.Count == 0)
        {
            Console.WriteLine("Aún no existen clientes registrados.");
            return;
        }

        foreach (Cliente cliente in clientes)
            cliente.MostrarInformacion();
    }
}
