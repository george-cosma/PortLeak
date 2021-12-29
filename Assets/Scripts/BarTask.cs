using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarTask : MonoBehaviour, IPointerClickHandler
{
    public Color FlashColor { get; private set; } = new Color(215f / 255, 242f / 255, 0f / 255);
    public Color DefaultColor { get; private set; }

    public bool Flashing { get; private set; } = false;
    public float FlashInterval = 0.5f;

    public Window LinkedWindow;

    private FlashState m_flashState = FlashState.INACTIVE;
    private float m_flashTimer = 0;
    protected Image m_Image;

    // Start is called before the first frame update
    void Start()
    {
        m_Image = GetComponent<Image>();
        DefaultColor = m_Image.color;
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
                m_Image.color = FlashColor;

                m_flashTimer = 0;
			}
            else if(m_flashState == FlashState.ACTIVE)
			{
                m_flashState = FlashState.INACTIVE;
                m_Image.color = DefaultColor;

                m_flashTimer = 0; 
            }
		}

    }

	public void RequestFocus() => Flashing = true;
	public void FocusRecieved() => Flashing = false;

	public void OnPointerClick(PointerEventData eventData)
	{
		FocusRecieved();
        LinkedWindow?.BringToFocus();
	}
}

public enum FlashState
{
    INACTIVE = 0,
    ACTIVE = 1
}