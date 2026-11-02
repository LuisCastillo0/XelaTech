namespace XelaTech.Utilidades;

public static class Entrada
{
    public static int LeerEntero(string mensaje, int minimo, int? maximo = null)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? texto = Console.ReadLine();
            if (int.TryParse(texto, out int numero) && numero >= minimo &&
                (!maximo.HasValue || numero <= maximo.Value))
                return numero;

            Console.WriteLine("Valor inválido. Intente nuevamente.");
        }
    }

    public static string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string texto = (Console.ReadLine() ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(texto))
                return texto;

            Console.WriteLine("Este campo no puede quedar vacío.");
        }
    }

    public static void Pausar()
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
    }
}
