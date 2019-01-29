using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Rigidbody>().velocity.y < -0.15 || this.GetComponent<Rigidbody>().velocity.y > 0.15)
        {
            this.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * (1f) * Time.deltaTime;
        }
    }
}
