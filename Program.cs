namespace Homework_8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Для использования укажите через пробел расширение файла и искомый текст");
                return;
            }

            string fileExtension = args[0];
            string searchText = args[1];

            string currentDirectory = Directory.GetCurrentDirectory();

            try
            {
                SearchFilesRecursive(currentDirectory, fileExtension, searchText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Что-то пошло не так: {ex.Message}");
            }
        }

        static void SearchFilesRecursive(string directory, string fileExtension, string searchText)
        {
            string[] matchingFiles = Directory.GetFiles(directory, $"*.{fileExtension}");

            foreach (string file in matchingFiles)
            {
                if (FileContainsText(file, searchText))
                {
                    Console.WriteLine(file);
                }
            }

            string[] subDirectories = Directory.GetDirectories(directory);

            foreach (string subDirectory in subDirectories)
            {
                SearchFilesRecursive(subDirectory, fileExtension, searchText);
            }
        }

        static bool FileContainsText(string filePath, string searchText)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.Contains(searchText))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}