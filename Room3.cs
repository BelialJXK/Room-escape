using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room3 : MonoBehaviour {
    //desk  
    public GameObject d1;
    public GameObject d2;
    public GameObject d3;
    public GameObject d4;
    public GameObject d5;
    public GameObject d6;
    //Bedside_table
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;
    public GameObject g5;
    public GameObject g6;
    public GameObject g7;
    public GameObject g8;
    //candle
    public GameObject candle1;
    public GameObject candle2;
    public GameObject candle3;
    public bool candle1_on = false;
    public bool candle2_on = false;
    public bool candle3_on = false;
    //bookcase
    public GameObject s1;
    public GameObject s4;
    public GameObject s3;
    public GameObject s2;
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
    public GameObject R3Door;
    public bool isLocked = false;
    public int locked = 0;
    public bool isOpening = false;
    //password
    public GameObject password1;
    public GameObject password2;
    public GameObject password3;
    public GameObject Top;
    public bool top_on = true;
    public GameObject key3;
    public GameObject hint3;
    public GameObject character;
    public Camera tpp;
    public Camera fpp;
    public static bool inRoom3 = false;
    public Button b1;
    public Button b2;
    public Sprite i1;
    public Sprite i2;
    public GameObject inf;

    void Start () {
        candle1.SetActive(false);
        candle2.SetActive(false);
        candle3.SetActive(false);
        password1.SetActive(false);
        password2.SetActive(false);
        password3.SetActive(false);
    }

    void Update () {
        if (inRoom3)
        {
            if (character.transform.position.z > 7.145 & character.transform.position.x > 12.6 && locked == 0)
            {
                isLocked = true;
                locked++;
                R3Door.transform.Rotate(new Vector3(0, -90f, 0));
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
                if (Ename.Equals("d1") || Ename.Equals("d2") || Ename.Equals("d3") || Ename.Equals("d4") || Ename.Equals("d5") || Ename.Equals("d6"))
                {
                    Desk(hit);
                }
                else if (Ename.Equals("R3D"))
                {
                    Door();
                }
                else if (Ename.Equals("g1") || Ename.Equals("g2") || Ename.Equals("g3") || Ename.Equals("g4") || Ename.Equals("g5") ||
                         Ename.Equals("g6") || Ename.Equals("g7") || Ename.Equals("g8") || Ename.Equals("g9") || Ename.Equals("key3"))
                {
                    Bedside_table(hit);
                }
                else if (Ename.Equals("1") || Ename.Equals("2") || Ename.Equals("3") || Ename.Equals("4") || Ename.Equals("5") ||
                    Ename.Equals("6") || Ename.Equals("7") || Ename.Equals("8") || Ename.Equals("9") || Ename.Equals("reset"))
                {
                    ps = Room1.Code(hit, "946846", color, ps, bubl, show, isLocked, white, red, green);
                }
                else if (Ename.Equals("candle1") || Ename.Equals("candle2") || Ename.Equals("candle3"))
                {
                    Candle(hit);
                }
                else if (Ename.Equals("s1") || Ename.Equals("s2") || Ename.Equals("s3") || Ename.Equals("s4"))
                {
                    Bookcase(hit);
                }
                else if (Ename.Equals("Hint3"))
                {
                    Character.Items.Add("hint3");
                    hint3.SetActive(false);
                    b2.gameObject.GetComponent<Image>().sprite = i2;
                    
                    inf.GetComponent<Text>().text = "you get hint3.";
                    StartCoroutine(clean());;
                }
                else if (Ename.Equals("switch"))
                {
                   top_on = Room1.Switch(Top, top_on);
                }
            }
        }
    }

    private void Desk(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "d1":
                d1.transform.localPosition = new Vector3(0.2510861f, 0.7788424f, 0.5f);
                StartCoroutine(Room1.Position(4.0f, d1, new Vector3(0.2510861f, 0.7788424f, 0.2488438f)));
                break;
            case "d2":
                d2.transform.localPosition = new Vector3(-0.2510089f, 0.7788424f, 0.5f);
                StartCoroutine(Room1.Position(4.0f, d2, new Vector3(-0.2510089f, 0.7788424f, 0.2488438f)));
                break;
            case "d3":
                d3.transform.localPosition = new Vector3(0.2510861f, 0.7788424f, 0.5f);
                StartCoroutine(Room1.Position(4.0f, d3, new Vector3(0.2510861f, 0.7788424f, 0.2488438f)));
                break;
            case "d4":
                d4.transform.localPosition = new Vector3(-0.2510089f, 0.7788424f, 0.5f);
                StartCoroutine(Room1.Position(4.0f, d4, new Vector3(-0.2510089f, 0.7788424f, 0.2488438f)));
                break;
            case "d5":
                d5.transform.localPosition = new Vector3(0.2510861f, 0.7788424f, 0.5f);
                StartCoroutine(Room1.Position(4.0f, d5, new Vector3(0.2510861f, 0.7788424f, 0.2488438f)));
                break;
            case "d6":
                d6.transform.localPosition = new Vector3(-0.2510089f, 0.7788424f, 0.5f);
                StartCoroutine(Room1.Position(4.0f, d6, new Vector3(-0.2510089f, 0.7788424f, 0.2488438f)));
                break;
        }
    }

    private void Candle(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "candle1":
                if (candle1_on)
                {
                    candle1.SetActive(false);
                    password1.SetActive(false);
                    candle1_on = false;
                }
                else
                {
                    candle1.SetActive(true);
                    password1.SetActive(true);
                    candle1_on = true;
                }
                break;
            case "candle2":
                if (candle2_on)
                {
                    candle2.SetActive(false);
                    password2.SetActive(false);
                    candle2_on = false;
                }
                else
                {
                    candle2.SetActive(true);
                    password2.SetActive(true);
                    candle2_on = true;
                }
                break;
            case "candle3":
                if (candle3_on)
                {
                    candle3.SetActive(false);
                    password3.SetActive(false);
                    candle3_on = false;
                }
                else
                {
                    candle3.SetActive(true);
                    password3.SetActive(true);
                    candle3_on = true;
                }
                break;
        }
    }

    private void Bookcase(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "s1":
                s1.transform.Rotate(new Vector3(0, 90f, 0));
                StartCoroutine(Center.Rotation(5.0f, s1, new Vector3(0, -90f, 0)));
                break;
            case "s2":
                s2.transform.Rotate(new Vector3(0, 90f, 0));
                StartCoroutine(Center.Rotation(5.0f, s2, new Vector3(0, -90f, 0)));
                break;
            case "s3":
                s3.transform.Rotate(new Vector3(0, -90f, 0));
                StartCoroutine(Center.Rotation(5.0f, s3, new Vector3(0, 90f, 0)));
                break;
            case "s4":
                s4.transform.Rotate(new Vector3(0, -90f, 0));
                StartCoroutine(Center.Rotation(5.0f, s4, new Vector3(0, 90f, 0)));
                break;
        }
    }

    private void Bedside_table(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "g1":
                g1.transform.Rotate(new Vector3(-90, 0, 0));
                StartCoroutine(Center.Rotation(5.0f, g1, new Vector3(90, 0, 0)));
                break;
            case "g2":
                g2.transform.localPosition = new Vector3(0, 0.7195432f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, g2, new Vector3(0, 0.7195432f, 0f)));
                break;
            case "g3":
                g3.transform.localPosition = new Vector3(0, 0.5698911f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, g3, new Vector3(0, 0.5698911f, 0)));
                break;
            case "g4":
                g4.transform.localPosition = new Vector3(0, 0.4227006f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, g4, new Vector3(0, 0.4227006f, 0)));
                break;
            case "g5":
                g5.transform.localPosition = new Vector3(0, 0.2434302f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, g5, new Vector3(0, 0.2434302f, 0)));
                break;
            case "g6":
                g6.transform.localPosition = new Vector3(0.1634076f, 1.013223f, 0.08f);
                StartCoroutine(Room1.Position(4.0f, g6, new Vector3(0.1634076f, 1.013223f, -0.06280849f)));
                break;
            case "g7":
                g7.transform.localPosition = new Vector3(0.1634076f, 0.9257064f, 0.08f);
                StartCoroutine(Room1.Position(4.0f, g7, new Vector3(0.1634076f, 0.9257064f, -0.06280849f)));
                break;
            case "g8":
                g8.transform.localPosition = new Vector3(0.1634076f, 0.8402296f, 0.08f);
                StartCoroutine(Room1.Position(4.0f, g8, new Vector3(0.1634076f, 0.8402296f, -0.06280849f)));
                break;
            case "key3":
                Character.Items.Add("key3");
                Destroy(key3);
                b1.gameObject.GetComponent<Image>().sprite = i1;
                
                inf.GetComponent<Text>().text = "you get key2.";
                StartCoroutine(clean());
                break;
        }
    }

    private void Door()
    {
        if (isLocked)
        {
            
            inf.GetComponent<Text>().text = "The door is locked,you need input correct password.";
            StartCoroutine(clean());;
        }
        else
        {
            R3Door.transform.Rotate(new Vector3(0, 90f, 0));
            isOpening = true;
            if (locked != 0)
            {
                StartCoroutine(Center.Rotation(5.0f, R3Door, new Vector3(0, -90f, 0)));
            }
        }
    }

    public IEnumerator clean()
    {
        yield return new WaitForSeconds(2);
        inf.GetComponent<Text>().text = "";
    }
}
