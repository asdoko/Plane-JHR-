using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public GameObject EnemyType1Prefab;
    public GameObject EnemyType2Prefab;
    public GameObject EnemyType3Prefab;
    public Transform playerTransform;
    public GameObject missilePrefab;

     float spawnInterval = 1.5f;

    private float cameraHeight;
    private float cameraWidth;
    private Camera mainCamera;
    private bool stopSpawning = false;
    private int bossDefeatedCount = 0;
    public void StopSpawning()
    {
        stopSpawning = true;
    }
    public void StartSpawning()
    {
        stopSpawning = false;
    }
    public void OnBossDefeated()
    {
        bossDefeatedCount++;
    }
    void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;
        StartCoroutine(SpawnAdvancedPlanes());
    }
    void Update()
    {
        RemoveOffScreenPlanes();

    }
    IEnumerator SpawnAdvancedPlanes()
    {
        while (true)
        {
            Debug.Log("bossdeafeated = " + bossDefeatedCount);
            if (!stopSpawning && bossDefeatedCount >= 1)
            {
                Debug.Log("bossdeafeated2222 = " + bossDefeatedCount);

                yield return new WaitForSeconds(spawnInterval);
                float spawnX = mainCamera.transform.position.x + cameraWidth / 2 + 1;
                float spawnY = Random.Range(-cameraHeight / 2, cameraHeight / 2);
                Vector2 spawnPosition = new Vector2(spawnX, spawnY);
                GameObject planePrefab;


                
                if (bossDefeatedCount == 2)
                {
                    planePrefab = (Random.Range(0, 2) == 0) ? EnemyType1Prefab : EnemyType2Prefab;
                }



                else if (bossDefeatedCount >= 3)
                {
                    int randomlndex = Random.Range(0, 3);
                    planePrefab = (randomlndex == 0) ? EnemyType1Prefab :
                        (randomlndex == 1) ? EnemyType2Prefab :
                        EnemyType3Prefab;
                }

                else
                {
                    yield return null;
                    continue;
                }
                GameObject enemyPlane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);
                EnemyMissileMaker2 enemyPlaneScript = enemyPlane.GetComponent<EnemyMissileMaker2>();
                enemyPlaneScript.playerTransform = playerTransform;
                enemyPlaneScript.missilePrefab = missilePrefab;
            }
            else
            {
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
    void RemoveOffScreenPlanes()
    {
        float leftBound = mainCamera.transform.position.x - cameraWidth / 2 - 1;
        GameObject[] allPlanes = GameObject.FindGameObjectsWithTag("Enemy2");

        foreach (GameObject plane in allPlanes)
        {
            if (plane.transform.position.x < leftBound)
            {

                Destroy(plane);
            }
        }
    }
}






