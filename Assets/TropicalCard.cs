using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;

class TropicalCard : StandardCard {

    public int fakeSuit;
    public int fakeRank;
    public int color;

    public TropicalCard()
    {
        fakeSuit = rnd.Range(0, 4);
        fakeRank = rnd.Range(0, 13) + 1;
        color = rnd.Range(0, 6);
    }

    public override void PrintLogMessage(int moduleId)
    {
		Debug.LogFormat("[The Deck of Many Things #{0}] Card Nº {1} is a Tropical {2} {3} of {4} (Real Value: {5} of {6}).", moduleId, order + 1, GetColor(color), GetRank(fakeRank), GetSuit(fakeSuit), GetRank(rank), GetSuit(suit));
    }

	public String GetColor(int color)
	{
		switch(color)
		{
			case 0: return "Blue";
			case 1: return "Pink";
			case 2: return "Purple";
			case 3: return "Green";
			case 4: return "Yellow";
			case 5: return "Orange";
		}

		return "";
	}

	public override void CalcValue(KMBombInfo bomb)
	{
		int[] rankSerie = new int[13];
		int[] suitSerie = new int[4];

		switch(fakeSuit)
		{
			case 0:
			{
				switch(fakeRank)
				{
					case 1:
					{
						rankSerie = new int[] {10, 10, 1, 1, 8, 1};
						suitSerie = new int[] {CLUBS, HEARTS, CLUBS, CLUBS, DIAMONDS, SPADES};
						break;
					}
					case 2:
					{
						rankSerie = new int[] {7, 11, 2, 1, 6, 4};
						suitSerie = new int[] {CLUBS, DIAMONDS, HEARTS, DIAMONDS, CLUBS, DIAMONDS};
						break;
					}
					case 3:
					{
						rankSerie = new int[] {11, 12, 9, 4, 10, 7};
						suitSerie = new int[] {HEARTS, HEARTS, HEARTS, CLUBS, SPADES, DIAMONDS};
						break;
					}
					case 4:
					{
						rankSerie = new int[] {3, 11, 3, 9, 3, 10};
						suitSerie = new int[] {SPADES, HEARTS, CLUBS, CLUBS, HEARTS, SPADES};
						break;
					}
					case 5:
					{
						rankSerie = new int[] {9, 13, 12, 11, 2, 9};
						suitSerie = new int[] {HEARTS, CLUBS, CLUBS, DIAMONDS, HEARTS, SPADES};
						break;
					}
					case 6:
					{
						rankSerie = new int[] {1, 2, 3, 12, 9, 13};
						suitSerie = new int[] {DIAMONDS, CLUBS, SPADES, HEARTS, HEARTS, CLUBS};
						break;
					}
					case 7:
					{
						rankSerie = new int[] {8, 8, 11, 12, 1, 2};
						suitSerie = new int[] {CLUBS, HEARTS, DIAMONDS, CLUBS, HEARTS, CLUBS};
						break;
					}
					case 8:
					{
						rankSerie = new int[] {4, 10, 8, 13, 12, 7};
						suitSerie = new int[] {DIAMONDS, DIAMONDS, HEARTS, CLUBS, CLUBS, CLUBS};
						break;
					}
					case 9:
					{
						rankSerie = new int[] {4, 3, 4, 9, 10, 3};
						suitSerie = new int[] {SPADES, SPADES, CLUBS, HEARTS, HEARTS, HEARTS};
						break;
					}
					case 10:
					{
						rankSerie = new int[] {6, 13, 6, 4, 1, 1};
						suitSerie = new int[] {SPADES, HEARTS, HEARTS, HEARTS, HEARTS, CLUBS};
						break;
					}
					case 11:
					{
						rankSerie = new int[] {3, 10, 3, 5, 2, 4};
						suitSerie = new int[] {HEARTS, SPADES, DIAMONDS, SPADES, SPADES, SPADES};
						break;
					}
					case 12:
					{
						rankSerie = new int[] {13, 8, 7, 5, 9, 1};
						suitSerie = new int[] {CLUBS, CLUBS, CLUBS, CLUBS, SPADES, DIAMONDS};
						break;
					}
					case 13:
					{
						rankSerie = new int[] {6, 4, 8, 12, 5, 12};
						suitSerie = new int[] {CLUBS, DIAMONDS, CLUBS, SPADES, CLUBS, CLUBS};
						break;
					}
				}
				break;
			}
			case 1:
			{
				switch(fakeRank)
				{
					case 1:
					{
						rankSerie = new int[] {12, 7, 2, 9, 12, 6};
						suitSerie = new int[] {CLUBS, CLUBS, CLUBS, SPADES, HEARTS, CLUBS};
						break;
					}
					case 2:
					{
						rankSerie = new int[] {7, 6, 4, 6, 13, 13};
						suitSerie = new int[] {SPADES, DIAMONDS, HEARTS, HEARTS, SPADES, DIAMONDS};
						break;
					}
					case 3:
					{
						rankSerie = new int[] {10, 7, 5, 11, 4, 3};
						suitSerie = new int[] {SPADES, DIAMONDS, SPADES, CLUBS, SPADES, SPADES};
						break;
					}
					case 4:
					{
						rankSerie = new int[] {2, 11, 10, 8, 13, 8};
						suitSerie = new int[] {CLUBS, SPADES, DIAMONDS, SPADES, CLUBS, CLUBS};
						break;
					}
					case 5:
					{
						rankSerie = new int[] {13, 13, 5, 7, 9, 8};
						suitSerie = new int[] {SPADES, DIAMONDS, HEARTS, HEARTS, DIAMONDS, SPADES};
						break;
					}
					case 6:
					{
						rankSerie = new int[] {8, 1, 6, 7, 2, 4};
						suitSerie = new int[] {DIAMONDS, SPADES, SPADES, SPADES, DIAMONDS, HEARTS};
						break;
					}
					case 7:
					{
						rankSerie = new int[] {3, 9, 10, 10, 3, 2};
						suitSerie = new int[] {CLUBS, HEARTS, HEARTS, SPADES, DIAMONDS, HEARTS};
						break;
					}
					case 8:
					{
						rankSerie = new int[] {1, 1, 1, 2, 7, 6};
						suitSerie = new int[] {HEARTS, CLUBS, SPADES, SPADES, SPADES, DIAMONDS};
						break;
					}
					case 9:
					{
						rankSerie = new int[] {7, 5, 8, 10, 8, 12};
						suitSerie = new int[] {HEARTS, DIAMONDS, SPADES, DIAMONDS, HEARTS, SPADES};
						break;
					}
					case 10:
					{
						rankSerie = new int[] {11, 7, 9, 4, 8, 8};
						suitSerie = new int[] {SPADES, HEARTS, DIAMONDS, DIAMONDS, CLUBS, HEARTS};
						break;
					}
					case 11:
					{
						rankSerie = new int[] {8, 12, 5, 7, 2, 11};
						suitSerie = new int[] {HEARTS, SPADES, CLUBS, CLUBS, CLUBS, SPADES};
						break;
					}
					case 12:
					{
						rankSerie = new int[] {12, 6, 13, 1, 11, 7};
						suitSerie = new int[] {SPADES, HEARTS, SPADES, HEARTS, SPADES, HEARTS};
						break;
					}
					case 13:
					{
						rankSerie = new int[] {2, 4, 1, 13, 5, 5};
						suitSerie = new int[] {DIAMONDS, HEARTS, HEARTS, SPADES, DIAMONDS, HEARTS};
						break;
					}
				}
				break;
			}
			case 2:
			{
				switch(fakeRank)
				{
					case 1:
					{
						rankSerie = new int[] {4, 10, 12, 13, 5, 8};
						suitSerie = new int[] {HEARTS, CLUBS, DIAMONDS, DIAMONDS, HEARTS, DIAMONDS};
						break;
					}
					case 2:
					{
						rankSerie = new int[] {7, 5, 9, 2, 3, 11};
						suitSerie = new int[] {DIAMONDS, CLUBS, SPADES, CLUBS, SPADES, HEARTS};
						break;
					}
					case 3:
					{
						rankSerie = new int[] {9, 4, 10, 10, 5, 11};
						suitSerie = new int[] {CLUBS, CLUBS, SPADES, HEARTS, SPADES, CLUBS};
						break;
					}
					case 4:
					{
						rankSerie = new int[] {6, 6, 10, 2, 13, 1};
						suitSerie = new int[] {DIAMONDS, SPADES, CLUBS, DIAMONDS, DIAMONDS, HEARTS};
						break;
					}
					case 5:
					{
						rankSerie = new int[] {13, 1, 11, 5, 8, 7};
						suitSerie = new int[] {DIAMONDS, HEARTS, SPADES, DIAMONDS, SPADES, SPADES};
						break;
					}
					case 6:
					{
						rankSerie = new int[] {8, 7, 2, 10, 12, 13};
						suitSerie = new int[] {SPADES, SPADES, DIAMONDS, CLUBS, DIAMONDS, SPADES};
						break;
					}
					case 7:
					{
						rankSerie = new int[] {12, 13, 5, 11, 10, 9};
						suitSerie = new int[] {DIAMONDS, SPADES, DIAMONDS, SPADES, DIAMONDS, DIAMONDS};
						break;
					}
					case 8:
					{
						rankSerie = new int[] {10, 9, 12, 8, 7, 11};
						suitSerie = new int[] {DIAMONDS, DIAMONDS, SPADES, CLUBS, CLUBS, DIAMONDS};
						break;
					}
					case 9:
					{
						rankSerie = new int[] {12, 6, 9, 3, 7, 5};
						suitSerie = new int[] {HEARTS, CLUBS, CLUBS, CLUBS, DIAMONDS, CLUBS};
						break;
					}
					case 10:
					{
						rankSerie = new int[] {5, 9, 3, 7, 1, 5};
						suitSerie = new int[] {SPADES, CLUBS, HEARTS, DIAMONDS, CLUBS, SPADES};
						break;
					}
					case 11:
					{
						rankSerie = new int[] {2, 9, 12, 3, 4, 3};
						suitSerie = new int[] {HEARTS, SPADES, HEARTS, SPADES, CLUBS, CLUBS};
						break;
					}
					case 12:
					{
						rankSerie = new int[] {9, 1, 6, 11, 3, 9};
						suitSerie = new int[] {SPADES, DIAMONDS, CLUBS, HEARTS, CLUBS, HEARTS};
						break;
					}
					case 13:
					{
						rankSerie = new int[] {2, 4, 7, 6, 10, 2};
						suitSerie = new int[] {SPADES, SPADES, SPADES, SPADES, CLUBS, SPADES};
						break;
					}
				}
				break;
			}
			case 3:
			{
				switch(fakeRank)
				{
					case 1:
					{
						rankSerie = new int[] {1, 5, 2, 8, 6, 6};
						suitSerie = new int[] {CLUBS, SPADES, SPADES, DIAMONDS, DIAMONDS, SPADES};
						break;
					}
					case 2:
					{
						rankSerie = new int[] {13, 11, 8, 1, 6, 13};
						suitSerie = new int[] {HEARTS, CLUBS, DIAMONDS, SPADES, SPADES, HEARTS};
						break;
					}
					case 3:
					{
						rankSerie = new int[] {3, 2, 11, 6, 9, 4};
						suitSerie = new int[] {DIAMONDS, HEARTS, HEARTS, CLUBS, CLUBS, CLUBS};
						break;
					}
					case 4:
					{
						rankSerie = new int[] {11, 3, 4, 4, 13, 9};
						suitSerie = new int[] {CLUBS, DIAMONDS, SPADES, SPADES, HEARTS, CLUBS};
						break;
					}
					case 5:
					{
						rankSerie = new int[] {10, 3, 11, 3, 1, 10};
						suitSerie = new int[] {HEARTS, HEARTS, CLUBS, DIAMONDS, SPADES, HEARTS};
						break;
					}
					case 6:
					{
						rankSerie = new int[] {1, 2, 13, 6, 4, 10};
						suitSerie = new int[] {SPADES, SPADES, HEARTS, DIAMONDS, HEARTS, CLUBS};
						break;
					}
					case 7:
					{
						rankSerie = new int[] {5, 8, 6, 13, 6, 2};
						suitSerie = new int[] {HEARTS, DIAMONDS, DIAMONDS, HEARTS, HEARTS, DIAMONDS};
						break;
					}
					case 8:
					{
						rankSerie = new int[] {5, 5, 4, 9, 12, 6};
						suitSerie = new int[] {DIAMONDS, HEARTS, DIAMONDS, DIAMONDS, SPADES, HEARTS};
						break;
					}
					case 9:
					{
						rankSerie = new int[] {5, 12, 1, 2, 11, 12};
						suitSerie = new int[] {CLUBS, CLUBS, DIAMONDS, HEARTS, HEARTS, HEARTS};
						break;
					}
					case 10:
					{
						rankSerie = new int[] {4, 3, 7, 3, 11, 3};
						suitSerie = new int[] {CLUBS, CLUBS, DIAMONDS, HEARTS, CLUBS, DIAMONDS};
						break;
					}
					case 11:
					{
						rankSerie = new int[] {11, 12, 7, 5, 4, 10};
						suitSerie = new int[] {DIAMONDS, DIAMONDS, HEARTS, HEARTS, DIAMONDS, DIAMONDS};
						break;
					}
					case 12:
					{
						rankSerie = new int[] {9, 8, 13, 8, 11, 12};
						suitSerie = new int[] {DIAMONDS, SPADES, CLUBS, HEARTS, DIAMONDS, DIAMONDS};
						break;
					}
					case 13:
					{
						rankSerie = new int[] {6, 2, 13, 12, 7, 5};
						suitSerie = new int[] {HEARTS, DIAMONDS, DIAMONDS, DIAMONDS, HEARTS, DIAMONDS};
						break;
					}
				}
				break;
			}
		}

		rank = rankSerie[color];
		suit = suitSerie[color];
	}
}