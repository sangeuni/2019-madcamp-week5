using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float jumpVelocity = 25;
    //private float fallMultiplier = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log(GetComponent<Rigidbody>().velocity);
            if (this.GetComponent<Rigidbody>().velocity.y > -0.1 && this.GetComponent<Rigidbody>().velocity.y < 0.1)
            {
                GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-15f * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(15f * Time.deltaTime, 0, 0);
        }
    }
}
