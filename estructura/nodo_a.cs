using System;
using SistemaInventarioInteligente.Modelos;

namespace SistemaInventarioInteligente.estructura
{
    /// <summary>
    /// Clase que representa un producto en el inventario
    /// </summary>
   public class NodoArbol
    {
        public string Categoria { get; set; }
        public List<Producto> Productos { get; set; }
        public NodoArbol Izquierda { get; set; }
        public NodoArbol Derecha { get; set; }

        public NodoArbol(string categoria)
        {
            Categoria = categoria;
            Productos = new List<Producto>();
            Izquierda = null;
            Derecha = null;
        }
    }
}