using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SistemaInventarioInteligente.Modelos;
using SistemaInventarioInteligente.algoritmos_f;
using SistemaInventarioInteligente.estructura;

namespace SistemaInventarioInteligente
{



    #region Sistema Principal

    public class SistemaInventario
    {
        private HashTableInventario inventario;
        private ArbolCategorias arbolCategorias;
        private GrafoRutas grafoRutas;
        private Queue<Venta> colaVentas;
        private Stack<Venta> pilaDevoluciones;

        public SistemaInventario()
        {
            inventario = new HashTableInventario();
            arbolCategorias = new ArbolCategorias();
            grafoRutas = new GrafoRutas();
            colaVentas = new Queue<Venta>();
            pilaDevoluciones = new Stack<Venta>();
            InicializarDatos();
        }

        private void InicializarDatos()
        {
            // Productos de ejemplo
            var productos = new List<Producto>
            {
                new Producto(101, "Arroz 1kg", "Abarrotes", 25.50m, 50, "Sucursal Centro"),
                new Producto(102, "Frijol 1kg", "Abarrotes", 30.00m, 40, "Sucursal Norte"),
                new Producto(103, "Azúcar 1kg", "Abarrotes", 22.00m, 60, "Sucursal Centro"),
                new Producto(201, "Leche 1L", "Lacteos", 28.00m, 35, "Sucursal Sur"),
                new Producto(202, "Yogurt 1L", "Lacteos", 32.50m, 25, "Sucursal Norte"),
                new Producto(203, "Queso 500g", "Lacteos", 65.00m, 20, "Sucursal Centro"),
                new Producto(301, "Pan Blanco", "Panaderia", 18.00m, 45, "Sucursal Sur"),
                new Producto(302, "Pan Integral", "Panaderia", 22.00m, 30, "Sucursal Centro"),
                new Producto(401, "Manzanas 1kg", "Frutas", 35.00m, 55, "Sucursal Norte"),
                new Producto(402, "Naranjas 1kg", "Frutas", 28.00m, 48, "Sucursal Sur"),
                new Producto(501, "Jabón", "Limpieza", 15.50m, 70, "Sucursal Centro"),
                new Producto(502, "Detergente", "Limpieza", 45.00m, 38, "Sucursal Norte")
            };

            foreach (var producto in productos)
            {
                inventario.Agregar(producto);
                arbolCategorias.Insertar(producto);
            }

            // Configurar rutas entre sucursales
            grafoRutas.AgregarRuta("Sucursal Centro", "Sucursal Norte");
            grafoRutas.AgregarRuta("Sucursal Centro", "Sucursal Sur");
            grafoRutas.AgregarRuta("Sucursal Norte", "Sucursal Sur");
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO INTELIGENTE            ║");
                Console.WriteLine("║   Estructura de Datos - TecNM                              ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
                Console.WriteLine("\n[1]  Gestión de Inventario (HashTable)");
                Console.WriteLine("[2]  Buscar Producto (Búsqueda Binaria/Secuencial)");
                Console.WriteLine("[3]  Procesar Venta (Cola)");
                Console.WriteLine("[4]  Procesar Devolución (Pila)");
                Console.WriteLine("[5]  Ver Productos por Categoría (Árbol BST)");
                Console.WriteLine("[6]  Ver Rutas entre Sucursales (Grafo BFS/DFS)");
                Console.WriteLine("[7]  Ordenar Productos (QuickSort/ShellSort/RadixSort)");
                Console.WriteLine("[8]  Cálculos Recursivos");
                Console.WriteLine("[9]  Análisis de Eficiencia de Algoritmos");
                Console.WriteLine("[0]  Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MenuInventario();
                        break;
                    case "2":
                        MenuBusqueda();
                        break;
                    case "3":
                        ProcesarVenta();
                        break;
                    case "4":
                        ProcesarDevolucion();
                        break;
                    case "5":
                        VerProductosPorCategoria();
                        break;
                    case "6":
                        MenuRutas();
                        break;
                    case "7":
                        MenuOrdenamiento();
                        break;
                    case "8":
                        MenuRecursividad();
                        break;
                    case "9":
                        AnalizarEficiencia();
                        break;
                    case "0":
                        Console.WriteLine("\n¡Gracias por usar el sistema!");
                        return;
                    default:
                        Console.WriteLine("\nOpción inválida. Presione Enter para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void MenuInventario()
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE INVENTARIO (HashTable) ===\n");
            Console.WriteLine("[1] Ver todos los productos");
            Console.WriteLine("[2] Agregar producto");
            Console.WriteLine("[3] Eliminar producto");
            Console.WriteLine("[4] Buscar producto por ID");
            Console.Write("\nSeleccione opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    var todos = inventario.ObtenerTodos();
                    Console.WriteLine($"\n=== PRODUCTOS EN INVENTARIO ({todos.Count}) ===\n");
                    foreach (var p in todos.OrderBy(x => x.Id))
                    {
                        Console.WriteLine(p);
                    }
                    break;

                case "2":
                    Console.Write("\nID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Nombre: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Categoría: ");
                    string categoria = Console.ReadLine();
                    Console.Write("Precio: ");
                    decimal precio = decimal.Parse(Console.ReadLine());
                    Console.Write("Stock: ");
                    int stock = int.Parse(Console.ReadLine());
                    Console.Write("Sucursal: ");
                    string sucursal = Console.ReadLine();

                    var nuevoProducto = new Producto(id, nombre, categoria, precio, stock, sucursal);
                    inventario.Agregar(nuevoProducto);
                    arbolCategorias.Insertar(nuevoProducto);
                    Console.WriteLine("\n✓ Producto agregado exitosamente");
                    break;

                case "3":
                    Console.Write("\nID del producto a eliminar: ");
                    int idEliminar = int.Parse(Console.ReadLine());
                    if (inventario.Eliminar(idEliminar))
                        Console.WriteLine("\n✓ Producto eliminado");
                    else
                        Console.WriteLine("\n✗ Producto no encontrado");
                    break;

                case "4":
                    Console.Write("\nID del producto: ");
                    int idBuscar = int.Parse(Console.ReadLine());
                    var producto = inventario.Buscar(idBuscar);
                    if (producto != null)
                        Console.WriteLine($"\n{producto}");
                    else
                        Console.WriteLine("\n✗ Producto no encontrado");
                    break;
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void MenuBusqueda()
        {
            Console.Clear();
            Console.WriteLine("=== BÚSQUEDA DE PRODUCTOS ===\n");
            Console.Write("Ingrese ID del producto a buscar: ");
            int id = int.Parse(Console.ReadLine());

            var productos = inventario.ObtenerTodos();

            // Búsqueda Secuencial
            Stopwatch sw1 = Stopwatch.StartNew();
            var resultado1 = AlgoritmosBusqueda.BusquedaSecuencial(productos, id);
            sw1.Stop();

            // Búsqueda Binaria (primero ordenamos)
            var productosOrdenados = productos.OrderBy(p => p.Id).ToList();
            Stopwatch sw2 = Stopwatch.StartNew();
            var resultado2 = AlgoritmosBusqueda.BusquedaBinaria(productosOrdenados, id);
            sw2.Stop();

            Console.WriteLine("\n=== RESULTADOS ===");
            if (resultado1 != null)
            {
                Console.WriteLine($"\n✓ Producto encontrado: {resultado1}");
            }
            else
            {
                Console.WriteLine("\n✗ Producto no encontrado");
            }

            Console.WriteLine("\n=== COMPARACIÓN DE TIEMPOS ===");
            Console.WriteLine($"Búsqueda Secuencial: {sw1.Elapsed.TotalMilliseconds:F4} ms");
            Console.WriteLine($"Búsqueda Binaria: {sw2.Elapsed.TotalMilliseconds:F4} ms");

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void ProcesarVenta()
        {
            Console.Clear();
            Console.WriteLine("=== PROCESAR VENTA (Cola) ===\n");
            Console.Write("ID del producto: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Cantidad: ");
            int cantidad = int.Parse(Console.ReadLine());

            var producto = inventario.Buscar(id);
            if (producto != null && producto.Stock >= cantidad)
            {
                decimal total = producto.Precio * cantidad;
                var venta = new Venta(producto.Id, producto.Nombre, cantidad, total);
                colaVentas.Enqueue(venta);
                producto.Stock -= cantidad;

                Console.WriteLine($"\n✓ Venta procesada: {venta}");
                Console.WriteLine($"Ventas en cola: {colaVentas.Count}");
            }
            else
            {
                Console.WriteLine("\n✗ Stock insuficiente o producto no encontrado");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void ProcesarDevolucion()
        {
            Console.Clear();
            Console.WriteLine("=== PROCESAR DEVOLUCIÓN (Pila) ===\n");

            if (colaVentas.Count > 0)
            {
                var venta = colaVentas.Dequeue();
                pilaDevoluciones.Push(venta);

                var producto = inventario.Buscar(venta.ProductoId);
                if (producto != null)
                {
                    producto.Stock += venta.Cantidad;
                }

                Console.WriteLine($"✓ Devolución procesada: {venta}");
                Console.WriteLine($"Devoluciones en pila: {pilaDevoluciones.Count}");
            }
            else
            {
                Console.WriteLine("✗ No hay ventas para devolver");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void VerProductosPorCategoria()
        {
            Console.Clear();
            Console.WriteLine("=== PRODUCTOS POR CATEGORÍA (Árbol BST) ===");
            arbolCategorias.MostrarInOrden();

            Console.WriteLine("\n\n¿Desea buscar una categoría específica? (S/N): ");
            if (Console.ReadLine().ToUpper() == "S")
            {
                Console.Write("Nombre de la categoría: ");
                string categoria = Console.ReadLine();
                var productos = arbolCategorias.BuscarPorCategoria(categoria);

                if (productos.Count > 0)
                {
                    Console.WriteLine($"\n=== PRODUCTOS EN {categoria.ToUpper()} ===");
                    foreach (var p in productos)
                    {
                        Console.WriteLine(p);
                    }
                }
                else
                {
                    Console.WriteLine("\n✗ No se encontraron productos en esa categoría");
                }
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void MenuRutas()
        {
            Console.Clear();
            Console.WriteLine("=== RUTAS ENTRE SUCURSALES (Grafo) ===");
            grafoRutas.MostrarRutas();

            Console.WriteLine("\n[1] Recorrido BFS");
            Console.WriteLine("[2] Recorrido DFS");
            Console.Write("\nSeleccione opción: ");
            string opcion = Console.ReadLine();

            Console.Write("Sucursal de inicio: ");
            string inicio = Console.ReadLine();

            List<string> recorrido;

            if (opcion == "1")
            {
                recorrido = grafoRutas.BFS(inicio);
                Console.WriteLine($"\n=== RECORRIDO BFS desde {inicio} ===");
            }
            else
            {
                recorrido = grafoRutas.DFS(inicio);
                Console.WriteLine($"\n=== RECORRIDO DFS desde {inicio} ===");
            }

            Console.WriteLine(string.Join(" -> ", recorrido));

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void MenuOrdenamiento()
        {
            Console.Clear();
            Console.WriteLine("=== ORDENAMIENTO DE PRODUCTOS ===\n");
            Console.WriteLine("[1] QuickSort por Precio");
            Console.WriteLine("[2] ShellSort por Stock");
            Console.WriteLine("[3] RadixSort por ID");
            Console.Write("\nSeleccione opción: ");
            string opcion = Console.ReadLine();

            var productos = new List<Producto>(inventario.ObtenerTodos());
            Stopwatch sw = new Stopwatch();

            switch (opcion)
            {
                case "1":
                    sw.Start();
                    AlgoritmosOrdenamiento.QuickSort(productos, 0, productos.Count - 1);
                    sw.Stop();
                    Console.WriteLine("\n=== PRODUCTOS ORDENADOS POR PRECIO (QuickSort) ===");
                    break;

                case "2":
                    sw.Start();
                    AlgoritmosOrdenamiento.ShellSort(productos);
                    sw.Stop();
                    Console.WriteLine("\n=== PRODUCTOS ORDENADOS POR STOCK (ShellSort) ===");
                    break;

                case "3":
                    sw.Start();
                    AlgoritmosOrdenamiento.RadixSort(productos);
                    sw.Stop();
                    Console.WriteLine("\n=== PRODUCTOS ORDENADOS POR ID (RadixSort) ===");
                    break;
            }

            foreach (var p in productos)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine($"\nTiempo de ejecución: {sw.Elapsed.TotalMilliseconds:F4} ms");
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void MenuRecursividad()
        {
            Console.Clear();
            Console.WriteLine("=== FUNCIONES RECURSIVAS ===\n");
            Console.WriteLine("[1] Calcular valor total del inventario");
            Console.WriteLine("[2] Contar productos con stock bajo");
            Console.WriteLine("[3] Calcular factorial (simulación)");
            Console.WriteLine("[4] Calcular Fibonacci (simulación)");
            Console.Write("\nSeleccione opción: ");
            string opcion = Console.ReadLine();

            var productos = inventario.ObtenerTodos();

            switch (opcion)
            {
                case "1":
                    Stopwatch sw1 = Stopwatch.StartNew();
                    decimal valorTotal = FuncionesRecursivas.CalcularValorTotalRecursivo(productos);
                    sw1.Stop();
                    Console.WriteLine($"\n✓ Valor total del inventario: ${valorTotal:F2}");
                    Console.WriteLine($"Tiempo de ejecución: {sw1.Elapsed.TotalMilliseconds:F4} ms");
                    break;

                case "2":
                    Console.Write("\nUmbral de stock bajo: ");
                    int umbral = int.Parse(Console.ReadLine());
                    Stopwatch sw2 = Stopwatch.StartNew();
                    int cantidad = FuncionesRecursivas.ContarStockBajoRecursivo(productos, umbral);
                    sw2.Stop();
                    Console.WriteLine($"\n✓ Productos con stock < {umbral}: {cantidad}");
                    Console.WriteLine($"Tiempo de ejecución: {sw2.Elapsed.TotalMilliseconds:F4} ms");
                    break;

                case "3":
                    Console.Write("\nNúmero para calcular factorial: ");
                    int n = int.Parse(Console.ReadLine());
                    if (n > 20)
                    {
                        Console.WriteLine("\n✗ Número muy grande (máximo 20)");
                    }
                    else
                    {
                        Stopwatch sw3 = Stopwatch.StartNew();
                        long factorial = FuncionesRecursivas.FactorialRecursivo(n);
                        sw3.Stop();
                        Console.WriteLine($"\n✓ Factorial de {n} = {factorial}");
                        Console.WriteLine($"Tiempo de ejecución: {sw3.Elapsed.TotalMilliseconds:F4} ms");
                    }
                    break;

                case "4":
                    Console.Write("\nPosición de Fibonacci: ");
                    int pos = int.Parse(Console.ReadLine());
                    if (pos > 30)
                    {
                        Console.WriteLine("\n✗ Número muy grande (máximo 30)");
                    }
                    else
                    {
                        Stopwatch sw4 = Stopwatch.StartNew();
                        int fibonacci = FuncionesRecursivas.FibonacciRecursivo(pos);
                        sw4.Stop();
                        Console.WriteLine($"\n✓ Fibonacci({pos}) = {fibonacci}");
                        Console.WriteLine($"Tiempo de ejecución: {sw4.Elapsed.TotalMilliseconds:F4} ms");
                    }
                    break;
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }

        private void AnalizarEficiencia()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        ANÁLISIS DE EFICIENCIA DE ALGORITMOS               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

            var productos = inventario.ObtenerTodos();
            int n = productos.Count;

            Console.WriteLine($"Tamaño del conjunto de datos: {n} productos\n");

            // Análisis de Búsqueda
            Console.WriteLine("=== ALGORITMOS DE BÚSQUEDA ===\n");
            
            int idBuscar = productos[productos.Count / 2].Id;
            
            Stopwatch sw = Stopwatch.StartNew();
            AlgoritmosBusqueda.BusquedaSecuencial(productos, idBuscar);
            sw.Stop();
            double tiempoSecuencial = sw.Elapsed.TotalMilliseconds;

            var productosOrdenados = productos.OrderBy(p => p.Id).ToList();
            sw.Restart();
            AlgoritmosBusqueda.BusquedaBinaria(productosOrdenados, idBuscar);
            sw.Stop();
            double tiempoBinaria = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Búsqueda Secuencial:  {tiempoSecuencial:F6} ms  [Complejidad: O(n)]");
            Console.WriteLine($"Búsqueda Binaria:     {tiempoBinaria:F6} ms  [Complejidad: O(log n)]");
            Console.WriteLine($"Mejora: {(tiempoSecuencial / tiempoBinaria):F2}x más rápida\n");

            // Análisis de Ordenamiento
            Console.WriteLine("=== ALGORITMOS DE ORDENAMIENTO ===\n");

            var lista1 = new List<Producto>(productos);
            sw.Restart();
            AlgoritmosOrdenamiento.QuickSort(lista1, 0, lista1.Count - 1);
            sw.Stop();
            double tiempoQuick = sw.Elapsed.TotalMilliseconds;

            var lista2 = new List<Producto>(productos);
            sw.Restart();
            AlgoritmosOrdenamiento.ShellSort(lista2);
            sw.Stop();
            double tiempoShell = sw.Elapsed.TotalMilliseconds;

            var lista3 = new List<Producto>(productos);
            sw.Restart();
            AlgoritmosOrdenamiento.RadixSort(lista3);
            sw.Stop();
            double tiempoRadix = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"QuickSort:   {tiempoQuick:F6} ms  [Complejidad: O(n log n) promedio]");
            Console.WriteLine($"ShellSort:   {tiempoShell:F6} ms  [Complejidad: O(n^1.5) aproximado]");
            Console.WriteLine($"RadixSort:   {tiempoRadix:F6} ms  [Complejidad: O(d * n)]");

            string masRapido = tiempoQuick < tiempoShell && tiempoQuick < tiempoRadix ? "QuickSort" :
                              tiempoShell < tiempoRadix ? "ShellSort" : "RadixSort";
            Console.WriteLine($"\nMás rápido: {masRapido}\n");

            // Análisis de Estructuras de Datos
            Console.WriteLine("=== ESTRUCTURAS DE DATOS ===\n");

            sw.Restart();
            inventario.Buscar(idBuscar);
            sw.Stop();
            double tiempoHash = sw.Elapsed.TotalMilliseconds;

            sw.Restart();
            arbolCategorias.BuscarPorCategoria("Abarrotes");
            sw.Stop();
            double tiempoArbol = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"HashTable (búsqueda):        {tiempoHash:F6} ms  [Complejidad: O(1) promedio]");
            Console.WriteLine($"Árbol BST (búsqueda):        {tiempoArbol:F6} ms  [Complejidad: O(log n)]");
            Console.WriteLine($"Cola (enqueue/dequeue):      O(1)");
            Console.WriteLine($"Pila (push/pop):             O(1)");
            Console.WriteLine($"Grafo (BFS/DFS):             O(V + E)\n");

            // Análisis de Recursividad
            Console.WriteLine("=== FUNCIONES RECURSIVAS ===\n");

            sw.Restart();
            FuncionesRecursivas.CalcularValorTotalRecursivo(productos);
            sw.Stop();
            double tiempoRecursivo = sw.Elapsed.TotalMilliseconds;

            decimal valorIterativo = 0;
            sw.Restart();
            foreach (var p in productos)
            {
                valorIterativo += p.Precio * p.Stock;
            }
            sw.Stop();
            double tiempoIterativo = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"Cálculo Recursivo:    {tiempoRecursivo:F6} ms  [Usa stack de llamadas]");
            Console.WriteLine($"Cálculo Iterativo:    {tiempoIterativo:F6} ms  [Más eficiente en memoria]");
            Console.WriteLine($"Diferencia: {(tiempoRecursivo / tiempoIterativo):F2}x\n");

            // Tabla resumen
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    TABLA RESUMEN                           ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ Operación              │ Tiempo (ms)  │ Complejidad       ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Búsqueda Secuencial    │ {tiempoSecuencial,-12:F6} │ O(n)              ║");
            Console.WriteLine($"║ Búsqueda Binaria       │ {tiempoBinaria,-12:F6} │ O(log n)          ║");
            Console.WriteLine($"║ QuickSort              │ {tiempoQuick,-12:F6} │ O(n log n)        ║");
            Console.WriteLine($"║ ShellSort              │ {tiempoShell,-12:F6} │ O(n^1.5)          ║");
            Console.WriteLine($"║ RadixSort              │ {tiempoRadix,-12:F6} │ O(d*n)            ║");
            Console.WriteLine($"║ HashTable              │ {tiempoHash,-12:F6} │ O(1)              ║");
            Console.WriteLine($"║ Árbol BST              │ {tiempoArbol,-12:F6} │ O(log n)          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            Console.WriteLine("\n=== CONCLUSIONES ===\n");
            Console.WriteLine("1. HashTable es la estructura más rápida para búsquedas directas");
            Console.WriteLine("2. Búsqueda binaria es significativamente más rápida que secuencial");
            Console.WriteLine("3. QuickSort generalmente es el algoritmo de ordenamiento más eficiente");
            Console.WriteLine("4. Las estructuras de datos apropiadas mejoran el rendimiento");
            Console.WriteLine("5. La recursividad es elegante pero puede ser menos eficiente");

            Console.WriteLine("\n\nPresione Enter para continuar...");
            Console.ReadLine();
        }
    }

    #endregion

    #region Programa Principal

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            try
            {
                SistemaInventario sistema = new SistemaInventario();
                sistema.MostrarMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
                Console.WriteLine("\nPresione Enter para salir...");
                Console.ReadLine();
            }
        }
    }

    #endregion
}
                    