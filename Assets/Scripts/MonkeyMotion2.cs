using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMotion2 : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody monkey;
    public LayerMask groundLayers;
    public float jumpForce = 7;
    public CapsuleCollider col;


    // Start is called before the first frame update
    void Start()
    {
        monkey = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        monkey.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            monkey.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
}
