using System;
using SistemaInventarioInteligente.Modelos;

namespace SistemaInventarioInteligente.estructura
{
    /// <summary>
    /// Clase que representa un producto en el inventario
    /// </summary>
   public class GrafoRutas
    {
        private Dictionary<string, List<string>> adyacencias;

        public GrafoRutas()
        {
            adyacencias = new Dictionary<string, List<string>>();
        }

        public void AgregarSucursal(string sucursal)
        {
            if (!adyacencias.ContainsKey(sucursal))
            {
                adyacencias[sucursal] = new List<string>();
            }
        }

        public void AgregarRuta(string origen, string destino)
        {
            AgregarSucursal(origen);
            AgregarSucursal(destino);
            adyacencias[origen].Add(destino);
            adyacencias[destino].Add(origen); // Grafo no dirigido
        }

        public List<string> BFS(string inicio)
        {
            List<string> visitados = new List<string>();
            Queue<string> cola = new Queue<string>();

            cola.Enqueue(inicio);
            visitados.Add(inicio);

            while (cola.Count > 0)
            {
                string actual = cola.Dequeue();
                
                if (adyacencias.ContainsKey(actual))
                {
                    foreach (var vecino in adyacencias[actual])
                    {
                        if (!visitados.Contains(vecino))
                        {
                            visitados.Add(vecino);
                            cola.Enqueue(vecino);
                        }
                    }
                }
            }

            return visitados;
        }

        public List<string> DFS(string inicio)
        {
            List<string> visitados = new List<string>();
            DFSRecursivo(inicio, visitados);
            return visitados;
        }

        private void DFSRecursivo(string nodo, List<string> visitados)
        {
            visitados.Add(nodo);

            if (adyacencias.ContainsKey(nodo))
            {
                foreach (var vecino in adyacencias[nodo])
                {
                    if (!visitados.Contains(vecino))
                    {
                        DFSRecursivo(vecino, visitados);
                    }
                }
            }
        }

        public void MostrarRutas()
        {
            Console.WriteLine("\n=== MAPA DE RUTAS ENTRE SUCURSALES ===");
            foreach (var sucursal in adyacencias.Keys)
            {
                Console.Write($"{sucursal} -> ");
                Console.WriteLine(string.Join(", ", adyacencias[sucursal]));
            }
        }
    }
}