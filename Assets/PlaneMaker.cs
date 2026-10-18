using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMaker : MonoBehaviour
{
    public GameObject SmallEmemyPrefab;
    public GameObject SmallEnemy2Prefab;
    public GameObject EnemyPrefab;
    public Transform playerTransform;
    public GameObject missilePrefab;

    public GameObject[] itemPrefab = new GameObject[6];

    public float spawnInterval = 2f;
    private float cameraHeight;
    private float cameraWidth;
    private int smallPlaneCount = 0;
    private int maxSmallPlanes = 3;
    private Camera mainCamera;
    private bool stopSpawning = false;
    public void StopSpawning()
    {
        stopSpawning = true;
    }
    public void StartSpawning()
    {
        stopSpawning = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = Camera.main.orthographicSize * 2;
        cameraWidth = cameraHeight * Camera.main.aspect;

        StartCoroutine(SpawnPlanes());
    }

    // Update is called once per frame
    void Update()
    {
        RemoveOffScreenPlanes();
    }
    IEnumerator SpawnPlanes()
    {
        while (true)
        {
            if (!stopSpawning)
            {
                yield return new WaitForSeconds(spawnInterval);
                float spawnX = mainCamera.transform.position.x + cameraWidth / 2 + 1;
                float spawnY = Random.Range(-cameraHeight / 2, cameraHeight / 2);
                Vector2 spawnPosition = new Vector2(spawnX, spawnY);
                GameObject planePrefab;

                if (smallPlaneCount < maxSmallPlanes)
                {
                    planePrefab = (Random.Range(0, 2) == 0) ? SmallEmemyPrefab : SmallEnemy2Prefab;
                    smallPlaneCount++;
                    GameObject enemyPlane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);
                    SmallMissileMaker enemyPlaneScript = enemyPlane.GetComponent<SmallMissileMaker>();
                    enemyPlaneScript.playerTransform = playerTransform;
                    enemyPlaneScript.missilePrefab = missilePrefab;


                }
                else
                {
                    planePrefab = EnemyPrefab;
                    smallPlaneCount = 0;
                    GameObject enemyPlane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);

                    EnemyMissileMaker enemyPlaneScript = enemyPlane.GetComponent<EnemyMissileMaker>();
                    enemyPlaneScript.playerTransform = playerTransform;
                    enemyPlaneScript.missilePrefab = missilePrefab;
                    int randomIndex = Random.Range(0, itemPrefab.Length);
                    enemyPlaneScript.Item1Prefab = itemPrefab[randomIndex];
                }
            }
            if (stopSpawning)
            {

                yield return new WaitForSeconds(spawnInterval);
            }

            }
    }
    void RemoveOffScreenPlanes()
    {
        float leftBound = mainCamera.transform.position.x - cameraWidth / 2 - 1;
        GameObject[] allPlanes = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject plane in allPlanes)
        {
            if (plane.transform.position.x < leftBound)
            {
                Destroy(plane);
            }
        }
    }
}
