using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

class MaritimeCard : StandardCard {

    public int fakeRank;

    public MaritimeCard()
    {
        fakeRank = rnd.Next() % 8 + 11;
        suit = rnd.Next() % 4;
    }

    public override void PrintLogMessage(int moduleId)
    {
		Debug.LogFormat("[The Deck of Many Things #{0}] Card Nº {1} is a Maritime {2} of {3} (Real Value: {4} of {3}).", moduleId, order + 1, fakeRank + "", GetSuit(suit), GetRank(rank));
    }

    public override void CalcValue(KMBombInfo bomb)
    {
        rank = fakeRank * (bomb.GetBatteryCount() + 1);
        if(bomb.GetPortPlates().Any((x) => x.Length == 0))
            rank += 10;
        rank /= (bomb.GetOnIndicators().Count() + 1);
        if(Array.Exists(bomb.GetSerialNumberLetters().ToArray(), x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U'))
            rank -= fakeRank;
        while(rank < 0)
            rank += 13;
        rank = rank % 13;
        if(rank == 0)
            rank = 13;
    }
}