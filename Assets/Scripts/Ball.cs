using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private GameObject p1;
    private GameObject p2;
    private int p1Points = 0;
    private int p2Points = 0;
    public Text pointsText1;
    public Text pointsText2;
    public Text gameOver;
    public int frame = 0;

    private Rigidbody ball;

    // Use this for initialization
    void Start()
    {
        p1 = GameObject.Find("Monkey1");
        p2 = GameObject.Find("Monkey2");
    }

    // Update is called once per frame
    void Update()
    {
        if (frame < 220)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            if (this.GetComponent<Rigidbody>().velocity.y < -0.1 || this.GetComponent<Rigidbody>().velocity.y > 0.1)
            {
                this.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * Time.deltaTime;
            }
        }
        frame++;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if(p1Points <= 9 && p2Points <= 9)
            {
                if (transform.position.x >= 0.4f)
                {
                    p1Points++;
                    pointsText1.text = (p1Points.ToString());
                    if (p1Points == 10)
                    {
                        gameOver.text = "Player 1 WIN!";
                        Destroy(this);
                    }
                    else
                    {
                        transform.position = new Vector3(-65f, 40f, 0);
                    }

                }
                else
                {
                    p2Points++;
                    pointsText2.text = (p2Points.ToString());
                    if (p2Points == 10)
                    {
                        gameOver.text = "Player 2 WIN!";
                        Destroy(this);
                    }
                    else
                    {
                        transform.position = new Vector3(65f, 40f, 0);
                    }

                }
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                p1.GetComponent<Rigidbody>().transform.position = new Vector3(-65f, -5f, 0f);
                p1.GetComponent<Rigidbody>().velocity = Vector3.zero;
                p2.GetComponent<Rigidbody>().transform.position = new Vector3(65f, -5f, 0f);
                p2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        else if (collision.gameObject.name == "Monkey1")
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.right * 5.5f);
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 6f);
        }

        else if (collision.gameObject.name == "Monkey2")
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.left * 5.5f);
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 6f);
        }

    }
}
