using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyCowApp : MonoBehaviour
{
    public RectTransform FloatersParent;
    public RectTransform SpawnPoint;
    public RectTransform DespawnPoint;

    
    public GameObject FilePrefab;
    public GameObject GoldenFilePrefab;
    public GameObject FolderPrefab;


    public float spawnInterval = 0.3f;
    private float m_spawnTimer = 0;

    private int spawnedFloaters = 0;
    public int forceSpawnTreshold = 20;


    // Update is called once per frame
    void Update()
    {
        if(m_spawnTimer < spawnInterval)
		{
            m_spawnTimer += Time.deltaTime;
		}
        else
		{
            m_spawnTimer = 0;
            if(spawnedFloaters > forceSpawnTreshold)
			{
                InstantiateFloater(GoldenFilePrefab);
                spawnedFloaters = 0;
                return;
            }

            int randValue = Random.Range(1, 100);
            if(randValue <= 60)
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
                InstantiateFloater(GoldenFilePrefab);
                spawnedFloaters = 0;
			}

		}
    }

	private void InstantiateFloater(GameObject prefab)
	{
		var instance = Instantiate(prefab, FloatersParent);
		Vector2 spawnPosition = SpawnPoint.anchoredPosition;

		spawnPosition.y = Random.Range(SpawnPoint.anchoredPosition.y - SpawnPoint.rect.height / 2, SpawnPoint.anchoredPosition.y + SpawnPoint.rect.height / 2);

        instance.GetComponent<GenericFloater>().DespawnPoint = DespawnPoint;

		instance.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
	}
}
