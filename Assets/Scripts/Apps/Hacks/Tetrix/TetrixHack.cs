using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrixHack : Hack
{

	[Header("Loading Objects")]
	[SerializeField]
	protected Slider LoadingBar;

	// === Challenge Objects ===
	[Header("Challenge Objects")]
	[SerializeField]
	protected List<GameObject> StaticColumns = new List<GameObject>(10);

	[Header("Tetrix Variables")]
	[SerializeField]
	protected float MoveDownTime = 1f;

	private float currentMoveDownTime = 0f;

	private GameObject playableColumn;
	private RectTransform playableColumn_RectTransform;
	private Vector2 winPosition;

	protected override void Start()
	{
		base.Start();

		int removedColIndex = Random.Range(0,StaticColumns.Count);
		playableColumn = StaticColumns[removedColIndex];
		StaticColumns.RemoveAt(removedColIndex);

		playableColumn_RectTransform = playableColumn.GetComponent<RectTransform>();
		winPosition = playableColumn_RectTransform.anchoredPosition;
		playableColumn_RectTransform.anchoredPosition = new Vector2(16, -16);
	}

	protected void Update()
	{
		if (!LinkedWindow.Focused) return;

		if (playableColumn_RectTransform.anchoredPosition.y >= -112)
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			{
				Vector2 newPosition = playableColumn_RectTransform.anchoredPosition + new Vector2(-32, 0);
				if (newPosition.x >= -144)
				{
					playableColumn_RectTransform.anchoredPosition = newPosition;
				}
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			{
				Vector2 newPosition = playableColumn_RectTransform.anchoredPosition + new Vector2(32, 0);
				if (newPosition.x <= 144)
				{
					playableColumn_RectTransform.anchoredPosition = newPosition;
				}
			}
		}
	}

	protected override void DoChallengeState()
	{
		if (currentMoveDownTime <= MoveDownTime)
		{
			currentMoveDownTime += Time.fixedDeltaTime;
		}
		else
		{
			currentMoveDownTime = 0;
			// Check for any collision
			if(playableColumn_RectTransform.anchoredPosition == winPosition )
			{
				ChallengeSuccess();
			}
			if(playableColumn_RectTransform.anchoredPosition.y == -112 && playableColumn_RectTransform.anchoredPosition.x != winPosition.x)
			{
				ChallengeFailure();
			}


			// Move Down
			Vector2 newPosition = playableColumn_RectTransform.anchoredPosition + new Vector2(0, -32);
			playableColumn_RectTransform.anchoredPosition = newPosition;
		}
	}

	protected override void DoLoadingState()
	{
		base.DoLoadingState();

		LoadingBar.value = currentLoadTime / totalLoadTime;
	}

}
