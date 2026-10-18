using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile1Maker : MonoBehaviour
{
    public GameObject missilePrefab;
    float fireDelay = 5f;
    int numberOfMissiles = 6;
    float missileSpeed = 5f;
    private bool canShoot = false;

    float burstDelay = 0.5f;
    int burstCount = 10;
    float angleStep;
    float angleOffest = 15f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartShootingAfterDelay(3f));
        angleStep = 360 / numberOfMissiles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartShootingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
        StartCoroutine(ShootMissiles());

    }
    IEnumerator ShootMissiles()
    {
        while (canShoot)
        {
            for (int burst = 0; burst < burstCount; burst++)
            {
                float currentAngle = burst * angleOffest;
                for (int i = 0; i < numberOfMissiles; i++)
                {
                    //missilePrefab.transform.localScale = new Vector2(missilePrefab.transform.localScale.x / 2f, missilePrefab.transform.localScale.y / 2f);

                    GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity, Camera.main.transform);
                    float missileDirX = Mathf.Cos((currentAngle * Mathf.PI) / 180f);
                    float missileDirY = Mathf.Sin((currentAngle * Mathf.PI) / 180f);
                    Vector2 missileDirection = new Vector2(missileDirX, missileDirY).normalized;
                    missile.GetComponent<Rigidbody2D>().velocity = missileDirection * missileSpeed;
                    currentAngle += angleStep;
                }
                yield return new WaitForSeconds(burstDelay);

            }
            yield return new WaitForSeconds(fireDelay);
        }
    }

}
