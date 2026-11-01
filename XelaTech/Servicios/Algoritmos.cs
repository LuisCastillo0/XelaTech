using XelaTech.Modelos;
namespace XelaTech.Servicios;

public static class Algoritmos
{
    public static List<Solicitud> BurbujaCosto(List<Solicitud> origen)
    {
        var datos = new List<Solicitud>(origen);
        for (int i = 0; i < datos.Count - 1; i++)
            for (int j = 0; j < datos.Count - i - 1; j++)
                if (datos[j].Total > datos[j + 1].Total)
                    (datos[j], datos[j + 1]) = (datos[j + 1], datos[j]);
        return datos;
    }

    public static List<Tecnico> SeleccionFinalizados(List<Tecnico> origen)
    {
        var datos = new List<Tecnico>(origen);
        for (int i = 0; i < datos.Count - 1; i++)
        {
            int mayor = i;
            for (int j = i + 1; j < datos.Count; j++)
                if (datos[j].Finalizados > datos[mayor].Finalizados) mayor = j;
            (datos[i], datos[mayor]) = (datos[mayor], datos[i]);
        }
        return datos;
    }

    public static List<Repuesto> ShellExistencia(List<Repuesto> origen)
    {
        var datos = new List<Repuesto>(origen);
        for (int salto = datos.Count / 2; salto > 0; salto /= 2)
            for (int i = salto; i < datos.Count; i++)
            {
                var temporal = datos[i];
                int j = i;
                while (j >= salto && datos[j - salto].Existencia > temporal.Existencia)
                { datos[j] = datos[j - salto]; j -= salto; }
                datos[j] = temporal;
            }
        return datos;
    }

    public static List<Solicitud> RapidoFecha(List<Solicitud> origen)
    {
        var datos = new List<Solicitud>(origen);
        Quick(datos, 0, datos.Count - 1);
        return datos;
    }
    private static void Quick(List<Solicitud> a, int izquierda, int derecha)
    {
        int i = izquierda, j = derecha;
        if (i >= j) return;
        DateTime pivote = a[(izquierda + derecha) / 2].Fecha;
        while (i <= j)
        {
            while (a[i].Fecha < pivote) i++;
            while (a[j].Fecha > pivote) j--;
            if (i <= j) { (a[i], a[j]) = (a[j], a[i]); i++; j--; }
        }
        if (izquierda < j) Quick(a, izquierda, j);
        if (i < derecha) Quick(a, i, derecha);
    }

    public static Solicitud? BinariaPorCodigo(List<Solicitud> origen, int codigo)
    {

        var datos = new List<Solicitud>(origen);
        for (int i = 1; i < datos.Count; i++)
        {
            var actual = datos[i]; int j = i - 1;
            while (j >= 0 && datos[j].Codigo > actual.Codigo)
            { datos[j + 1] = datos[j]; j--; }
            datos[j + 1] = actual;
        }
        int inicio = 0, fin = datos.Count - 1;
        while (inicio <= fin)
        {
            int medio = (inicio + fin) / 2;
            if (datos[medio].Codigo == codigo) return datos[medio];
            if (datos[medio].Codigo < codigo) inicio = medio + 1;
            else fin = medio - 1;
        }
        return null;
    }
}
