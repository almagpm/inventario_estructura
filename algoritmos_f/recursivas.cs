using System;
using SistemaInventarioInteligente.Modelos;

namespace SistemaInventarioInteligente.algoritmos_f
{
    /// <summary>
    /// Clase que representa un producto en el inventario
    /// </summary>
    public static class FuncionesRecursivas
    {
        // Calcular valor total del inventario recursivamente
        public static decimal CalcularValorTotalRecursivo(List<Producto> productos, int indice = 0)
        {
            if (indice >= productos.Count)
                return 0;

            decimal valorActual = productos[indice].Precio * productos[indice].Stock;
            return valorActual + CalcularValorTotalRecursivo(productos, indice + 1);
        }

        // Contar productos con stock bajo recursivamente
        public static int ContarStockBajoRecursivo(List<Producto> productos, int umbral, int indice = 0)
        {
            if (indice >= productos.Count)
                return 0;

            int cuenta = productos[indice].Stock < umbral ? 1 : 0;
            return cuenta + ContarStockBajoRecursivo(productos, umbral, indice + 1);
        }

        // Factorial para simulaciones (ejemplo académico)
        public static long FactorialRecursivo(int n)
        {
            if (n <= 1)
                return 1;
            return n * FactorialRecursivo(n - 1);
        }

        // Fibonacci para simulaciones (ejemplo académico)
        public static int FibonacciRecursivo(int n)
        {
            if (n <= 1)
                return n;
            return FibonacciRecursivo(n - 1) + FibonacciRecursivo(n - 2);
        }
    }
}