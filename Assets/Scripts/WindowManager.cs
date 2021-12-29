using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WindowManager : ScriptableSingleton<WindowManager>
{
	private Window m_focusedWindow = null;
	private Transform m_windowsParent = null;
	private Transform m_barTasksParent = null;

	public void OnEnable()
	{
		m_windowsParent = GameObject.FindGameObjectWithTag("WindowsParent").GetComponent<Transform>();
		m_barTasksParent = GameObject.FindGameObjectWithTag("Bar").GetComponent<Transform>();
	}

	public void AnnounceNewFocus(Window sender)
	{
		if (sender == m_focusedWindow) return;

		m_focusedWindow = sender;
		sender.transform.SetAsLastSibling();
	}

	public void CreateWindow(GameObject windowPrefab, GameObject barTaskPrefab)
	{
		Window window = Instantiate(windowPrefab, m_windowsParent).GetComponent<Window>();
		BarTask barTask = Instantiate(barTaskPrefab,m_barTasksParent).GetComponent<BarTask>();
		
		window.LinkedBarTask = barTask;
		barTask.LinkedWindow = window;

		AnnounceNewFocus(window);
	}
}