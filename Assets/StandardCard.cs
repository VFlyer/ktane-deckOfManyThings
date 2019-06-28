using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

class StandardCard
{
    static public readonly int SPADES = 3;
    static public readonly int HEARTS = 2;
    static public readonly int DIAMONDS = 1;
    static public readonly int CLUBS = 0;

	static protected System.Random rnd = new System.Random();

    public int suit;
    public int rank;
    public int order;

    public StandardCard()
    {
        suit = rnd.Next() % 4;
        rank = rnd.Next() % 13 + 1;
    }

    public static String GetSuit(int suit)
    {
        switch(suit)
        {
            case 3: return "Spades";
            case 2: return "Hearts";
            case 1: return "Diamonds";
            case 0: return "Clubs";
        }

        return "";
    }

    public static String GetRank(int rank)
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

        return "";
    }

    public virtual void PrintLogMessage(int moduleId)
    {
		Debug.LogFormat("[The Deck of Many Things #{0}] Card Nº {1} is a Standard {2} of {3}.", moduleId, order + 1, GetRank(rank), GetSuit(suit));
    }

    public virtual void CalcValue(KMBombInfo bomb)
    {

    }
}