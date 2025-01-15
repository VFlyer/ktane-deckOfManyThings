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
    public KMBombModule modSelf;
    public KMAudio AudioSelf;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved = false;
    private bool animating = false;

    public GameObject[] cards;
    public DeckRenderAnimator deckRenderAnimator;
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

    string date;
    int startTime;

    StandardCard[] deck;
    List<StandardCard> deckCreated;
    List<int> validRanks;
    List<int> validSuits;
    int currentCard = -1;
    int solution = -1;
    List<string> logs;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        GetComponent<KMBombModule>().OnActivate += Activate;

        btns[0].OnInteract += delegate () { ResetCards(); return false; };
        btns[1].OnInteract += delegate () { HandleSubmit(); return false; };
        prevCard.OnInteract += delegate () { PrevCard(); return false; };
        nextCard.OnInteract += delegate () { NextCard(); return false; };
    }

    void Activate()
    {
        date = DateTime.Now.DayOfWeek.ToString();
        startTime = (int)(bomb.GetTime() / 60);
        
        int cnt = 0, maxAttempts = 3;
        
        do
        {
            cnt++;
            if (logs == null)
                logs = new List<string>();
            else
                logs.Clear();
            FillDeck();
            CalcInitialValid();
            CalcSolution();
        } while( (solution > 14 || solution == -1) && cnt < maxAttempts);
        if (solution >= 0 && solution <= 14)
            logs.Insert(0, string.Format("After {0} attempt(s) the module was able to generate a deck that has the first valid card within 15 cards.", cnt));
        else
            logs.Insert(0, string.Format("After {0} attempt(s) the module was unable to generate a deck that has the first valid card within 15 cards.", cnt));
        foreach (string log in logs)
        {
            Debug.LogFormat("[The Deck of Many Things #{0}] {1}", moduleId, log);
        }
    }


    void FillDeck()
    {
        bool bruteForceGen = false;

        deckCreated = new List<StandardCard>();

        logs.Add("[The Deck of Many Things #" + moduleId + "] ------------Cards------------");
        if (bruteForceGen)
        {
            for (int i = 0; i < 40; i++)
                deckCreated.Add(new StandardCard(0, 12));
        }
        else
        {
            string[] debugList = { "Standard", "Metropolitan", "Maritime", "Arctic", "Tropical", "Oasis", "Celestial" };
            int[] cardDistributions = { 12, 8, 4, 4, 4, 6, 2 };

            while (cardDistributions.Sum() > 0)
            {
                int idxRandom = new int[] { 0, 1, 2, 3, 4, 5, 6 }.Where(a => cardDistributions[a] > 0).PickRandom();
                switch (debugList[idxRandom])
                {
                    default:
                    case "Standard":
                        deckCreated.Add(new StandardCard());
                        break;
                    case "Metropolitan":
                        deckCreated.Add(new MetropolitanCard());
                        break;
                    case "Maritime":
                        deckCreated.Add(new MaritimeCard());
                        break;
                    case "Arctic":
                        deckCreated.Add(new ArcticCard());
                        break;
                    case "Tropical":
                        deckCreated.Add(new TropicalCard());
                        break;
                    case "Oasis":
                        deckCreated.Add(new OasisCard());
                        break;
                    case "Celestial":
                        deckCreated.Add(new CelestialCard());
                        break;
                }
                cardDistributions[idxRandom]--;
            }


            /*
            for (int i = 0; i < 13; i++)
                deck.Add(new StandardCard());

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

            deck = deck.Shuffle();
            */
        }
        SetUpAllCards();
        for (int i = 0; i < deckCreated.Count; i++)
        {
            deckCreated[i].order = i;
            deckCreated[i].CalcValue(bomb);
            logs.Add(deckCreated[i].PrintLogMessage());

            //SetUpCard(i);
        }
        deck = deckCreated.ToArray();
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

        if (deckCreated[0].GetType() == typeof(StandardCard))
        {
            if (deckCreated[0].rank >= 2 && deckCreated[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.SPADES });
            else if (deckCreated[0].rank >= 7 && deckCreated[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.CLUBS });
            else validSuits.AddRange(new int[] { StandardCard.CLUBS, StandardCard.SPADES });
        }
        else if (deckCreated[0].GetType() == typeof(MetropolitanCard))
        {
            if (deckCreated[0].rank >= 2 && deckCreated[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.HEARTS });
            else if (deckCreated[0].rank >= 7 && deckCreated[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.SPADES });
            else validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.CLUBS });
        }
        else if (deckCreated[0].GetType() == typeof(MaritimeCard))
        {
            if (deckCreated[0].rank >= 2 && deckCreated[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.SPADES, StandardCard.HEARTS });
            else if (deckCreated[0].rank >= 7 && deckCreated[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.CLUBS, StandardCard.DIAMONDS });
            else validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.CLUBS });
        }
        else if (deckCreated[0].GetType() == typeof(ArcticCard))
        {
            if (deckCreated[0].rank >= 2 && deckCreated[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.SPADES });
            else if (deckCreated[0].rank >= 7 && deckCreated[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.DIAMONDS });
            else validSuits.AddRange(new int[] { StandardCard.SPADES, StandardCard.CLUBS });
        }
        else if (deckCreated[0].GetType() == typeof(TropicalCard))
        {
            if (deckCreated[0].rank >= 2 && deckCreated[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.SPADES, StandardCard.CLUBS });
            else if (deckCreated[0].rank >= 7 && deckCreated[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.DIAMONDS });
            else validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.CLUBS });
        }
        else if (deckCreated[0].GetType() == typeof(OasisCard))
        {
            if (deckCreated[0].rank >= 2 && deckCreated[0].rank <= 6) validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.SPADES });
            else if (deckCreated[0].rank >= 7 && deckCreated[0].rank <= 9) validSuits.AddRange(new int[] { StandardCard.HEARTS, StandardCard.DIAMONDS });
            else validSuits.AddRange(new int[] { StandardCard.DIAMONDS, StandardCard.CLUBS });
        }
        else
        {
            validSuits.Add(StandardCard.SPADES);
        }

        logs.Add("------------Solving------------");
        logs.Add("Starting Valid Ranks: [ " + GetRanks(validRanks) + "]. Starting Valid Suits: [ " + GetSuits(validSuits) + "].");
    }

    void CalcSolution()
    {
        int lastCard = 0;
        int celestialCardCount = 0;

        for (int i = 0; i < 40; i++)
        {
            if (deckCreated[i].GetType() == typeof(CelestialCard))
            {
                logs.Add("No valid cards from card " + (lastCard + 1) + " to card " + (i + 1) + ".");
                lastCard = i;
                celestialCardCount++;
                validRanks = ((CelestialCard)deckCreated[i]).GetValidRanks(bomb, validRanks, startTime, celestialCardCount, i);
                validSuits = ((CelestialCard)deckCreated[i]).GetValidSuits(bomb, validSuits, startTime, date, deckCreated[0]);
                logs.Add("Card " + (i + 1) + " changed Valid Ranks to [ " + GetRanks(validRanks) + "] and Valid Suits to [ " + GetSuits(validSuits) + "].");
            }
            else if (validSuits.Contains(deckCreated[i].suit) && validRanks.Contains(deckCreated[i].rank))
            {
                logs.Add("The first valid card is card Nº " + (i + 1) + ".");
                solution = i;
                return;
            }
        }

        logs.Add("No valid cards from card " + (lastCard + 1) + " to card 40. Solution is no card.");
        solution = -1;
    }

    string GetRanks(List<int> ranks)
    {
        string res = "";

        foreach (int rank in ranks)
        {
            res += StandardCard.GetRank(rank) + " ";
        }

        return res;
    }

    string GetSuits(List<int> suits)
    {
        string res = "";

        foreach (int suit in suits)
        {
            res += StandardCard.GetSuit(suit) + " ";
        }

        return res;
    }

    void SetUpAllCards()
    {
        string[] AllPossibleNames = { "StandardCard", "MetropolitanCard", "MaritimeCard", "ArcticCard", "TropicalCard", "OasisCard", "CelestialCard" };
        for (int x = 0; x < deckCreated.Count; x++)
        {
            switch (Array.IndexOf(AllPossibleNames,deckCreated[x].GetType().Name))
            {
                case 0: // Standard Card
                    deckRenderAnimator.allCards[x].ChangeFrontMat(standardDeck[4 * ConvertRank(deckCreated[x].rank) + (deckCreated[x]).suit]);
                    break;
                case 1: // Metropolitan Card
                    deckRenderAnimator.allCards[x].ChangeFrontMat(metropolitanDeck[4 * ConvertRank(((MetropolitanCard)deckCreated[x]).rank) + ((MetropolitanCard)deckCreated[x]).fakeSuit]);
                    break;
                case 2: // Maritime Card
                    deckRenderAnimator.allCards[x].ChangeFrontMat(maritimeDeck[4 * (((MaritimeCard)deckCreated[x]).fakeRank - 11) + ((MaritimeCard)deckCreated[x]).suit]);
                    break;
                case 3: // Arctic Card
                    deckRenderAnimator.allCards[x].ChangeFrontMat(articDeck[4 * ConvertRank(((ArcticCard)deckCreated[x]).fakeRank) + ((ArcticCard)deckCreated[x]).fakeSuit]);
                    break;
                case 4: // Tropical Card
                    switch (((TropicalCard)deckCreated[x]).color)
                    {
                        case 0:
                            {// Blue
                                deckRenderAnimator.allCards[x].ChangeFrontMat(tropicalBlueDeck[4 * ConvertRank(((TropicalCard)deckCreated[x]).fakeRank) + ((TropicalCard)deckCreated[x]).fakeSuit]);
                                break;
                            }
                        case 1:
                            {// Pink
                                deckRenderAnimator.allCards[x].ChangeFrontMat(tropicalPinkDeck[4 * ConvertRank(((TropicalCard)deckCreated[x]).fakeRank) + ((TropicalCard)deckCreated[x]).fakeSuit]);
                                break;
                            }
                        case 2:
                            {// Purple
                                deckRenderAnimator.allCards[x].ChangeFrontMat(tropicalPurpleDeck[4 * ConvertRank(((TropicalCard)deckCreated[x]).fakeRank) + ((TropicalCard)deckCreated[x]).fakeSuit]);
                                break;
                            }
                        case 3:
                            {// Green
                                deckRenderAnimator.allCards[x].ChangeFrontMat(tropicalGreenDeck[4 * ConvertRank(((TropicalCard)deckCreated[x]).fakeRank) + ((TropicalCard)deckCreated[x]).fakeSuit]);
                                break;
                            }
                        case 4:
                            {// Yellow
                                deckRenderAnimator.allCards[x].ChangeFrontMat(tropicalYellowDeck[4 * ConvertRank(((TropicalCard)deckCreated[x]).fakeRank) + ((TropicalCard)deckCreated[x]).fakeSuit]);
                                break;
                            }
                        case 5:
                            {// Orange
                                deckRenderAnimator.allCards[x].ChangeFrontMat(tropicalOrangeDeck[4 * ConvertRank(((TropicalCard)deckCreated[x]).fakeRank) + ((TropicalCard)deckCreated[x]).fakeSuit]);
                                break;
                            }
                    }
                    break;
                case 5: // Oasis Card
                    deckRenderAnimator.allCards[x].ChangeFrontMat(oasisDeck[4 * ConvertRank(((OasisCard)deckCreated[x]).fakeRank) + ((OasisCard)deckCreated[x]).fakeSuit]);
                    break;
                case 6: // Celestial Card
                    deckRenderAnimator.allCards[x].ChangeFrontMat(celestialDeck[((CelestialCard)deckCreated[x]).arcana]);
                    break;
            }
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
        AudioSelf.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
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
            modSelf.HandlePass();
        }
        else
        {
            if (currentCard != -1 && solution != -1)
                Debug.LogFormat("[The Deck of Many Things #{0}] Strike! Incorrectly selected card Nº {1}. Expected card Nº {2}.", moduleId, currentCard + 1, solution + 1);
            else if (currentCard == -1 && solution != -1)
                Debug.LogFormat("[The Deck of Many Things #{0}] Strike! Incorrectly selected no card. Expected card Nº {1}.", moduleId, solution + 1);
            else if (currentCard != -1 && solution == -1)
                Debug.LogFormat("[The Deck of Many Things #{0}] Strike! Incorrectly selected card Nº {1}. Expected no card.", moduleId, currentCard + 1);

            modSelf.HandleStrike();
        }
    }

    void ResetCards()
    {
        AudioSelf.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        btns[0].AddInteractionPunch(.5f);
        if (animating)
            return;

        if (currentCard == -1)
            return;

        StartCoroutine(ResetCardsAnim());
    }

    IEnumerator ResetCardsAnim()
    {
        int stop = currentCard;

        for (int i = 0; i <= stop; i++)
        {
            animating = true;
            yield return PrevCardAnim();
        }

        yield return null;
    }

    void PrevCard()
    {
        if (animating)
            return;
        if (currentCard == -1)
            return;

        animating = true;
        StartCoroutine(PrevCardAnim());
    }

    IEnumerator PrevCardAnim()
    {

        AudioSelf.PlaySoundAtTransform("card_flick_2", transform);

        yield return deckRenderAnimator.HandleFlipBackwardsAnim();

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
        StartCoroutine(NextCardAnim());
    }

    IEnumerator NextCardAnim()
    {

        AudioSelf.PlaySoundAtTransform("card_flick_2", transform);

        yield return deckRenderAnimator.HandleFlipForwardAnim();

        animating = false;

        yield return null;
    }

    //twitch plays
    IEnumerator TwitchHandleForcedSolve()
    {
        Debug.LogFormat("[The Deck of Many Things #{0}] Issuing force solve viva TP Handler.", moduleId);
        while (solution != currentCard)
        {
            if (currentCard > solution)
            {
                prevCard.OnInteract();
            }
            else
            {
                nextCard.OnInteract();
            }
            yield return null;
            while (animating)
                yield return true;
        }
        while (animating)
            yield return true;
        btns[1].OnInteract();
        yield return true;
    }

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
    private readonly string TwitchHelpMessage = "Go to a specified card with \"!{0} card <#>\" Valid cards are from card 1 to card 40. Flip to the next or previous card with \"!{0} card prev/previous/next/right/left/r/l\" To submit the current card: \"!{0} submit\" To put the cards back to initial state: \"!{0} reset\" Get the current card infomation with \"!{0} card info\"";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        if (Application.isEditor)
            command = command.Trim();

        if (Regex.IsMatch(command, @"^\s*reset\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            btns[0].OnInteract();
            yield break;
        }
        else if (Regex.IsMatch(command, @"^\s*submit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            btns[1].OnInteract();
            yield break;
        }
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(command, @"^\s*card\s+", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            string potentialSubCmd = command.Substring(4).Trim();
            if (Regex.IsMatch(potentialSubCmd, @"^\d+$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                yield return null;
                int dest;
                int.TryParse(parameters[1], out dest);
                dest--;
                if (currentCard < dest)
                {
                    int start = currentCard;
                    for (int i = 0; i < dest - start; i++)
                    {
                        nextCard.OnInteract();
                        do
                        {
                            yield return string.Format("trycancel The card flipping animation has been canceled. The card has stopped on card Nº {0}", currentCard);
                        }
                        while (animating);
                    }
                }
                else if (currentCard > dest)
                {
                    int start = currentCard;
                    for (int i = 0; i < start - dest; i++)
                    {
                        prevCard.OnInteract();
                        do
                        {
                            yield return string.Format("trycancel The card flipping animation has been canceled. The card has stopped on card Nº {0}", currentCard);
                        }
                        while (animating);
                    }
                }
                else
                {
                    yield return "sendtochaterror The module is already on the specified card!";
                }
            }
            else if (Regex.IsMatch(potentialSubCmd, @"^info$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                StandardCard selectedCard = deckCreated.ElementAtOrDefault(currentCard);
                if (selectedCard == null)
                    yield return string.Format("sendtochat That is not a card. I don't know what that is.");
                else
                    yield return string.Format("sendtochat {0}", selectedCard.PrintTPCardInfo());
                yield break;
            }
            else if (Regex.IsMatch(potentialSubCmd, @"^(next|r(ight)?)$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (currentCard == 39)
                {
                    yield return "sendtochaterror {0}, you are already at the end of the deck!";
                    yield break;
                }
                yield return null;
                nextCard.OnInteract();
                yield break;
            }
            else if (Regex.IsMatch(potentialSubCmd, @"^(prev(ious)?|l(eft)?)$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            {
                if (currentCard == -1)
                {
                    yield return "sendtochaterror {0}, you are already at the start of the deck!";
                    yield break;
                }
                yield return null;
                prevCard.OnInteract();
                yield break;
            }
            else
            {
                yield return string.Format("sendtochaterror Unknown subcommand \"{0}\"", potentialSubCmd);
                yield break;
            }
            yield break;
        }
    }
}