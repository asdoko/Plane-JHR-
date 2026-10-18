using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMaker2 : MonoBehaviour
{
    public GameObject[] bossPrefabs;
    private int bossIndex = 0;
    private int healthIncrement = 100;

    private float spawnInterval = 30f;

    private bool isBossAlive = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnBossDeath()
    {
        isBossAlive = false;
        if (bossIndex >= bossPrefabs.Length)
        {
            SceneManager.LoadScene("CompleteScene");
            return;
        }
        StartCoroutine(StartSpawnTimer());
    }
    IEnumerator StartSpawnTimer()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnNextBoss();
    }
    void SpawnNextBoss()
    {
        if (!isBossAlive)
        {
            Transform spawnPoint = Camera.main.transform;
            GameObject boss = Instantiate(bossPrefabs[bossIndex], spawnPoint.position, Quaternion.identity, Camera.main.transform);

            if (EnemyMissileMaker2.OnBossAppear != null)
            {
                EnemyMissileMaker2.OnBossAppear.Invoke();
            }

            if (bossIndex >= 1)
            {
                BossMissile2Maker bossMissileType2 = boss.GetComponent<BossMissile2Maker>();

                bossMissileType2.playerTransfrom = FindObjectOfType<PlaneCollider>().transform;
            }
            BossCollider bossController = boss.GetComponent<BossCollider>();
            if (bossController != null)
            {
                bossController.bossSpawner = this;
            }
            bossController.health = (bossIndex + 1) * healthIncrement;
            bossIndex++;
            isBossAlive = true;
        }
    }
}
