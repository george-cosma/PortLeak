using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoldenFile : GenericFloater, IPointerClickHandler
{
	public event Action OnClick;

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick?.Invoke();
		Destroy(this.gameObject);
	}
}
