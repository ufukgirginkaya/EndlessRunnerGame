using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] float appleSpawnChance = .3f;
    [SerializeField] float coinSpawnChance = .5f;
    [SerializeField] float coinSeperationLength = 2f;

    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };

    List<int> availableLanes = new List<int> { 0, 1, 2};
    List<int> availableParts = new List<int> { 0, 1, 2, 3, 4};

    LevelGenerator levelGenerator;
    ScoreManager scoreManager;
    private void Start()
    {
        SpawnFence();
        SpawnApple();
        SpawnCoin();
    }

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    void SpawnFence()
    {
        int fenceNumberToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fenceNumberToSpawn; i++)
        {
            if (availableLanes.Count <= 0)
            {
                break;
            }

            int selectedLane = SelectLane();
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);

        }
    }

    

    void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return; 
        

        int selectedLane = SelectLane();
        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Apple newApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
        newApple.Init(levelGenerator);
    }


    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return; 
        
        int selectedLane = SelectLane();

        int maxCoins = 6;
        int coinsToSpawn = Random.Range(1, maxCoins);

        float topZpos = transform.position.z + (coinSeperationLength * 2);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPosZ = topZpos - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPosZ);
            Coin newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoin.Init(scoreManager);
            
        }

    }

   

    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }

    private int SelectPart()
    {
        int randomPartIndex = Random.Range(0, availableParts.Count);
        int selectedPart = availableLanes[randomPartIndex];
        availableLanes.RemoveAt(randomPartIndex);
        return selectedPart;
    }
}
