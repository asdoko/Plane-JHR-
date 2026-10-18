using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plane"))
        {
            DestroyEnemies(); // 적 제거
            Destroy(gameObject); // 아이템 제거
        }


    }
    void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemies1 = GameObject.FindGameObjectsWithTag("Enemy2");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);

        }
        foreach (GameObject enemy1 in enemies1)
        {
            Destroy(enemy1);

        }
    }
}
