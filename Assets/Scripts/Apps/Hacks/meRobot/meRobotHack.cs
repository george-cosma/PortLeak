using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class meRobotHack : Hack
{
	[Header("Loading Objects")]
	[SerializeField]
	protected Slider LoadingBar;

	[Header("Challenge Objects")]
	[SerializeField]
	protected List<GameObject> Images;
	[SerializeField]
	protected TMP_Dropdown DropDown;
	[SerializeField]
	protected Button ValidateButton;

	private bool challengeStarted;
	private int correctIndex = -1;

	protected override void Start()
	{
		base.Start();

		ValidateButton.onClick.AddListener(() =>
		{
			// TODO: make this fault-tolerant
			if(DropDown.value == correctIndex)
			{
				ChallengeSuccess();
			}
			else
			{
				ChallengeFailure();
			}
		});
	}

	protected override void DoLoadingState()
	{
		base.DoLoadingState();

		LoadingBar.value = currentLoadTime / totalLoadTime;
	}

	public override void ChallengeFailure()
	{
		challengeStarted = false;
		base.ChallengeFailure();
	}

	public override void ChallengeSuccess()
	{
		challengeStarted = false;
		correctIndex = -1;
		base.ChallengeSuccess();
	}

	protected override void DoChallengeState()
	{
		if (!challengeStarted)
		{
			challengeStarted = true;

			correctIndex = Random.Range(0, Images.Count);

			foreach (var image in Images)
			{
				image.SetActive(false);
			}

			Images[correctIndex].SetActive(true);
		}
	}

}
