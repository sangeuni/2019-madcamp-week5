using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//3D
public class CameraMotion : MonoBehaviour
{
    float cut = 10f;

    private bool camAvailable;
    public static WebCamTexture backCam;

    private Texture defaultBackground;
    private Color color;

    public RawImage background;
    public Image monkey1_left, monkey2_left, monkey1_right, monkey2_right, monkey1_jump, monkey2_jump, monkey1_smash, monkey2_smash, ready, go;
    public Transform effect;
    public AspectRatioFitter fit;
    public Transform c1, c2;
    private GameObject monkey1, monkey2, ball;
    int monkey1_left_index, monkey1_right_index, monkey2_left_index, monkey2_right_index, monkey1_jump_index, monkey2_jump_index, monkey1_smash_index, monkey2_smash_index;
    int allindex = 2; // Interval to visualize button click on motion
    int detect_width, detect_height, detect_index, detect_borderindex;
    int monkey1_right_x, monkey1_right_y, monkey2_right_x, monkey2_right_y, monkey1_left_x, monkey1_left_y, monkey2_left_x, monkey2_left_y, monkey1_jump_x, monkey1_jump_y, monkey2_jump_x, monkey2_jump_y, monkey1_smash_x, monkey1_smash_y, monkey2_smash_x, monkey2_smash_y;
    Color[] monkey2_left_prevarr, monkey2_left_newarr, monkey1_left_prevarr, monkey1_left_newarr, monkey2_right_prevarr, monkey2_right_newarr, monkey1_right_prevarr, monkey1_right_newarr, monkey1_jump_prevarr, monkey1_jump_newarr, monkey2_jump_prevarr, monkey2_jump_newarr, monkey1_smash_prevarr, monkey1_smash_newarr, monkey2_smash_prevarr, monkey2_smash_newarr, border_arr;

    int cutindex = 0;

    private Color tempcolor, tempcolor2;


    private void Start()
    {
        Debug.Log("Start!!");



        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected;");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                print(Screen.width + " " + Screen.height);
            }

        }
        if (backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }
        
        backCam.Play();
        print("WIDTH : " + backCam.width + " HEIGHT :  " + backCam.height);
        background.texture = backCam;

        camAvailable = true;
        Vars();

        ready.enabled = true;
        go.enabled = false;
        tempcolor = backCam.GetPixel(100, 100);   //테스트용
        
    }

    private void Update()
    {
        //tempcolor2 = backCam.GetPixel(540, 100);
        
        //if (Distance(tempcolor, tempcolor2)>0.2)
        //{
        //    Debug.Log("Detected!!");
        //}
        //tempcolor = tempcolor2;

        if (monkey1_left_index < allindex) monkey1_left_index++;
        else monkey1_left.enabled = true;

        if (monkey1_right_index < allindex) monkey1_right_index++;
        else monkey1_right.enabled = true;

        if (monkey2_left_index < allindex) monkey2_left_index++;
        else monkey2_left.enabled = true;

        if (monkey2_right_index < allindex) monkey2_right_index++;
        else monkey2_right.enabled = true;

        if (monkey1_jump_index < allindex) monkey1_jump_index++;
        else monkey1_jump.enabled = true;

        if (monkey2_jump_index < allindex) monkey2_jump_index++;
        else monkey2_jump.enabled = true;

        if (monkey1_smash_index < allindex) monkey1_smash_index++;
        else monkey1_smash.enabled = true;

        if (monkey2_smash_index < allindex) monkey2_smash_index++;
        else monkey2_smash.enabled = true;


        if (cutindex == 150)
        {
            ready.enabled = false;
            go.enabled = true;
        }
        if (cutindex == 220)
        {
            go.enabled = false;
            cut = 0.65f;
        }
        cutindex++;
        //if (DetectPixels(monkey1_left_x, monkey1_left_y, detect_width, detect_height, 1) > cut)
        if (Detect_Borders(monkey1_left_x, monkey1_left_y, 3) > cut)
        {
            if (monkey1_left.enabled)
            {
                monkey1_left.enabled = false;
                monkey1_left_index = 0;

                //monkey1.GetComponent<Rigidbody>().AddForce(Vector3.left * 12);
                monkey1.GetComponent<Rigidbody>().transform.Translate(145f * Time.deltaTime, 0, 0);
            }
        }

        //if (DetectPixels(monkey1_right_x, monkey1_right_y, detect_width, detect_height, 2) > cut)
        if (Detect_Borders(monkey1_right_x, monkey1_right_y, 2) > cut)
        {
            if (monkey1_right.enabled)
            {
                monkey1_right.enabled = false;
                monkey1_right_index = 0;

                //monkey1.GetComponent<Rigidbody>().AddForce(Vector3.right * 12);
                monkey1.GetComponent<Rigidbody>().transform.Translate(-145f * Time.deltaTime, 0, 0);
            }
        }

        //if (DetectPixels(monkey2_left_x, monkey2_left_y, detect_width, detect_height, 3) > cut)
        if (Detect_Borders(monkey2_left_x, monkey2_left_y, 4) > cut)
        {
            if (monkey2_left.enabled)
            {
                monkey2_left.enabled = false;
                monkey2_left_index = 0;

                //monkey2.GetComponent<Rigidbody>().AddForce(Vector3.left * 12);
                monkey2.GetComponent<Rigidbody>().transform.Translate(145f * Time.deltaTime, 0, 0);
            }
        }

        //if (DetectPixels(monkey2_right_x, monkey2_right_y, detect_width, detect_height, 4) > cut)
        if (Detect_Borders(monkey2_right_x, monkey2_right_y, 1) > cut)
        {

            if (monkey2_right.enabled)
            {
                monkey2_right.enabled = false;
                monkey2_right_index = 0;

                //monkey2.GetComponent<Rigidbody>().AddForce(Vector3.right * 12);
                monkey2.GetComponent<Rigidbody>().transform.Translate(-145f * Time.deltaTime, 0, 0);
            }
        }
        if (Detect_Borders(monkey2_jump_x, monkey2_jump_y, 5) > cut)
        {

            if (monkey2_jump.enabled)
            {
                monkey2_jump.enabled = false;
                monkey2_jump_index = 0;

                //monkey2.GetComponent<Rigidbody>().AddForce(Vector3.right * 12);

                if (monkey2.GetComponent<Rigidbody>().velocity.y > -0.1 && monkey2.GetComponent<Rigidbody>().velocity.y < 0.1)
                {
                    monkey2.GetComponent<Rigidbody>().velocity = Vector3.up * 25f;
                }
                else{
                    monkey2.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * (2f) * Time.deltaTime;
                }
            }
        }
        if (Detect_Borders(monkey1_jump_x, monkey1_jump_y, 6) > cut)
        {

            if (monkey1_jump.enabled)
            {
                monkey1_jump.enabled = false;
                monkey1_jump_index = 0;

                //monkey1.GetComponent<Rigidbody>().AddForce(Vector3.right * 12);
                if (monkey1.GetComponent<Rigidbody>().velocity.y > -0.1 && monkey1.GetComponent<Rigidbody>().velocity.y < 0.1)
                {
                    monkey1.GetComponent<Rigidbody>().velocity = Vector3.up * 25f;
                }
                else
                {
                    monkey1.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * (2f) * Time.deltaTime;
                }
            }
        }
        
        if (Vector3.Distance(ball.transform.position, monkey1.transform.position) <= 25)
        {
            if (Detect_Borders(monkey1_smash_x, monkey1_smash_y, 7) > cut)
            {
                if (monkey1_smash.enabled)
                {
                    monkey1_smash.enabled = false;
                    monkey1_smash_index = 0;

                    Instantiate(effect, ball.transform.position, effect.rotation);
                    ball.GetComponent<Rigidbody>().AddForce(Vector3.up * 3f);
                    ball.GetComponent<Rigidbody>().AddForce(Vector3.right * 6f);
                }
            }

        }
        if (Vector3.Distance(ball.transform.position, monkey2.transform.position) <= 25)
        {
            if (Detect_Borders(monkey2_smash_x, monkey2_smash_y, 8) > cut)
            {

                if (monkey2_smash.enabled)
                {
                    monkey2_smash.enabled = false;
                    monkey2_smash_index = 0;

                    Instantiate(effect, ball.transform.position, effect.rotation);
                    ball.GetComponent<Rigidbody>().AddForce(Vector3.up * 3f);
                    ball.GetComponent<Rigidbody>().AddForce(Vector3.left * 6f);
                }
            }
        }
            
        if (!camAvailable)
            return;
        float ratio = (float)backCam.width / (float)backCam.height;
        //fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(-1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
    
    float Distance(Color a, Color b)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(a.r - b.r), 2) + Mathf.Pow(Mathf.Abs(a.g - b.g), 2) + Mathf.Pow(Mathf.Abs(a.b - b.b), 2));
    }

    float Detect_Borders(int x, int y, int choose)
    {
        int up = 0, i;
        switch (choose)
        {

            case 1:
                monkey2_right_prevarr = (Color[])monkey2_right_newarr.Clone();
                for (i = 0; i < detect_width; i++)
                {
                    monkey2_right_newarr[up] = backCam.GetPixel(x + i, y);
                    if (Distance(monkey2_right_prevarr[up], monkey2_right_newarr[up]) > cut) return Distance(monkey2_right_prevarr[up], monkey2_right_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey2_right_newarr[up] = backCam.GetPixel(x + detect_width - 1, y + i);
                    if (Distance(monkey2_right_prevarr[up], monkey2_right_newarr[up]) > cut) return Distance(monkey2_right_prevarr[up], monkey2_right_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_width; i++)
                {
                    monkey2_right_newarr[up] = backCam.GetPixel(x + detect_width - 1 - i, y + detect_height - 1);
                    if (Distance(monkey2_right_prevarr[up], monkey2_right_newarr[up]) > cut) return Distance(monkey2_right_prevarr[up], monkey2_right_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey2_right_newarr[up] = backCam.GetPixel(x, y + detect_height - 1 - i);
                    if (Distance(monkey2_right_prevarr[up], monkey2_right_newarr[up]) > cut) return Distance(monkey2_right_prevarr[up], monkey2_right_newarr[up]);
                    up++;
                }

                break;
            case 2:
                monkey1_right_prevarr = (Color[])monkey1_right_newarr.Clone();
                for (i = 0; i < detect_width; i++)
                {
                    monkey1_right_newarr[up] = backCam.GetPixel(x + i, y);
                    if (Distance(monkey1_right_prevarr[up], monkey1_right_newarr[up]) > cut) return Distance(monkey1_right_prevarr[up], monkey1_right_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey1_right_newarr[up] = backCam.GetPixel(x + detect_width - 1, y + i);
                    if (Distance(monkey1_right_prevarr[up], monkey1_right_newarr[up]) > cut) return Distance(monkey1_right_prevarr[up], monkey1_right_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_width; i++)
                {
                    monkey1_right_newarr[up] = backCam.GetPixel(x + detect_width - 1 - i, y + detect_height - 1);
                    if (Distance(monkey1_right_prevarr[up], monkey1_right_newarr[up]) > cut) return Distance(monkey1_right_prevarr[up], monkey1_right_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey1_right_newarr[up] = backCam.GetPixel(x, y + detect_height - 1 - i);
                    if (Distance(monkey1_right_prevarr[up], monkey1_right_newarr[up]) > cut) return Distance(monkey1_right_prevarr[up], monkey1_right_newarr[up]);
                    up++;
                }
                break;
            case 3:
                monkey1_left_prevarr = (Color[])monkey1_left_newarr.Clone();
                for (i = 0; i < detect_width; i++)
                {
                    monkey1_left_newarr[up] = backCam.GetPixel(x + i, y);
                    if (Distance(monkey1_left_prevarr[up], monkey1_left_newarr[up]) > cut) return Distance(monkey1_left_prevarr[up], monkey1_left_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey1_left_newarr[up] = backCam.GetPixel(x + detect_width - 1, y + i);
                    if (Distance(monkey1_left_prevarr[up], monkey1_left_newarr[up]) > cut) return Distance(monkey1_left_prevarr[up], monkey1_left_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_width; i++)
                {
                    monkey1_left_newarr[up] = backCam.GetPixel(x + detect_width - 1 - i, y + detect_height - 1);
                    if (Distance(monkey1_left_prevarr[up], monkey1_left_newarr[up]) > cut) return Distance(monkey1_left_prevarr[up], monkey1_left_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey1_left_newarr[up] = backCam.GetPixel(x, y + detect_height - 1 - i);
                    if (Distance(monkey1_left_prevarr[up], monkey1_left_newarr[up]) > cut) return Distance(monkey1_left_prevarr[up], monkey1_left_newarr[up]);
                    up++;
                }
                break;
            case 4:
                monkey2_left_prevarr = (Color[])monkey2_left_newarr.Clone();
                for (i = 0; i < detect_width; i++)
                {
                    monkey2_left_newarr[up] = backCam.GetPixel(x + i, y);
                    if (Distance(monkey2_left_prevarr[up], monkey2_left_newarr[up]) > cut) return Distance(monkey2_left_prevarr[up], monkey2_left_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey2_left_newarr[up] = backCam.GetPixel(x + detect_width - 1, y + i);
                    if (Distance(monkey2_left_prevarr[up], monkey2_left_newarr[up]) > cut) return Distance(monkey2_left_prevarr[up], monkey2_left_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_width; i++)
                {
                    monkey2_left_newarr[up] = backCam.GetPixel(x + detect_width - 1 - i, y + detect_height - 1);
                    if (Distance(monkey2_left_prevarr[up], monkey2_left_newarr[up]) > cut) return Distance(monkey2_left_prevarr[up], monkey2_left_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey2_left_newarr[up] = backCam.GetPixel(x, y + detect_height - 1 - i);
                    if (Distance(monkey2_left_prevarr[up], monkey2_left_newarr[up]) > cut) return Distance(monkey2_left_prevarr[up], monkey2_left_newarr[up]);
                    up++;
                }
                break;
            case 5:
                monkey2_jump_prevarr = (Color[])monkey2_jump_newarr.Clone();
                for (i = 0; i < detect_width; i++)
                {
                    monkey2_jump_newarr[up] = backCam.GetPixel(x + i, y);
                    if (Distance(monkey2_jump_prevarr[up], monkey2_jump_newarr[up]) > cut) return Distance(monkey2_jump_prevarr[up], monkey2_jump_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey2_jump_newarr[up] = backCam.GetPixel(x + detect_width - 1, y + i);
                    if (Distance(monkey2_jump_prevarr[up], monkey2_jump_newarr[up]) > cut) return Distance(monkey2_jump_prevarr[up], monkey2_jump_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_width; i++)
                {
                    monkey2_jump_newarr[up] = backCam.GetPixel(x + detect_width - 1 - i, y + detect_height - 1);
                    if (Distance(monkey2_jump_prevarr[up], monkey2_jump_newarr[up]) > cut) return Distance(monkey2_jump_prevarr[up], monkey2_jump_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey2_jump_newarr[up] = backCam.GetPixel(x, y + detect_height - 1 - i);
                    if (Distance(monkey2_jump_prevarr[up], monkey2_jump_newarr[up]) > cut) return Distance(monkey2_jump_prevarr[up], monkey2_jump_newarr[up]);
                    up++;
                }
                break;
            case 6:
                monkey1_jump_prevarr = (Color[])monkey1_jump_newarr.Clone();
                for (i = 0; i < detect_width; i++)
                {
                    monkey1_jump_newarr[up] = backCam.GetPixel(x + i, y);
                    if (Distance(monkey1_jump_prevarr[up], monkey1_jump_newarr[up]) > cut) return Distance(monkey1_jump_prevarr[up], monkey1_jump_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey1_jump_newarr[up] = backCam.GetPixel(x + detect_width - 1, y + i);
                    if (Distance(monkey1_jump_prevarr[up], monkey1_jump_newarr[up]) > cut) return Distance(monkey1_jump_prevarr[up], monkey1_jump_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_width; i++)
                {
                    monkey1_jump_newarr[up] = backCam.GetPixel(x + detect_width - 1 - i, y + detect_height - 1);
                    if (Distance(monkey1_jump_prevarr[up], monkey1_jump_newarr[up]) > cut) return Distance(monkey1_jump_prevarr[up], monkey1_jump_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey1_jump_newarr[up] = backCam.GetPixel(x, y + detect_height - 1 - i);
                    if (Distance(monkey1_jump_prevarr[up], monkey1_jump_newarr[up]) > cut) return Distance(monkey1_jump_prevarr[up], monkey1_jump_newarr[up]);
                    up++;
                }
                break;
            case 7:
                monkey1_smash_prevarr = (Color[])monkey1_smash_newarr.Clone();
                for (i = 0; i < detect_width; i++)
                {
                    monkey1_smash_newarr[up] = backCam.GetPixel(x + i, y);
                    if (Distance(monkey1_smash_prevarr[up], monkey1_smash_newarr[up]) > cut) return Distance(monkey1_smash_prevarr[up], monkey1_smash_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey1_smash_newarr[up] = backCam.GetPixel(x + detect_width - 1, y + i);
                    if (Distance(monkey1_smash_prevarr[up], monkey1_smash_newarr[up]) > cut) return Distance(monkey1_smash_prevarr[up], monkey1_smash_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_width; i++)
                {
                    monkey1_smash_newarr[up] = backCam.GetPixel(x + detect_width - 1 - i, y + detect_height - 1);
                    if (Distance(monkey1_smash_prevarr[up], monkey1_smash_newarr[up]) > cut) return Distance(monkey1_smash_prevarr[up], monkey1_smash_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey1_smash_newarr[up] = backCam.GetPixel(x, y + detect_height - 1 - i);
                    if (Distance(monkey1_smash_prevarr[up], monkey1_smash_newarr[up]) > cut) return Distance(monkey1_smash_prevarr[up], monkey1_smash_newarr[up]);
                    up++;
                }
                break;
            case 8:
                monkey2_smash_prevarr = (Color[])monkey2_smash_newarr.Clone();
                for (i = 0; i < detect_width; i++)
                {
                    monkey2_smash_newarr[up] = backCam.GetPixel(x + i, y);
                    if (Distance(monkey2_smash_prevarr[up], monkey2_smash_newarr[up]) > cut) return Distance(monkey2_smash_prevarr[up], monkey2_smash_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey2_smash_newarr[up] = backCam.GetPixel(x + detect_width - 1, y + i);
                    if (Distance(monkey2_smash_prevarr[up], monkey2_smash_newarr[up]) > cut) return Distance(monkey2_smash_prevarr[up], monkey2_smash_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_width; i++)
                {
                    monkey2_smash_newarr[up] = backCam.GetPixel(x + detect_width - 1 - i, y + detect_height - 1);
                    if (Distance(monkey2_smash_prevarr[up], monkey2_smash_newarr[up]) > cut) return Distance(monkey2_smash_prevarr[up], monkey2_smash_newarr[up]);
                    up++;
                }
                for (i = 0; i < detect_height; i++)
                {
                    monkey2_smash_newarr[up] = backCam.GetPixel(x, y + detect_height - 1 - i);
                    if (Distance(monkey2_smash_prevarr[up], monkey2_smash_newarr[up]) > cut) return Distance(monkey2_smash_prevarr[up], monkey2_smash_newarr[up]);
                    up++;
                }
                break;

            default:
                break;
        }


        return 0;


    }
    

    void Vars()
    {
        detect_height = (backCam.height * 1) / 10;
        detect_width = detect_height;
        detect_index = detect_height * detect_width;
        detect_borderindex = (2 * detect_height) + (2 * detect_width);

        monkey2_left_newarr = new Color[detect_borderindex];
        monkey1_left_newarr = new Color[detect_borderindex];
        monkey2_right_newarr = new Color[detect_borderindex];
        monkey1_right_newarr = new Color[detect_borderindex];
        monkey1_jump_newarr = new Color[detect_borderindex];
        monkey2_jump_newarr = new Color[detect_borderindex];
        monkey1_smash_newarr = new Color[detect_borderindex];
        monkey2_smash_newarr = new Color[detect_borderindex];



        monkey1_left_x = 580; monkey1_left_y = 216;
        monkey1_right_x = 322; monkey1_right_y = 216;
        monkey2_left_x = 270; monkey2_left_y = 216;
        monkey2_right_x = 0 + 4; monkey2_right_y = 216;
        monkey1_jump_x = 456; monkey1_jump_y = 4;
        monkey2_jump_x = 136; monkey2_jump_y = 4;
        monkey1_smash_x = 370; monkey1_smash_y = 380;
        monkey2_smash_x = 220; monkey2_smash_y = 380;


        monkey1_left_index = allindex;
        monkey1_right_index = allindex;
        monkey2_left_index = allindex;
        monkey2_right_index = allindex;
        monkey2_jump_index = allindex;
        monkey1_jump_index = allindex;
        monkey2_smash_index = allindex;
        monkey1_smash_index = allindex;


        monkey1 = GameObject.Find("Monkey1");
        monkey2 = GameObject.Find("Monkey2");
        ball = GameObject.Find("Ball");
        monkey1_left.enabled = true;
        monkey1_right.enabled = true;
        monkey2_left.enabled = true;
        monkey2_right.enabled = true;
        monkey1_jump.enabled = true;
        monkey2_jump.enabled = true;
        monkey1_smash.enabled = true;
        monkey2_smash.enabled = true;

    }

    
    private float DetectPixels(int x, int y, int w, int h, int ind)
    {
        float answer;

        switch (ind)
        {
            case 1:
                monkey1_left_prevarr = (Color[])monkey1_left_newarr.Clone();
                monkey1_left_newarr = backCam.GetPixels(x, y, w, h);
                for (int i = 0; i < monkey1_left_newarr.Length; i++)
                {
                    answer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(monkey1_left_prevarr[i].r - monkey1_left_newarr[i].r), 2) + Mathf.Pow(Mathf.Abs(monkey1_left_prevarr[i].g - monkey1_left_newarr[i].g), 2) + Mathf.Pow(Mathf.Abs(monkey1_left_prevarr[i].b - monkey1_left_newarr[i].b), 2));
                    if (answer > cut)
                    {
                        return answer;
                    }
                }

                break;
            case 2:
                monkey1_right_prevarr = (Color[])monkey1_right_newarr.Clone();
                monkey1_right_newarr = backCam.GetPixels(x, y, w, h);
                for (int i = 0; i < monkey1_right_newarr.Length; i++)
                {
                    answer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(monkey1_right_prevarr[i].r - monkey1_right_newarr[i].r), 2) + Mathf.Pow(Mathf.Abs(monkey1_right_prevarr[i].g - monkey1_right_newarr[i].g), 2) + Mathf.Pow(Mathf.Abs(monkey1_right_prevarr[i].b - monkey1_right_newarr[i].b), 2));
                    if (answer > cut)
                    {
                        return answer;
                    }
                }

                break;

            case 3:
                monkey2_left_prevarr = (Color[])monkey2_left_newarr.Clone();
                monkey2_left_newarr = backCam.GetPixels(x, y, w, h);
                for (int i = 0; i < monkey2_left_newarr.Length; i++)
                {
                    answer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(monkey2_left_prevarr[i].r - monkey2_left_newarr[i].r), 2) + Mathf.Pow(Mathf.Abs(monkey2_left_prevarr[i].g - monkey2_left_newarr[i].g), 2) + Mathf.Pow(Mathf.Abs(monkey2_left_prevarr[i].b - monkey2_left_newarr[i].b), 2));
                    if (answer > cut)
                    {
                        return answer;
                    }
                }

                break;
            case 4:
                monkey2_right_prevarr = (Color[])monkey2_right_newarr.Clone();
                monkey2_right_newarr = backCam.GetPixels(x, y, w, h);
                for (int i = 0; i < monkey2_right_newarr.Length; i++)
                {
                    answer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(monkey2_right_prevarr[i].r - monkey2_right_newarr[i].r), 2) + Mathf.Pow(Mathf.Abs(monkey2_right_prevarr[i].g - monkey2_right_newarr[i].g), 2) + Mathf.Pow(Mathf.Abs(monkey2_right_prevarr[i].b - monkey2_right_newarr[i].b), 2));
                    if (answer > cut)
                    {
                        return answer;
                    }
                }

                break;
            case 5:
                monkey2_jump_prevarr = (Color[])monkey2_jump_newarr.Clone();
                monkey2_jump_newarr = backCam.GetPixels(x, y, w, h);
                for (int i = 0; i < monkey2_jump_newarr.Length; i++)
                {
                    answer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(monkey2_jump_prevarr[i].r - monkey2_jump_newarr[i].r), 2) + Mathf.Pow(Mathf.Abs(monkey2_jump_prevarr[i].g - monkey2_jump_newarr[i].g), 2) + Mathf.Pow(Mathf.Abs(monkey2_jump_prevarr[i].b - monkey2_jump_newarr[i].b), 2));
                    if (answer > cut)
                    {
                        return answer;
                    }
                }

                break;
            case 6:
                monkey1_jump_prevarr = (Color[])monkey1_jump_newarr.Clone();
                monkey1_jump_newarr = backCam.GetPixels(x, y, w, h);
                for (int i = 0; i < monkey1_jump_newarr.Length; i++)
                {
                    answer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(monkey1_jump_prevarr[i].r - monkey1_jump_newarr[i].r), 2) + Mathf.Pow(Mathf.Abs(monkey1_jump_prevarr[i].g - monkey1_jump_newarr[i].g), 2) + Mathf.Pow(Mathf.Abs(monkey1_jump_prevarr[i].b - monkey1_jump_newarr[i].b), 2));
                    if (answer > cut)
                    {
                        return answer;
                    }
                }

                break;
            case 7:
                monkey1_smash_prevarr = (Color[])monkey1_smash_newarr.Clone();
                monkey1_smash_newarr = backCam.GetPixels(x, y, w, h);
                for (int i = 0; i < monkey1_smash_newarr.Length; i++)
                {
                    answer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(monkey1_smash_prevarr[i].r - monkey1_smash_newarr[i].r), 2) + Mathf.Pow(Mathf.Abs(monkey1_smash_prevarr[i].g - monkey1_smash_newarr[i].g), 2) + Mathf.Pow(Mathf.Abs(monkey1_smash_prevarr[i].b - monkey1_smash_newarr[i].b), 2));
                    if (answer > cut)
                    {
                        return answer;
                    }
                }

                break;
            case 8:
                monkey2_smash_prevarr = (Color[])monkey2_smash_newarr.Clone();
                monkey2_smash_newarr = backCam.GetPixels(x, y, w, h);
                for (int i = 0; i < monkey2_smash_newarr.Length; i++)
                {
                    answer = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(monkey2_smash_prevarr[i].r - monkey2_smash_newarr[i].r), 2) + Mathf.Pow(Mathf.Abs(monkey2_smash_prevarr[i].g - monkey2_smash_newarr[i].g), 2) + Mathf.Pow(Mathf.Abs(monkey2_smash_prevarr[i].b - monkey2_smash_newarr[i].b), 2));
                    if (answer > cut)
                    {
                        return answer;
                    }
                }

                break;

            default:
                break;

        }
        return 0;
    }
}