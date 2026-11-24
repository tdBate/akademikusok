namespace akademikusok;

public class Feladatok
{
    private List<Tag> tagok;
    private List<Tagsag> tagsagok;

    public Feladatok()
    {
        Adatbazis a = new Adatbazis();
        a.Feltolt();
        tagok = a.Tagok;
        tagsagok = a.Tagsagok;

        Jelenleg();
        Mas();
        Console.WriteLine();
        Mikor();
        Console.WriteLine();
        Arany();
        Console.WriteLine();
        Atlag();
        Console.WriteLine();
        Teller();
        Console.WriteLine();
        Rendes();
    }

    // 2. feladat
    private void Jelenleg()
    {
        var elok = tagok
            .Where(t => t.Elhunyt == null)
            .OrderBy(t => t.Nev)
            .Select(t => t.Nev);

        foreach (string nev in elok)
        {
            Console.WriteLine(nev);
        }
    }

    // 3. feladat
    private void Mas()
    {
        var valasztasok = tagsagok
            .Where(x => (x.Tipus == "r" || x.Tipus == "l") && x.Tag.Identitas != "")
            .OrderBy(x => x.Ev)
            .Select(x => $"{x.Tag.Nev} {x.Tag.Identitas} {x.Tipus} {x.Ev}");

        foreach (string sor in valasztasok)
        {
            Console.WriteLine(sor);
        }
    }

    // 4. feladat
    private void Mikor()
    {
        var elso = tagsagok
            .GroupBy(x => x.TagId)
            .Select(g => new { Nev = g.First().Tag.Nev, Ev = g.Min(x => x.Ev) })
            .OrderBy(x => x.Nev);

        foreach (var item in elso)
        {
            Console.WriteLine($"{item.Nev} {item.Ev}");
        }
    }

    // 5. feladat
    private void Arany()
    {
        int nok = tagok.Count(t => t.Nem == "n");
        double arany = (double)nok / tagok.Count;

        Console.WriteLine(Math.Round(arany, 4));
    }

    // 6. feladat
    private void Atlag()
    {
        var kulonbsegek = tagsagok
            .GroupBy(x => x.TagId)
            .Select(g => new
            {
                Levelezo = g.Where(x => x.Tipus == "l").Select(x => (int?)x.Ev).Min(),
                Rendes = g.Where(x => x.Tipus == "r").Select(x => (int?)x.Ev).Min()
            })
            .Where(x => x.Levelezo != null && x.Rendes != null)
            .Select(x => x.Rendes.GetValueOrDefault() - x.Levelezo.GetValueOrDefault());

        Console.WriteLine(Math.Round(kulonbsegek.Average(), 2));
    }

    // 7. feladat
    private void Teller()
    {
        Tag teller = tagok.First(t => t.Nev == "Teller Ede");
        int kezdet = tagsagok
            .Where(x => x.TagId == teller.Id && x.Tipus == "t")
            .Min(x => x.Ev);

        var tiszteletiek = tagsagok
            .Where(x => x.Tipus == "t"
                        && x.Ev <= kezdet
                        && (teller.Elhunyt == null
                            || x.Tag.Elhunyt == null
                            || x.Tag.Elhunyt >= teller.Elhunyt))
            .OrderBy(x => x.Ev)
            .ThenBy(x => x.Tag.Nev)
            .Select(x => $"{x.Tag.Nev} {x.Ev} {x.Tag.Elhunyt}");

        foreach (string sor in tiszteletiek)
        {
            Console.WriteLine(sor);
        }
    }

    // 8. feladat
    private void Rendes()
    {
        var rendesek = tagsagok
            .Where(x => x.Tipus == "r" && x.Ev >= 1901 && x.Ev <= 2000)
            .GroupBy(x => x.Ev)
            .OrderBy(g => g.Key);

        foreach (var csoport in rendesek)
        {
            Console.WriteLine(csoport.Key);

            foreach (var item in csoport.OrderBy(x => x.Tag.Nev))
            {
                Console.WriteLine($"    {item.Tag.Nev}");
            }
        }
    }
}
