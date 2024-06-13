using System;
using System.Collections.Generic;
using System.Threading;

namespace ClaseHilos
{
    internal class Producto
    {
        public string Nombre { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public int CantidadEnStock { get; set; }

        public Producto(string nombre, decimal precioUnitario, int cantidadEnStock)
        {
            Nombre = nombre;
            PrecioUnitarioDolares = precioUnitario;
            CantidadEnStock = cantidadEnStock;
        }

        public void ActualizarStock()
        {
            CantidadEnStock += 10;
        }

        public void ActualizarPrecioDolar()
        {
            PrecioUnitarioDolares *= 500;
        }

        public void ActualizarPrecios()
        {
            PrecioUnitarioDolares *= 1.10m; // Ajustar precios según la política de precios (+10% por inflación)
        }
    }

    internal class Solution
    {
        static List<Producto> productos = new List<Producto>
        {
            new Producto("Camisa", 10, 50),
            new Producto("Pantalón", 8, 30),
            new Producto("Zapatilla/Champión", 7, 20),
            new Producto("Campera", 25, 100),
            new Producto("Gorra", 16, 10)
        };

        static Semaphore semaphore = new Semaphore(0, 3); // Semáforo con 3 tokens

        static void Tarea1()
        {
            Thread tarea1 = new Thread(() =>
            {
                Thread.CurrentThread.Name = "Tarea1";
                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} iniciado.");
                foreach (Producto producto in productos)
                {
                    producto.ActualizarStock();
                }
                semaphore.Release();
                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} finalizado.");
            });
            tarea1.Start();
        }

        static void Tarea2()
        {
            Thread tarea2 = new Thread(() =>
            {
                Thread.CurrentThread.Name = "Tarea2";
                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} iniciado.");
                foreach (Producto producto in productos)
                {
                    producto.ActualizarPrecioDolar();
                }
                semaphore.Release();
                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} finalizado.");
            });
            tarea2.Start();
        }

        static void Tarea3()
        {
            Thread tarea3 = new Thread(() =>
            {
                Thread.CurrentThread.Name = "Tarea3";
                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} iniciado.");
                semaphore.WaitOne();
                semaphore.WaitOne();
                foreach (Producto producto in productos)
                {
                    producto.ActualizarPrecios();
                }
                semaphore.Release();
                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} finalizado.");
            });
            tarea3.Start();
        }

        static void Tarea4()
        {
            Thread tarea4 = new Thread(() =>
            {
                Thread.CurrentThread.Name = "Tarea4";
                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} iniciado.");
                semaphore.WaitOne();
                Console.WriteLine("Informe de Inventario:");
                foreach (Producto producto in productos)
                {
                    Console.WriteLine($"Producto: {producto.Nombre}, Cantidad en Stock: {producto.CantidadEnStock}, Precio Total: {producto.CantidadEnStock * producto.PrecioUnitarioDolares}");
                }
                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} finalizado.");
            });
            tarea4.Start();
        }

        internal static void Execute()
        {
            Tarea1();
            Tarea2();
            Tarea3();
            Tarea4();

            Console.ReadLine();
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Threading;

//namespace ClaseHilos
//{
//    internal class Producto
//    {
//        public string Nombre { get; set; }
//        public decimal PrecioUnitarioDolares { get; set; }
//        public int CantidadEnStock { get; set; }

//        public Producto(string nombre, decimal precioUnitario, int cantidadEnStock)
//        {
//            Nombre = nombre;
//            PrecioUnitarioDolares = precioUnitario;
//            CantidadEnStock = cantidadEnStock;
//        }

//        public void ActualizarStock()
//        {
//            CantidadEnStock += 10;
//        }

//        public void ActualizarPrecioDolar()
//        {
//            PrecioUnitarioDolares *= 500;
//        }

//        public void ActualizarPrecios()
//        {
//            PrecioUnitarioDolares *= 1.10m; // Ajustar precios según la política de precios (+10% por inflación)
//        }
//    }

//    internal class Solution
//    {
//        static List<Producto> productos = new List<Producto>
//        {
//            new Producto("Camisa", 10, 50),
//            new Producto("Pantalón", 8, 30),
//            new Producto("Zapatilla/Champión", 7, 20),
//            new Producto("Campera", 25, 100),
//            new Producto("Gorra", 16, 10)
//        };

//        static Mutex mutex = new Mutex();

//        static void Tarea1()
//        {
//            Thread tarea1 = new Thread(() =>
//            {
//                Thread.CurrentThread.Name = "Tarea1";
//                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} iniciado.");
//                lock (mutex)
//                {
//                    foreach (Producto producto in productos)
//                    {
//                        producto.ActualizarStock();
//                    }
//                }
//                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} finalizado.");
//            });
//            tarea1.Start();
//        }

//        static void Tarea2()
//        {
//            Thread tarea2 = new Thread(() =>
//            {
//                Thread.CurrentThread.Name = "Tarea2";
//                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} iniciado.");
//                lock (mutex)
//                {
//                    foreach (Producto producto in productos)
//                    {
//                        producto.ActualizarPrecioDolar();
//                    }
//                }
//                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} finalizado.");
//            });
//            tarea2.Start();
//        }

//        static void Tarea3()
//        {
//            Thread tarea3 = new Thread(() =>
//            {
//                Thread.CurrentThread.Name = "Tarea3";
//                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} iniciado.");
//                lock (mutex)
//                {
//                    foreach (Producto producto in productos)
//                    {
//                        producto.ActualizarPrecios();
//                    }
//                }
//                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} finalizado.");
//            });
//            tarea3.Start();
//        }

//        static void Tarea4()
//        {
//            Thread tarea4 = new Thread(() =>
//            {
//                Thread.CurrentThread.Name = "Tarea4";
//                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} iniciado.");
//                lock (mutex)
//                {
//                    Console.WriteLine("Informe de Inventario:");
//                    foreach (Producto producto in productos)
//                    {
//                        Console.WriteLine($"Producto: {producto.Nombre}, Cantidad en Stock: {producto.CantidadEnStock}, Precio Total: {producto.CantidadEnStock * producto.PrecioUnitarioDolares}");
//                    }
//                }
//                Console.WriteLine($"Hilo {Thread.CurrentThread.Name} finalizado.");
//            });
//            tarea4.Start();
//        }

//        internal static void Execute()
//        {
//            Tarea1();
//            Tarea2();
//            Tarea3();
//            Tarea4();

//            Console.ReadLine();
//        }
//    }
//}