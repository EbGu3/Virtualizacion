using StockDAL.Data;

namespace ManagmentStock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string opcionSeleccionada = "";

            FileManagmentStock fileManagmentStock = new();

            do
            {
                Console.WriteLine("...Bienvenido a TechSpace 💻");
                Console.WriteLine("\n");
                Console.WriteLine("\t Opciones 😎");
                Console.WriteLine("\t 1) Ver todos los productos");
                Console.WriteLine("\t 2) Obtener inventario de producto");
                Console.WriteLine("\t 3) Eliminar producto");
                Console.WriteLine("\t 4) Salir");
                Console.Write("> ");
                opcionSeleccionada = Console.ReadLine();
                opcionSeleccionada = opcionSeleccionada == null ? string.Empty : opcionSeleccionada;

                switch (opcionSeleccionada)
                {
                    case "1":
                        fileManagmentStock.PrintStock();
                        break;
                    case "2":
                        Console.WriteLine("Cual es el producto?");
                        Console.Write(">");
                        string nameProduct = Console.ReadLine();
                        int cantidadDisponible = fileManagmentStock.FindStockByProduct(nameProduct);
                        if (cantidadDisponible > 0)
                            Console.WriteLine($"Producto: {nameProduct} | Disponible : {cantidadDisponible}");
                        break;
                    case "3":
                        Console.WriteLine("3");
                        break;
                    case null:
                    default:
                        Console.WriteLine("Opcion no valida ❌");
                        break;
                }
                Console.ReadLine();
                Console.Clear();
            } while (!opcionSeleccionada.Equals("4"));
        }
    }
}
