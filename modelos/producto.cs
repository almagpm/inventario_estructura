using System;

namespace SistemaInventarioInteligente.Modelos
{
    /// <summary>
    /// Clase que representa un producto en el inventario
    /// </summary>
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Sucursal { get; set; }

        public Producto(int id, string nombre, string categoria, decimal precio, int stock, string sucursal)
        {
            Id = id;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
            Stock = stock;
            Sucursal = sucursal;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nombre: {Nombre}, Categoría: {Categoria}, Precio: ${Precio:F2}, Stock: {Stock}, Sucursal: {Sucursal}";
        }
    }
}