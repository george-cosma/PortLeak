using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoldenFile : GenericFloater, IPointerClickHandler
{
	public DirtyCowApp app;

	public void OnPointerClick(PointerEventData eventData)
	{
		app.ChallengeComplete();
		Destroy(this.gameObject);
	}
}
