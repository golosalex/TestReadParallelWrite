using TestReadParallelWrite;

internal class Program
{
    private static async Task Main(string[] args)
    {
        const int numOfReadWriters = 5; // для красивого вывода в консоль предполагается число пар читатель-писатель<15 по числу цветов консоли за исключением черного
        
        
        // предполагаю что код запустится на винде.
        Console.BufferWidth= numOfReadWriters*31;
        Console.SetWindowSize(200, 50);
        
        List<ReaderWriter> readesriters = new List<ReaderWriter>();
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < numOfReadWriters; i++)
        {
            readesriters.Add(new ReaderWriter(ReaderWriter.rnd.Next(1,4), ReaderWriter.rnd.Next(4), 5 ,i%10+1)); 
            tasks.Add(readesriters[i].Quest());
            
        }
        await Task.WhenAll(tasks);
        

        
    }
}