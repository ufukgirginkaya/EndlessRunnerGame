using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Settings")]
    [SerializeField] int startingChunkNumber = 12;
    [Tooltip("Do not change chunk size!")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ= -2f;
    

    List<GameObject> chunks = new List<GameObject>();

    private void Start()
    {
        SpawnChunks();

    }

    private void Update()
    {
         MoveChunks();
    }

    public void ChangeChunkSpeed(float speedAmount)
    {
        float finalMoveSpeed = moveSpeed + speedAmount;
        finalMoveSpeed = Mathf.Clamp(finalMoveSpeed, minMoveSpeed, maxMoveSpeed);

        

        if (finalMoveSpeed != moveSpeed)
        {
            moveSpeed = finalMoveSpeed;

            float finalGravityZ = Physics.gravity.z - speedAmount;
            finalGravityZ = Mathf.Clamp(finalGravityZ, minGravityZ, maxGravityZ);

            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, finalGravityZ);
            cameraController.ChangeCameraFOV(speedAmount);
            
        }
        

    }

    private void SpawnChunks()
    {
        for (int i = 0; i < startingChunkNumber; i++)
        {
            GameObject newChunkGO = Instantiate(chunkPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + i * chunkLength), Quaternion.identity, chunkParent);

            chunks.Add(newChunkGO);

            Chunk newChunk = newChunkGO.GetComponent<Chunk>();
            newChunk.Init(this, scoreManager);
        }
    }

    private void AddNewChunk()
    {
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(transform.position.x, transform.position.y, chunks[chunks.Count - 1].transform.position.z + chunkLength), Quaternion.identity, chunkParent);
        chunks.Add(newChunk);
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];

            chunk.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                AddNewChunk();
            }
        }
    }
}
