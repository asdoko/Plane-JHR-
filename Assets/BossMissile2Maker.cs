using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile2Maker : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform playerTransfrom;
    public float missileSpeed = 8f;
    public float fireDelay = 0.3f;
    public int missileCount = 20;
    public float pauseDuration = 5f;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootMissiles());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ShootMissiles()
    {
        while (canShoot)
        {
            for (int i = 0; i < missileCount; i++)
            {
                Vector2 direction = (playerTransfrom.position - transform.position).normalized;
                GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity, Camera.main.transform);
                missile.GetComponent<Rigidbody2D>().velocity = direction * missileSpeed;
                
                yield return new WaitForSeconds(fireDelay);

            }
            yield return new WaitForSeconds(pauseDuration);
        }
    }

}
