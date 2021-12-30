using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Window : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public Button ExitButton;
    public TMP_Text WindowLabel;
    public string Title = "No Title!";
    public bool Focused = false;
	
	public BarTask LinkedBarTask;

	protected RectTransform m_Canvas_RectTransform;
	protected RectTransform m_Bar_RectTransform;
	protected RectTransform m_RectTransform;

    // Start is called before the first frame update
    protected virtual void Start()
    {
		m_Canvas_RectTransform = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
		m_Bar_RectTransform = GameObject.FindGameObjectWithTag("Bar").GetComponent<RectTransform>();

		m_RectTransform = GetComponent<RectTransform>();
        WindowLabel.SetText(Title);
        ExitButton.onClick.AddListener(ExitButton_Clicked);
    }

	protected virtual void Update() { }

	public virtual void ExitButton_Clicked()
	{
		Exit();
	}

	public virtual void Exit()
	{
		if (LinkedBarTask != null)
		{
			Destroy(LinkedBarTask.gameObject);
		}
		Destroy(this.gameObject);
	}

	public virtual void OnDrag(PointerEventData eventData)
	{
		//Debug.Log($"{this.gameObject.name} => Selected: {eventData.selectedObject}, Used: {eventData.used}");
		if(eventData.selectedObject != null) return;

		// x pixel delta ...... ScreenHeight
		// y pixel delta ...... canvasHeight
		// y = canvasHeight * x / ScreenHeight

		var delta = new Vector2(
			x: eventData.delta.x * m_Canvas_RectTransform.rect.width / Screen.width,
			y: eventData.delta.y * m_Canvas_RectTransform.rect.height / Screen.height
		);


		var newPosition = m_RectTransform.anchoredPosition + delta;
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
		BringToFocus();
	}

	public void RequestFocus()
	{
		LinkedBarTask?.RequestFocus();
	}

	public void BringToFocus()
	{
		Focused = true;
		LinkedBarTask?.FocusRecieved();
		WindowManager.instance.AnnounceNewFocus(this);
	}
}
