namespace ProjektALM_Denik;

public class Den
{
    private DateTime datum;
    private SeznamZaznamu zaznamy;

    public DateTime Datum => datum;
    public SeznamZaznamu Zaznamy => zaznamy;


    public Den(DateTime datum)
    {
        this.datum = datum;
        this.zaznamy = new SeznamZaznamu();
    }
}