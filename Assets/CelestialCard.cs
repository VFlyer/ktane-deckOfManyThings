using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;

class CelestialCard : StandardCard {

    public int arcana;

    public CelestialCard()
    {
        arcana = rnd.Range(0, 22);
    }

    public override String PrintLogMessage()
    {
		return "Card Nº " + (order + 1) + " is a Celestial card: " + GetArcana(arcana) + ".";
    }

	public String GetArcana(int arcana)
	{
		switch(arcana)
		{
			case 0: return "0 - The Fool";
			case 1: return "I - The Magician";
			case 2: return "II - The High Priestess";
			case 3: return "III - The Empress";
			case 4: return "IV - The Emperor";
			case 5: return "V - The Hierophant";
			case 6: return "VI - The Lovers";
			case 7: return "VII - The Chariot";
			case 8: return "VIII - Justice";
			case 9: return "IX - The Hermit";
			case 10: return "X - Wheel of Fortune";
			case 11: return "XI - Strength";
			case 12: return "XII - The Hanged Man";
			case 13: return "XIII - Death";
			case 14: return "XIV - Temperance";
			case 15: return "XV - The Devil";
			case 16: return "XVI - The Tower";
			case 17: return "XVII - THe Star";
			case 18: return "XVIII - The Moon";
			case 19: return "XIX - The Sun";
			case 20: return "XX - Judgement";
			case 21: return "XXI - The World";
		}

		return "";
	}

	public List<int> GetValidRanks(KMBombInfo bomb, List<int> ranks, int startTime, int celestialCount, int cardNumber)
	{
		switch(arcana)
		{
			case 0:
			{
				List<int> ret = new List<int>();
				if(bomb.GetModuleNames().Count() > startTime)
					ret.AddRange(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13});
				else ret.Add(2);
				return ret;
			}
			case 3:
			case 4:
			{
				List<int> ret = new List<int>();
				ret.AddRange(new int[] {1, 11, 12, 13});
				return ret;
			}
			case 8:
			{
				List<int> ret = new List<int>();
				if(bomb.GetSerialNumberNumbers().Sum() % 2 == 0)
					ret.AddRange(new int[] {1, 3, 5, 7, 9, 11, 13});
				else ret.AddRange(new int[] {2, 4, 6, 8, 10, 12});
				return ret;
			}
			case 9:
			{
				List<int> ret = new List<int>();
				if(celestialCount == 1)
					ret.AddRange(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13});
				else ret.AddRange(new int[] {8, 11});
				return ret;					
			}
			case 10:
			{
				List<int> ret = new List<int>();
				if(bomb.GetSerialNumberNumbers().Sum() % 22 == 10)
				{
					ret.AddRange(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13});
					return ret;
				}
				arcana = bomb.GetSerialNumberNumbers().Sum() % 22;
				return GetValidRanks(bomb, ranks, startTime, celestialCount, cardNumber);
			}
			case 11:
			{
				List<int> ret = new List<int>();
				if(bomb.GetModuleNames().Count >= 20 || startTime >= 30)
					ret.AddRange(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13});
				else ret.AddRange(new int[] {12, 1});
				return ret;
			}
			case 12:
			{
				List<int> ret = new List<int>();
				ret.AddRange(new int[] {6, 8, 9});
				return ret;
			}
			case 13:
			{
				List<int> ret = new List<int>();
				for(int i = 1; i <= 13; i++)
				{
					if(!ranks.Exists(x => x == i))
						ret.Add(i);
				}
				return ret;
			}
			case 15:
			{
				List<int> ret = new List<int>();
				ret.AddRange(new int[] {6});
				return ret;
			}
			case 16:
			{
				List<int> ret = new List<int>();
				for(int i = 1; i <= 13; i++)
					if(i <= bomb.GetModuleNames().Count())
						ret.Add(i);
				return ret;
			}
			case 18:
			{
				List<int> ret = new List<int>();
				if(bomb.GetModuleNames().Exists(x => x == "The Moon"))
					ret.AddRange(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13});
				else ret.AddRange(new int[] {12, 1});
				return ret;
			}
			case 19:
			{
				List<int> ret = new List<int>();
				if(bomb.GetModuleNames().Exists(x => x == "The Sun"))
					ret.AddRange(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13});
				else ret.AddRange(new int[] {13, 1});
				return ret;
			}
			case 20:
			{
				List<int> ret = new List<int>();
				if(cardNumber >= 29)
					ret.AddRange(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13});
				else ret.AddRange(new int[] {10});
				return ret;
			}
			case 21:
			{
				List<int> ret = new List<int>();
				foreach(int number in bomb.GetSerialNumberNumbers())
				{
					if(number == 0)
						ret.Add(10);
					else	
						ret.Add(number);
				}
				foreach(char letter in bomb.GetSerialNumberLetters())
				{
					if(letter == 'J')
						ret.Add(11);
					else if(letter == 'Q')
						ret.Add(12);
					else if(letter == 'K')
						ret.Add(13);
					else if (letter == 'A' && !ret.Exists(x => x == 1))	
						ret.Add(1);
				}

				return ret;
			}
		}

		return ranks;
	}

	public List<int> GetValidSuits(KMBombInfo bomb, List<int> suits, int startTime, String date, StandardCard firstCard)
	{
		switch(arcana)
		{
			case 1:
			{
				List<int> ret = new List<int>();
				foreach(char letter in bomb.GetSerialNumberLetters())
				{
					if(letter == 'S')
						ret.Add(SPADES);
					else if(letter == 'D')
						ret.Add(DIAMONDS);
					else if(letter == 'H')
						ret.Add(HEARTS);
					else if (letter == 'C')	
						ret.Add(CLUBS);
				}

				if(ret.Count() == 0)
					ret.AddRange(new int[] {CLUBS, DIAMONDS, HEARTS, SPADES});

				return ret;
			}
			case 2:
			{
				List<int> ret = new List<int>();
				if( bomb.GetBatteryCount() != 0 &&
					bomb.GetBatteryCount() != bomb.GetBatteryHolderCount() &&
					bomb.GetBatteryCount() != 2 * bomb.GetBatteryHolderCount() &&
					bomb.GetOnIndicators().Count() != 0 &&
					bomb.GetOffIndicators().Count() != 0 &&
					bomb.GetPortPlateCount() != 0 )
					ret.AddRange(new int[] {CLUBS, DIAMONDS, HEARTS, SPADES});
				else ret.Add(CLUBS);
				return ret;
			}
			case 3:
			{
				List<int> ret = new List<int>();
				ret.AddRange(new int[] {HEARTS, DIAMONDS});
				return ret;
			}
			case 4:
			{
				List<int> ret = new List<int>();
				ret.AddRange(new int[] {SPADES, CLUBS});
				return ret;
			}
			case 5:
			{
				List<int> ret = new List<int>();
				if(date == "Saturday" || date == "Sunday" )
					ret.AddRange(new int[] {CLUBS, DIAMONDS, HEARTS, SPADES});
				else ret.Add(DIAMONDS);
				return ret;
			}
			case 6:
			{
				List<int> ret = new List<int>();
				ret.Add(HEARTS);
				return ret;
			}
			case 7:
			{
				List<int> ret = new List<int>();
				if( startTime % 7 == 0 ||
					bomb.GetModuleNames().Count() % 7 == 0 ||
					bomb.GetSerialNumberNumbers().Sum() % 7 == 0 )
					ret.AddRange(new int[] {CLUBS, DIAMONDS, HEARTS, SPADES});
				else ret.Add(SPADES);
				return ret;
			}
			case 10:
			{
				List<int> ret = new List<int>();
				if(bomb.GetSerialNumberNumbers().Sum() % 22 == 10)
				{
					ret.AddRange(new int[] {CLUBS, DIAMONDS, HEARTS, SPADES});
					return ret;
				}
				arcana = bomb.GetSerialNumberNumbers().Sum() % 22;
				return GetValidSuits(bomb, suits, startTime, date, firstCard);
			}
			case 13:
			{
				List<int> ret = new List<int>();
				for(int i = 0; i <= 3; i++)
				{
					if(!suits.Exists(x => x == i))
						ret.Add(i);
				}
				return ret;
			}
			case 14:
			{
				List<int> ret = new List<int>();
				if( bomb.GetModuleNames().Count() - bomb.GetSolvableModuleNames().Count() != 0 )
					ret.AddRange(new int[] {CLUBS, DIAMONDS, HEARTS, SPADES});
				else ret.Add(SPADES);
				return ret;
			}
			case 15:
			{
				List<int> ret = new List<int>();
				ret.AddRange(new int[] {HEARTS, DIAMONDS});
				return ret;
			}
			case 17:
			{
				List<int> ret = new List<int>();
				
				if(firstCard.GetType() == typeof(StandardCard))
				{
					if(firstCard.rank >= 2 && firstCard.rank <= 6) ret.AddRange(new int[] {StandardCard.SPADES});
					else if(firstCard.rank >= 7 && firstCard.rank <= 9) ret.AddRange(new int[] {StandardCard.CLUBS});
					else ret.AddRange(new int[] {StandardCard.DIAMONDS, StandardCard.HEARTS});
				}
				else if(firstCard.GetType() == typeof(MetropolitanCard))
				{
					if(firstCard.rank >= 2 && firstCard.rank <= 6) ret.AddRange(new int[] {StandardCard.SPADES, StandardCard.CLUBS});
					else if(firstCard.rank >= 7 && firstCard.rank <= 9) ret.AddRange(new int[] {StandardCard.SPADES});
					else ret.AddRange(new int[] {StandardCard.CLUBS});
				}
				else if(firstCard.GetType() == typeof(MaritimeCard))
				{
					if(firstCard.rank >= 2 && firstCard.rank <= 6) ret.AddRange(new int[] {StandardCard.SPADES});
					else if(firstCard.rank >= 7 && firstCard.rank <= 9) ret.AddRange(new int[] {StandardCard.CLUBS});
					else ret.AddRange(new int[] {StandardCard.CLUBS});
				}
				else if(firstCard.GetType() == typeof(ArticCard))
				{
					if(firstCard.rank >= 2 && firstCard.rank <= 6) ret.AddRange(new int[] {StandardCard.SPADES});
					else if(firstCard.rank >= 7 && firstCard.rank <= 9) ret.AddRange(new int[] {StandardCard.SPADES, StandardCard.CLUBS});
					else ret.AddRange(new int[] {StandardCard.HEARTS, StandardCard.DIAMONDS});
				}
				else if(firstCard.GetType() == typeof(TropicalCard))
				{
					if(firstCard.rank >= 2 && firstCard.rank <= 6) ret.AddRange(new int[] {StandardCard.HEARTS, StandardCard.DIAMONDS});
					else if(firstCard.rank >= 7 && firstCard.rank <= 9) ret.AddRange(new int[] {StandardCard.SPADES, StandardCard.CLUBS});
					else ret.AddRange(new int[] {StandardCard.CLUBS});
				}
				else if(firstCard.GetType() == typeof(OasisCard))
				{
					if(firstCard.rank >= 2 && firstCard.rank <= 6) ret.AddRange(new int[] {StandardCard.SPADES});
					else if(firstCard.rank >= 7 && firstCard.rank <= 9) ret.AddRange(new int[] {StandardCard.SPADES, StandardCard.CLUBS});
					else ret.AddRange(new int[] {StandardCard.CLUBS});
				}
				else
				{
					ret.AddRange(new int[] {SPADES, DIAMONDS, HEARTS, CLUBS});
				}

				return ret;
			}
		}

		return suits;
	}
}