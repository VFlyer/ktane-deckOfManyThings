using rnd = UnityEngine.Random;

class StandardCard
{
    public const int SPADES = 3;
    public const int HEARTS = 2;
    public const int DIAMONDS = 1;
    public const int CLUBS = 0;

    public int suit;
    public int rank;
    public int order;

    public StandardCard(int suitIdx, int rankIdx)
    {
        suit = suitIdx;
        rank = rankIdx + 1;
    }

    public StandardCard()
    {
        suit = rnd.Range(0, 4);
        rank = rnd.Range(0, 13) + 1;
    }

    public static string GetSuit(int suit)
    {
        switch(suit)
        {
            case 3: return "Spades";
            case 2: return "Hearts";
            case 1: return "Diamonds";
            case 0: return "Clubs";
        }

        return "?";
    }

    public static string GetRank(int rank)
    {
        switch(rank)
        {
            case 1: return "Ace";
            case 2: return "2";
            case 3: return "3";
            case 4: return "4";
            case 5: return "5";
            case 6: return "6";
            case 7: return "7";
            case 8: return "8";
            case 9: return "9";
            case 10: return "10";
            case 11: return "Jack";
            case 12: return "Queen";
            case 13: return "King";
        }

        return "?";
    }

    public virtual string PrintLogMessage()
    {
        return string.Format("Card Nº {0} is a Standard {1} of {2}.", order + 1, GetRank(rank), GetSuit(suit));
    }

    public virtual string PrintTPCardInfo()
    {
        return string.Format("Card Nº {0} is a Standard {1} of {2}.", order + 1, GetRank(rank), GetSuit(suit));
    }

    public virtual void CalcValue(KMBombInfo bomb)
    {

    }
}