using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Window : MonoBehaviour, IDragHandler, IPointerDownHandler
{
	private RectTransform m_Canvas_RectTransform;
	private RectTransform m_Bar_RectTransform;
	private RectTransform m_RectTransform;
    public Button ExitButton;
    public TMP_Text WindowLabel;
    public string Title = "No Title!";
    public bool Focused = false;
	// public BarTask Task;

    // Start is called before the first frame update
    void Start()
    {
		m_Canvas_RectTransform = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
		m_Bar_RectTransform = GameObject.FindGameObjectWithTag("Bar").GetComponent<RectTransform>();

		m_RectTransform = GetComponent<RectTransform>();
        WindowLabel.SetText(Title);
        ExitButton.onClick.AddListener(TryExit);
    }

	public virtual void TryExit()
	{
        Destroy(this.gameObject);
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		if(eventData.selectedObject != null) return;

		var newPosition = m_RectTransform.anchoredPosition + eventData.delta;
		Vector2 bottomLeft = new Vector2(0,0);
		Vector2 topRight = new Vector2(m_RectTransform.rect.width, m_RectTransform.rect.height);

		// Keep the window on screen, and above the bar. The bar should always be visible.
		if (newPosition.x + bottomLeft.x < 0) return;
		if (newPosition.y + bottomLeft.y < m_Bar_RectTransform.rect.height) return;
		if (newPosition.x + topRight.x > m_Canvas_RectTransform.rect.width) return;
		if (newPosition.y + topRight.y > m_Canvas_RectTransform.rect.height) return;	

		m_RectTransform.anchoredPosition = newPosition;
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		// WindowManager.AnnounceNewFocus(this);
		Focused = true;
	}

	public void RequestFocus()
	{
		// Task.RequestFocus();
	}
}
