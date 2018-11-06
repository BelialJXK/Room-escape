using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Character : MonoBehaviour
{
    
    public bool IsPlay = true;
    CharacterController controller;
    protected Animator _animator;
    public float m_speed = 5f;
    public static Cameras cam = Cameras.FPPcamera;
    public static Cameras cam1;
    public static GameObject  TPPC;
    public static GameObject FPPC;
    public static Vector3 location;
    public static int Is_backESC = 0;
    private Transform m_Transform;
    Vector3 move_direction;
    private bool Rotating = false;
    public bool ishiding = true;
    public static bool getKey = false;
    public static List<string> Items = new List<string>();   
    public static float correctSecond;
    public static float correctMinute;
    public float currentSecond;
    public float currentMinute;
    
    //Inventory
    public  GameObject Information;
    public  GameObject countdwon;
    public  bool showItem = false;
    public  Button bhint1;
    public  Button bhint2;
    public  Button bhint3;
    public  Button bhint4;
    public  Button bhint5;
    public  Button bhint6;
    public Sprite h1;
    public  GameObject hint1;
    public  GameObject hint2;
    public  GameObject hint3;
    public  GameObject hint4;
    public  GameObject hint5;
    public  GameObject hint6;
    public  Button key1;
    public  Button key2;
    public  Button key3;
    public  Button key4;
    public  Button key5;
    public  Button key6;
    public  Button bubl;
    public  GameObject information;
    public bool InventoryON=false;
    public int mouse=0;
    [DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);
    
    public  enum Cameras
    {   
        TPPcamera, FPPcamera
    }
    private void Awake()
    {   
        TPPC = GameObject.Find("TPPcamera");
        FPPC = GameObject.Find("FPPcamera");
        controller = this.GetComponent<CharacterController>();
        _animator = this.GetComponent<Animator>();     
    }
    // Use this for initialization
    void Start()
    {
        //character active video
        _animator = GetComponent<Animator>();              
        m_Transform = gameObject.GetComponent<Transform>();   
        TPPC.SetActive(false);
        //倒计时
        StartCoroutine(CountDown());

        hiding();
        bhint1.onClick.AddListener(b1);
        bhint2.onClick.AddListener(b2);
        bhint3.onClick.AddListener(b3);
        bhint4.onClick.AddListener(b4);
        bhint5.onClick.AddListener(b5);
        bhint6.onClick.AddListener(b6);
    }

    

    void Update()
    {   
        if (Is_backESC == 1)
        {
            transform.position = location;
            Is_backESC = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESC_minMenu();
        }
        if (InventoryON)
        {

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryON = true;
            if (!showItem)
            {
                showUP();
                showItem = true;
            }
            else
            {
                hiding();
                showItem = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ViewChange();
        }

        HideCursor();
        if (ishiding)
        {
            RotateByMouse();
        }
        if (getKey)
        {
            SceneManager.LoadScene("main_menu");
        }
        Move();
        if (transform.position.z > 10 && transform.position.x > -6 && transform.position.x < 6)
        {
            Room2.inRoom2 = true;
            Center.inCent = false;
        }
        else if(transform.position.z <- 10 && transform.position.x > -6 && transform.position.x < 6)
        {
            Room1.inRoom1 = true;
            Center.inCent = false;
        }
        else if(transform.position.z > 0 && transform.position.x > 9 )
        {
            Room3.inRoom3 = true;
            Center.inCent = false;
        }
        else if (transform.position.z < 0 && transform.position.x < -9 )
        {
            Room4.inRoom4 = true;
            Center.inCent = false;
        }
        else if (transform.position.z > 0 && transform.position.x < -9)
        {
            Room5.inRoom5 = true;
            Center.inCent = false;
        }
        else if (transform.position.z <0 && transform.position.x > 9)
        {
            Room6.inRoom6 = true;
            Center.inCent = false;
        }
        else
        {            
            Center.inCent = true;
            Room6.inRoom6 = false;
            Room5.inRoom5 = false;
            Room4.inRoom4 = false;
            Room3.inRoom3 = false;
            Room2.inRoom2 = false;
            Room1.inRoom1 = false;
        }      
    }


    private IEnumerator CountDown()
    {
        if (Is_backESC == 1)
        {
            currentSecond = correctSecond;
            currentMinute = correctMinute;
        }
        else
        {
            currentSecond = 59;
            currentMinute = 19;
        }
        while (currentSecond >= 0)
        {
            countdwon.GetComponent<Text>().text = "Time : " + currentMinute + "." + currentSecond + "s";
            yield return new WaitForSeconds(1);
            if (currentSecond == 0)
            {
                currentMinute--;
                currentSecond = 59;
            }
            else
            {
                currentSecond--;
                if (currentMinute == 0 && currentSecond == 0)
                {
                    countdwon.GetComponent<Text>().text = "Time : 0.0s";
                    information.GetComponent<Text>().text = "Time is over.you lost";
                    StartCoroutine(wait_3s());
                    break;
                }
            }
        }          
    }

    public  IEnumerator wait_3s()
    {
        yield return new WaitForSeconds(3);
        information.GetComponent<Text>().text = "";
        SceneManager.LoadScene("main_menu");
    }

    private void PlayMusic()
    {
        if (IsPlay)
        {
            MenuMusic.audioSource.Pause();
            IsPlay = false;
        }
        else
        {
            MenuMusic.audioSource.Play();
            IsPlay = true;
        }
    }

    public static void  ViewChange()
    {
        if (cam == Cameras.TPPcamera)
        {
            FPPC.SetActive(true);
            TPPC.SetActive(false);
            cam = Cameras.FPPcamera;
        }
        else
        {
            TPPC.SetActive(true);
            FPPC.SetActive(false);
            cam = Cameras.TPPcamera;
        }
    }

    void Move()
    {   //a（-1） & d（1）（左右运动）
        float h = Input.GetAxis("Horizontal");
        //w（1） & s（-1）（前后运动）
        float v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f)
        {   // 打开奔跑动画
            _animator.SetBool("isMoving", true);
            move_direction = new Vector3(h, 0, v);//从键盘输入的方向值
            Vector3 current_direction = transform.TransformPoint(move_direction) - transform.position;//用以自己为参考的目标点的世界坐标减去自己的世界坐标
            controller.Move(current_direction * m_speed * Time.deltaTime);//move移动方向是以世界坐标为参考方向的
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }
    
    //人物跟随鼠标转向
    void RotateByMouse()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Rotating = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Rotating = false;
        }
        if (!Rotating)
        {
            m_Transform.Rotate(Vector3.up, Input.GetAxis("Mouse X"));
        }
    }

    public static void SendPosition(Vector3 p,float min,float sec,List<string> item)
    {
        location =p;
        item.ForEach(i => Items.Add(i));
        correctSecond= sec;
        correctMinute = min;
        Is_backESC = 1;
    } 

    void ESC_minMenu()
    {   
        //save character current position and scene
        Save save = new Save();
        save.Minute = currentMinute;
        save.Second = currentSecond;       
        Items.ForEach(i => save.Items.Add(i));
        save.characte_postion.Add(transform.position.x);
        save.characte_postion.Add(transform.position.y);
        save.characte_postion.Add(transform.position.z);
 
        string str = Application.dataPath; //D:\罗兰\毕业设计\test\test\Assets\
        //str = str + "\\Assets\\";
 
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(str + "\\Resources\\ESC.save", FileMode.OpenOrCreate);

        bf.Serialize(file, save);
        file.Close();
        if (cam == Cameras.TPPcamera)
        {   
            cam1 = cam;
            SceneManager.LoadScene("ESC");
        }
        else
        {
            cam1 = cam;
            SceneManager.LoadScene("ESC");
        }
    }

    public void HideCursor()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ishiding = false;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ishiding = true;
        }
        if (!ishiding)
        {
            if (mouse == 0)
            {
                mouse = 1;
                Cursor.visible = true;
                SetCursorPos(Screen.width/2,Screen.height/2);
            }          
            
        }
        else
        {            
            Cursor.visible = false;
            mouse = 0;
        }
    }

    void b1()
    {
        if (bhint1.gameObject.GetComponent<Image>().sprite.name.Substring(0,4).Equals(h1.name))
        {
            information.SetActive(true);
            information.GetComponent<Text>().text = hint1.GetComponent<TextMesh>().text;
        }
        
    }
    void b2()
    {   
        if (bhint2.gameObject.GetComponent<Image>().sprite.name.Substring(0, 4).Equals(h1.name))
        {
            information.SetActive(true);
            information.GetComponent<Text>().text = hint2.GetComponent<TextMesh>().text;
        }
    }
    void b3()
    {
        if (bhint3.gameObject.GetComponent<Image>().sprite.name.Substring(0, 4).Equals(h1.name))
        {
            information.SetActive(true);
            information.GetComponent<Text>().text = hint3.GetComponent<TextMesh>().text;
        }
    }
    void b4()
    {
        if (bhint4.gameObject.GetComponent<Image>().sprite.name.Substring(0, 4).Equals(h1.name))
        {
            information.SetActive(true);
            information.GetComponent<Text>().text = hint4.GetComponent<TextMesh>().text;
        }
    }
    void b5()
    {
        if (bhint5.gameObject.GetComponent<Image>().sprite.name.Substring(0, 4).Equals(h1.name))
        {
            information.SetActive(true);
            information.GetComponent<Text>().text = hint5.GetComponent<TextMesh>().text;
        }
    }
    void b6()
    {
        if (bhint6.gameObject.GetComponent<Image>().sprite.name.Substring(0, 4).Equals(h1.name))
        {
            information.SetActive(true);
            information.GetComponent<Text>().text = hint6.GetComponent<TextMesh>().text;
        }
    }

    public  void showUP()
    {
        information.SetActive(true);
        bhint1.gameObject.SetActive(true);
        bhint2.gameObject.SetActive(true);
        bhint3.gameObject.SetActive(true);
        bhint4.gameObject.SetActive(true);
        bhint5.gameObject.SetActive(true);
        bhint6.gameObject.SetActive(true);
        key1.gameObject.SetActive(true);
        key2.gameObject.SetActive(true);
        key3.gameObject.SetActive(true);
        key4.gameObject.SetActive(true);
        key5.gameObject.SetActive(true);
        key6.gameObject.SetActive(true);
        bubl.gameObject.SetActive(true);

    }

    public  void hiding()
    {
        information.GetComponent<Text>().text = "";
        bhint1.gameObject.SetActive(false);
        bhint2.gameObject.SetActive(false);
        bhint3.gameObject.SetActive(false);
        bhint4.gameObject.SetActive(false);
        bhint5.gameObject.SetActive(false);
        bhint6.gameObject.SetActive(false);
        key1.gameObject.SetActive(false);
        key2.gameObject.SetActive(false);
        key3.gameObject.SetActive(false);
        key4.gameObject.SetActive(false);
        key5.gameObject.SetActive(false);
        key6.gameObject.SetActive(false);
        bubl.gameObject.SetActive(false);
    }


}
