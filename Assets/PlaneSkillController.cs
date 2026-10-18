using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSkillController : MonoBehaviour
{
    public GameObject SkillPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateBomb();
        }
    }
    void ActivateBomb()
    {
        GameObject SkillObject = GameObject.Find("Skill");
        if (SkillObject == null)
        {
            SkillObject = Instantiate(SkillPrefab, transform.position, Quaternion.identity);
        }
        SkillController skill = SkillObject.GetComponent<SkillController>();
        if (skill != null)
        {
            skill.gameObject.SetActive(true);
            skill.ActivateBomb(gameObject);
        }
    }
}
