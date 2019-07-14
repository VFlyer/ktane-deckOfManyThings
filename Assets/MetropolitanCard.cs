using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;

class MetropolitanCard : StandardCard {

    public int fakeSuit;

    public MetropolitanCard()
    {
        fakeSuit = rnd.Range(0, 4);
        rank = rnd.Range(0, 13) + 1;
    }

    public override void PrintLogMessage(int moduleId)
    {
		Debug.LogFormat("[The Deck of Many Things #{0}] Card Nº {1} is a Metropolitan {2} of {3} (Real Value: {2} of {4}).", moduleId, order + 1, GetRank(rank), GetSuit(fakeSuit), GetSuit(suit));
    }

    public override void CalcValue(KMBombInfo bomb)
    {
        int[] serie = new int[4];
        if(bomb.GetBatteryCount() % 2 == 0)
        {
            switch(fakeSuit)
            {
                case 0: serie = new int[] {HEARTS, CLUBS, SPADES, DIAMONDS}; break;
                case 1: serie = new int[] {CLUBS, HEARTS, DIAMONDS, SPADES}; break;
                case 2: serie = new int[] {DIAMONDS, SPADES, HEARTS, CLUBS}; break;
                case 3: serie = new int[] {SPADES, DIAMONDS, CLUBS, HEARTS}; break;
            }
        }
        else
        {
            switch(fakeSuit)
            {
                case 0: serie = new int[] {HEARTS, DIAMONDS, SPADES, CLUBS}; break;
                case 1: serie = new int[] {CLUBS, SPADES, DIAMONDS, HEARTS}; break;
                case 2: serie = new int[] {DIAMONDS, CLUBS, HEARTS, SPADES}; break;
                case 3: serie = new int[] {SPADES, HEARTS, CLUBS, DIAMONDS}; break;
            }
        }

        suit = serie[bomb.GetOnIndicators().Count() % 4];
    }
}