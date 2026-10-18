using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    public float health;
    public float movespeed = 2f;
    public float verticalRange = 2f;
    private float initialYPosition;
    private bool movingUp = true;

    private PlaneMaker planeMaker;
    public BossMaker2 bossSpawner;
    private EnemyMaker enemyMaker;


    // Start is called before the first frame update
    void Start()
    {
        planeMaker = FindObjectOfType<PlaneMaker>();
        enemyMaker = FindObjectOfType<EnemyMaker>();

        //bossSpawner = FindObjectOfType<BossMaker2>();
        if (planeMaker != null)
        {
            planeMaker.StopSpawning();
            enemyMaker.StopSpawning();

        }
        transform.position = new Vector3(transform.position.x+  13f, transform.position.y, 1);
        initialYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertically();
    }
    void MoveVertically()
    {
        if (movingUp)
        {
            transform.Translate(Vector2.up * movespeed * Time.deltaTime);
            if (transform.position.y >= initialYPosition + verticalRange)
            {
                movingUp = false;
            }

        }
        else
        {
            transform.Translate(Vector2.down * movespeed * Time.deltaTime);
            if (transform.position.y <= initialYPosition - verticalRange)
            {
                movingUp = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlaneMissile"))
        {
            Debug.Log("hit");
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
        if (planeMaker != null)
        {
            planeMaker.StartSpawning();
            enemyMaker.StartSpawning();

        }
        if (bossSpawner != null)
        {
            bossSpawner.OnBossDeath();
        }
        enemyMaker.OnBossDefeated();
        Destroy(gameObject);
    }
}
