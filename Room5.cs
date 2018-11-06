using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room5 : MonoBehaviour {
    //box
    public GameObject open;
    public GameObject key5;
    public GameObject reset1;
    public GameObject show1;
    public GameObject bubl1;
    public Texture red1;
    public Texture green1;
    public Texture white1;
    public int color1 = 3;
    public string ps1 = "";
    public bool isLockedBox = false;
    public int boxop = 0;
    //monster
    public GameObject monster1;
    public GameObject monster2;
    public GameObject bear;
    public GameObject pumpkin;
    //projector
    public GameObject projectorLight;
    public GameObject password_real;
    public GameObject password_fake;
    int light_on = 0;
    //code password
    public GameObject reset;
    public GameObject show;
    public GameObject bubl;
    public Texture red;
    public Texture green;
    public Texture white;
    public int color = 3;
    public string ps = "";
    //Door
    public GameObject R5Door;
    public bool isLocked = false;
    public int locked = 0;
    public bool isOpening = false;
    //top lamp
    public GameObject Top;
    public bool top_on = true;  
    public GameObject hint5;
    public GameObject character;
    public Camera tpp;
    public Camera fpp;
    public static bool inRoom5 = false;
    public Button b1;
    public Button b2;
    public Sprite i1;
    public Sprite i2;
    public GameObject inf;

    void Start () {
        password_fake.SetActive(false);
        password_real.SetActive(false);
        projectorLight.SetActive(false);
	}

	void Update () {
        if (inRoom5)
        {
            if (character.transform.position.z > 7.284 & character.transform.position.x < -12.57 && locked == 0)
            {
                isLocked = true;
                locked++;
                R5Door.transform.Rotate(new Vector3(0, -90f, 0));
                isOpening = false;
            }
            if (!isLockedBox&&boxop==1)
            {
                open.transform.localPosition = new Vector3(0, 0.0466f, -0.1f);
                boxop++;
            }
            eventManage();
            if (bubl.GetComponent<MeshRenderer>().material.mainTexture == green)
            {
                isLocked = false;
            }
            if (bubl1.GetComponent<MeshRenderer>().material.mainTexture == green)
            {
                isLockedBox = false;
                boxop = 1;
            }
        }
	}

    public  void eventManage()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            RaycastHit hit;

            if (Character.cam == Character.Cameras.FPPcamera)
            {
                ray = fpp.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = tpp.ScreenPointToRay(Input.mousePosition);
            }

            if (Physics.Raycast(ray, out hit))
            {
                string Ename = hit.collider.name;
                Debug.LogError(Ename);
                if (Ename.Equals("R5D"))
                {
                    Door();
                }
                else if (Ename.Equals("1") || Ename.Equals("2") || Ename.Equals("3") || Ename.Equals("4") || Ename.Equals("5") ||
                        Ename.Equals("6") || Ename.Equals("7") || Ename.Equals("8") || Ename.Equals("9") || Ename.Equals("reset"))
                {
                    ps = Room1.Code(hit, "317469", color, ps, bubl, show, isLocked, white, red, green);
                }
                else if (Ename.Equals("Hint5"))
                {
                    Character.Items.Add("hint5");
                    hint5.SetActive(false);
                    b2.gameObject.GetComponent<Image>().sprite = i2;
                    
                    inf.GetComponent<Text>().text = "you get hint5.";
                    StartCoroutine(clean());
                }
                else if (Ename.Equals("switch"))
                {
                    top_on = Room1.Switch(Top, top_on);
                }
                else if (Ename.Equals("key5"))
                {
                    Character.Items.Add("key5");
                    Destroy(key5);
                    b1.gameObject.GetComponent<Image>().sprite = i1;
                    
                    inf.GetComponent<Text>().text = "you get key5.";
                    StartCoroutine(clean());
                }
                else if (Ename.Equals("11") || Ename.Equals("22") || Ename.Equals("33") || Ename.Equals("44") || Ename.Equals("55") ||
                        Ename.Equals("66") || Ename.Equals("77") || Ename.Equals("88") || Ename.Equals("99") || Ename.Equals("reset1") || Ename.Equals("0"))
                {
                    ps1 = Room1.Code(hit, "180310", color1, ps1, bubl1, show1, isLockedBox, white1, red1, green);
                }
                else if (Ename.Equals("monster1") || Ename.Equals("monster2") || Ename.Equals("bear") || Ename.Equals("pumpkin") )
                {
                    Monster(hit);
                }
                else if (Ename.Equals("projector") )
                {
                    projector();
                }
            }
        }
    }

    private void projector()
    {
        if (light_on == 0)
        {   
            projectorLight.SetActive(true);
            password_fake.SetActive(true);
            light_on = 1;
        }
        else if (light_on == 1)
        {   //change color  ,white#FFFFFFFF purple#800080FF
            if (Character.Items.Contains("purpleLamp"))
            {
                Color purple;
                if (ColorUtility.TryParseHtmlString("#800080FF", out purple))
                {
                    projectorLight.GetComponent<Light>().color = purple;
                    password_fake.SetActive(false);
                    password_real.SetActive(true);
                    light_on = 2;
                }
            }
            else
            {
                projectorLight.SetActive(false);
                password_fake.SetActive(false);
                light_on = 0;
            }          
        }else if (light_on == 2)
        {
            Color white;
            if (ColorUtility.TryParseHtmlString("#FFFFFFFF", out white))
            {                
                projectorLight.SetActive(false);
                password_real.SetActive(false);
                light_on = 0;
                projectorLight.GetComponent<Light>().color = white;
            }          
        }
    }

    private void Monster(RaycastHit hit)
    {
        if (hit.collider.name.Equals("monster1"))
        {
            monster1.transform.Rotate(new Vector3(0,180f, 0));
            StartCoroutine(Center.Rotation(5.0f, monster1, new Vector3(0, -180f, 0)));
        }
        else if (hit.collider.name.Equals("monster2"))
        {
            monster2.transform.Rotate(new Vector3(0, 180f, 0));
            StartCoroutine(Center.Rotation(5.0f, monster2, new Vector3(0, -180f, 0)));
        }
        else if (hit.collider.name.Equals("bear"))
        {
            bear.transform.Rotate(new Vector3(0, 180f, 0));
            StartCoroutine(Center.Rotation(5.0f, bear, new Vector3(0, -180f, 0)));
        }
        else if (hit.collider.name.Equals("pumpkin"))
        {
            pumpkin.transform.Rotate(new Vector3(0, 180f, 0));
            StartCoroutine(Center.Rotation(5.0f, pumpkin, new Vector3(0, -180f, 0)));
        }
    }

    public IEnumerator clean()
    {
        yield return new WaitForSeconds(2);
        inf.GetComponent<Text>().text = "";
    }

    private void Door()
    {
        if (isLocked)
        {
            
            inf.GetComponent<Text>().text = "The door is locked,you need input correct password.";
            StartCoroutine(clean());
        }
        else
        {
            R5Door.transform.Rotate(new Vector3(0, 90f, 0));
            isOpening = true;
            if (locked != 0)
            {
                StartCoroutine(Center.Rotation(5.0f, R5Door, new Vector3(0, -90f, 0)));
            }
        }
    }
}
