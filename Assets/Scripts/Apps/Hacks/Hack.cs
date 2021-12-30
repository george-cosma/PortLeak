using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : MonoBehaviour
{
	[SerializeField]
	protected Window LinkedWindow;


	[Header("Holders")]
	[SerializeField]
	protected GameObject LoadingObjectsHolder;
	[SerializeField]
	protected GameObject ChallengeObjectsHolder;

	// protected GameObject CleanChallenge;

	[Header("State Machine")]
	public HackState hackState = HackState.LOADING;

	[SerializeField]
	protected float totalLoadTime = 10;
	[SerializeField]
	protected float currentLoadTime = 0;
	[SerializeField]
	protected float minTimeForChallenges = 5;
	[SerializeField]
	protected float minTimeBetweenChallenges = 3;

	protected float lastChallengeTime = 0;

	[SerializeField]
	protected int challengesDone = 0;
	[SerializeField]
	protected int maxChallenges = 1;

	protected virtual void Start()
	{
		SetHackState(HackState.LOADING);
		
		// CleanChallenge = Instantiate(ChallengeObjectsHolder, ChallengeObjectsHolder.transform.parent);
		// CleanChallenge.SetActive(false);
		// CleanChallenge.name = "Challenge - Clean State";

	}

	protected virtual void FixedUpdate()
	{
		switch (hackState)
		{
			case HackState.LOADING:
				DoLoadingState();
				if(ShouldStartChallenge())
				{
					SetHackState(HackState.CHALLENGE);
					LinkedWindow.RequestFocus();
				}
				break;
				
			case HackState.CHALLENGE:
				DoChallengeState();
				break;

			default:
				break;
		}

	}

	#region Challenge State

	protected virtual void DoChallengeState()
	{
		ChallengeSuccess();
	}

	public virtual void ChallengeSuccess()
	{
		SetHackState(HackState.LOADING);
		challengesDone++;
		lastChallengeTime = currentLoadTime;
	}

	public virtual void ChallengeFailure()
	{
		DoFailure();
	}

	#endregion

	#region Loading State

	protected virtual void DoLoadingState()
	{
		if (currentLoadTime < totalLoadTime)
		{
			currentLoadTime += Time.fixedDeltaTime;
		}
		else
		{
			DoSuccess();
		}
	}

	protected virtual bool ShouldStartChallenge()
	{
		if (challengesDone >= maxChallenges) return false;

		var timeSinceLastChallenge = currentLoadTime - lastChallengeTime;

		return minTimeForChallenges <= currentLoadTime && timeSinceLastChallenge >= minTimeBetweenChallenges;
	}

	#endregion

	protected virtual void DoSuccess()
	{
		// TODO: mark a hack done
		LinkedWindow.Exit();
	}

	protected virtual void DoFailure()
	{
		LinkedWindow.Exit();
	}

	/*
	protected void ResetChallenge()
	{
		if (ChallengeObjectsHolder != null)
			Destroy(ChallengeObjectsHolder);
		
		ChallengeObjectsHolder = Instantiate(CleanChallenge, CleanChallenge.transform.parent);
	}
	*/

	protected void SetHackState(HackState newState)
	{
		hackState = newState;
		switch (newState)
		{
			case HackState.LOADING:
				LoadingObjectsHolder.SetActive(true);
				ChallengeObjectsHolder.SetActive(false);
				break;
			
			case HackState.CHALLENGE:
				// TODO: ResetChallenge();

				LoadingObjectsHolder.SetActive(false);
				ChallengeObjectsHolder.SetActive(true);
				break;
			
			default: 
				break;
		}
	}
}
