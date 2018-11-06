using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room6 : MonoBehaviour {
    //photo  
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;
    public GameObject p7;
    public GameObject p8;
    public GameObject p9;
    public GameObject p10;
    public GameObject p11;
    public GameObject p12;
    //lamp
    public GameObject LampR;
    public GameObject LampL;
    public bool lampl = false;
    public bool lampr = false;
    public GameObject purple_lamp;
    //cushion
    public GameObject cushion;
    public GameObject s1;
    public GameObject s4;
    public GameObject s3;
    public GameObject s2;
    //Wardrobe
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject key6;
    public GameObject hint6;
    public GameObject character;
    //desk
    public GameObject R2;
    public GameObject R1;
    public GameObject R3;
    public GameObject L;
    //code password
    public GameObject reset;
    public GameObject show;
    public GameObject bubl;
    public Texture red;
    public Texture green;
    public Texture white;
    public int color = 3;
    public string ps = "";
    public Camera tpp;
    public Camera fpp;
    public GameObject Top;
    public bool top_on = true;
    //Door
    public GameObject R6Door;
    public bool isLocked = false;
    public int locked = 0;
    public bool isOpening = false;
    public static bool inRoom6 = false;
    public Button b1;
    public Button b2;
    public Sprite i1;
    public Sprite i2;
    public GameObject inf;
    public Button b3;
    public Sprite i3;

    void Start()
    {
        LampL.SetActive(false);
        LampR.SetActive(false);
    }
    void Update () {
        if (inRoom6)
        {
            if (character.transform.position.z < -7.284 & character.transform.position.x > 12.57 && locked == 0)
            {
                isLocked = true;
                locked++;
                R6Door.transform.Rotate(new Vector3(0, -90f, 0));
                isOpening = false;
            }
            eventManage();
            if (bubl.GetComponent<MeshRenderer>().material.mainTexture == green)
            {
                isLocked = false;
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
                if (Ename.Equals("LeftDoor") || Ename.Equals("RightDoor") || Ename.Equals("Bottom") || Ename.Equals("key6"))
                {
                    Wardrobe(hit);
                }
                else if (Ename.Equals("purpleLamp"))
                {
                    Character.Items.Add("purpleLamp");
                    Destroy(purple_lamp);

                    b3.gameObject.GetComponent<Image>().sprite = i3;
                    
                    inf.GetComponent<Text>().text = "you get purple bulb.";
                    StartCoroutine(clean());
                }
                else if (Ename.Equals("R6D"))
                {
                    Door();
                }
                else if (Ename.Equals("p1") || Ename.Equals("p2") || Ename.Equals("p3") || Ename.Equals("p4") || Ename.Equals("p5") ||
                         Ename.Equals("p6") || Ename.Equals("p7") || Ename.Equals("p8") || Ename.Equals("p9") || Ename.Equals("p10")
                         || Ename.Equals("p11") || Ename.Equals("p12"))
                {
                    Photo(hit);
                }
                else if (Ename.Equals("1") || Ename.Equals("2") || Ename.Equals("3") || Ename.Equals("4") || Ename.Equals("5") ||
                    Ename.Equals("6") || Ename.Equals("7") || Ename.Equals("8") || Ename.Equals("9") || Ename.Equals("reset"))
                {
                    ps = Room1.Code(hit, "159", color, ps, bubl, show, isLocked, white, red, green);
                }
                else if (Ename.Equals("R1") || Ename.Equals("R2") || Ename.Equals("R3") || Ename.Equals("L"))
                {
                    Desk(hit);
                }
                else if (Ename.Equals("LampR") || Ename.Equals("LampL"))
                {
                    Lamp(hit);
                }
                else if (Ename.Equals("cushion") || Ename.Equals("s1") || Ename.Equals("s2") || Ename.Equals("s3") || Ename.Equals("s4"))
                {
                    Cushion(hit);
                }
                else if (Ename.Equals("Hint6"))
                {
                    Character.Items.Add("hint6");
                    hint6.SetActive(false);
                    b2.gameObject.GetComponent<Image>().sprite = i2;
                    
                    inf.GetComponent<Text>().text = "you get hint6.";
                   StartCoroutine( clean());
                }
                else if (Ename.Equals("switch"))
                {
                    top_on = Room1.Switch(Top, top_on);
                }
            }
        }
    }

    private void Wardrobe(RaycastHit hit)
    {
        string name = hit.collider.name;
        switch (name)
        {
            case "LeftDoor":
                LeftDoor.transform.Rotate(new Vector3(0, -90f, 0));
                StartCoroutine(Center.Rotation(5.0f, LeftDoor, new Vector3(0, 90f, 0)));
                break;
            case "RightDoor":
                RightDoor.transform.Rotate(new Vector3(0, -90f, 0));
                StartCoroutine(Center.Rotation(5.0f, RightDoor, new Vector3(0, 90f, 0)));
                break;
            case "key6":
                Character.Items.Add("key6");
                Destroy(key6);
                b1.gameObject.GetComponent<Image>().sprite = i1;              
                inf.GetComponent<Text>().text = "you get key6.";
                StartCoroutine(clean());
                break;
        }
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
            R6Door.transform.Rotate(new Vector3(0, 90f, 0));
            isOpening = true;
            if (locked != 0)
            {
                StartCoroutine(Center.Rotation(5.0f, R6Door, new Vector3(0, -90f, 0)));
            }
        }
    }

    private void Lamp(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "LampL":
                if (lampl)
                {
                    LampL.SetActive(false);
                    lampl = false;
                }
                else
                {
                    LampL.SetActive(true);
                    lampl = true;
                }
                break;
            case "LampR":
                if (lampr)
                {
                    LampR.SetActive(false);
                    lampr = false;
                }
                else
                {
                    LampR.SetActive(true);
                    lampr = true;
                }
                break;
        }
    }

    private void Photo(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "p1":
                p1.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p1, new Vector3(0, 0, -180f)));
                break;
            case "p2":
                p2.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p2, new Vector3(0, 0, -180f)));
                break;
            case "p3":
                p3.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p3, new Vector3(0, 0, -180f)));
                break;
            case "p4":
                p4.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p4, new Vector3(0, 0, -180f)));
                break;
            case "p5":
                p5.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p5, new Vector3(0, 0, -180f)));
                break;
            case "p6":
                p6.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p6, new Vector3(0, 0, -180f)));
                break;
            case "p7":
                p7.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p7, new Vector3(0, 0, -180f)));
                break;
            case "p8":
                p8.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p8, new Vector3(0, 0, -180f)));
                break;
            case "p9":
                p9.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p9, new Vector3(0, 0, -180f)));
                break;
            case "p10":
                p10.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p10, new Vector3(0, 0, -180f)));
                break;
            case "p11":
                p11.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p11, new Vector3(0, 0, -180f)));
                break;
            case "p12":
                p12.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, p12, new Vector3(0, 0, -180f)));
                break;
        }
    }

    private void Desk(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "L":
                L.transform.localPosition = new Vector3(0.308f, 0.7983999f, 0.8f);
                StartCoroutine(Room1.Position(4.0f, L, new Vector3(0.308f, 0.7983999f, 0.4344f)));
                break;
            case "R1":
                R1.transform.localPosition = new Vector3(-0.4864f, 0.8023999f, 0.8f);
                StartCoroutine(Room1.Position(4.0f, R1, new Vector3(-0.4864f, 0.8023999f, 0.4344f)));
                break;
            case "R2":
                R2.transform.localPosition = new Vector3(-0.4864f, 0.5743999f, 0.8f);
                StartCoroutine(Room1.Position(4.0f, R2, new Vector3(-0.4864f, 0.5743999f, 0.4344f)));
                break;
            case "R3":
                R3.transform.localPosition = new Vector3(-0.4864f, 0.3104f, 0.8f);
                StartCoroutine(Room1.Position(4.0f, R3, new Vector3(-0.4864f, 0.3104f, 0.4344f)));
                break;
        }
    }

    public IEnumerator clean()
    {
        yield return new WaitForSeconds(2);
        inf.GetComponent<Text>().text = "";
    }

    private void Cushion(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "cushion":
                cushion.transform.Rotate(new Vector3(70f, 0, 0));
                StartCoroutine(Center.Rotation(3.0f, cushion, new Vector3(-70, 0,0)));
                break;
            case "s1":
                s1.transform.Rotate(new Vector3(-90f, 0, 0));
                StartCoroutine(Center.Rotation(3.0f, s1, new Vector3(-90f, 0, 0)));
                break;
            case "s3":
                s3.transform.Rotate(new Vector3(-90f, 0, 0));
                StartCoroutine(Center.Rotation(3.0f, s3, new Vector3(-90f, 0, 0)));
                break;
            case "s2":
                s2.transform.Rotate(new Vector3(-90f, 0, 0));
                StartCoroutine(Center.Rotation(3.0f, s2, new Vector3(-90f, 0, 0)));
                break;
            case "s4":
                s4.transform.Rotate(new Vector3(-90f, 0, 0));
                StartCoroutine(Center.Rotation(3.0f, s4, new Vector3(-90f, 0, 0)));
                break;
        }
    }
}
