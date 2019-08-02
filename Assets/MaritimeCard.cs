using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;

class MaritimeCard : StandardCard {

    public int fakeRank;

    public MaritimeCard()
    {
        fakeRank = rnd.Range(0, 8) + 11;
        suit = rnd.Range(0, 4);
    }

    public override String PrintLogMessage()
    {
		return "Card Nº " + (order + 1) + " is a Maritime " + fakeRank + " of " + GetSuit(suit) + " (Real Value: " + GetRank(rank) + " of " + GetSuit(suit) + ").";
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