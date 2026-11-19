namespace ProjektALM_Denik;

using System.IO;

using System.Text.Json;

public class UlozisteDeniku
{
    public void Ulozit(Denik denik)
    {
        try
        {
            List<DenProUlozeni> data = denik.ZiskatDataProUlozeni();

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            string cesta = Path.GetFullPath("denik.json");

            File.WriteAllText(cesta, json);

            Console.WriteLine("Deník byl úspěšně uložen.");
            Console.WriteLine("Umístění souboru: " + cesta);
        }
        catch (Exception e)
        {
            Console.WriteLine("Deník se nepodařilo uložit.");
            Console.WriteLine("Důvod: " + e.Message);
        }
    }
    
    public void Nacist(Denik denik)
    {
        try
        {
        string cesta = Path.GetFullPath("denik.json");
        string json = File.ReadAllText(cesta);

        List<DenProUlozeni> data = JsonSerializer.Deserialize<List<DenProUlozeni>>(json) ?? new List<DenProUlozeni>();
        
        denik.NacistData(data);
        Console.WriteLine("Deník byl úspěšně načten.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Deník se nepodařilo načíst.");
            Console.WriteLine("Důvod: " + e.Message);
        }
        
    }
}