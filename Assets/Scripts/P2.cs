using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2 : MonoBehaviour {

    public float speed = 2.0f;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(-1.994487f, 0.96f, 0.3427706f);
    }
    // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector3.left);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector3.right);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector3.up);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector3.down);
            }

        }
    }

