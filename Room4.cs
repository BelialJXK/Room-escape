using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room4 : MonoBehaviour {
    //垃圾桶
    public GameObject l_1;
    public GameObject l_2;
    public GameObject l_3;
    public GameObject L1;
    public GameObject L2;
    public GameObject L3;
    //书桌
    public GameObject d11;
    public GameObject d12;
    public GameObject d13;  
    public GameObject d21;
    public GameObject d22;
    public GameObject d23;
    public GameObject d31;
    public GameObject d32;
    public GameObject d33;
    //台灯
    public GameObject lamp1;
    public GameObject password1;
    public GameObject lamp2;
    public GameObject password2;
    public GameObject lamp3;
    public GameObject password3;
    public bool lamp1_on = false;
    public bool lamp2_on = false;
    public bool lamp3_on = false;
    //书柜
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;
    public GameObject s5;
    public GameObject s6;
    public GameObject r4key1;
    public GameObject r4key2;
    public GameObject r4key3;
    public bool rkey1=false;
    public bool rkey2 = false;
    public bool rkey3 = false;
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
    public GameObject R4Door;
    public bool isLocked = false;
    public int locked = 0;
    public bool isOpening = false;
    //top lamp
    public GameObject Top;
    public bool top_on = true;
    public GameObject key4;
    public GameObject hint4;
    public GameObject character;
    public GameObject pig;
    public Camera tpp;
    public Camera fpp;
    public GameObject arrow;
    public static bool inRoom4 = false;
    public Button b1;
    public Button b2;
    public Sprite i1;
    public Sprite i2;
    public GameObject inf;

    void Start () {
        password1.SetActive(false);
        password2.SetActive(false);
        password3.SetActive(false);
        arrow.SetActive(false);
        lamp1.SetActive(false);
        lamp2.SetActive(false);
        lamp3.SetActive(false);
    }

    void Update () {
        if (inRoom4)
        {
            if (character.transform.position.z < -7.145 & character.transform.position.x < -12.6 && locked == 0)
            {
                isLocked = true;
                locked++;
                R4Door.transform.Rotate(new Vector3(0, -90f, 0));
                isOpening = false;
            }
            if (!top_on)
            {
                arrow.SetActive(true);
            }
            else
            {
                arrow.SetActive(false);
            }
            passwordShpw();
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
                if (Ename.Equals("l1") || Ename.Equals("l2") || Ename.Equals("l3") || Ename.Equals("L1") || Ename.Equals("L2") || Ename.Equals("L3"))
                {
                    trash_can(hit);
                }
                else if (Ename.Equals("R4D"))
                {
                    Door();
                }
                else if (Ename.Equals("d11") || Ename.Equals("d12") || Ename.Equals("d13") || Ename.Equals("d21") || Ename.Equals("d22") ||
                         Ename.Equals("d23") || Ename.Equals("d31") || Ename.Equals("d32") || Ename.Equals("d33") )
                {
                    Desk(hit);
                }
                else if (Ename.Equals("1") || Ename.Equals("2") || Ename.Equals("3") || Ename.Equals("4") || Ename.Equals("5") ||
                        Ename.Equals("6") || Ename.Equals("7") || Ename.Equals("8") || Ename.Equals("9") || Ename.Equals("reset"))
                {
                    ps = Room1.Code(hit, "311592", color, ps, bubl, show, isLocked, white, red, green);
                }
                else if (Ename.Equals("lamp1") || Ename.Equals("lamp2") || Ename.Equals("lamp3"))
                {
                    Lamp(hit);
                }
                else if (Ename.Equals("s1") || Ename.Equals("s2") || Ename.Equals("s3") || Ename.Equals("s4") || Ename.Equals("s5") || Ename.Equals("s6"))
                {
                    Bookcase(hit);
                }
                else if (Ename.Equals("Hint4"))
                {
                    Character.Items.Add("hint4");
                    hint4.SetActive(false);
                    b2.gameObject.GetComponent<Image>().sprite = i2;
                    
                    inf.GetComponent<Text>().text = "you get hint1.";
                    StartCoroutine(clean());
                }
                else if (Ename.Equals("switch"))
                {
                    top_on = Room1.Switch(Top, top_on);
                }else if (Ename.Equals("r4key1") || Ename.Equals("r4key2") || Ename.Equals("r4key3"))
                {
                    BookCaseKey(hit);
                }else if (Ename.Equals("pig"))
                {
                    Pig();
                }else if (Ename.Equals("key4"))
                {
                    Character.Items.Add("key4");
                    Destroy(key4);
                    b1.gameObject.GetComponent<Image>().sprite = i1;
                    
                    inf.GetComponent<Text>().text = "you get key2.";
                    StartCoroutine(clean());
                }
            }
        }
    }

    public IEnumerator clean()
    {
        yield return new WaitForSeconds(2);
        inf.GetComponent<Text>().text = "";
    }

    private void Pig()
    {
        pig.transform.Rotate(new Vector3(0,-180f,0));
        StartCoroutine(Center.Rotation(5.0f, pig, new Vector3(0, 180f, 0)));
    }

    private void trash_can(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "L1":
                L1.transform.Rotate(new Vector3(0, 180f, 0));
                StartCoroutine(Center.Rotation(5.0f, L1, new Vector3(0, -180f, 0)));
                l_1.transform.localPosition=new Vector3(1f, -0.3f,0);
                StartCoroutine(Room1.Position(4.0f, l_1, new Vector3(0, 0.4736f, 0)));
                break;
            case "L2":
                L2.transform.Rotate(new Vector3(0, 180f, 0));
                StartCoroutine(Center.Rotation(5.0f, L2, new Vector3(0, -180f, 0)));
                l_2.transform.localPosition = new Vector3(1f, -0.3f, 0);
                StartCoroutine(Room1.Position(4.0f, l_2, new Vector3(0, 0.4736f, 0)));
                break;
            case "L3":
                L3.transform.Rotate(new Vector3(0, 180f, 0));
                StartCoroutine(Center.Rotation(5.0f, L3, new Vector3(0, -180f, 0)));
                l_3.transform.localPosition = new Vector3(1f, -0.3f, 0);
                StartCoroutine(Room1.Position(4.0f, l_3, new Vector3(0, 0.4736f, 0)));
                break;
        }
    }

    private void Desk(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "d11":
                d11.transform.localPosition = new Vector3(0,0,0.3f);
                StartCoroutine(Room1.Position(4.0f, d11, new Vector3(0,0,0)));
                break;
            case "d12":
                d12.transform.localPosition = new Vector3(-0.4988959f, -0.02291433f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, d12, new Vector3(-0.4988959f, -0.02291433f, 0.003327618f)));
                break;
            case "d13":
                d13.transform.localPosition = new Vector3(-0.4988959f, -0.2234892f, 0.5f);
                StartCoroutine(Room1.Position(4.0f, d13, new Vector3(-0.4988959f, -0.2234892f, 0.003327618f)));
                break;
            case "d21":
                d21.transform.localPosition = new Vector3(0, 0, 0.3f);
                StartCoroutine(Room1.Position(4.0f, d21, new Vector3(0, 0, 0)));
                break;
            case "d22":
                d22.transform.localPosition = new Vector3(-0.4988959f, -0.02291433f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, d22, new Vector3(-0.4988959f, -0.02291433f, 0.003327618f)));
                break;
            case "d23":
                d23.transform.localPosition = new Vector3(-0.4988959f, -0.2234892f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, d23, new Vector3(-0.4988959f, -0.2234892f, 0.003327618f)));
                break;          
            case "d31":
                d31.transform.localPosition = new Vector3(0, 0, 0.3f);
                StartCoroutine(Room1.Position(4.0f, d31, new Vector3(0, 0, 0)));
                break;
            case "d32":
                d32.transform.localPosition = new Vector3(-0.4988959f, -0.02291433f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, d32, new Vector3(-0.4988959f, -0.02291433f, 0.003327618f)));
                break;
            case "d33":
                d33.transform.localPosition = new Vector3(-0.4988959f, -0.2234892f, 0.3f);
                StartCoroutine(Room1.Position(4.0f, d33, new Vector3(-0.4988959f, -0.2234892f, 0.003327618f)));
                break;
        }
    }

    private void Lamp(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "lamp1":
                if (lamp1_on)
                {
                    lamp1.SetActive(false);                 
                    lamp1_on = false;
                }
                else
                {
                    lamp1.SetActive(true);
                    lamp1_on = true;
                }
                break;
            case "lamp2":
                if (lamp2_on)
                {
                    lamp2.SetActive(false);
                    lamp2_on = false;
                }
                else
                {
                    lamp2.SetActive(true);
                    lamp2_on = true;
                }
                break;
            case "lamp3":
                if (lamp3_on)
                {
                    lamp3.SetActive(false);
                    lamp3_on = false;
                }
                else
                {
                    lamp3.SetActive(true);
                    lamp3_on = true;
                }
                break;
        }
    }

    private void Bookcase(RaycastHit hit)
    {
        if (hit.collider.name.Equals("s1") || hit.collider.name.Equals("s2"))
        {
            if (rkey1)
            {
                r4key1.SetActive(true);
                if (hit.collider.name.Equals("s1"))
                {
                    s1.transform.Rotate(new Vector3(0, 90f, 0));
                    StartCoroutine(Center.Rotation(5.0f, s1, new Vector3(0, -90f, 0)));
                }
                else
                {
                    s2.transform.Rotate(new Vector3(0, -90f, 0));
                    StartCoroutine(Center.Rotation(5.0f, s2, new Vector3(0, 90f, 0)));
                }                  
            }else 
            {
                
                inf.GetComponent<Text>().text = "you need key of the bookcase1.";
                StartCoroutine(clean());
            }
        }
        else if(hit.collider.name.Equals("s3") || hit.collider.name.Equals("s4")){
            if (rkey2)
            {
                r4key2.SetActive(true);
                if (hit.collider.name.Equals("s3"))
                {
                    Debug.LogError(1);
                    s3.transform.Rotate(new Vector3(0, 90f, 0));
                    StartCoroutine(Center.Rotation(5.0f, s3, new Vector3(0, -90f, 0)));
                }
                else
                {
                    Debug.LogError(2);
                    s4.transform.Rotate(new Vector3(0, -90f, 0));
                    StartCoroutine(Center.Rotation(5.0f, s4, new Vector3(0, 90f, 0)));
                }
            }
            else
            {
                
                inf.GetComponent<Text>().text ="you need key of the bookcase2." ;
                StartCoroutine(clean());
            }
        }
        else if (hit.collider.name.Equals("s5") || hit.collider.name.Equals("s6")){
            if (rkey3)
            {
                r4key3.SetActive(true);
                if (hit.collider.name.Equals("s5"))
                {
                    s5.transform.Rotate(new Vector3(0, 90f, 0));
                    StartCoroutine(Center.Rotation(5.0f, s5, new Vector3(0, -90f, 0)));
                }
                else
                {
                    s6.transform.Rotate(new Vector3(0, -90f, 0));
                    StartCoroutine(Center.Rotation(5.0f, s6, new Vector3(0, 90f, 0)));
                }
            }
            else
            {
                
                inf.GetComponent<Text>().text = "you need key of the bookcase 3.";
                StartCoroutine(clean());
            }
        }
    }

    private void BookCaseKey(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "r4key1":
                r4key1.SetActive(false);
                r4key1.transform.localPosition = new Vector3(0.3293f, -0.061f, 0.01f);
                r4key1.transform.Rotate(new Vector3(0, -30f, 0));
                rkey1 = true;
                
                inf.GetComponent<Text>().text = "you get the key of book case";
                StartCoroutine(clean());
                break;
            case "r4key2":
                r4key2.SetActive(false);
                r4key2.transform.localPosition = new Vector3(0.3293f, -0.061f, 0.01f);
                r4key2.transform.Rotate(new Vector3(0, -30f, 0));
                rkey2 = true;
                
                inf.GetComponent<Text>().text = "you get the key of book case";
                StartCoroutine(clean());
                break;
            case "r4key3":
                r4key3.SetActive(false);
                r4key3.transform.localPosition = new Vector3(0.3293f, -0.061f, 0.01f);
                r4key3.transform.Rotate(new Vector3(0, -30f, 0));
                rkey3 = true;
                
                inf.GetComponent<Text>().text = "you get the key of book case";
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
            R4Door.transform.Rotate(new Vector3(0, 90f, 0));
            isOpening = true;
            if (locked != 0)
            {
                StartCoroutine(Center.Rotation(5.0f, R4Door, new Vector3(0, -90f, 0)));
            }
        }
    }

    private void passwordShpw()
    {
        if (!top_on)
        {
            if (lamp1_on)
            {
                password1.SetActive(true);
            }
            else
            {
                password1.SetActive(false);
            }
            if (lamp2_on)
            {
                password2.SetActive(true);
            }
            else
            {
                password2.SetActive(false);
            }
            if (lamp3_on)
            {
                password3.SetActive(true);
            }
            else
            {
                password3.SetActive(false);
            }
        }
        else
        {
            password1.SetActive(false);
            password2.SetActive(false);
            password3.SetActive(false);
        }       
    }
}
