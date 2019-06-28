using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

class OasisCard : StandardCard {

    public int fakeSuit;
    public int fakeRank;

    public OasisCard()
    {
        fakeSuit = rnd.Next() % 4;
        fakeRank = rnd.Next() % 13 + 1;
    }

    public override void PrintLogMessage(int moduleId)
    {
		Debug.LogFormat("[The Deck of Many Things #{0}] Card Nº {1} is an Oasis {2} of {3} (Real Value: {4} of {5}).", moduleId, order + 1, GetRank(fakeRank), GetFakeSuit(fakeSuit), GetRank(rank), GetSuit(suit));
    }

	public String GetFakeSuit(int suit)
	{
		switch(suit)
		{
			case 0: return "Moons";
			case 1: return "Hands";
			case 2: return "Suns";
			case 3: return "Stars";
		}

		return "";
	}

	public override void CalcValue(KMBombInfo bomb)
	{
		int value = fakeRank;

		switch(fakeSuit)
		{
			case 0: value *= bomb.GetPortCount(); break;
			case 1: value *= bomb.GetSerialNumberNumbers().Sum(); break;
			case 2: value *= bomb.GetIndicators().Count(); break;
			case 3: value *= bomb.GetBatteryCount(); break;
		}

		switch(value % 52)
		{
			case 0: suit = SPADES; rank = 4; break;
			case 1: suit = CLUBS; rank = 4; break;
			case 2: suit = SPADES; rank = 7; break;
			case 3: suit = DIAMONDS; rank = 12; break;
			case 4: suit = SPADES; rank = 9; break;
			case 5: suit = HEARTS; rank = 2; break;
			case 6: suit = DIAMONDS; rank = 3; break;
			case 7: suit = HEARTS; rank = 7; break;
			case 8: suit = HEARTS; rank = 1; break;
			case 9: suit = CLUBS; rank = 13; break;
			case 10: suit = DIAMONDS; rank = 6; break;
			case 11: suit = DIAMONDS; rank = 9; break;
			case 12: suit = HEARTS; rank = 5; break;
			case 13: suit = HEARTS; rank = 3; break;
			case 14: suit = CLUBS; rank = 1; break;
			case 15: suit = SPADES; rank = 3; break;
			case 16: suit = HEARTS; rank = 8; break;
			case 17: suit = CLUBS; rank = 8; break;
			case 18: suit = DIAMONDS; rank = 8; break;
			case 19: suit = CLUBS; rank = 2; break;
			case 20: suit = SPADES; rank = 2; break;
			case 21: suit = HEARTS; rank = 6; break;
			case 22: suit = DIAMONDS; rank = 11; break;
			case 23: suit = DIAMONDS; rank = 5; break;
			case 24: suit = SPADES; rank = 5; break;
			case 25: suit = CLUBS; rank = 10; break;
			case 26: suit = HEARTS; rank = 13; break;
			case 27: suit = CLUBS; rank = 7; break;
			case 28: suit = CLUBS; rank = 13; break;
			case 29: suit = DIAMONDS; rank = 1; break;
			case 30: suit = SPADES; rank = 13; break;
			case 31: suit = DIAMONDS; rank = 10; break;
			case 32: suit = DIAMONDS; rank = 13; break;
			case 33: suit = HEARTS; rank = 12; break;
			case 34: suit = SPADES; rank = 12; break;
			case 35: suit = DIAMONDS; rank = 4; break;
			case 36: suit = CLUBS; rank = 6; break;
			case 37: suit = DIAMONDS; rank = 2; break;
			case 38: suit = HEARTS; rank = 10; break;
			case 39: suit = SPADES; rank = 11; break;
			case 40: suit = SPADES; rank = 8; break;
			case 41: suit = HEARTS; rank = 11; break;
			case 42: suit = CLUBS; rank = 11; break;
			case 43: suit = SPADES; rank = 6; break;
			case 44: suit = CLUBS; rank = 9; break;
			case 45: suit = CLUBS; rank = 3; break;
			case 46: suit = SPADES; rank = 1; break;
			case 47: suit = HEARTS; rank = 4; break;
			case 48: suit = SPADES; rank = 10; break;
			case 49: suit = CLUBS; rank = 5; break;
			case 50: suit = HEARTS; rank = 9; break;
			case 51: suit = DIAMONDS; rank = 7; break;
		}
	}
}