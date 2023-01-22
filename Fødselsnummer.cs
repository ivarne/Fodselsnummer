public class Fødeslnummer
{
    public IEnumerable<string> Generate(DateTime date, Kjønn kjønn)
    {
        var datestring = date.ToString("ddMMyy");

        foreach (var individnummer in GetPossibleIndividnumer(date, kjønn))
        {
            var kontrollsiffer = Kontrollsiffer(datestring, individnummer);
            if (kontrollsiffer != -1)
            {
                yield return $"{datestring}{individnummer:D3}{kontrollsiffer:D2}";
            }
        }
    }

    public IEnumerable<int> GetPossibleIndividnumer(DateTime date, Kjønn kjønn)
    {
        var add = kjønn == Kjønn.Gutt ? 1 : 0;
        switch (date.Year)
        {
            case (>= 1800) and (< 1900):
                for (int i = 500 + add; i <= 750; i += 2)
                {
                    yield return i;
                }
                break;
            case (>= 1900) and (<= 1944):
                for (int i = 0 + add; i <= 499; i += 2)
                {
                    yield return i;
                }
                break;
            case (>= 1945) and (<= 1999):
                for (int i = 0 + add; i <= 499; i += 2)
                {
                    yield return i;
                }
                for (int i = 900 + add; i <= 999; i += 2)
                {
                    yield return i;
                }
                break;
            case (>= 2000) and (< 2039):
                for (int i = 500 + add; i <= 999; i += 2)
                {
                    yield return i;
                }
                break;
            default:
                throw new Exception("Unknown periode");

        }
    }
    public int Kontrollsiffer(ReadOnlySpan<char> dato, int individnummer)
    {
        int T1 = dato[0] - '0';
        int T2 = dato[1] - '0';
        int T3 = dato[2] - '0';
        int T4 = dato[3] - '0';
        int T5 = dato[4] - '0';
        int T6 = dato[5] - '0';
        int T7 = (individnummer / 100) % 10;
        int T8 = (individnummer / 10) % 10;
        int T9 = individnummer % 10;
        int T10 = 0; // Gjett (blir korrigert)
        int T11 = 0; // Gjett (blir korrigert)
        //                3,         7,         6,         1,         8,         9,         4,         5,         2,          1
        int k1sum = (T1 * 3) + (T2 * 7) + (T3 * 6) + (T4 * 1) + (T5 * 8) + (T6 * 9) + (T7 * 4) + (T8 * 5) + (T9 * 2) + (T10 * 1);
        int k1rest = k1sum % 11;
        if (k1rest == 1)
        {
            return -1;
        }
        else if (k1rest == 0)
        {
            T10 = 0;
        }
        else
        {
            T10 = 11 - k1rest;
        }
        //                5,         4,         3,         2,         7,         6,         5,         4,         3,          2,          1
        int k2sum = (T1 * 5) + (T2 * 4) + (T3 * 3) + (T4 * 2) + (T5 * 7) + (T6 * 6) + (T7 * 5) + (T8 * 4) + (T9 * 3) + (T10 * 2) + (T11 * 1);
        int k2rest = k2sum % 11;
        if (k2rest == 1)
        {
            return -1;
        }
        else if (k2rest == 0)
        {
            T11 = 0;
        }
        else
        {
            T11 = 11 - k2rest;
        }
        return (T10 * 10) + T11;
    }
}
public enum Kjønn
{
    Gutt,
    Jente,
}