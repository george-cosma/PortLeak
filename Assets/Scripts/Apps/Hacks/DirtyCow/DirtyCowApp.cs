using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtyCowApp : Hack
{

	// === Loading Objects ===
	[Header("Loading Objects")]
	[SerializeField]
	protected Slider LoadingBar;

	// === Challenge Objects ===
	[Header("Challenge Objects")]
	[SerializeField]
	protected RectTransform FloatersParent;
    [SerializeField]
	protected RectTransform SpawnPoint;
    [SerializeField]
	protected RectTransform DespawnPoint;

	// === === ===

	[Header("Prefabs")]
	[SerializeField]
	protected  GameObject FilePrefab;
    [SerializeField]
	protected  GameObject GoldenFilePrefab;
    [SerializeField]
	protected  GameObject FolderPrefab;

	// === Challenge Variables===

	[Header("Dirty Cow Variables")]
	public float spawnInterval = 0.75f;
    private float m_spawnTimer = 0;

    public int forceSpawnTreshold = 12;
    private int spawnedFloaters = 0;

	
	protected override void DoLoadingState()
	{
		base.DoLoadingState();

		LoadingBar.value = currentLoadTime / totalLoadTime;
	}

	protected override void DoChallengeState()
	{
		if (m_spawnTimer < spawnInterval)
		{
			m_spawnTimer += Time.deltaTime;
		}
		else
		{
			m_spawnTimer = 0;
			int randValue = Random.Range(1, 100);
			if (randValue > 95 || spawnedFloaters > forceSpawnTreshold)
			{
				var goldenFile = InstantiateFloater(GoldenFilePrefab);
				goldenFile.GetComponent<GoldenFile>().OnClick += () => ChallengeSuccess();

				spawnedFloaters = 0;
			}
			else if (randValue > 60)
			{
				InstantiateFloater(FolderPrefab);
				spawnedFloaters++;
			}
			else
			{
				InstantiateFloater(FilePrefab);
				spawnedFloaters++;
			}

		}
	}

	private GameObject InstantiateFloater(GameObject prefab)
	{
		var instance = Instantiate(prefab, FloatersParent);
		Vector2 spawnPosition = SpawnPoint.anchoredPosition;

		spawnPosition.y = Random.Range(SpawnPoint.anchoredPosition.y - SpawnPoint.rect.height / 2, SpawnPoint.anchoredPosition.y + SpawnPoint.rect.height / 2);

        instance.GetComponent<GenericFloater>().DespawnPoint = DespawnPoint;

		instance.GetComponent<RectTransform>().anchoredPosition = spawnPosition;

		return instance;
	}
}
