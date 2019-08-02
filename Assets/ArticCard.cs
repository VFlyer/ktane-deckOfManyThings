using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;

class ArticCard : StandardCard {

    public int fakeSuit;
    public int fakeRank;

    public ArticCard()
    {
        fakeSuit = rnd.Range(0, 4);
        fakeRank = rnd.Range(0, 13) + 1;
    }

    public override String PrintLogMessage()
    {
		return "Card Nº " + (order + 1) + " is an Arctic " + GetRank(fakeRank) + " of " +  GetSuit(fakeSuit) + " (Real Value: " + GetRank(rank) + " of " + GetSuit(suit) + ").";
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