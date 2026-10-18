using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileMaker : MonoBehaviour
{
    float health = 3f;
    public GameObject missilePrefab;
    public Transform playerTransform;
    private bool isShooting = false;
    private bool isDead = false;
    public GameObject Item1Prefab;
    float dropChance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootMissiles());
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsVisibleForm(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (other.CompareTag("PlaneMissile"))
        {
            Destroy(other.gameObject);
            
            
            TakeDamage(1f);
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        if (isDead) return;
        isDead = true;
        if (Random.value < dropChance)
        {
            Instantiate(Item1Prefab, transform.position, Quaternion.identity, Camera.main.transform);
        }
        Destroy(gameObject);
    }

    IEnumerator ShootMissiles()
    {
        while (true)
        {
            float randomTime = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(randomTime);

            if (playerTransform != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
                missile.GetComponent<EnemyMissileController>().SetDirection(direction);
            }
        }
    }
    bool IsVisibleForm(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
