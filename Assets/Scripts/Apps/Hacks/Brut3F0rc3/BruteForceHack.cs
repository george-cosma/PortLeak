using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BruteForceHack : Hack
{
	[Header("Loading Objects")]
	[SerializeField]
	protected Slider LoadingBar;

	// === Challenge Objects ===
	[Header("Challenge Objects")]
	[SerializeField]
	protected List<GameObject> LetterHolders = new List<GameObject>();
	[SerializeField]
	protected GameObject LetterPrefab;
	[SerializeField]
	protected RectTransform SelectorRT;

	[Header("Btrut3F0rc3 Settings")]
	[SerializeField]
	protected Color DefaultLetterColor;
	[SerializeField]
	protected Color CorrectLetterColor;
	[SerializeField]
	protected float LetterSpawnDelay = 1f;
	[SerializeField]
	protected List<string> Words = new List<string>(new string[] 
	{ 
		"matrix",
		"rotten",
		"spring",
		"leader",
		"barrel",
		"degree",
		"winner",
		"letter",
		"assume",
		"parade",
		"python"
	});


	private RectTransform rt;
	private string correctWord = "";
	private int currentLetter = 1;
	private float currentLetterTimer = 0f;

	protected override void Start()
	{
		base.Start();
		correctWord = Words[Random.Range(0, Words.Count)];
		rt = GetComponent<RectTransform>();
	}

	protected override void DoLoadingState()
	{
		base.DoLoadingState();

		LoadingBar.value = currentLoadTime / totalLoadTime;
	}

	protected void Update()
	{
		if (!LinkedWindow.Focused) return;
		if (hackState != HackState.CHALLENGE) return;
		if (currentLetter > 6) return;
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			bool hitALetter = false;
			float moveAmount = 0;
			foreach (var letter in LetterHolders[currentLetter - 1].GetComponentsInChildren<Letter>())
			{
				var letterRT = letter.rt;
				moveAmount = letterRT.rect.width;
				if (SelectorRT.Overlaps(letterRT,true))
				{
					hitALetter = true;
					if(letter.correct_letter == true)
					{
						currentLetter++;
						if (currentLetter > 6)
						{
							ChallengeSuccess();
							return;
						}
						SelectorRT.parent.GetComponent<RectTransform>().anchoredPosition += new Vector2(moveAmount, 0);
						return;
					}
				}
			}
			// only wrong letter(s) hit:
			if (hitALetter)
			{
				currentLetter--;
				if (currentLetter <= 0)
				{
					ChallengeFailure();
					return;
				}
				SelectorRT.parent.GetComponent<RectTransform>().anchoredPosition -= new Vector2(moveAmount, 0);
			}
		}
	}

	protected override void DoChallengeState()
	{

		if (currentLetterTimer < LetterSpawnDelay)
		{
			currentLetterTimer += Time.fixedDeltaTime;
		}
		else
		{
			currentLetterTimer = 0;
			for (int index = 0; index < LetterHolders.Count; index++)
			{
				GameObject letterHolder = LetterHolders[index];

				var letter = Instantiate(LetterPrefab, letterHolder.transform).GetComponent<Letter>();
				letter.Start();
				letter.correct_letter = Random.Range(0f, 1f) >= 0.8f;
				if (letter.correct_letter)
				{
					letter.Label.text = correctWord[index].ToString().ToUpper();
					letter.Label.color = CorrectLetterColor;
				}
				else
				{
					do
					{
						letter.Label.text = ((char)('A' + Random.Range(0, 26))).ToString();
					}
					while (letter.Label.text == correctWord[index].ToString().ToUpper());
					letter.Label.color = DefaultLetterColor;
				}
			}
		}

	}

}
