
using System;
using System.Globalization;
using System.Text;

namespace ProjektALM_Denik;

public class UzivatelskeRozhrani
{
    private Denik denik;
    private UlozisteDeniku uloziste;

    public UzivatelskeRozhrani(Denik denik)
    {
        this.denik = denik;
        uloziste = new UlozisteDeniku();
    }

    public void Spustit()
    {
        string prikaz = "";

        do
        {
            Console.Clear();
            VypisUvodniObrazovku();
            VypisStavDeniku();
            VypisPrikazy();

            Console.Write("Zadej příkaz: ");
            prikaz = Console.ReadLine();

            switch (prikaz)
            {
                case "novy":
                    PridatNovyZaznam();
                    break;

                case "nacti":
                    uloziste.Nacist(denik);
                    Console.ReadKey();
                    break;

                case "dalsi":
                    denik.DalsiDen();
                    break;

                case "predchozi":
                    denik.PredchoziDen();
                    break;

                case "zacatek":
                    denik.Zacatek();
                    break;

                case "konec":
                    denik.Konec();
                    break;

                case "smaz":
                    SmazatZaznam();
                    break;

                case "uloz":
                    uloziste.Ulozit(denik);
                    Console.ReadKey();
                    break;

                case "zavri":
                    break;

                default:
                    Console.WriteLine("Chybný příkaz. Zkus to znovu.");
                    Console.ReadKey();
                    break;
            }

        } while (prikaz != "zavri");
    }

    private void VypisUvodniObrazovku()
    {
        Console.WriteLine("========================\n       MŮJ DENÍK\n========================");
    }

    private void VypisStavDeniku()
    {
        if (denik.JePrazdny())
        {
            Console.WriteLine("Deník je prázdný.");
        }
        else
        {
            denik.VypisAktualniDen();
            denik.VypisInformace();
        }
    }

    private void VypisPrikazy()
    {
        if (denik.JePrazdny())
        {
            Console.WriteLine(
                "Dostupné příkazy:\nnovy  - Přidat nový záznam\nnacti - Načíst deník\nuloz - Uložit deník do souboru\nzavri - Ukončit program");
        }
        else
        {
            Console.WriteLine(
                "Dostupné příkazy:\nnovy  - Přidat nový záznam\nnacti - Načíst deník\ndalsi - Přejít na následující den\npredchozi  - Přejít na předchozí den\nzacatek - Přejít na první den\nkonec - Přejít na poslední den\nsmaz - Smazat aktuální záznam\nuloz - Ulož deník do souboru\nzavri - Ukončit program");
        }
    }

    private void PridatNovyZaznam()
    {
        bool vysledek = false;
        DateTime prevedenyDatum;

        do
        {
            Console.Write("Zadejte datum (dd.MM.yyyy): ");
            string datumText = Console.ReadLine();

            vysledek = DateTime.TryParseExact(
                datumText,
                "dd.MM.yyyy",
                null,
                DateTimeStyles.None,
                out prevedenyDatum);

            if (!vysledek)
            {
                Console.WriteLine("Neplatný formát data, zadejte ho znovu.");
                continue;
            }

            if (!denik.JePlatneDatum(prevedenyDatum))
            {
                Console.WriteLine("Neplatné datum. Můžeš přidat záznam pouze k aktuálnímu nebo následujícímu dni.");
                vysledek = false;
            }

        } while (!vysledek);

        Console.WriteLine("Zadejte text záznamu.");
        Console.WriteLine("Zadávání ukončíte napsáním 'konec' na samostatný řádek.");

        StringBuilder text = new StringBuilder();
        bool prvniRadek = true;

        while (true)
        {
            string radek = Console.ReadLine() ?? "konec";

            if (radek == "konec")
            {
                break;
            }

            if (!prvniRadek)
            {
                text.AppendLine();
            }

            text.Append(radek);
            prvniRadek = false;
        }

        denik.PridatZaznamPodleData(prevedenyDatum, text.ToString());
    }

    private void SmazatZaznam()
    {
        Console.Clear();

        Den den = denik.ZiskatAktualniDen();

        if (den == null)
        {
            Console.WriteLine("Není žádný den k mazání.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Záznamy pro aktuální den:");

        for (int i = 0; i < den.Zaznamy.PocetZaznamu; i++)
        {
            Console.Write(i + 1 + ".");
            Console.WriteLine(den.Zaznamy.Ziskat(i).Text);
        }

        int index;
        bool platnyVstup;

        Console.WriteLine("Zadejte číslo záznamu ke smazání");

        do
        {
            string vstup = Console.ReadLine();

            platnyVstup = int.TryParse(vstup, out index);

            if (!platnyVstup)
            {
                Console.WriteLine("Neplatné zadání. Zadejte celé číslo:");
            }
            else if (index < 1 || index > den.Zaznamy.PocetZaznamu)
            {
                Console.WriteLine("Takový záznam neexistuje. Zadejte jiné číslo:");
                platnyVstup = false;
            }

        } while (!platnyVstup);

        Console.WriteLine(den.Zaznamy.Ziskat(index - 1).Text);

        char znak = ' ';

        while (znak != 'y' && znak != 'n')
        {
            Console.WriteLine("Opravdu chcete tento záznam smazat? (y/n)");
            znak = char.ToLower(Console.ReadKey().KeyChar);
        }

        if (znak == 'y')
        {
            denik.SmazatZaznam(index - 1);
        }
    }
}
