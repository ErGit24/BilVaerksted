using static System.Console;

namespace Værsted.Code;

class Program
{
    public readonly List<BilInfo> biler = new List<BilInfo>(); // Opretter en liste til at gemme BilInfo-objekter på


    static void Main(string[] args)
    {
        new Program().Start(); // Opretter en ny instans af Program-klassen under og kalder den foir Start-metoden
    }

    public void Start()
    {
        while (true)
        {
            // Viser en liste over registrerede biler eller en besked om, at der ikke er nogen biler endnu indtil tilføjes.
            Clear();
            if (biler.Count == 0) 
                
            {
                WriteLine("Ingen biler er registreret.");
            }
            else
            {
                WriteLine("Liste over registrerede biler:");
                foreach (BilInfo bil in biler)
                {
                    WriteLine($"{bil.Mærke} {bil.Model} - {bil.Nummerplade}");
                }
            }
            #region Menu
            WriteLine("\n*** HOVEDMENU ***");
            WriteLine("\n1. Registrer ny kunde");
            WriteLine("2. Vis Kunde Kontaktinfo");
            WriteLine("3. Afslut");
            WriteLine("\nVælg en af de overstående muligheder:");

            // Læser brugerens indtastning og håndterer menuvalget.
            // dvs man kun kan vælge fra 1-3 alt andet ville give fejl og skal prøve igen
            if (int.TryParse(ReadLine(), out int valg) && valg >= 1 && valg <= 3)
            {
                switch (valg)
                {
                    // her starter oprettelse af ny kunde ved hjælp af "OprettelseNyKunde" klasse
                    case 1:
                        BilInfo nyBil = OprettelseNyKunde.OpretNyKunde();
                        if (nyBil != null)
                        {
                            biler.Add(nyBil);
                            WriteLine(GetSynBesked(nyBil));
                        }
                        WriteLine("\nTryk ENTER for at fortsætte");
                        ReadLine();
                        break;

                    // her kan vi søge efter kundens nummerplade, derefter vil kundens for og efternavn blive vist
                    case 2:
                        Write("Indtast bilens nummerplade: ");
                        string? nummerplade = ReadLine();
                        BilInfo? bil = biler.Find(b => b.Nummerplade.Equals
                        (nummerplade, StringComparison.OrdinalIgnoreCase));

                        if (bil != null)
                        {
                            WriteLine($"\nKundens navn: {bil.BilKunde.KundeNavn}");
                            WriteLine($"Kundens telefonnummer: {bil.BilKunde.KundeTelefonnummer}");
                        }
                        else
                        {
                            WriteLine("Bilen blev ikke fundet.");
                        }
                        WriteLine("\nTryk på en tast for at vende tilbage...");
                        ReadKey();
                        break;
                    case 3:
                        WriteLine("Afslutter applikationen.");
                        return;
                }
            }
            else
            {
                // hvis man spammer eller taster andet end 1-3 så popper den her besked frem
                // og fortæller brugeren at han skal prøve igen.
                WriteLine("**UGYLDIGT INDSTAST**");
                WriteLine("\nIndtast venligst en af mulighederne");
                WriteLine("\nTryk ENTER for at fortsætte....");
                ReadLine();
            }

            #endregion Menu
        }
    }
    private static string SynFejlOgDefekteBiler(BilInfo bil)
    {
        // Tjekker om bilen er en Fiat Punto eller Alfa Romeo Giulia
        // uanset om det staves er med stort eller småt.
       
        if ((bil.Mærke.Equals("Fiat", StringComparison.OrdinalIgnoreCase) &&
             bil.Model.Equals("Punto", StringComparison.OrdinalIgnoreCase)) ||
            (bil.Mærke.Equals("Alfa Romeo", StringComparison.OrdinalIgnoreCase) &&
             bil.Model.Equals("Giulia", StringComparison.OrdinalIgnoreCase)))
        {
            WriteLine("\nBilen skal til syn");

            if (bil.Model.Equals("Punto", StringComparison.OrdinalIgnoreCase))
            {
                return "Udstødning";
            }
            else if (bil.Model.Equals("Giulia", StringComparison.OrdinalIgnoreCase))
            {
                return "Styretøjet";
            }
        }

        return "";
    }

    public string GetSynBesked(BilInfo bil)
    {
        string? fejlBesked = SynFejlOgDefekteBiler(bil);

        if (!string.IsNullOrEmpty(fejlBesked))
        {
            return $"Bilen har følgende fabriksfejl: {fejlBesked}";
        }
        else if (TidTilSyn(bil))
        {
            return "\nBilen skal til syn";
        }
        else
        {
            return "\nBilen skal ikke til syn";
        }
    }

    public static bool TidTilSyn(BilInfo bil)
    {
        // når der er gået 5 år eller mere, skal bilen til syn
        int synTidsLængde = 5; 
        int bilensÅrgang = DateTime.Now.Year - bil.FørsteRegistrering.Year;
        return bilensÅrgang >= synTidsLængde;
    }
}
