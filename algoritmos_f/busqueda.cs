using System;
using SistemaInventarioInteligente.Modelos;

namespace SistemaInventarioInteligente.algoritmos_f
{
    /// <summary>
    /// Clase que representa un producto en el inventario
    /// </summary>
    public static class AlgoritmosBusqueda
    {
        // Búsqueda Secuencial
        public static Producto BusquedaSecuencial(List<Producto> lista, int id)
        {
            foreach (var producto in lista)
            {
                if (producto.Id == id)
                    return producto;
            }
            return null;
        }

        // Búsqueda Binaria (requiere lista ordenada por ID)
        public static Producto BusquedaBinaria(List<Producto> lista, int id)
        {
            int inicio = 0;
            int fin = lista.Count - 1;

            while (inicio <= fin)
            {
                int medio = (inicio + fin) / 2;

                if (lista[medio].Id == id)
                    return lista[medio];
                else if (lista[medio].Id < id)
                    inicio = medio + 1;
                else
                    fin = medio - 1;
            }

            return null;
        }
    }
}