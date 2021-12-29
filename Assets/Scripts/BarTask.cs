using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarTask : MonoBehaviour, IPointerClickHandler
{
    public Color FlashColor { get; private set; } = new Color(255f/255, 234f/255, 71f/255);
    public Color DefaultColor { get; private set; }

    public bool Flashing { get; private set; } = false;
    public float FlashInterval = 5;

    public Window window;
    public Image icon;

    private FlashState m_flashState = FlashState.INACTIVE;
    private float m_flashTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        DefaultColor = icon.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_flashTimer < FlashInterval)
		{
            m_flashTimer += Time.deltaTime;
		}
        else
		{
            if(Flashing && m_flashState == FlashState.INACTIVE)
			{
                m_flashState = FlashState.ACTIVE;
                icon.color = FlashColor;

                m_flashTimer = 0;
			}
            else if(m_flashState == FlashState.ACTIVE)
			{
                m_flashState = FlashState.INACTIVE;
                icon.color = DefaultColor;

                m_flashTimer = 0; 
            }
		}

    }

	public void RequestFocus() => Flashing = true;
	public void FocusRecieved() => Flashing = false;

	public void OnPointerClick(PointerEventData eventData)
	{
		FocusRecieved();
        window.BringToFocus();
	}
}

public enum FlashState
{
    INACTIVE = 0,
    ACTIVE = 1
}