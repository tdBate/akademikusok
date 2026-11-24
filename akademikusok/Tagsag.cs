namespace akademikusok;

public class Tagsag
{
    private int id;
    private Tag tag;
    private string tipus;
    private int ev;

    public Tagsag(int id, Tag tag, string tipus, int ev)
    {
        this.id = id;
        this.tag = tag;
        this.tipus = tipus;
        this.ev = ev;
    }

    public int Id => id;

    public int TagId => tag.Id;

    public Tag Tag
    {
        get => tag;
        set => tag = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Tipus
    {
        get => tipus;
        set => tipus = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Ev
    {
        get => ev;
        set => ev = value;
    }
}
