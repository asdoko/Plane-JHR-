using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public float speed = 3f;
    public float minRandomTime = 0f;
    public float maxRandomTime = 2f;
    public int durability;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Plane").transform;

        targetDirection = Vector2.left;
        StartCoroutine(ChangeDirectionRandomly());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = targetDirection * speed;
    }

    IEnumerator ChangeDirectionRandomly()
    {
        float randomTime = Random.Range(minRandomTime, maxRandomTime);
        yield return new WaitForSeconds(randomTime);

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        targetDirection = directionToPlayer;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
