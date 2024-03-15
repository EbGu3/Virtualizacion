using StockDAL.NewFolder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDAL.Data
{
    public class FileManagmentStock
    {
        private List<Stock> stockNow = new List<Stock>();

        public FileManagmentStock ()
        {
            SetMock();
        }

        private void SetMock ()
        {
            var pathFileStock = GePathStockFile();
            if (File.Exists(pathFileStock))
                File.Delete(pathFileStock);

            List<Stock> stockList = new()
            {
                new Stock {
                    Id = GetId(),
                    Product = "IPad Mini",
                    Description = "IOS 16, Color rojo, Ram 8 GB",
                    Disponible = true,
                    Amount = 10
                },
                new Stock {
                    Id = GetId(),
                    Product = "Samsung Tablet",
                    Description = "Android 18, Color azul, Ram 12 GB",
                    Disponible = true, Amount = 4
                },
                new Stock {
                    Id = GetId(),
                    Product = "Razer Blade",
                    Description = "Windows 10 Home, Ram 32GB, SSD 1TB",
                    Disponible = true,
                    Amount = 4
                },
                new Stock {
                    Id = GetId(),
                    Product = "Sony Audifonos",
                    Description = "Bluetooth, Color Negro, 12 horas de uso",
                    Disponible = true,
                    Amount = 4
                }
            };

            WriteInFile(stockList);
        }

        private string GetId ()
            => $"{Guid.NewGuid().ToString("N")}{DateTime.Now.ToString("fffHHmmss")}";

        private void WriteInFile (List<Stock> productos)
        {
            try
            {
                string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StockData");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string pathStockFile = GePathStockFile();

                using StreamWriter streamWriter = new StreamWriter(pathStockFile, true);
                foreach (var productItem in productos)
                {
                    string lineWrite = $"{productItem.Id} | {productItem.Product} | {productItem.Description} | {productItem.Disponible} | {productItem.Amount}";
                    streamWriter.WriteLine(lineWrite);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ReadInFile ()
        {
            string pathStockFile = GePathStockFile();
            using StreamReader streamReader = new StreamReader(pathStockFile);
            while (!streamReader.EndOfStream) {
                string? readLine = streamReader.ReadLine();
                if (readLine == null)
                    continue;

                string[] productInfo = readLine.Split('|');
                Stock product = new() {
                     Id = productInfo[0],
                     Product = productInfo[1],
                     Description = productInfo[2],
                     Disponible = Convert.ToBoolean(productInfo[3]),
                     Amount = Convert.ToInt32(productInfo[4])
                };
                stockNow.Add(product);
            }
        }

        private string GePathStockFile ()
        {
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StockData");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return Path.Combine(directory, "stockFile.txt");
        }

        public bool UpdateStock (string productName, int amount)
        {
            try
            {
                ReadInFile();
                if (stockNow.Count < 1)
                    throw new Exception("No hay productos registrados");

                var indexProduct = stockNow
                                    .FindIndex(productItem => productItem.Product == productName);

                if (indexProduct.Equals(-1))
                    throw new Exception($"El producto no esta registro en el inventario. Producto = {amount}");

                stockNow[indexProduct].Amount = amount;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar producto: {ex.Message}");
                return false;
            }
        }

        public bool DeleteProduct (string productName)
        {
            try
            {
                ReadInFile();
                if (stockNow.Count < 1)
                    throw new Exception("No hay productos registrados");

                stockNow = stockNow.Where(productItem => productItem.Product != productName)
                                   .ToList();

                WriteInFile(stockNow);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al borrar producto: {ex.Message}");
                return false;
            }
        }

        public int FindStockByProduct(string productName)
        {
            try
            {
                ReadInFile();
                if (stockNow.Count < 1)
                    throw new Exception("No hay productos registrados");

                var productFind = stockNow.Where(productItem => productItem.Product == productName)
                                          .FirstOrDefault();

                if (productFind == null)
                    throw new Exception($"No existe el producto en el inventario. Producto = {productName}");

                return productFind.Amount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al encontrar el producto: {ex.Message}");
                return -1;
            }
        }
        
        public void PrintStock()
        {
            ReadInFile();
            foreach (var product in stockNow)
            {
                Console.WriteLine($"Producto: {product.Product} | Precio: {product.Amount} | Descripcion: {product.Description} | Disponible: {product.Disponible}");
            }
        }
    }
}
