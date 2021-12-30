using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtyCowApp : MonoBehaviour
{
    [SerializeField]
    protected Window LinkedWindow;

	[SerializeField]
	protected GameObject LoadingObjectsHolder;
	[SerializeField]
	protected GameObject ChallengeObjectsHolder;

	// === Challenge Objects ===

	[SerializeField]
	protected RectTransform FloatersParent;
    [SerializeField]
	protected RectTransform SpawnPoint;
    [SerializeField]
	protected RectTransform DespawnPoint;

	// === Loading Objects ===

	[SerializeField]
	protected Slider LoadingBar;

	// === === ===

	[SerializeField]
	protected  GameObject FilePrefab;
    [SerializeField]
	protected  GameObject GoldenFilePrefab;
    [SerializeField]
	protected  GameObject FolderPrefab;


    public HackState hackState = HackState.LOADING;



	// === Challenge ===

	private bool m_challengeDone = false;

    public float spawnInterval = 0.3f;
    private float m_spawnTimer = 0;

    private int spawnedFloaters = 0;
    public int forceSpawnTreshold = 20;

	// === Loading ===

	public float totalLoadTime = 15f;
	public float currentLoadTime = 0f;

	void Start()
	{
		LoadingObjectsHolder.SetActive(true);
		ChallengeObjectsHolder.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (hackState == HackState.LOADING)
		{
			DoLoading();

			if(!m_challengeDone && currentLoadTime >= totalLoadTime/2 )
			{
				hackState = HackState.CHALLENGE;
				LoadingObjectsHolder.SetActive(false);
				ChallengeObjectsHolder.SetActive(true);
				LinkedWindow.RequestFocus();
			}
		}
		else if (hackState == HackState.CHALLENGE)
		{
			DoChallenge();
		}
	}

	public void ChallengeComplete()
	{
		hackState = HackState.LOADING;
		LoadingObjectsHolder.SetActive(true);
		ChallengeObjectsHolder.SetActive(false);
		m_challengeDone = true;
	}

	private void DoLoading()
	{
		if (currentLoadTime < totalLoadTime)
		{
			currentLoadTime += Time.deltaTime;
			LoadingBar.value = currentLoadTime / totalLoadTime;
		}
		else
		{
			DoSuccess();
		}
	}

	private void DoChallenge()
	{
		if (m_spawnTimer < spawnInterval)
		{
			m_spawnTimer += Time.deltaTime;
		}
		else
		{
			m_spawnTimer = 0;
			if (spawnedFloaters > forceSpawnTreshold)
			{
				InstantiateFloater(GoldenFilePrefab);
				spawnedFloaters = 0;
				return;
			}

			int randValue = Random.Range(1, 100);
			if (randValue <= 60)
			{
				InstantiateFloater(FilePrefab);
				spawnedFloaters++;
			}
			else if (randValue <= 95)
			{
				InstantiateFloater(FolderPrefab);
				spawnedFloaters++;
			}
			else
			{
				var goldenFile = InstantiateFloater(GoldenFilePrefab);
				goldenFile.GetComponent<GoldenFile>().app = this;
				spawnedFloaters = 0;
			}

		}
	}

	private void DoSuccess()
	{
		throw new System.NotImplementedException();
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
