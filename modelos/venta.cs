using System;

namespace SistemaInventarioInteligente.Modelos
{
    /// <summary>
    /// Clase que representa una venta en el inventario
    /// </summary>
    public class Venta
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }

        public Venta(int productoId, string nombreProducto, int cantidad, decimal total)
        {
            ProductoId = productoId;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
            Total = total;
            Fecha = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Venta: {NombreProducto} x{Cantidad} = ${Total:F2} - {Fecha:dd/MM/yyyy HH:mm}";
        }
    }
}