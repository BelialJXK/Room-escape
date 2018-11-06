using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room1 : MonoBehaviour {
    //photo  
    public GameObject JA;
    public GameObject AR;
    public GameObject LC;
    public GameObject SH;
    public GameObject NA;
    public GameObject Jback;
    public GameObject Aback;
    public GameObject Lback;
    public GameObject Sback;
    public GameObject Nback;
    //lamp
    public GameObject LampR;
    public GameObject LampL;
    public bool lampl = false;
    public bool lampr = false;
    public GameObject Top;
    public bool top_on = true;
    //cushion
    public GameObject cushion1;
    public GameObject cushion2;
    //Wardrobe
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject Bottom;
    public GameObject key1;
    public GameObject hint1;
    public GameObject character;
    //desk
    public GameObject r1;
    public GameObject r2;
    public GameObject l1;
    public GameObject l2;
    public GameObject m;
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
    //Door
    public GameObject R1Door;
    public bool isLocked = false;
    public int locked=0;
    public bool isOpening = false;
    public static bool inRoom1 = false;

    public Button b1;
    public Button b2;
    public Sprite i1;
    public Sprite i2;
    public GameObject inf;
    // Use this for initialization
    void Start () {
        LampL.SetActive(false);
        LampR.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
        //check character that is in the room?
        if (inRoom1)
        {
            if (character.transform.position.z < -14.5 && locked == 0)
            {
                isLocked = true;
                locked++;
                R1Door.transform.Rotate(new Vector3(0, -90f, 0));
                isOpening = false;
            }
            eventManage();
            if(bubl.GetComponent<MeshRenderer>().material.mainTexture == green)
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
                //Debug.LogError(Ename);
                if (Ename.Equals("LeftDoor") || Ename.Equals("RightDoor") || Ename.Equals("Bottom") || Ename.Equals("key1"))
                {
                    Wardrobe(hit);
                }
                else if (Ename.Equals("R1D"))
                {
                    Door();
                }
                else if (Ename.Equals("JA") || Ename.Equals("AR") || Ename.Equals("LC") || Ename.Equals("SH") || Ename.Equals("NA")||
                         Ename.Equals("Jback") || Ename.Equals("Aback") || Ename.Equals("Lback") || Ename.Equals("Sback") || Ename.Equals("Nback"))
                {
                    Photo(hit);
                }
                else if (Ename.Equals("1") || Ename.Equals("2") || Ename.Equals("3") || Ename.Equals("4") || Ename.Equals("5") ||
                    Ename.Equals("6") || Ename.Equals("7") || Ename.Equals("8") || Ename.Equals("9") || Ename.Equals("reset"))
                {
                    ps=Code( hit,  "693",  color,  ps,  bubl,  show,  isLocked,  white,  red,  green);
                }
                else if (Ename.Equals("r1") || Ename.Equals("r2") || Ename.Equals("l1") || Ename.Equals("l2") || Ename.Equals("m"))
                {
                    Desk(hit);
                }
                else if (Ename.Equals("LampR") || Ename.Equals("LampL"))
                {
                    Lamp(hit);
                }
                else if (Ename.Equals("cushion1") || Ename.Equals("cushion2"))
                {
                    Cushion(hit);
                }
                else if (Ename.Equals("Hint1"))
                {
                    Hint();
                }
                else if (Ename.Equals("switch"))
                {
                    top_on=Switch(Top,top_on);
                }
            }
        }
    }

    private void Wardrobe(RaycastHit hit)
    {
        string name = hit.collider.name;
        switch(name)
        {
            case "LeftDoor":
                LeftDoor.transform.Rotate(new Vector3(0, 90f, 0));
                StartCoroutine(Center.Rotation(5.0f, LeftDoor, new Vector3(0, -90f, 0)));
                break;
            case "RightDoor":
                RightDoor.transform.Rotate(new Vector3(0, -90f, 0));
                StartCoroutine(Center.Rotation(5.0f, RightDoor, new Vector3(0, 90f, 0)));
                break;
            case "Bottom":
                Bottom.transform.localPosition = (new Vector3(0, 0.26f, 0.7f));
                StartCoroutine(Position(4.0f, Bottom, new Vector3(0, 0.26f, 0.25f)));
                break;
            case "key1":
                Character.Items.Add("key1");
                Destroy(key1);
                b1.gameObject.GetComponent<Image>().sprite = i1;
                
                inf.GetComponent<Text>().text = "you get key1.";
                StartCoroutine(clean());
                break;
        }
    }

    public  IEnumerator  clean()
    {
        yield return new WaitForSeconds(2);
        inf.GetComponent<Text>().text = "";
    }
    private void Door()
    {           
        //first time in the room,she need get password.
        if (isLocked)
        {   
            inf.GetComponent<Text>().text = "The door is locked,you need input correct password.";
            StartCoroutine(clean());
        }
        else
        {
            R1Door.transform.Rotate(new Vector3(0, 90f, 0));
            isOpening = true;
            if (locked!=0)
            {
                StartCoroutine(Center.Rotation(5.0f, R1Door, new Vector3(0, -90f, 0)));
            }
            
        }
        
    }

    private void Photo(RaycastHit hit)
    {
        string name = hit.collider.name;
        switch (name)
        {   
            //Front
            case "JA":
                JA.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, JA, new Vector3(0, 0, -180f)));
                break;
            case "AR":
                AR.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, AR, new Vector3(0, 0, -180f)));
                break;
            case "LC":
                LC.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, LC, new Vector3(0, 0, -180f)));
                break;
            case "SH":
                SH.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, SH, new Vector3(0, 0, -180f)));
                break;
            case "NA":
                NA.transform.Rotate(new Vector3(0, 0, 180f));
                StartCoroutine(Center.Rotation(5.0f, NA, new Vector3(0, 0, -180f)));
                break;
            //Back
            case "Jback":
                Jback.transform.Rotate(new Vector3(0, 0, 150f));
                StartCoroutine(Center.Rotation(3.0f, Jback, new Vector3(0, 0, -150f))); 
                break;
            case "Aback":
                Aback.transform.Rotate(new Vector3(0, 0, 150f));
                StartCoroutine(Center.Rotation(3.0f, Aback, new Vector3(0, 0, -150f)));
                break;
            case "Lback":
                Lback.transform.Rotate(new Vector3(0, 0, 150f));
                StartCoroutine(Center.Rotation(3.0f, Lback, new Vector3(0, 0, -150f)));
                break;
            case "Sback":
                
                inf.GetComponent<Text>().text = "It is locked.";
                StartCoroutine(clean());
                break;
            case "Nback":
                
                inf.GetComponent<Text>().text = "It is locked.";
                StartCoroutine(clean());
                break;
        }
    }

    public static string Code(RaycastHit hit,string password,int color,string ps,GameObject bubl,GameObject show,bool isLocked,Texture white, Texture red, Texture green)
    {
        string name = hit.collider.name;
        if (color != 1)
        {
            if (hit.collider.name.Equals("reset"))
            {
                ps = "";
                bubl.GetComponent<MeshRenderer>().material.mainTexture = white;
                show.GetComponent<TextMesh>().text = ps;
                color = 3;
            }
            else
            {
                ps += hit.collider.name.Substring(0,1);
                
                if (color == 2)
                {   //start new round input password
                    bubl.GetComponent<MeshRenderer>().material.mainTexture = white;
                    show.GetComponent<TextMesh>().text = ps;
                    color = 3;
                }
                if (ps.Length < password.Length+1)
                {
                    show.GetComponent<TextMesh>().text = ps;
                    if (ps.Length == password.Length && ps.Equals(password))
                    {
                        bubl.GetComponent<MeshRenderer>().material.mainTexture = green;
                        color = 1;
                    }
                    else if (ps.Length == password.Length && !ps.Equals(password))
                    {
                        bubl.GetComponent<MeshRenderer>().material.mainTexture = red;
                        color = 2;
                        ps = "";
                    }
                }
            }
        }
        return ps;
    }

    void Desk(RaycastHit hit)
    {
        switch (hit.collider.name)
        {
            case "r1":
                r1.transform.localPosition = new Vector3(0.7785438f, -0.04612518f, -0.45f);
                StartCoroutine(Position(3.0f, r1, new Vector3(0.7785438f, -0.04612518f, -0.2429063f)));
                break;
            case "r2":
                r2.transform.localPosition = new Vector3(0.7785438f, -0.1423045f, -0.45f);
                StartCoroutine(Position(3.0f, r2, new Vector3(0.7785438f, -0.1423045f, -0.2429063f)));
                break;
            case "l1":
                l1.transform.localPosition = new Vector3(0.02821602f, -0.04612518f, -0.45f);
                StartCoroutine(Position(3.0f, l1, new Vector3(0.02821602f, -0.1423045f, -0.2429063f)));
                break;
            case "l2":
                l2.transform.localPosition = new Vector3(0.02821602f, -0.1423045f, -0.45f);
                StartCoroutine(Position(3.0f, l2, new Vector3(0.02821602f, -0.1423045f, -0.2429063f)));
                break;
            case "m":
                m.transform.localPosition = new Vector3(0.4033799f, -0.04612518f, -0.45f);
                StartCoroutine(Position(3.0f, m, new Vector3(0.4033799f, -0.04612518f, -0.2429063f)));
                break;
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

    private  void Cushion(RaycastHit hit)
    {      
        switch (hit.collider.name)
        {
            case "cushion2":
                cushion2.transform.Rotate(new Vector3(0, 0,-60));
                StartCoroutine(Center.Rotation(3.0f, cushion2,new Vector3(0, 0, 60)));
                break;
            case "cushion1":
                cushion1.transform.Rotate(new Vector3(60f, 0, 0));
                StartCoroutine(Center.Rotation(3.0f, cushion1, new Vector3(-60f, 0, 0)));
                break;          
        }
    }

    public void Hint()
    {   
        Character.Items.Add("hint1");
        hint1.SetActive(false);
        b2.gameObject.GetComponent<Image>().sprite = i2;
        
        inf.GetComponent<Text>().text = "you get hint1.";
        StartCoroutine(clean());
    }

    public static bool Switch(GameObject Top ,bool top_on)
    {   
        if (top_on)
        {
            Top.SetActive(false);
            top_on = false;
            return top_on;
        }
        else
        {
            Top.SetActive(true);
            top_on = true;
            return top_on;
        }
    }

    public static IEnumerator Position(float waitTime, GameObject door, Vector3 vector)
    {
        yield return new WaitForSeconds(waitTime);
        door.transform.localPosition=vector;
    }
}
