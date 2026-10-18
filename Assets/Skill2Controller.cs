using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Controller : MonoBehaviour
{
    private SpriteRenderer bombRenderer;
    private float damagePerSecond = 50f;
    private float duration = 2f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        bombRenderer = GetComponent<SpriteRenderer>();
        bombRenderer.color = new Color(1f, 1f, 1f, 0.9f);
        StartCoroutine(DeactivateBombAfterDelay(duration));
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 pos = new Vector2(player.transform.position.x + 26f, player.transform.position.y);
            transform.position = pos;
        }
    }
    public void Initialize(GameObject player)
    {
        this.player = player;
    }
    private IEnumerator DeactivateBombAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            

            /*
            EnemyMissileMaker2 enemy1 = other.GetComponent<EnemyMissileMaker2>();
            if (enemy1 != null)
            {
                enemy1.TakeDamage(damagePerSecond * Time.deltaTime);
            }
            */

            EnemyMissileMaker enemy2 = other.GetComponent<EnemyMissileMaker>();
            if (enemy2 != null)
            {
                enemy2.TakeDamage(damagePerSecond * Time.deltaTime);
            }


            SmallMissileMaker enemy3 = other.GetComponent<SmallMissileMaker>();
            if (enemy3 != null)
            {
                Destroy(enemy3.gameObject);
            }


            EnemyCollider enemy4 = other.GetComponent<EnemyCollider>();
            if (enemy4 != null)
            {
                Destroy(enemy4.gameObject);
            }

            EnemyMissileController enemy5 = other.GetComponent<EnemyMissileController>();
            if (enemy5 != null)
            {
                Destroy(enemy5.gameObject);
            }



        }

        if (other.CompareTag("BossMissile"))
        {
            MissileDelate enemy = other.GetComponent<MissileDelate>();
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }

        if (other.CompareTag("Enemy2"))
        {
            EnemyMissileMaker2 enemy1 = other.GetComponent<EnemyMissileMaker2>();
            if (enemy1 != null)
            {
                enemy1.TakeDamage(damagePerSecond * Time.deltaTime);
            }

        }

        if (other.CompareTag("Boss"))
        {
            BossCollider enemy = other.GetComponent<BossCollider>();
            if (enemy != null)
            {
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }




    }
    }
