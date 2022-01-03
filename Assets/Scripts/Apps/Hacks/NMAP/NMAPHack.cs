using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NMAPHack : Hack
{
	[Header("Loading Objects")]
	[SerializeField]
	protected Slider LoadingBar;
	
	// === Challenge Objects ===
	[Header("Challenge Objects")]
	[SerializeField]
	protected GameObject ContinueQuestionHolder;
	[SerializeField]
	protected Button ContinueYesButton;
	[SerializeField]
	protected Button ContinueNoButton;

	[SerializeField]
	protected GameObject CrashQuestionHolder;
	[SerializeField]
	protected Button CrashYesButton;
	[SerializeField]
	protected Button CrashNoButton;

	private bool challengeStarted = false;

	protected override void Start()
	{
		base.Start();

		ContinueYesButton.onClick.AddListener(() => ChallengeSuccess());
		CrashNoButton.onClick.AddListener(() => ChallengeSuccess());

		ContinueNoButton.onClick.AddListener(() => ChallengeFailure());
		CrashYesButton.onClick.AddListener(() => ChallengeFailure());
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
		base.ChallengeSuccess();
	}

	protected override void DoChallengeState()
	{
		if (!challengeStarted)
		{
			challengeStarted = true;

			int randomValue = Random.Range(0, 2);
			if (randomValue == 0)
			{
				ContinueQuestionHolder.SetActive(true);
				CrashQuestionHolder.SetActive(false);
			}
			else
			{
				ContinueQuestionHolder.SetActive(false);
				CrashQuestionHolder.SetActive(true);
			}
		}
	}

}
