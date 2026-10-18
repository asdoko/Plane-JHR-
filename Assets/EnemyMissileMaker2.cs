using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileMaker2 : MonoBehaviour
{
    private float health = 10f;
    public GameObject missilePrefab;
    public Transform playerTransform;
    private float missileInterval = 1f;
    private bool isDead = false;

    public static System.Action OnBossAppear;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(ShootMissiles());
        if (OnBossAppear != null)
            OnBossAppear += HandleBossAppearance;
    }

    private void OnDestroy()
    {
        if (OnBossAppear != null)
            OnBossAppear -= HandleBossAppearance;
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
        if(other.CompareTag("PlaneMissile"))
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
        Destroy(gameObject);
    }

    IEnumerator ShootMissiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(missileInterval);

            if (playerTransform != null)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
                missile.GetComponent<EnemyMissileController>().SetDirection(direction);
            }
        }
    }
    void HandleBossAppearance()
    {
        health *= 2;
        missileInterval /= 1.2f;
    }
    bool IsVisibleForm(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
