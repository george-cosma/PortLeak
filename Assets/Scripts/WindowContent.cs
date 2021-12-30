using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowContent : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    protected Window LinkedWindow;

	public void OnDrag(PointerEventData eventData)
	{
		eventData.Use();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		LinkedWindow.BringToFocus();
		eventData.Use();
	}

}
