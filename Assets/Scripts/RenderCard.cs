using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCard : MonoBehaviour {

	public Renderer frontCard, backCard;
	public GameObject frontCardObject, backCardObject;


	// Use this for initialization
	void Start () {

	}

	public void ChangeFrontMat(Material selectedMat)
    {
		frontCard.material = selectedMat;
    }
	public void ChangeBackMat(Material selectedMat)
	{
		backCard.material = selectedMat;
	}
	public void RotateCard(Vector3 direction)
    {
		frontCardObject.transform.Rotate(-direction);
		backCardObject.transform.Rotate(direction);
    }

	// Update is called once per frame
	void Update () {

	}
}
