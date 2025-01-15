using rnd = UnityEngine.Random;
using System.Collections.Generic;

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

    public override string PrintLogMessage()
    {
		return string.Format("Card Nº {0} is a Tropical {1} {2} of {3} (Real Value: {4} of {5}).",
			order + 1, GetColor(color), GetRank(fakeRank), GetSuit(fakeSuit), GetRank(rank), GetSuit(suit));
    }

	public override string PrintTPCardInfo()
	{
		return string.Format("Card Nº {0} is a Tropical {1} {2} of {3}.",
			order + 1, GetColor(color), GetRank(fakeRank), GetSuit(fakeSuit));
	}

	public string GetColor(int color)
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

		return "?";
	}

	public override void CalcValue(KMBombInfo bomb)
	{
		int[] rankSeries = new int[6];
		int[] suitSeries = new int[6];
		Dictionary<int, int[]>[] backlogTrueRanks = new Dictionary<int, int[]>[] {
			new Dictionary<int, int[]> {
				{ 1, new int[] {10, 10, 1, 1, 8, 1} },
				{ 2, new int[] {7, 11, 2, 1, 6, 4} },
			}// Clubs
		};

		// Ordered from Blue, Pink, Purple, Green, Yellow, Orange; NW, W, SW, NE, E, SE
		switch (fakeSuit)
		{
			case 0: // Clubs
			{
				switch(fakeRank)
				{
					case 1:
					{
						rankSeries = new int[] {10, 10, 1, 1, 8, 1};
						suitSeries = new int[] {CLUBS, HEARTS, CLUBS, CLUBS, DIAMONDS, SPADES};
						break;
					}
					case 2:
					{
						rankSeries = new int[] {7, 11, 2, 1, 6, 4};
						suitSeries = new int[] {CLUBS, DIAMONDS, HEARTS, DIAMONDS, CLUBS, DIAMONDS};
						break;
					}
					case 3:
					{
						rankSeries = new int[] {11, 12, 9, 4, 10, 7};
						suitSeries = new int[] {HEARTS, HEARTS, HEARTS, CLUBS, SPADES, DIAMONDS};
						break;
					}
					case 4:
					{
						rankSeries = new int[] {3, 11, 3, 9, 3, 10};
						suitSeries = new int[] {SPADES, HEARTS, CLUBS, CLUBS, HEARTS, SPADES};
						break;
					}
					case 5:
					{
						rankSeries = new int[] {9, 13, 12, 11, 2, 9};
						suitSeries = new int[] {HEARTS, CLUBS, CLUBS, DIAMONDS, HEARTS, SPADES};
						break;
					}
					case 6:
					{
						rankSeries = new int[] {1, 2, 3, 12, 9, 13};
						suitSeries = new int[] {DIAMONDS, CLUBS, SPADES, HEARTS, HEARTS, CLUBS};
						break;
					}
					case 7:
					{
						rankSeries = new int[] {8, 8, 11, 12, 1, 2};
						suitSeries = new int[] {CLUBS, HEARTS, DIAMONDS, CLUBS, DIAMONDS, CLUBS};
						break;
					}
					case 8:
					{
						rankSeries = new int[] {4, 10, 8, 13, 12, 7};
						suitSeries = new int[] {DIAMONDS, DIAMONDS, HEARTS, CLUBS, CLUBS, CLUBS};
						break;
					}
					case 9:
					{
						rankSeries = new int[] {4, 3, 4, 9, 10, 3};
						suitSeries = new int[] {SPADES, SPADES, CLUBS, HEARTS, HEARTS, HEARTS};
						break;
					}
					case 10:
					{
						rankSeries = new int[] {6, 13, 6, 4, 1, 1};
						suitSeries = new int[] {SPADES, HEARTS, HEARTS, HEARTS, HEARTS, CLUBS};
						break;
					}
					case 11:
					{
						rankSeries = new int[] {3, 10, 3, 5, 2, 4};
						suitSeries = new int[] {HEARTS, SPADES, DIAMONDS, SPADES, SPADES, SPADES};
						break;
					}
					case 12:
					{
						rankSeries = new int[] {13, 8, 7, 5, 9, 1};
						suitSeries = new int[] {CLUBS, CLUBS, CLUBS, CLUBS, SPADES, DIAMONDS};
						break;
					}
					case 13:
					{
						rankSeries = new int[] {6, 4, 8, 12, 5, 12};
						suitSeries = new int[] {CLUBS, DIAMONDS, CLUBS, SPADES, CLUBS, CLUBS};
						break;
					}
				}
				break;
			}
			case 1: // Diamonds
			{
				switch(fakeRank)
				{
					case 1:
					{
						rankSeries = new int[] {12, 7, 2, 9, 12, 6};
						suitSeries = new int[] {CLUBS, CLUBS, CLUBS, SPADES, HEARTS, CLUBS};
						break;
					}
					case 2:
					{
						rankSeries = new int[] {7, 6, 4, 6, 13, 13};
						suitSeries = new int[] {SPADES, DIAMONDS, HEARTS, HEARTS, SPADES, DIAMONDS};
						break;
					}
					case 3:
					{
						rankSeries = new int[] {10, 7, 5, 11, 4, 3};
						suitSeries = new int[] {SPADES, DIAMONDS, SPADES, CLUBS, SPADES, SPADES};
						break;
					}
					case 4:
					{
						rankSeries = new int[] {2, 11, 10, 8, 13, 8};
						suitSeries = new int[] {CLUBS, SPADES, DIAMONDS, SPADES, CLUBS, CLUBS};
						break;
					}
					case 5:
					{
						rankSeries = new int[] {13, 13, 5, 7, 9, 8};
						suitSeries = new int[] {SPADES, DIAMONDS, HEARTS, HEARTS, DIAMONDS, SPADES};
						break;
					}
					case 6:
					{
						rankSeries = new int[] {8, 1, 6, 7, 2, 4};
						suitSeries = new int[] {DIAMONDS, SPADES, SPADES, SPADES, DIAMONDS, HEARTS};
						break;
					}
					case 7:
					{
						rankSeries = new int[] {3, 9, 10, 10, 3, 2};
						suitSeries = new int[] {CLUBS, HEARTS, HEARTS, SPADES, DIAMONDS, HEARTS};
						break;
					}
					case 8:
					{
						rankSeries = new int[] {1, 1, 1, 2, 7, 6};
						suitSeries = new int[] {HEARTS, CLUBS, SPADES, SPADES, SPADES, DIAMONDS};
						break;
					}
					case 9:
					{
						rankSeries = new int[] {7, 5, 8, 10, 8, 12};
						suitSeries = new int[] {HEARTS, DIAMONDS, SPADES, DIAMONDS, HEARTS, SPADES};
						break;
					}
					case 10:
					{
						rankSeries = new int[] {11, 7, 9, 4, 8, 8};
						suitSeries = new int[] {SPADES, HEARTS, DIAMONDS, DIAMONDS, CLUBS, HEARTS};
						break;
					}
					case 11:
					{
						rankSeries = new int[] {8, 12, 5, 7, 2, 11};
						suitSeries = new int[] {HEARTS, SPADES, CLUBS, CLUBS, CLUBS, SPADES};
						break;
					}
					case 12:
					{
						rankSeries = new int[] {12, 6, 13, 1, 11, 7};
						suitSeries = new int[] {SPADES, HEARTS, SPADES, HEARTS, SPADES, HEARTS};
						break;
					}
					case 13:
					{
						rankSeries = new int[] {2, 4, 1, 13, 5, 5};
						suitSeries = new int[] {DIAMONDS, HEARTS, HEARTS, SPADES, DIAMONDS, HEARTS};
						break;
					}
				}
				break;
			}
			case 2: // Hearts
			{
				switch(fakeRank)
				{
					case 1:
					{
						rankSeries = new int[] {4, 10, 12, 13, 5, 8};
						suitSeries = new int[] {HEARTS, CLUBS, DIAMONDS, DIAMONDS, HEARTS, DIAMONDS};
						break;
					}
					case 2:
					{
						rankSeries = new int[] {7, 5, 9, 2, 3, 11};
						suitSeries = new int[] {DIAMONDS, CLUBS, SPADES, CLUBS, SPADES, HEARTS};
						break;
					}
					case 3:
					{
						rankSeries = new int[] {9, 4, 10, 10, 5, 11};
						suitSeries = new int[] {CLUBS, CLUBS, SPADES, HEARTS, SPADES, CLUBS};
						break;
					}
					case 4:
					{
						rankSeries = new int[] {6, 6, 10, 2, 13, 1};
						suitSeries = new int[] {DIAMONDS, SPADES, CLUBS, DIAMONDS, DIAMONDS, HEARTS};
						break;
					}
					case 5:
					{
						rankSeries = new int[] {13, 1, 11, 5, 8, 7};
						suitSeries = new int[] {DIAMONDS, HEARTS, SPADES, DIAMONDS, SPADES, SPADES};
						break;
					}
					case 6:
					{
						rankSeries = new int[] {8, 7, 2, 10, 12, 13};
						suitSeries = new int[] {SPADES, SPADES, DIAMONDS, CLUBS, DIAMONDS, SPADES};
						break;
					}
					case 7:
					{
						rankSeries = new int[] {12, 13, 5, 11, 10, 9};
						suitSeries = new int[] {DIAMONDS, SPADES, DIAMONDS, SPADES, DIAMONDS, DIAMONDS};
						break;
					}
					case 8:
					{
						rankSeries = new int[] {10, 9, 12, 8, 7, 11};
						suitSeries = new int[] {DIAMONDS, DIAMONDS, SPADES, CLUBS, CLUBS, DIAMONDS};
						break;
					}
					case 9:
					{
						rankSeries = new int[] {12, 6, 9, 3, 7, 5};
						suitSeries = new int[] {HEARTS, CLUBS, CLUBS, CLUBS, DIAMONDS, CLUBS};
						break;
					}
					case 10:
					{
						rankSeries = new int[] {5, 9, 3, 7, 1, 5};
						suitSeries = new int[] {SPADES, CLUBS, HEARTS, DIAMONDS, CLUBS, SPADES};
						break;
					}
					case 11:
					{
						rankSeries = new int[] {2, 9, 12, 3, 4, 3};
						suitSeries = new int[] {HEARTS, SPADES, HEARTS, SPADES, CLUBS, CLUBS};
						break;
					}
					case 12:
					{
						rankSeries = new int[] {9, 1, 6, 11, 3, 9};
						suitSeries = new int[] {SPADES, DIAMONDS, CLUBS, HEARTS, CLUBS, HEARTS};
						break;
					}
					case 13:
					{
						rankSeries = new int[] {2, 4, 7, 6, 10, 2};
						suitSeries = new int[] {SPADES, SPADES, SPADES, SPADES, CLUBS, SPADES};
						break;
					}
				}
				break;
			}
			case 3: // Spades
			{
				switch(fakeRank)
				{
					case 1:
					{
						rankSeries = new int[] {1, 5, 2, 8, 6, 6};
						suitSeries = new int[] {CLUBS, SPADES, SPADES, DIAMONDS, DIAMONDS, SPADES};
						break;
					}
					case 2:
					{
						rankSeries = new int[] {13, 11, 8, 1, 6, 13};
						suitSeries = new int[] {HEARTS, CLUBS, DIAMONDS, SPADES, SPADES, HEARTS};
						break;
					}
					case 3:
					{
						rankSeries = new int[] {3, 2, 11, 6, 9, 4};
						suitSeries = new int[] {DIAMONDS, HEARTS, HEARTS, CLUBS, CLUBS, CLUBS};
						break;
					}
					case 4:
					{
						rankSeries = new int[] {11, 3, 4, 4, 13, 9};
						suitSeries = new int[] {CLUBS, DIAMONDS, SPADES, SPADES, HEARTS, CLUBS};
						break;
					}
					case 5:
					{
						rankSeries = new int[] {10, 3, 11, 3, 1, 10};
						suitSeries = new int[] {HEARTS, HEARTS, CLUBS, DIAMONDS, SPADES, HEARTS};
						break;
					}
					case 6:
					{
						rankSeries = new int[] {1, 2, 13, 6, 4, 10};
						suitSeries = new int[] {SPADES, SPADES, HEARTS, DIAMONDS, HEARTS, CLUBS};
						break;
					}
					case 7:
					{
						rankSeries = new int[] {5, 8, 6, 13, 6, 2};
						suitSeries = new int[] {HEARTS, DIAMONDS, DIAMONDS, HEARTS, HEARTS, DIAMONDS};
						break;
					}
					case 8:
					{
						rankSeries = new int[] {5, 5, 4, 9, 12, 6};
						suitSeries = new int[] {DIAMONDS, HEARTS, DIAMONDS, DIAMONDS, SPADES, HEARTS};
						break;
					}
					case 9:
					{
						rankSeries = new int[] {5, 12, 1, 2, 11, 12};
						suitSeries = new int[] {CLUBS, CLUBS, DIAMONDS, HEARTS, HEARTS, HEARTS};
						break;
					}
					case 10:
					{
						rankSeries = new int[] {4, 3, 7, 3, 11, 3};
						suitSeries = new int[] {CLUBS, CLUBS, DIAMONDS, HEARTS, CLUBS, DIAMONDS};
						break;
					}
					case 11:
					{
						rankSeries = new int[] {11, 12, 7, 5, 4, 10};
						suitSeries = new int[] {DIAMONDS, DIAMONDS, HEARTS, HEARTS, DIAMONDS, DIAMONDS};
						break;
					}
					case 12:
					{
						rankSeries = new int[] {9, 8, 13, 8, 11, 12};
						suitSeries = new int[] {DIAMONDS, SPADES, CLUBS, HEARTS, DIAMONDS, DIAMONDS};
						break;
					}
					case 13:
					{
						rankSeries = new int[] {6, 2, 13, 12, 7, 5};
						suitSeries = new int[] {HEARTS, DIAMONDS, DIAMONDS, DIAMONDS, HEARTS, DIAMONDS};
						break;
					}
				}
				break;
			}
		}

		rank = rankSeries[color];
		suit = suitSeries[color];
	}
}