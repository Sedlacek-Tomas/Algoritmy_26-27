
using System;
using System.Collections.Generic;

namespace ProjektALM_Denik;

public class Denik
{
    private SpojovySeznamDni dny;
    private UzelDen aktualniDen;

    public Denik()
    {
        dny = new SpojovySeznamDni();
        aktualniDen = null;
    }

    public void PridatNovyDen(Den den)
    {
        dny.PridatNaKonec(den);
        aktualniDen = dny.ZiskatPosledni();
    }

    public void DalsiDen()
    {
        if (aktualniDen != null && aktualniDen.Dalsi != null)
        {
            aktualniDen = aktualniDen.Dalsi;
        }
    }

    public void PredchoziDen()
    {
        if (aktualniDen == null || aktualniDen == dny.Prvni)
        {
            return;
        }

        UzelDen aktualni = dny.Prvni;

        while (aktualni.Dalsi != aktualniDen)
        {
            aktualni = aktualni.Dalsi;
        }

        aktualniDen = aktualni;
    }

    public void Zacatek()
    {
        if (!dny.JePrazdny())
        {
            aktualniDen = dny.Prvni;
        }
    }

    public void Konec()
    {
        if (!dny.JePrazdny())
        {
            aktualniDen = dny.ZiskatPosledni();
        }
    }

    public void PridatZaznam(string text)
    {
        if (aktualniDen != null)
        {
            Zaznam zaznam = new Zaznam(text);
            aktualniDen.Den.Zaznamy.Pridat(zaznam);
        }
    }

    public int PocetZaznamu()
    {
        int pocet = 0;
        UzelDen aktualni = dny.Prvni;

        while (aktualni != null)
        {
            pocet += aktualni.Den.Zaznamy.PocetZaznamu;
            aktualni = aktualni.Dalsi;
        }

        return pocet;
    }

    public int PocetDnu()
    {
        return dny.PocetDnu;
    }

    public Den ZiskatAktualniDen()
    {
        if (aktualniDen == null)
        {
            return null;
        }

        return aktualniDen.Den;
    }

    public void SmazatZaznam(int index)
    {
        if (aktualniDen != null)
        {
            aktualniDen.Den.Zaznamy.Smazat(index);
            SmazatPrazdnyDen();
        }
    }

    public void SmazatPrazdnyDen()
    {
        if (aktualniDen == null)
        {
            return;
        }

        if (aktualniDen.Den.Zaznamy.PocetZaznamu == 0)
        {
            UzelDen dalsi = aktualniDen.Dalsi;
            UzelDen predchozi = dny.ZiskatPredchozi(aktualniDen);

            dny.Odebrat(aktualniDen);

            if (dalsi != null)
            {
                aktualniDen = dalsi;
            }
            else if (predchozi != null)
            {
                aktualniDen = predchozi;
            }
            else
            {
                aktualniDen = null;
            }
        }
    }

    public void PridatDen(DateTime datum)
    {
        Den den = new Den(datum);

        if (dny.JePrazdny())
        {
            dny.PridatNaKonec(den);
            aktualniDen = dny.ZiskatPosledni();
        }
        else
        {
            dny.PridatZa(aktualniDen, den);
            aktualniDen = aktualniDen.Dalsi;
        }
    }

    public void PridatZaznamPodleData(DateTime datum, string text)
    {
        if (aktualniDen == null)
        {
            PridatDen(datum);
            PridatZaznam(text);
            return;
        }

        if (datum.Date == aktualniDen.Den.Datum.Date)
        {
            PridatZaznam(text);
        }
        else if (datum.Date == aktualniDen.Den.Datum.Date.AddDays(1))
        {
            PridatDen(datum);
            PridatZaznam(text);
        }
        else
        {
            Console.WriteLine("Neplatné datum. Můžeš přidat záznam pouze k aktuálnímu dni nebo k následujícímu dni.");
        }
    }

    public void VypisAktualniDen()
    {
        if (aktualniDen != null)
        {
            Console.WriteLine("Aktuální den: " +
                              aktualniDen.Den.Datum.ToString("dd.MM.yyyy"));

            for (int i = 0; i < aktualniDen.Den.Zaznamy.PocetZaznamu; i++)
            {
                Console.WriteLine((i + 1) + ". " +
                                  aktualniDen.Den.Zaznamy.Ziskat(i).Text);
            }
        }
    }

    public void VypisInformace()
    {
        Console.WriteLine("Počet dnů: " + PocetDnu());
        Console.WriteLine("Počet záznamů v aktuálním dni: " +
                          PocetZaznamuAktualnihoDne());
    }

    public bool JePrazdny()
    {
        return dny.JePrazdny();
    }

    public bool JePlatneDatum(DateTime datum)
    {
        if (JePrazdny())
        {
            return true;
        }
        else if (aktualniDen.Den.Datum.Date == datum.Date)
        {
            return true;
        }
        else if (aktualniDen.Den.Datum.Date.AddDays(1) == datum.Date)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<DenProUlozeni> ZiskatDataProUlozeni()
    {
        List<DenProUlozeni> data = new List<DenProUlozeni>();

        UzelDen aktualni = dny.Prvni;

        while (aktualni != null)
        {
            DenProUlozeni den = new DenProUlozeni();

            den.Datum = aktualni.Den.Datum;

            List<string> zaznamy = new List<string>();

            for (int i = 0; i < aktualni.Den.Zaznamy.PocetZaznamu; i++)
            {
                zaznamy.Add(aktualni.Den.Zaznamy.Ziskat(i).Text);
            }

            den.Zaznamy = zaznamy;

            data.Add(den);

            aktualni = aktualni.Dalsi;
        }

        return data;
    }

    public void NacistData(List<DenProUlozeni> data)
    {
        dny = new SpojovySeznamDni();
        aktualniDen = null;

        foreach (var den in data)
        {
            Den novyDen = new Den(den.Datum);

            foreach (var zaznam in den.Zaznamy)
            {
                Zaznam novyZaznam = new Zaznam(zaznam);
                novyDen.Zaznamy.Pridat(novyZaznam);
            }

            PridatNovyDen(novyDen);
        }
    }

    public int PocetZaznamuAktualnihoDne()
    {
        if (aktualniDen == null)
        {
            return 0;
        }

        return aktualniDen.Den.Zaznamy.PocetZaznamu;
    }
}
