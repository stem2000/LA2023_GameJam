using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, 0f, 20f) * Time.deltaTime);
        transform.Rotate(0, 0, 60f * Time.deltaTime, Space.Self);
    }
}
