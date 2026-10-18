using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDelate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsVisibleForm(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }
    bool IsVisibleForm(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}

