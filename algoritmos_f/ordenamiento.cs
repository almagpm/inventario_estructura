using System;
using SistemaInventarioInteligente.Modelos;

namespace SistemaInventarioInteligente.algoritmos_f
{
    /// <summary>
    /// Clase que representa un producto en el inventario
    /// </summary>
    public static class AlgoritmosOrdenamiento
    {
        // QuickSort
        public static void QuickSort(List<Producto> lista, int inicio, int fin)
        {
            if (inicio < fin)
            {
                int pivote = Particionar(lista, inicio, fin);
                QuickSort(lista, inicio, pivote - 1);
                QuickSort(lista, pivote + 1, fin);
            }
        }

        private static int Particionar(List<Producto> lista, int inicio, int fin)
        {
            decimal pivote = lista[fin].Precio;
            int i = inicio - 1;

            for (int j = inicio; j < fin; j++)
            {
                if (lista[j].Precio <= pivote)
                {
                    i++;
                    var temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;
                }
            }

            var temp2 = lista[i + 1];
            lista[i + 1] = lista[fin];
            lista[fin] = temp2;

            return i + 1;
        }

        // ShellSort
        public static void ShellSort(List<Producto> lista)
        {
            int n = lista.Count;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    var temp = lista[i];
                    int j = i;

                    while (j >= gap && lista[j - gap].Stock > temp.Stock)
                    {
                        lista[j] = lista[j - gap];
                        j -= gap;
                    }

                    lista[j] = temp;
                }
                gap /= 2;
            }
        }

        // RadixSort (por ID)
        public static void RadixSort(List<Producto> lista)
        {
            if (lista.Count == 0) return;

            int max = lista.Max(p => p.Id);
            
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountingSortPorDigito(lista, exp);
            }
        }

        private static void CountingSortPorDigito(List<Producto> lista, int exp)
        {
            int n = lista.Count;
            Producto[] output = new Producto[n];
            int[] count = new int[10];

            for (int i = 0; i < n; i++)
            {
                int digito = (lista[i].Id / exp) % 10;
                count[digito]++;
            }

            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                int digito = (lista[i].Id / exp) % 10;
                output[count[digito] - 1] = lista[i];
                count[digito]--;
            }

            for (int i = 0; i < n; i++)
            {
                lista[i] = output[i];
            }
        }
    }

}