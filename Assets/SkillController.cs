using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private SpriteRenderer bombRenderer;
    private float damagePerSecond = 50f;
    private float duration = 5f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        bombRenderer = GetComponent<SpriteRenderer>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && player != null)
        {
            transform.position = player.transform.position;
        }
    }
    public void ActivateBomb(GameObject player)
    {
        gameObject.SetActive(true);
        this.player = player;
        
        bombRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        StartCoroutine(DeactivateBombAfterDelay(duration));
    }
    private IEnumerator DeactivateBombAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            BossCollider enemy = other.GetComponent<BossCollider>();
            if (enemy != null)
            {
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
            }
            EnemyMissileMaker2 enemy1 = other.GetComponent<EnemyMissileMaker2>();
            if (enemy1 != null)
            {
                enemy1.TakeDamage(damagePerSecond * Time.deltaTime);
            }
            EnemyMissileMaker enemy2 = other.GetComponent<EnemyMissileMaker>();
            if (enemy2 != null)
            {
                enemy2.TakeDamage(damagePerSecond * Time.deltaTime);
            }




        }
        if (other.CompareTag("EnemyMissile"))
        {
            Destroy(other.gameObject);
        }
    }
}
