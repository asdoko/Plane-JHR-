using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 15f;
    private Vector2 direction;
    private Camera mainCamera;
    private Rigidbody2D rb;


    GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        shield = GameObject.Find("shild");
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndBounce();
    }
    void MoveAndBounce()
    {
        rb.velocity = direction * speed;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        if (transform.position.x <= bottomLeft.x)
        {
            transform.position = new Vector3(bottomLeft.x, transform.position.y, transform.position.z);
            direction.x = Mathf.Abs(direction.x);
        }
        else if (transform.position.x >= topRight.x)
        {
            transform.position = new Vector3(topRight.x, transform.position.y, transform.position.z);
            direction.x = -Mathf.Abs(direction.x);
        }
        if (transform.position.y <= bottomLeft.y)
        {
            transform.position = new Vector3(transform.position.x, bottomLeft.y, transform.position.z);
            direction.y = Mathf.Abs(direction.y);
        }
        else if (transform.position.y >= topRight.y)
        {
            transform.position = new Vector3(transform.position.x, topRight.y, transform.position.z);
            direction.y = -Mathf.Abs(direction.y);
        }
    }
    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Plane")
        {
            GameObject obj = GameObject.Find("Plane");
            PlaneCollider plane = obj.GetComponent<PlaneCollider>();

            if (gameObject.name == "SpeedItem(Clone)")
            {
                Debug.Log("speed up");

                if(plane.moveSpeed <= 40)
                    plane.moveSpeed += 10;
            }
            if (gameObject.name == "SmallItem(Clone)")
            {
                Debug.Log("small");
                if (plane.gameObject.transform.localScale.x > 2 || obj.transform.localScale.x > 2)
                {
                    plane.gameObject.transform.localScale -= new Vector3(0.3f, 0.3f, 0f);
                    obj.transform.localScale -= new Vector3(0.3f, 0.3f, 0f);
                }
            }
            if (gameObject.name == "BigItem(Clone)")
            {
                Debug.Log("big");
                plane.UpgradeWeapon(1);
                
            }
            if (gameObject.name == "TwoItem(Clone)")
            {
                Debug.Log("Two misile");
                plane.UpgradeWeapon(2);

            }
            if (gameObject.name == "BombItem 1(Clone)")
            {
                Debug.Log("Bomb");

            }
            if (gameObject.name == "XItem(Clone)")
            {
                Debug.Log("X");
                if(plane.x<5)
                    plane.x += 1;

            }
            if (gameObject.name == "HeartItem(Clone)")
            {
                Debug.Log("heart");
                if(plane.life<5)
                    plane.life += 1;

            }
            Destroy(gameObject);
        }
           
    }
}
