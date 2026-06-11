try
{
    SistemaBiblioteca prueba = new SistemaBiblioteca();

    //prueba.Iniciar();




}

catch (FileNotFoundException)
{
    Console.WriteLine("El archivo no existe.");
}
catch (DirectoryNotFoundException)
{
    Console.WriteLine("La ruta especificada no es válida.");
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("No posee los permisos suficientes.");
}
catch (IOException ex)
{
    Console.WriteLine($"Error de acceso al archivo: {ex.Message}");
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine();
Console.WriteLine("Presione una tecla para salir...");
Console.ReadKey();