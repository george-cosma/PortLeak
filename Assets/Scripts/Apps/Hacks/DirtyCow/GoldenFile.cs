using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoldenFile : GenericFloater, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Clicked");
		Destroy(this.gameObject);
	}
}
