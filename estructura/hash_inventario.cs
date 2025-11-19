using System;
using SistemaInventarioInteligente.Modelos;

namespace SistemaInventarioInteligente.estructura
{
    /// <summary>
    /// Clase que representa un producto en el inventario
    /// </summary>
    /// 
    public class HashTableInventario
    {
        private List<Producto>[] tabla;
        private int capacidad;

        public HashTableInventario(int capacidad = 100)
        {
            this.capacidad = capacidad;
            tabla = new List<Producto>[capacidad];
            for (int i = 0; i < capacidad; i++)
            {
                tabla[i] = new List<Producto>();
            }
        }

        private int Hash(int id)
        {
            return Math.Abs(id % capacidad);
        }

        public void Agregar(Producto producto)
        {
            int indice = Hash(producto.Id);
            tabla[indice].Add(producto);
        }

       public Producto Buscar(int id)
        {
            int indice = Hash(id);
            return tabla[indice].FirstOrDefault(p => p.Id == id);
        }

        public bool Eliminar(int id)
        {
            int indice = Hash(id);
            var producto = tabla[indice].FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                tabla[indice].Remove(producto);
                return true;
            }
            return false;
        }

        public List<Producto> ObtenerTodos()
        {
            List<Producto> todos = new List<Producto>();
            foreach (var lista in tabla)
            {
                todos.AddRange(lista);
            }
            return todos;
        }
    }

   
}