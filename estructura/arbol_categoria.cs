using System;
using SistemaInventarioInteligente.Modelos;

namespace SistemaInventarioInteligente.estructura
{
    /// <summary>
    /// Clase que representa un producto en el inventario
    /// </summary>
    public class ArbolCategorias
    {
        public NodoArbol Raiz { get; private set; }

        public void Insertar(Producto producto)
        {
            Raiz = InsertarRecursivo(Raiz, producto);
        }

        private NodoArbol InsertarRecursivo(NodoArbol nodo, Producto producto)
        {
            if (nodo == null)
            {
                NodoArbol nuevoNodo = new NodoArbol(producto.Categoria);
                nuevoNodo.Productos.Add(producto);
                return nuevoNodo;
            }

            int comparacion = string.Compare(producto.Categoria, nodo.Categoria, StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
            {
                nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, producto);
            }
            else if (comparacion > 0)
            {
                nodo.Derecha = InsertarRecursivo(nodo.Derecha, producto);
            }
            else
            {
                nodo.Productos.Add(producto);
            }

            return nodo;
        }

        public List<Producto> BuscarPorCategoria(string categoria)
        {
            return BuscarRecursivo(Raiz, categoria);
        }

        private List<Producto> BuscarRecursivo(NodoArbol nodo, string categoria)
        {
            if (nodo == null)
                return new List<Producto>();

            int comparacion = string.Compare(categoria, nodo.Categoria, StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
                return BuscarRecursivo(nodo.Izquierda, categoria);
            else if (comparacion > 0)
                return BuscarRecursivo(nodo.Derecha, categoria);
            else
                return nodo.Productos;
        }

        public void MostrarInOrden()
        {
            MostrarInOrdenRecursivo(Raiz);
        }

        private void MostrarInOrdenRecursivo(NodoArbol nodo)
        {
            if (nodo != null)
            {
                MostrarInOrdenRecursivo(nodo.Izquierda);
                Console.WriteLine($"\nCategoría: {nodo.Categoria} ({nodo.Productos.Count} productos)");
                foreach (var producto in nodo.Productos)
                {
                    Console.WriteLine($"  - {producto.Nombre}");
                }
                MostrarInOrdenRecursivo(nodo.Derecha);
            }
        }
    }
}