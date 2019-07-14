using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using System.Text.RegularExpressions;
using rnd = UnityEngine.Random;

public class deckOfManyThingsScript : MonoBehaviour
{
    public KMBombInfo bomb;
    public KMAudio Audio;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved = false;
    private bool animating = false;

    public GameObject[] cards;
    public KMSelectable[] btns;
    public KMSelectable prevCard;
    public KMSelectable nextCard;

    public Material[] standardDeck;
    public Material[] metropolitanDeck;
    public Material[] maritimeDeck;
    public Material[] articDeck;
    public Material[] tropicalBlueDeck;
    public Material[] tropicalPinkDeck;
    public Material[] tropicalPurpleDeck;
    public Material[] tropicalGreenDeck;
    public Material[] tropicalYellowDeck;
    public Material[] tropicalOrangeDeck;
    public Material[] oasisDeck;
    public Material[] celestialDeck;

    String date;
    int startTime;

    StandardCard[] deck;
    List<int> validRanks;
    List<int> validSuits;
    int currentCard = -1;
    int solution = -1;

    void Awake()
    {
        date = DateTime.Now.DayOfWeek.ToString();
        startTime = (int)(bomb.GetTime() / 60);

        moduleId = moduleIdCounter++;

        btns[0].OnInteract += delegate () { ResetCards(); return false; };
        btns[1].OnInteract += delegate () { HandleSubmit(); return false; };
        prevCard.OnInteract += delegate () { PrevCard(); return false; };
        nextCard.OnInteract += delegate () { NextCard(); return false; };
    }

    void Start()
    {
        FillDeck();
        CalcInitialValid();
        CalcSolution();
    }

    void FillDeck()
    {
        deck = new StandardCard[40];

        Debug.LogFormat("[The Deck of Many Things #{0}] ------------Cards------------", moduleId);

        for (int i = 0; i < 13; i++)
            deck[i] = new StandardCard();

        for (int i = 13; i < 21; i++)
            deck[i] = new MetropolitanCard();

        for (int i = 21; i < 25; i++)
            deck[i] = new MaritimeCard();

        for (int i = 25; i < 28; i++)
            deck[i] = new ArticCard();

        for (int i = 28; i < 33; i++)
            deck[i] = new TropicalCard();

        for (int i = 33; i < 38; i++)
            deck[i] = new OasisCard();

        for (int i = 38; i < 40; i++)
            deck[i] = new CelestialCard();

        deck = deck.OrderBy(x => rnd.Range(0, 1000)).ToArray();

        for (int i = 0; i < deck.Length; i++)
        {
            deck[i].order = i;
            deck[i].CalcValue(bomb);
            deck[i].PrintLogMessage(moduleId);

            SetUpCard(i);
        }
    }

    void CalcInitialValid()
    {
        validRanks = new List<int>();
        validSuits = new List<int>();

        if (bomb.IsIndicatorPresent(Indicator.SND)) validRanks.Add(2);
        if (bomb.IsIndicatorPresent(Indicator.IND) || bomb.IsPortPresent(Port.Parallel)) validRanks.Add(3);
        if (bomb.IsPortPresent(Port.Serial)) validRanks.Add(4);
        if (bomb.IsIndicatorPresent(Indicator.MSA)) validRanks.Add(5);
        if (bomb.IsIndicatorPresent(Indicator.NSA) || bomb.IsPortPresent(Port.DVI)) validRanks.Add(6);
        if (bomb.IsIndicatorPresent(Indicator.FRQ)) validRanks.Add(7);
        if (bomb.IsIndicatorPresent(Indicator.TRN) || bomb.IsPortPresent(Port.StereoRCA)) validRanks.Add(8);
        if (bomb.IsIndicatorPresent(Indicator.BOB)) validRanks.Add(9);
        if (bomb.IsIndicatorPresent(Indicator.SIG)) validRanks.Add(10);
        if (bomb.IsIndicatorPresent(Indicator.FRK)) validRanks.Add(11);
        if (bomb.IsIndicatorPresent(Indicator.CLR) || bomb.IsPortPresent(Port.RJ45)) validRanks.Add(12);
        if (bomb.IsIndicatorPresent(Indicator.CAR) || bomb.IsPortPresent(Port.PS2)) validRanks.Add(13);
        validRanks.Add(1);

        if (deck[0].GetType() == typeof(StandardCard))
        {
            if (deck[0].rank >= 2 && deck[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.SPADES });
            else if (deck[0].rank >= 7 && deck[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.CLUBS });
            else validSuits.AddRange(new int[] { StandardCard.CLUBS, StandardCard.SPADES });
        }
        else if (deck[0].GetType() == typeof(MetropolitanCard))
        {
            if (deck[0].rank >= 2 && deck[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.HEARTS });
            else if (deck[0].rank >= 7 && deck[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.SPADES });
            else validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.CLUBS });
        }
        else if (deck[0].GetType() == typeof(MaritimeCard))
        {
            if (deck[0].rank >= 2 && deck[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.SPADES, StandardCard.HEARTS });
            else if (deck[0].rank >= 7 && deck[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.CLUBS, StandardCard.DIAMONDS });
            else validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.CLUBS });
        }
        else if (deck[0].GetType() == typeof(ArticCard))
        {
            if (deck[0].rank >= 2 && deck[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.SPADES });
            else if (deck[0].rank >= 7 && deck[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.DIAMONDS });
            else validSuits.AddRange(new int[] { StandardCard.SPADES, StandardCard.CLUBS });
        }
        else if (deck[0].GetType() == typeof(TropicalCard))
        {
            if (deck[0].rank >= 2 && deck[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.SPADES, StandardCard.CLUBS });
            else if (deck[0].rank >= 7 && deck[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.DIAMONDS });
            else validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.CLUBS });
        }
        else if (deck[0].GetType() == typeof(OasisCard))
        {
            if (deck[0].rank >= 2 && deck[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.SPADES });
            else if (deck[0].rank >= 7 && deck[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.DIAMONDS });
            else validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.CLUBS });
        }
        else
        {
            validSuits.Add(StandardCard.SPADES);
        }

        Debug.LogFormat("[The Deck of Many Things #{0}] ------------Solving------------", moduleId);
        Debug.LogFormat("[The Deck of Many Things #{0}] Starting Valid Ranks are [ {1}]. Starting Valid Suits are [ {2}].", moduleId, GetRanks(validRanks), GetSuits(validSuits));
    }

    void CalcSolution()
    {
        int lastCard = 0;
        int celestialCardCount = 0;

        for (int i = 0; i < 40; i++)
        {
            if (deck[i].GetType() == typeof(CelestialCard))
            {
                Debug.LogFormat("[The Deck of Many Things #{0}] No valid cards from card {1} to card {2}.", moduleId, lastCard + 1, i + 1);
                lastCard = i;
                celestialCardCount++;
                validRanks = ((CelestialCard)deck[i]).GetValidRanks(bomb, validRanks, startTime, celestialCardCount, i);
                validSuits = ((CelestialCard)deck[i]).GetValidSuits(bomb, validSuits, startTime, date, deck[0]);
                Debug.LogFormat("[The Deck of Many Things #{0}] Card {1} changed Valid Ranks to [ {2}] and Valid Suits to [ {3}].", moduleId, i + 1, GetRanks(validRanks), GetSuits(validSuits));
            }
            else if (validSuits.Contains(deck[i].suit) && validRanks.Contains(deck[i].rank))
            {
                Debug.LogFormat("[The Deck of Many Things #{0}] Solution is card Nº {1}.", moduleId, i + 1);
                solution = i;
                return;
            }
        }

        Debug.LogFormat("[The Deck of Many Things #{0}] No valid cards from card {1} to card 40. Solution is no card.", moduleId, lastCard + 1);
    }

    String GetRanks(List<int> ranks)
    {
        string res = "";

        foreach (int rank in ranks)
        {
            res += StandardCard.GetRank(rank) + " ";
        }

        return res;
    }

    String GetSuits(List<int> suits)
    {
        string res = "";

        foreach (int suit in suits)
        {
            res += StandardCard.GetSuit(suit) + " ";
        }

        return res;
    }

    void SetUpCard(int i)
    {
        if (deck[i].GetType() == typeof(StandardCard))
        {
            cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = standardDeck[4 * ConvertRank(((StandardCard)deck[i]).rank) + ((StandardCard)deck[i]).suit];
        }
        else if (deck[i].GetType() == typeof(MetropolitanCard))
        {
            cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = metropolitanDeck[4 * ConvertRank(((MetropolitanCard)deck[i]).rank) + ((MetropolitanCard)deck[i]).fakeSuit];
        }
        else if (deck[i].GetType() == typeof(MaritimeCard))
        {
            cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = maritimeDeck[4 * (((MaritimeCard)deck[i]).fakeRank - 11) + ((MaritimeCard)deck[i]).suit];
        }
        else if (deck[i].GetType() == typeof(ArticCard))
        {
            cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = articDeck[4 * ConvertRank(((ArticCard)deck[i]).fakeRank) + ((ArticCard)deck[i]).fakeSuit];
        }
        else if (deck[i].GetType() == typeof(TropicalCard))
        {
            switch (((TropicalCard)deck[i]).color)
            {
                case 0:
                    {
                        cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = tropicalBlueDeck[4 * ConvertRank(((TropicalCard)deck[i]).fakeRank) + ((TropicalCard)deck[i]).fakeSuit];
                        break;
                    }
                case 1:
                    {
                        cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = tropicalPinkDeck[4 * ConvertRank(((TropicalCard)deck[i]).fakeRank) + ((TropicalCard)deck[i]).fakeSuit];
                        break;
                    }
                case 2:
                    {
                        cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = tropicalPurpleDeck[4 * ConvertRank(((TropicalCard)deck[i]).fakeRank) + ((TropicalCard)deck[i]).fakeSuit];
                        break;
                    }
                case 3:
                    {
                        cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = tropicalGreenDeck[4 * ConvertRank(((TropicalCard)deck[i]).fakeRank) + ((TropicalCard)deck[i]).fakeSuit];
                        break;
                    }
                case 4:
                    {
                        cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = tropicalYellowDeck[4 * ConvertRank(((TropicalCard)deck[i]).fakeRank) + ((TropicalCard)deck[i]).fakeSuit];
                        break;
                    }
                case 5:
                    {
                        cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = tropicalOrangeDeck[4 * ConvertRank(((TropicalCard)deck[i]).fakeRank) + ((TropicalCard)deck[i]).fakeSuit];
                        break;
                    }
            }
        }
        else if (deck[i].GetType() == typeof(OasisCard))
        {
            cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = oasisDeck[4 * ConvertRank(((OasisCard)deck[i]).fakeRank) + ((OasisCard)deck[i]).fakeSuit];
        }
        else if (deck[i].GetType() == typeof(CelestialCard))
        {
            cards[i].transform.Find("front").gameObject.GetComponentInChildren<Renderer>().material = celestialDeck[((CelestialCard)deck[i]).arcana];
        }
    }

    int ConvertRank(int i)
    {
        if (i >= 2 && i <= 10)
            return i - 2;

        if (i == 1)
            return 9;

        if (i == 11)
            return 10;

        if (i == 13)
            return 11;

        return i;
    }

    void HandleSubmit()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        btns[1].AddInteractionPunch(.5f);

        if (animating || moduleSolved)
            return;

        if (currentCard == solution)
        {
            if (currentCard != -1)
                Debug.LogFormat("[The Deck of Many Things #{0}] Correctly selected card Nº {1}. Module solved.", moduleId, currentCard + 1);
            else
                Debug.LogFormat("[The Deck of Many Things #{0}] Correctly selected no card. Module solved.", moduleId);
            moduleSolved = true;
            GetComponent<KMBombModule>().HandlePass();
        }
        else
        {
            if (currentCard != 0 && solution != 0)
                Debug.LogFormat("[The Deck of Many Things #{0}] Strike! Selected card Nº {1}. Expected card Nº {2}.", moduleId, currentCard + 1, solution + 1);
            else if (currentCard == 0 && solution != 0)
                Debug.LogFormat("[The Deck of Many Things #{0}] Strike! Selected no card. Expected card Nº {1}.", moduleId, solution + 1);
            else if (currentCard != 0 && solution == 0)
                Debug.LogFormat("[The Deck of Many Things #{0}] Strike! Selected card Nº {1}. Expected no card.", moduleId, currentCard + 1);

            GetComponent<KMBombModule>().HandleStrike();
        }
    }

    void ResetCards()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        btns[0].AddInteractionPunch(.5f);

        if (animating)
            return;

        if (currentCard == -1)
            return;

        StartCoroutine("ResetCardsAnim");
    }

    IEnumerator ResetCardsAnim()
    {
        int stop = currentCard;

        for (int i = 0; i <= stop; i++)
        {
            animating = true;
            yield return PrevCardAnim();
        }

        yield return new WaitForSeconds(0.0f);
    }

    void PrevCard()
    {
        if (animating)
            return;

        if (currentCard == -1)
            return;

        animating = true;
        StartCoroutine("PrevCardAnim");
    }

    IEnumerator PrevCardAnim()
    {
        float yDelta = 0.00039f - 0.00004f * currentCard;

        Audio.PlaySoundAtTransform("card_flick_2", transform);

        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = cards[currentCard].transform.localPosition;
            pos.x += 0.0078f;
            pos.y += yDelta;
            cards[currentCard].transform.localPosition = pos;
            cards[currentCard].transform.Find("bg").Rotate(0, 0, 18f);
            cards[currentCard].transform.Find("front").Rotate(0, 0, -18f);
            yield return new WaitForSeconds(0.002f);
        }

        currentCard--;
        animating = false;
    }

    void NextCard()
    {
        if (animating)
            return;

        if (currentCard == 39)
            return;

        currentCard++;
        animating = true;
        StartCoroutine("NextCardAnim");
    }

    IEnumerator NextCardAnim()
    {
        float yDelta = 0.00039f - 0.00004f * currentCard;

        Audio.PlaySoundAtTransform("card_flick_2", transform);

        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = cards[currentCard].transform.localPosition;
            pos.x -= 0.0078f;
            pos.y -= yDelta;
            cards[currentCard].transform.localPosition = pos;
            cards[currentCard].transform.Find("bg").Rotate(0, 0, -18f);
            cards[currentCard].transform.Find("front").Rotate(0, 0, 18f);
            yield return new WaitForSeconds(0.002f);
        }

        animating = false;

        yield return new WaitForSeconds(0.0f);
    }

    //twitch plays
    private bool cmdIsValid(string cmd)
    {
        char[] valids = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        if ((cmd.Length >= 1) && (cmd.Length <= 2))
        {
            foreach (char c in cmd)
            {
                if (!valids.Contains(c))
                {
                    return false;
                }
            }
            int temp = 0;
            int.TryParse(cmd, out temp);
            if (temp < 1 || temp > 40)
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} card <#> [Goes to card #, valid #'s are 1-40] | !{0} submit [Submits the current card] | !{0} reset [Goes back to initial state]";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        if (Regex.IsMatch(command, @"^\s*reset\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            btns[0].OnInteract();
            yield break;
        }
        if (Regex.IsMatch(command, @"^\s*submit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            btns[1].OnInteract();
            yield break;
        }
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*card\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            if(parameters.Length == 2)
            {
                if (cmdIsValid(parameters[1]))
                {
                    yield return null;
                    int dest = 0;
                    int.TryParse(parameters[1], out dest);
                    dest--;
                    if (currentCard < dest)
                    {
                        int start = currentCard;
                        for (int i = 0; i < dest - start; i++)
                        {
                            nextCard.OnInteract();
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                    else if (currentCard > dest)
                    {
                        int start = currentCard;
                        for (int i = 0; i < start - dest; i++)
                        {
                            prevCard.OnInteract();
                            yield return new WaitForSeconds(0.2f);
                        }
                    }
                    else
                    {
                        yield return "sendtochat I'm already on this card!";
                    }
                }
                yield break;
            }
            yield break;
        }
    }
}