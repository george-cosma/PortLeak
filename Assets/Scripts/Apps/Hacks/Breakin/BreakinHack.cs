using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BreakinHack : Hack
{

	[Header("Loading Objects")]
	[SerializeField]
	protected Slider LoadingBar;

	// === Challenge Objects ===
	[Header("Challenge Objects")]
	[SerializeField]
	protected TMP_Text StatusLabel;
	[SerializeField]
	protected ChargeButton ChargeButton;
	[SerializeField]
	protected Slider ChargeBar;


	[Header("Breakin Variables")]
	[SerializeField]
	protected float MaxChargeTime = 10f;
	[SerializeField]
	protected string DefaultStatus = "Hold down to charge";
	[SerializeField]
	protected string ChargingStatus = "Charging...";

	private float currentChargeTime = 0f;

	protected override void Start()
	{
		base.Start();

		StatusLabel.text = DefaultStatus;
		ChargeButton.OnStartCharging += () => StatusLabel.text = ChargingStatus;
		ChargeButton.OnStopCharging += () => StatusLabel.text = DefaultStatus;
	}

	protected override void DoChallengeState()
	{
		if (ChargeButton.IsPressed)
		{

			if (currentChargeTime <= MaxChargeTime)
			{
				currentChargeTime += Time.fixedDeltaTime;
				ChargeBar.value = currentChargeTime / MaxChargeTime;
			}
			else
			{
				ChallengeSuccess();
			}
		}
	}

	protected override void DoLoadingState()
	{
		base.DoLoadingState();

		LoadingBar.value = currentLoadTime / totalLoadTime;
	}

}
