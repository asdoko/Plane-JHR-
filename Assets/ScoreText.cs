using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI deathTime;
    float resultTime;
    // Start is called before the first frame update
    void Start()
    {
        deathTime.text = PlaneCollider.deathtime.ToString("F1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
