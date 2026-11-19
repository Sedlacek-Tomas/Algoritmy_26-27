namespace ProjektALM_Denik;

public class SeznamZaznamu
{
    private Zaznam[] zaznamy;
    private int pocetZaznamu;
    
    public int PocetZaznamu => pocetZaznamu;

    public SeznamZaznamu()
    {
        this.zaznamy = new Zaznam[5];
        this.pocetZaznamu = 0;
    }

    public void Pridat(Zaznam zaznam)
    {
        if (pocetZaznamu == zaznamy.Length)
        {
            Zaznam[] novePole = new Zaznam[zaznamy.Length * 2];
            for (int i = 0; i < pocetZaznamu; i++)
            {
                novePole[i] = zaznamy[i];
            }

            zaznamy = novePole;
        }
        zaznamy[pocetZaznamu] = zaznam;
        pocetZaznamu++;
    }

    public Zaznam Ziskat(int index)
    {
        if (index < pocetZaznamu && index >= 0)
        {
            return zaznamy[index];
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    public void Smazat(int index)
    {
        if (index < pocetZaznamu && index >= 0)
        {
            for (int i = index; i < pocetZaznamu - 1; i++)
            {
                // Přesun následujícího záznamu na aktuální pozici
                zaznamy[i] = zaznamy[i + 1];
            }

            pocetZaznamu--;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    public void Vymazat()
    {
        zaznamy = new Zaznam[5];
        pocetZaznamu = 0;
    }

    public bool JePrazdny()
    {
        return pocetZaznamu == 0;
    }
}