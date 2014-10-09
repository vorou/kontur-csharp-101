private static string PluralizeRubles(int count)
{
    string nominative = "рубль";
    string genitive = "рубля";
    string genitivePlural = "рублей";

    if (count % 10 == 1 && ((count - 11) % 100 != 0))
    {
        return nominative;
    }
    if (((count % 10 == 2) || (count % 10 == 4) || (count % 10 == 3)) && ((count - 12) % 100 != 0) && ((count - 13) % 100 != 0) &&
        ((count - 14) % 100 != 0)) return genitive;
    return genitivePlural;
}
