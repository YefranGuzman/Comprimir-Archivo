using System;
using System.IO;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Construir la configuración para leer el appsettings.json
            var config = new ConfigurationBuilder()
                            .SetBasePath(AppContext.BaseDirectory)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();


            // Leer los valores de configuración
            var sourceFolder = config["Settings:SourceFolder"];
            var destinationFolder = config["Settings:DestinationFolder"];

            Console.WriteLine($"Carpeta de origen: {sourceFolder}");
            Console.WriteLine($"Carpeta de destino: {destinationFolder}");

            // Comprobar que las carpetas de origen y destino existen
            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine($"La carpeta de origen no existe: {sourceFolder}");
                return;
            }

            if (!Directory.Exists(destinationFolder))
            {
                Console.WriteLine($"La carpeta de destino no existe: {destinationFolder}");
                return;
            }

            // Crear el archivo ZIP en la carpeta de destino
            string zipPath = Path.Combine(destinationFolder, "archivo_comprimido.zip");

            // Comprimir la carpeta
            ZipFile.CreateFromDirectory(sourceFolder, zipPath);

            Console.WriteLine($"Carpeta comprimida exitosamente en: {zipPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
