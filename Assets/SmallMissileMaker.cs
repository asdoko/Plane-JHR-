using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMissileMaker : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform playerTransform;
    private bool isShooting = false;
    public GameObject Item1Prefab;
    public float dropChance = 1f;

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

    IEnumerator ShootMissiles()
    {
        while (true)
        {
            float randomTime = Random.Range(0.5f, 9f);
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
