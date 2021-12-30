using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotesApp : MonoBehaviour
{
    private static string notesText = "Do not forget to do the dishes...!";
	private static List<NotesApp> notesApps = new List<NotesApp>();

    [SerializeField]
    protected TMP_Text m_NotesContents;

	private void OnEnable()
	{
		notesApps.Add(this);
		UpdateText();
	}
	private void OnDisable()
	{
		notesApps.Remove(this);
	}

	public void UpdateText()
	{
		m_NotesContents.text = notesText;
	}

	public static void SetNotesText(string text)
	{
        notesText = text;
		foreach (var notesApp in notesApps)
		{
			notesApp.UpdateText();
		}
	}
}
