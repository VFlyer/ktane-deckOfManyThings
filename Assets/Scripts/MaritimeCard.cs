using System.Linq;
using KModkit;
using rnd = UnityEngine.Random;

class MaritimeCard : StandardCard {

    public int fakeRank;

    public MaritimeCard()
    {
        fakeRank = rnd.Range(0, 8) + 11;
        suit = rnd.Range(0, 4);
    }

    public override string PrintLogMessage()
    {
		return string.Format("Card Nº {0} is a Maritime {1} of {2} (Real Value: {3} of {4}).",
            order + 1, fakeRank, GetSuit(suit), GetRank(rank), GetSuit(suit) );
    }

    public override string PrintTPCardInfo()
    {
        return string.Format("Card Nº {0} is a Maritime {1} of {2}.",
            order + 1, fakeRank, GetSuit(suit));
    }

    public override void CalcValue(KMBombInfo bomb)
    {
        rank = fakeRank * (bomb.GetBatteryCount() + 1);
        if (bomb.GetPortPlates().Any((x) => x.Length == 0))
            rank += 10;
        rank /= bomb.GetOnIndicators().Count() + 1;
        if (bomb.GetSerialNumberLetters().Any(x => "AEIOU".Contains(x)))
            rank -= fakeRank;
        while (rank < 0)
            rank += 13;
        rank = rank % 13;
        if (rank == 0)
            rank = 13;
    }
}