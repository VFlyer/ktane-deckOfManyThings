using System;
using System.Linq;
using KModkit;
using rnd = UnityEngine.Random;

class ArcticCard : StandardCard {

    public int fakeSuit;
    public int fakeRank;

    public ArcticCard()
    {
        fakeSuit = rnd.Range(0, 4);
        fakeRank = rnd.Range(0, 13) + 1;
    }

    public override string PrintLogMessage()
    {
		return string.Format("Card Nº {0} is an Arctic {1} of {2} (Real Value: {3} of {4}).",
            order + 1, GetRank(fakeRank), GetSuit(fakeSuit), GetRank(rank), GetSuit(suit));
    }
    public override string PrintTPCardInfo()
    {
        return string.Format("Card Nº {0} is an Arctic {1} of {2}.",
         order + 1, GetRank(fakeRank), GetSuit(fakeSuit));
    }
    public override void CalcValue(KMBombInfo bomb)
    {
        int[] serie;

        int serialSum = bomb.GetSerialNumberNumbers().Sum();

        switch(bomb.GetBatteryCount())
        {
            case 0: serie = new int[] {2, 10, 12, 13, 5, 6, 11, 7, 8, 3, 9, 4, 1}; break;
            case 1:
            case 2: serie = new int[] {9, 12, 2, 5, 4, 1, 7, 13, 3, 11, 8, 10, 6}; break;
            case 3:
            case 4: serie = new int[] {12, 6, 10, 9, 4, 2, 11, 13, 8, 7, 1, 3, 5}; break;
            default: serie = new int[] {10, 11, 12, 13, 3, 9, 2, 8, 1, 7, 5, 6, 4}; break;
        }

        rank = serie[ (serialSum + Array.FindIndex(serie, x => x == fakeRank)) % serie.Length ];
    
        switch(bomb.GetPortCount())
        {
            case 0: serie = new int[] {HEARTS, DIAMONDS, SPADES, CLUBS}; break;
            case 1:
            case 2: serie = new int[] {CLUBS, SPADES, DIAMONDS, HEARTS}; break;
            case 3:
            case 4: serie = new int[] {DIAMONDS, CLUBS, HEARTS, SPADES}; break;
            default: serie = new int[] {SPADES, DIAMONDS, CLUBS, HEARTS}; break;
        }

        suit = serie[ (serialSum + Array.FindIndex(serie, x => x == fakeSuit)) % serie.Length ];
    }
}