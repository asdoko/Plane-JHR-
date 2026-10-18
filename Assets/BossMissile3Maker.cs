using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile3Maker : MonoBehaviour
{
    public GameObject missilePrefab;
    public float initialMissileSpeed = 1f;
    public float finalMissileSpeed = 5f;
    public int splitMissileCount = 10;
    public float minRandomDelay = 0.3f;
    public float maxRandomDelay = 0.8f;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootMissile());
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ShootMissile()
    {
        while (canShoot)
        {
            float randomInitialDelay = Random.Range(minRandomDelay, maxRandomDelay);
            yield return new WaitForSeconds(randomInitialDelay);
            GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            missile.GetComponent<Rigidbody2D>().velocity = Vector2.left * initialMissileSpeed;
            float randomSplitDelay = Random.Range(minRandomDelay, maxRandomDelay);
            yield return new WaitForSeconds(randomSplitDelay);
            if (missile != null)
            {
                SplitMissile(missile.transform.position);
            }
            

        }
    }
    void SplitMissile(Vector2 splitPosition)
    {
        float angleStep = 360f / splitMissileCount;
        float angle = 0f;
        for (int i = 0; i < splitMissileCount; i++)
        {
            GameObject splitMissile = Instantiate(missilePrefab, splitPosition, Quaternion.identity, Camera.main.transform);
            float missileDirX = Mathf.Cos((angle * Mathf.PI) / 180f);
            float missileDirY = Mathf.Sin((angle * Mathf.PI) / 180f);
            Vector2 missileDirection = new Vector2(missileDirX, missileDirY).normalized;
            splitMissile.GetComponent<Rigidbody2D>().velocity = missileDirection * finalMissileSpeed;
            angle += angleStep;
        }
    }
}
