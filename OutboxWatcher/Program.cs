using Npgsql;
using OutboxWatcher;
using System.Text;
using System.Text.Json;

class Program
{
    private static readonly int interval = 10000; // 10 sekunder
    private static readonly HttpClient httpClient = new HttpClient();

    static void Main(string[] args)
    {
        Console.WriteLine("Starter Outbox-processor...");

        string connectionString = "Host=116.203.199.232;Port=5432;Database=ClubDB;Username=Ea;Password=EaErSjov";
        var processor = new OutboxProcessor(connectionString);

        var timer = new Timer(async _ => await processor.RunAsync(), null, 0, 10000);

        Console.WriteLine("Tryk ENTER for at afslutte...");
        Console.ReadLine();
    }

}
