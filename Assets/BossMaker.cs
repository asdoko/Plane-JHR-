using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMaker : MonoBehaviour
{
    public GameObject[] bossPrefabs;
    private int bossIndex = 0;
    private float spawnInterval = 30f;
    private int healthIncrement = 200;
    Vector3 setParent;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBoss());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnNextBoss();
        }
    }
    void SpawnNextBoss()
    {
        Transform spawnPoint = Camera.main.transform;
        GameObject boss = Instantiate(bossPrefabs[bossIndex], spawnPoint.position, Quaternion.identity, Camera.main.transform);
        BossCollider bossController = boss.GetComponent<BossCollider>();
        bossController.health = (bossIndex + 1) * healthIncrement;
        bossIndex = (bossIndex + 1) % bossPrefabs.Length;
    }

}
