using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneCollider : MonoBehaviour
{
    public GameObject SkillPrefab;
    public int weaponState = 0;
    private float missileScaleMultiplier = 1f;
    private int missileCount = 1;
    float fireRate = 0.1f;
    private float nextFireTime = 0f;
    public GameObject missilePrefab;
    public float missileSpeed = 30f;
    public Transform firePoint;
    public float moveSpeed = 8f;
    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 moveVelocity;
    Transform cameraTransform;
    Vector3 offset;
    private float cameraSpeed = 5f;
    private Vector2 movement;
    public int x = 2;
    public int life = 3;
    public static float deathtime;

    // Start is called before the first frame update
    void Start()
    {


        weaponState = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if (x > 0 && Input.GetKeyDown(KeyCode.X))
        {
            x--;
            ActivateBomb();
        }
        
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time >= nextFireTime)
        {
            FireMissile();
            nextFireTime = Time.time + fireRate;
        }

        movement.x = Input.GetAxis("Horizontal") * moveSpeed;
        movement.y = Input.GetAxis("Vertical") * moveSpeed;
        if (movement == Vector2.zero)
        {
            transform.Translate(Vector2.right * cameraSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(movement * Time.deltaTime);
            transform.Translate(Vector2.right * cameraSpeed * Time.deltaTime);
        }

        ClampToScreen();



       
    }

    void ActivateBomb()
    {
        GameObject SkillObject = Instantiate(SkillPrefab, transform.position, Quaternion.identity);
        Skill2Controller skill = SkillObject.GetComponent<Skill2Controller>();
        if (skill != null)
        {
            skill.Initialize(gameObject);
        }
    }
    void FireMissile()
    {

        for ( int i = 0; i < missileCount; i++)
        {
            GameObject Fire = Instantiate(missilePrefab, firePoint.position, Quaternion.identity);
            Fire.transform.localScale *= missileScaleMultiplier;
            float missileHeight = Fire.transform.localScale.y / 10f;
            float baseSpacing = 0.05f;
            float yOffset = (i - (missileCount - 1) / 2f) *(missileHeight + baseSpacing);
            Fire.transform.position += new Vector3(0, yOffset, 0);
            Fire.GetComponent<Rigidbody2D>().velocity = Vector2.right * missileSpeed;
        }



    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy") || other.CompareTag("BossMissile") )
        {
            Destroy(other.gameObject);
            life--;
            if (life <= 0)
            {
                deathtime = Time.timeSinceLevelLoad;
                SceneManager.LoadScene("Clear");

            }
            
        }
        if ( other.CompareTag("Boss"))
        {
            
            life--;
            if (life <= 0)
            {
                deathtime = Time.timeSinceLevelLoad;
                SceneManager.LoadScene("Clear");

            }
            
        }
    }

    public void UpgradeWeapon(int upgradeType)
    {
        switch (upgradeType)
        {
            case 1:
                if(missileScaleMultiplier <= 2)
                {
                    missileScaleMultiplier *= 1.5f;
                }
                break;
            case 2:
                if(missileCount < 6)
                {
                    missileCount += 1;
                }
                break;
            case 3:
                break;
        }
    }

    void ClampToScreen()
    {
        Camera cam = Camera.main;
        Vector3 minBounds = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 maxBounds = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);
        transform.position = clampedPosition;
    }
}
