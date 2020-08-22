using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckRenderAnimator : MonoBehaviour {

	public RenderCard[] allCards;

	public GameObject startPoint, midPoint, endPoint;
	public float cardThickness = 0;
	int curIdx = 0;
    private bool isAnimating = false;
	// Use this for initialization
	void Start () {
        for (int x = 0; x < allCards.Length; x++)
        {
			RenderCard card = allCards[x];
			card.transform.localPosition = startPoint.transform.localPosition + Vector3.up * cardThickness * (allCards.Length - 1 - x);
		}
	}
	public bool isAnimatingCard()
    {
		return isAnimating;
    }
	public int getCurIdx()
    {
		return curIdx;
    }
	public IEnumerator HandleFlipForwardAnim()
	{
		isAnimating = true;
		int idx = curIdx;
		if (idx < allCards.Length)
		{
			RenderCard card = allCards[idx];
			Vector3 startDestination = startPoint.transform.localPosition + Vector3.up * cardThickness * (allCards.Length - 1 - idx),
				endDestination = endPoint.transform.localPosition + (Vector3.up * cardThickness * idx),
				midPointDestination = midPoint.transform.localPosition;

			for (int x = 0; x <= 5; x++)
			{
				card.RotateCard(Vector3.forward * 15);
				card.gameObject.transform.localPosition = (midPointDestination * x + startDestination * (5 - x)) / 5f;
				yield return null;
			}
			for (int x = 0; x <= 5; x++)
			{
				card.RotateCard(Vector3.forward * 15);
				card.gameObject.transform.localPosition = (endDestination * x + midPointDestination * (5 - x)) / 5f;
				yield return null;
			}
			curIdx++;
		}
		yield return null;
		isAnimating = false;
	}
	public IEnumerator HandleFlipBackwardsAnim()
	{
		isAnimating = true;
		if (curIdx > 0)
		{
			curIdx--;
			RenderCard card = allCards[curIdx];
			Vector3 startDestination = startPoint.transform.localPosition + Vector3.up * cardThickness * (allCards.Length - 1 - curIdx),
				endDestination = endPoint.transform.localPosition + (Vector3.up * cardThickness * curIdx),
				midPointDestination = midPoint.transform.localPosition;
			for (int x = 0; x <= 5; x++)
			{
				card.RotateCard(Vector3.forward * -15);
				card.gameObject.transform.localPosition = (endDestination * (5 - x) + midPointDestination * x) / 5f;
				yield return null;
			}
			for (int x = 0; x <= 5; x++)
			{
				card.RotateCard(Vector3.forward * -15);
				card.gameObject.transform.localPosition = (midPointDestination * (5 - x) + startDestination * x) / 5f;
				yield return null;
			}
		}
		yield return null;
		isAnimating = false;
	}

	public IEnumerator HandleFlipAllForwardAnim()
    {
		isAnimating = true;
		for (int idx = curIdx; idx < allCards.Length; idx++)
		{
			RenderCard card = allCards[idx];
			Vector3 startDestination = startPoint.transform.localPosition + Vector3.up * cardThickness * (allCards.Length - 1 - idx),
				endDestination = endPoint.transform.localPosition + (Vector3.up * cardThickness * idx),
				midPointDestination = midPoint.transform.localPosition;

			for (int x = 0; x <= 5; x++)
			{
				card.RotateCard(Vector3.forward * 15);
				card.gameObject.transform.localPosition = (midPointDestination * x + startDestination * (5 - x)) / 5f;
				yield return null;
			}
			for (int x = 0; x <= 5; x++)
			{
				card.RotateCard(Vector3.forward * 15);
				card.gameObject.transform.localPosition = (endDestination * x + midPointDestination * (5 - x)) / 5f;
				yield return null;
			}
			curIdx++;
		}
		yield return null;
		isAnimating = false;
    }

	public IEnumerator HandleFlipAllBackwardsAnim()
	{
		isAnimating = true;
		for (int idx = curIdx; idx > 0; idx--)
		{
			curIdx--;
			RenderCard card = allCards[curIdx];
			Vector3 startDestination = startPoint.transform.localPosition + Vector3.up * cardThickness * (allCards.Length - 1 - idx),
				endDestination = endPoint.transform.localPosition + (Vector3.up * cardThickness * idx),
				midPointDestination = midPoint.transform.localPosition;
			for (int x = 0; x <= 5; x++)
			{
				card.RotateCard(Vector3.forward * -15);
				card.gameObject.transform.localPosition = (endDestination * (5 - x) + midPointDestination * x) / 5f;
				yield return null;
			}
			for (int x = 0; x <= 5; x++)
			{
				card.RotateCard(Vector3.forward * -15);
				card.gameObject.transform.localPosition = (midPointDestination * (5 - x) + startDestination * x) / 5f;
				yield return null;
			}
		}
		yield return null;
		isAnimating = false;
	}

}
