namespace ProjektALM_Denik;

public class UzelDen
{
    private Den den;
    private UzelDen dalsi;

    public Den Den => den;
    public UzelDen Dalsi
    {
        get => dalsi;
        set => dalsi = value;
    }

    public UzelDen(Den den)
    {
        this.den = den;
    }
}