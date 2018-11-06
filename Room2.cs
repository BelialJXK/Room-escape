using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Room2 : MonoBehaviour {  
    public GameObject key2;
    public bool getkey2 = false;
    //projector
    public GameObject projectorL;
    public GameObject projectorR;
    public GameObject projectorM;
    int light_on1 = 0;
    int light_on2 = 0;
    int light_on3 = 0;
    //书柜
    public GameObject s1;
    public GameObject casekey;
    public bool casekey1=false;
    public GameObject s2;
    //桌子
    public GameObject r1;
    public GameObject r2;
    public GameObject l1;
    public GameObject l2;
    public GameObject m;
    //灯
    public GameObject lamp1;
    public GameObject lamp2;
    public GameObject lamp3;
    public bool lamp1_on = false;
    public bool lamp2_on = false;
    public bool lamp3_on = false;
    public bool showkey1 = false;
    public bool showkey2 = false;
    public bool showkey3 = false;
    //4*3box
    public GameObject B1;
    public GameObject B2;
    public GameObject B3;
    public GameObject B4;
    public GameObject B5;
    public GameObject B6;
    public GameObject B7;
    public GameObject B8;
    public GameObject B9;
    public GameObject B10;
    public GameObject B11;
    public GameObject B12;
    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;
    public GameObject T8;
    public GameObject T9;
    public GameObject T10;
    public GameObject T11;
    public GameObject T12;
    //out keys
    public GameObject ekey1;
    public GameObject ekey2;
    public GameObject ekey3;
    public GameObject ekey4;
    public GameObject ekey5;
    public GameObject ekey6;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;
    public bool k1 = false;
    public bool k2 = false;
    public bool k3= false;
    public bool k4 = false;
    public bool k5 = false;
    public bool k6 = false;
    public GameObject outlight;
    public int num_key=0;
    //book
    public GameObject book;
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
    public GameObject R2Door;
    public bool isLocked = false;
    public int locked = 0;
    public bool isOpening = false;
    //Exit door
    public GameObject outdoor;
    public bool isLockedOut = true;
    //top lamp
    public GameObject Top;
    public bool top_on = true;
    public GameObject hint2;
    public GameObject character;
    public Camera tpp;
    public Camera fpp;
    public static bool inRoom2 = false;
    public Button b1;
    public Button b2;
    public Sprite i1;
    public Sprite i2;
    public GameObject inf;

    void Start () {
        key2.SetActive(false);
        lamp1.SetActive(false);
        lamp2.SetActive(false);
        lamp3.SetActive(false);
        ekey1.SetActive(false);
        ekey2.SetActive(false);
        ekey3.SetActive(false);
        ekey4.SetActive(false);
        ekey5.SetActive(false);
        ekey6.SetActive(false);
        projectorL.SetActive(false);
        projectorR.SetActive(false);
        projectorM.SetActive(false);
    }	

	void Update () {
        if (inRoom2)
        {
            if (character.transform.position.z > 14.5 && locked == 0)
            {
                isLocked = true;
                locked++;
                R2Door.transform.Rotate(new Vector3(0, -90f, 0));
                isOpening = false;
            }
            if (num_key == 6)
            {
                Color green;
                if (ColorUtility.TryParseHtmlString("#00FF0000", out green))
                {
                    outlight.GetComponent<Light>().color = green;
                    isLockedOut = false;
                    num_key++;
                }
            }
            if (showkey3 && showkey2 && showkey1&&!getkey2)
            {
                key2.SetActive(true);
                getkey2 = true;
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
                if (Ename.Equals("R2D"))
                {
                    Door();
                }
                else if (Ename.Equals("1") || Ename.Equals("2") || Ename.Equals("3") || Ename.Equals("4") || Ename.Equals("5") ||
                        Ename.Equals("6") || Ename.Equals("7") || Ename.Equals("8") || Ename.Equals("9") || Ename.Equals("reset"))
                {
                    ps = Room1.Code(hit, "251715", color, ps, bubl, show, isLocked, white, red, green);
                }
                else if (Ename.Equals("Hint2"))
                {
                    Character.Items.Add("hint2");
                    hint2.SetActive(false);
                    b2.gameObject.GetComponent<Image>().sprite = i2;
                    
                    inf.GetComponent<Text>().text = "you get hint2.";
                    StartCoroutine(clean());;
                }
                else if (Ename.Equals("switch"))
                {
                    top_on = Room1.Switch(Top, top_on);
                }
                else if (Ename.Equals("key2"))
                {
                    Character.Items.Add("key2");
                    Destroy(key2);
                    b1.gameObject.GetComponent<Image>().sprite = i1;
                    
                    inf.GetComponent<Text>().text = "you get key2.";
                    StartCoroutine(clean());
                }
                else if (Ename.Equals("casekey"))
                {
                    casekey.SetActive(false);
                    casekey.transform.localPosition = new Vector3(0.3293f, -0.061f, 0.01f);
                    casekey.transform.Rotate(new Vector3(0, -30f, 0));
                    casekey1 = true;
                    
                    inf.GetComponent<Text>().text ="you get case key.";
                    StartCoroutine(clean());
                }
                else if (Ename.Equals("projectorL")|| Ename.Equals("projectorR")|| Ename.Equals("projectorM"))
                {
                    Projector(hit);
                }else if (Ename.Equals("B1") || Ename.Equals("B2") || Ename.Equals("B3") || Ename.Equals("B4") || Ename.Equals("B5") ||
                         Ename.Equals("B6") || Ename.Equals("B7") || Ename.Equals("B8") || Ename.Equals("B9") || Ename.Equals("B10")
                         || Ename.Equals("B11") || Ename.Equals("B12")|| Ename.Equals("T1") || Ename.Equals("T2") || Ename.Equals("T3") || 
                         Ename.Equals("T4") || Ename.Equals("T5") || Ename.Equals("T6") || Ename.Equals("T7") || Ename.Equals("T8") || 
                         Ename.Equals("T9") || Ename.Equals("T10")|| Ename.Equals("T11") || Ename.Equals("T12"))
                {
                    Box(hit);
                }else if(Ename.Equals("s1") || Ename.Equals("s2"))
                {
                    Bookcase(hit);
                }
                else if (Ename.Equals("r1") || Ename.Equals("r2") || Ename.Equals("l1") || Ename.Equals("l2") || Ename.Equals("m"))
                {
                    Desk(hit);
                }
                else if (Ename.Equals("lamp1") || Ename.Equals("lamp2") || Ename.Equals("lamp3"))
                {
                    Lamp(hit);
                }
                else if (Ename.Equals("book"))
                {
                    Book();
                }
                else if (Ename.Equals("e1") || Ename.Equals("e2") || Ename.Equals("e3") || Ename.Equals("e4") || Ename.Equals("e5") ||
                        Ename.Equals("e6"))
                {
                    Outkey(hit);
                }
                else if (Ename.Equals("out"))
                {
                    Exit();
                }
            }
        }
    }

    public IEnumerator clean()
    {
        yield return new WaitForSeconds(2);
        inf.GetComponent<Text>().text = "";
    }

    private void Bookcase(RaycastHit hit)
    {

        if (hit.collider.name.Equals("s1") || hit.collider.name.Equals("s2"))
        {
            
            if (casekey1)
            {
                casekey.SetActive(true);
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

            }
            else
            {
                
                inf.GetComponent<Text>().text = "you need key of the bookcase.";
                StartCoroutine(clean());
            }
        }
    }

    private void Desk(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "r1":
                r1.transform.localPosition = new Vector3(0.7785438f, -0.04612518f, -0.45f);
                StartCoroutine(Room1.Position(3.0f, r1, new Vector3(0.7785438f, -0.04612518f, -0.2429063f)));
                break;
            case "r2":
                r2.transform.localPosition = new Vector3(0.7785438f, -0.1423045f, -0.45f);
                StartCoroutine(Room1.Position(3.0f, r2, new Vector3(0.7785438f, -0.1423045f, -0.2429063f)));
                break;
            case "l1":
                l1.transform.localPosition = new Vector3(0.02821602f, -0.04612518f, -0.45f);
                StartCoroutine(Room1.Position(3.0f, l1, new Vector3(0.02821602f, -0.1423045f, -0.2429063f)));
                break;
            case "l2":
                l2.transform.localPosition = new Vector3(0.02821602f, -0.1423045f, -0.45f);
                StartCoroutine(Room1.Position(3.0f, l2, new Vector3(0.02821602f, -0.1423045f, -0.2429063f)));
                break;
            case "m":
                m.transform.localPosition = new Vector3(0.4033799f, -0.04612518f, -0.45f);
                StartCoroutine(Room1.Position(3.0f, m, new Vector3(0.4033799f, -0.04612518f, -0.2429063f)));
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

    private void Box(RaycastHit hit)
    {
        switch (hit.collider.name)
        {   //Top
            case "T1":
                T1.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T1, new Vector3(90f,0,0)));
                break;
            case "T2":
                T2.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T2, new Vector3(90f,0,0)));
                break;
            case "T3":
                T3.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T3, new Vector3(90f,0,0)));
                break;
            case "T4":
                T4.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T4, new Vector3(90f,0,0)));
                break;
            case "T5":
                T5.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T5, new Vector3(90f,0,0)));
                break;
            case "T6":
                T6.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T6, new Vector3(90f,0,0)));
                break;
            case "T7":
                T7.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T7, new Vector3(90f,0,0)));
                break;
            case "T8":
                T8.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T8, new Vector3(90f,0,0)));
                break;
            case "T9":
                T9.transform.Rotate(new Vector3(-90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, T9, new Vector3(90f,0,0)));
                break;
            case "T10":
                T10.transform.Rotate(new Vector3(-90f, 0, 0));
                StartCoroutine(Center.Rotation(5.0f, T10, new Vector3(90f,0,0)));
                break;
            case "T11":
                T11.transform.Rotate(new Vector3(-90f, 0, 0));
                StartCoroutine(Center.Rotation(5.0f, T11, new Vector3(90f,0,0)));
                break;
            case "T12":
                T12.transform.Rotate(new Vector3(-90f, 0, 0));
                StartCoroutine(Center.Rotation(5.0f, T12, new Vector3(90f,0,0)));
                break;
            //Bottom
            case "B1":
                B1.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B1, new Vector3(-90f,0,0)));
                break;
            case "B2":
                B2.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B2, new Vector3(-90f,0,0)));
                break;
            case "B3":
                B3.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B3, new Vector3(-90f,0,0)));
                break;
            case "B4":
                B4.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B4, new Vector3(-90f,0,0)));
                break;
            case "B5":
                B5.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B5, new Vector3(-90f,0,0)));
                break;
            case "B6":
                B6.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B6, new Vector3(-90f,0,0)));
                break;
            case "B7":
                B7.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B7, new Vector3(-90f,0,0)));
                break;
            case "B8":
                B8.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B8, new Vector3(-90f,0,0)));
                break;
            case "B9":
                B9.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B9, new Vector3(-90f,0,0)));
                break;
            case "B10":
                B10.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B10, new Vector3(-90f,0,0)));
                break;
            case "B11":
                B11.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B11, new Vector3(-90f,0,0)));
                break;
            case "B12":
                B12.transform.Rotate(new Vector3(90f,0,0));
                StartCoroutine(Center.Rotation(5.0f, B12, new Vector3(-90f,0,0)));
                break;
        }
    }

    private void Outkey(RaycastHit hit)
    {
        if (hit.collider.name.Equals("e1")&&Character.Items.Contains("key1") && !k1)
        {
            ekey1.SetActive(true);
            p1.GetComponent<MeshRenderer>().material.mainTexture = green;
            num_key++;
            k1 = true;
        }
        else if (hit.collider.name.Equals("e2") && Character.Items.Contains("key2") && !k2)
        {
            ekey2.SetActive(true);
            p2.GetComponent<MeshRenderer>().material.mainTexture = green;
            num_key++;
            k2 = true;
        }
        else if (hit.collider.name.Equals("e3") && Character.Items.Contains("key3") && !k3)
        {
            ekey3.SetActive(true);
            num_key++;
            k3 = true;
            p3.GetComponent<MeshRenderer>().material.mainTexture = green;
        }
        else if (hit.collider.name.Equals("e4") && Character.Items.Contains("key4") && !k4)
        {
            ekey4.SetActive(true);
            num_key++;
            k4= true;
            p4.GetComponent<MeshRenderer>().material.mainTexture = green;
        }
        else if (hit.collider.name.Equals("e5") && Character.Items.Contains("key5") && !k5)
        {
            ekey5.SetActive(true);
            num_key++;
            k5 = true;
            p5.GetComponent<MeshRenderer>().material.mainTexture = green;
        }
        else if (hit.collider.name.Equals("e6") && Character.Items.Contains("key6") && !k6)
        {
            ekey6.SetActive(true);
            num_key++;
            k6 = true;
            p6.GetComponent<MeshRenderer>().material.mainTexture = green;
        }
    }

    private void Book()
    {   
        book.transform.Rotate(new Vector3(0, -90f, 0));
        StartCoroutine(Center.Rotation(3.0f, book, new Vector3(0, 90f, 0)));
    }

    private void Projector(RaycastHit hit)
    {
        if (hit.collider.name.Equals("projectorL"))
        {   //change color  ,red #FF000000 blue #0000FF00 green #00FF0000
            if (light_on1 == 0)
            {
                projectorL.SetActive(true);
                light_on1 = 1;
                showkey1 = true;
            }
            else if (light_on1 == 1)
            {       
                    Color red;
                    if (ColorUtility.TryParseHtmlString("#FF000000", out red))
                    {
                        projectorL.GetComponent<Light>().color = red;
                        light_on1 = 2;
                        showkey1 = false;
                    }
            }
            else if (light_on1 == 2)
            {
                Color green;
                if (ColorUtility.TryParseHtmlString("#00FF0000", out green))
                {
                    projectorL.GetComponent<Light>().color = green;
                    light_on1 = 3;
                }
            }
            else if (light_on1 == 3)
            {
                Color blue;
                if (ColorUtility.TryParseHtmlString("#0000FF00", out blue))
                {
                    projectorL.SetActive(false);
                    projectorL.GetComponent<Light>().color = blue;                 
                    light_on1 = 0;
                }

            }
        }
        else if (hit.collider.name.Equals("projectorR"))
        {
            if (light_on2 == 0)
            {
                projectorR.SetActive(true);
                light_on2 = 1;
            }
            else if (light_on2 == 1)
            {
                Color red;
                if (ColorUtility.TryParseHtmlString("#FF000000", out red))
                {
                    projectorR.GetComponent<Light>().color = red;
                    light_on2 = 2;
                }
            }
            else if (light_on2 == 2)
            {
                Color green;
                if (ColorUtility.TryParseHtmlString("#00FF0000", out green))
                {
                    projectorR.GetComponent<Light>().color = green;
                    light_on2 = 3;
                    showkey2 = true;
                }
            }
            else if (light_on2 == 3)
            {
                Color blue;
                if (ColorUtility.TryParseHtmlString("#0000FF00", out blue))
                {
                    projectorR.SetActive(false);
                    projectorR.GetComponent<Light>().color = blue;
                    light_on2 = 0;
                    showkey2 = false;
                }
            }
        }
        else if (hit.collider.name.Equals("projectorM"))
        {
            if (light_on3 == 0)
            {
                projectorM.SetActive(true);
                light_on3 = 1;
            }
            else if (light_on3 == 1)
            {
                Color red;
                if (ColorUtility.TryParseHtmlString("#FF000000", out red))
                {
                    projectorM.GetComponent<Light>().color = red;
                    light_on3 = 2;
                    showkey3 = true;
                }
            }
            else if (light_on3 == 2)
            {
                Color green;
                if (ColorUtility.TryParseHtmlString("#00FF0000", out green))
                {
                    projectorM.GetComponent<Light>().color = green;
                    light_on3 = 3;
                    showkey3 = false;
                }
            }
            else if (light_on3 == 3)
            {
                Color blue;
                if (ColorUtility.TryParseHtmlString("#0000FF00", out blue))
                {
                    projectorM.SetActive(false);
                    projectorM.GetComponent<Light>().color = blue;
                    light_on3 = 0;
                }
            }
        }   
    }

    private void Exit()
    {
        if (isLockedOut)
        {
            
            inf.GetComponent<Text>().text = "The door is locked,you need collect 6 keys.";
            StartCoroutine(clean());
        }
        else
        {
            outdoor.transform.Rotate(new Vector3(0, 90f, 0));
            isOpening = true;           
            StartCoroutine(GameOver(10f));
            
            inf.GetComponent<Text>().text = "you win this game .you will back to Main menu after 10s.";
            StartCoroutine(clean());
        }
    }

    IEnumerator GameOver(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);       
        SceneManager.LoadScene("main_menu");
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
            R2Door.transform.Rotate(new Vector3(0, 90f, 0));
            isOpening = true;
            if (locked != 0)
            {
                StartCoroutine(Center.Rotation(5.0f, R2Door, new Vector3(0, -90f, 0)));
            }
        }
    }
}
