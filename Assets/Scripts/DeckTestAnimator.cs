using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTestAnimator : MonoBehaviour {

    public KMSelectable flipAllCards, flipACardRight, flipACardLeft;
	public DeckRenderAnimator deckRenderer;
	public KMAudio audioSelf;

	bool forceAbandon = false;
	// Use this for initialization
	void Start () {

		flipAllCards.OnInteract += delegate {
			if (!deckRenderer.isAnimatingCard())
			{
				forceAbandon = false;
				StartCoroutine(HandleFlipBackAndForth());
				KMBombModule modSelf = GetComponent<KMBombModule>();
				if (modSelf != null)
					modSelf.HandlePass();
			}
			else if (!forceAbandon)
            {
				forceAbandon = true;
            }
            return false;
		};
		flipACardLeft.OnInteract += delegate
		{
			if (!deckRenderer.isAnimatingCard() && deckRenderer.getCurIdx() > 0)
				StartCoroutine(HandleAnimFlipL());
			return false;
		};
		flipACardRight.OnInteract += delegate
		{
			if (!deckRenderer.isAnimatingCard() && deckRenderer.getCurIdx() < deckRenderer.allCards.Length)
				StartCoroutine(HandleAnimFlipR());
			return false;
		};

	}
	IEnumerator HandleFlipBackAndForth()
    {
		
		while (!forceAbandon)
        {
            for (int x = deckRenderer.getCurIdx(); x < deckRenderer.allCards.Length; x++)
				yield return HandleAnimFlipR();
			for (int x = deckRenderer.getCurIdx(); x > 0; x--)
				yield return HandleAnimFlipL();
		}
		yield return null;
    }
	IEnumerator HandleAnimFlipR()
    {
		audioSelf.PlaySoundAtTransform("card_flick_2", transform);
		yield return deckRenderer.HandleFlipForwardAnim();
		yield return null;
    }
	IEnumerator HandleAnimFlipL()
	{
		audioSelf.PlaySoundAtTransform("card_flick_2", transform);
		yield return deckRenderer.HandleFlipBackwardsAnim();
		yield return null;
	}


	// Update is called once per frame
	void Update () {

	}
}
