namespace akademikusok;

public class Tag
{
    private int id;
    private string nev;
    private string nem;
    private int? szuletett;
    private int? elhunyt;
    private string identitas;

    public Tag(int id, string nev, string nem, int? szuletett, int? elhunyt, string identitas)
    {
        this.id = id;
        this.nev = nev;
        this.nem = nem;
        this.szuletett = szuletett;
        this.elhunyt = elhunyt;
        this.identitas = identitas;
    }

    public int Id => id;

    public string Nev
    {
        get => nev;
        set => nev = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Nem
    {
        get => nem;
        set => nem = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int? Szuletett
    {
        get => szuletett;
        set => szuletett = value;
    }

    public int? Elhunyt
    {
        get => elhunyt;
        set => elhunyt = value;
    }

    public string Identitas
    {
        get => identitas;
        set => identitas = value ?? throw new ArgumentNullException(nameof(value));
    }
}
