using static System.Console;


namespace Værsted.Code;


class OprettelseNyKunde
{
    /// <summary>
    /// Her er en metode til at oprette en ny kunde og bil på.
    /// </summary>
    public static BilInfo OpretNyKunde()
    {
        // her indtastes kundeoplysninger
        Write("Kundens fornavn: ");
        string? fornavn = ReadLine();
        Write("Kundens efternavn: ");
        string? efternavn = ReadLine();
        Write("Kundens telefon nummer: ");
        string? telefonnummer = ReadLine();

        // Oprettelse af en ny KundeInfo instans derefter går videre til bilens info nede
        KundeInfo nyKunde = new KundeInfo
        {
            KundeNavn = $"{fornavn} {efternavn}",
            KundeTelefonnummer = telefonnummer
        };

        // her indtastes  biloplysninger
        Write("Angiv bilens nummerplade: ");
        string? nummerplade = ReadLine();
        Write("Angiv bilens mærke: ");
        string? mærke = ReadLine();
        Write("Angiv bilens model: ");
        string? model = ReadLine();

        // Indtastning af bilens motorstørrelse med en validering dvs. at der skal tastes et tal for at gå videre
        float motorstørrelse;
        while (true)
        {
            Write("Angiv bilens motorstørrelse: ");
            if (float.TryParse(ReadLine(), out motorstørrelse))
            {
                break;
            }
            else
            {
                WriteLine("Ugyldig indtastning. Indtast venligst en gyldig motorstørrelse.");
            }
        }

        // Indtastning af bilens registreringsdato med en validering. sørger for at input af bruger matcher dato formaten
        DateTime førsteRegistrering;
        while (true)
        {
            Write("Angiv registreringsdato (dd.MM.yyyy): ");
            if (DateTime.TryParseExact(ReadLine(), "dd.MM.yyyy", null,
                System.Globalization.DateTimeStyles.None,
                out DateTime registreringsDato))
            {
                førsteRegistrering = registreringsDato;
                break;
            }
            else
            {
                WriteLine("Ugyldig indtastning. Indtast dato i formatet dd.MM.yyyy.");
            }
        }

        // Validering og håndtering af bilens alder og synsdato
        if (mærke.Equals("Alfa Romeo", StringComparison.OrdinalIgnoreCase) &&
            model.Equals("Giulia", StringComparison.OrdinalIgnoreCase))
        {
            // Opret en ny BilInfo instans uden sidste synsdato, da det er en Alfa Romeo Giulia og har fabriksfejl
            return new BilInfo
            {
                Nummerplade = nummerplade,
                BilKunde = nyKunde,
                Mærke = mærke,
                Model = model,
            };
        }
        else
        {
            int år = DateTime.Now.Year - førsteRegistrering.Year;

            if (år >= 5)
            {
                while (true)
                {
                    Write("Bilen er over 5 år gammel, angiv bilens sidste synsdato (dd.MM.yyyy): ");
                    if (DateTime.TryParseExact(ReadLine(), "dd.MM.yyyy", null,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime sidsteSyn))
                    {
                        // Opret en ny BilInfo instans med sidste synsdato
                        return new BilInfo
                        {
                            Nummerplade = nummerplade,
                            BilKunde = nyKunde,
                            Mærke = mærke,
                            Model = model,
                            FørsteRegistrering = førsteRegistrering,
                            MotorStørrelse = motorstørrelse,
                            SidsteSyn = sidsteSyn
                        };
                    }
                    else
                    {
                        WriteLine("Ugyldig indtastning. Indtast dato i formatet dd.MM.yyyy.");
                    }
                }
            }
            else
            {
                return new BilInfo
                {
                    Nummerplade = nummerplade,
                    BilKunde = nyKunde,
                    Mærke = mærke,
                    Model = model,
                    FørsteRegistrering = førsteRegistrering,
                    MotorStørrelse = motorstørrelse,
                    SidsteSyn = DateTime.Now
                };
            }
        }

    }
}
