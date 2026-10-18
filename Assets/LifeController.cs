using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public List<GameObject> hearts;
    public PlaneCollider player;

    // Start is called before the first frame update
    void Start()
    {
         UpdateHearts(player.life);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHearts(player.life);
    }
    public void UpdateHearts(int life)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < life)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
