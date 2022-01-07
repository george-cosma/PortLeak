using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChargeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    public bool IsPressed { get; private set; } = false;

	public event Action OnStartCharging;
	public event Action OnStopCharging;

	public void OnPointerDown(PointerEventData eventData)
	{
		IsPressed = true;
		OnStartCharging?.Invoke();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		IsPressed = false;
		OnStopCharging?.Invoke();
	}
}
