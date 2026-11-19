namespace ProjektALM_Denik;

public class SpojovySeznamDni
{
    private UzelDen prvni;
    private UzelDen posledni;
    private int pocetDnu;

    public int PocetDnu => pocetDnu;
    public UzelDen Prvni => prvni;
    
    public bool JePrazdny()
    {
        return PocetDnu == 0;
    }

    public void PridatNaKonec(Den den)
    {
        if (JePrazdny())
        {
            UzelDen novy = new UzelDen(den);
            prvni = novy;
            posledni = novy;
        }
        else
        {
            UzelDen novy = new UzelDen(den);
            posledni.Dalsi = novy;
            posledni = novy;

        }

        pocetDnu++;
    }
    
    public UzelDen ZiskatPosledni()
    {
        return posledni;
    }
    
    public UzelDen ZiskatPredchozi(UzelDen uzel)
    {
        if (uzel == null || uzel == prvni)
        {
            return null;
        }

        UzelDen aktualni = prvni;

        while (aktualni != null)
        {
            if (aktualni.Dalsi == uzel)
            {
                return aktualni;
            }

            aktualni = aktualni.Dalsi;
        }

        return null;
    }
    
    public void VypisSeznam()
    {
        UzelDen aktualni = prvni;

        while (aktualni != null)
        {
            Console.WriteLine(aktualni.Den.Datum);
            aktualni = aktualni.Dalsi;
        }
    }

    public bool Obsahuje(Den den)
    {
        UzelDen aktualni = prvni;
        while (aktualni != null)
        {
            if (aktualni.Den.Datum == den.Datum)
            {
                return true;
            }

            aktualni = aktualni.Dalsi;
        }

        return false;
    }

    public void PridatZa(UzelDen aktualni, Den den)
    {
        if (aktualni == null)
        {
            return;
        }
        UzelDen novy = new UzelDen(den);
        novy.Dalsi = aktualni.Dalsi;
        aktualni.Dalsi = novy;
        if (aktualni == posledni)
        {
            posledni = novy;
        }

        pocetDnu++;
    }

    public void Odebrat(UzelDen uzel)
    {
        if (uzel == null || JePrazdny())
        {
            return;
        }

        UzelDen predchozi = null;
        UzelDen aktualni = prvni;

        if (uzel == prvni)
        {
            prvni = prvni.Dalsi;

            if (prvni == null)
            {
                posledni = null;
            }
        }
        else
        {
            while (aktualni != null)
            {
                if (aktualni.Dalsi == uzel)
                {
                    predchozi = aktualni;
                    break;
                }

                aktualni = aktualni.Dalsi;
            }

            if (predchozi == null)
            {
                return;
            }

            predchozi.Dalsi = uzel.Dalsi;

            if (uzel == posledni)
            {
                posledni = predchozi;
            }
        }

        uzel.Dalsi = null;
        pocetDnu--;
    }
}