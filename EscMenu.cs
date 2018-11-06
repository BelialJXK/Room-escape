using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour {
    public Button Save;
    public Button Load;
    public Button MainMenu;
    public Button Cancel;
    public FileStream file ;
    public GameObject sf;
    string load;


    void Start () {
        load = Application.dataPath + "\\Resources\\ESC.save";
        //load = load + "\\Assets\\GAME.save";
        Cursor.visible = true;
        Save.onClick.AddListener(save);
        Load.onClick.AddListener(load1);
        MainMenu.onClick.AddListener(mainmenu);
        Cancel.onClick.AddListener(cancel);

    }
    private void Update()
    {
        if (!File.Exists(load))
        {
            Load.GetComponent<Button>().enabled = false;
        }
        else
        {
            Load.GetComponent<Button>().enabled = true;
        }
    }
    private void mainmenu()
    {
        SceneManager.LoadScene("main_menu");     
    }

    private void cancel()
    {   //load last position
        //path = path + "\\Assets\\ESC.save";
        if (File.Exists(load))
        {   //get data
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(load, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            //back to scene
            SceneManager.LoadScene("Game1");
            //get position
            Vector3 position = new Vector3();
            position.x = save.characte_postion[0];
            position.y = save.characte_postion[1];
            position.z = save.characte_postion[2];
            //send position to character
            Character.SendPosition(position,save.Minute,save.Second,save.Items);         
        }
    }

    private void load1()
    {   //1. read date and load to the last location
        string path = Application.dataPath + "\\Resources\\Save.save";
        file = File.Open(load, FileMode.Open);
        if (File.Exists(path))
        {   //get game data(character current position and scene)
            BinaryFormatter bf = new BinaryFormatter();            
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            SceneManager.LoadScene("Game1");
            //get position
            Vector3 position = new Vector3();
            position.x = save.characte_postion[0];
            position.y = save.characte_postion[1];
            position.z = save.characte_postion[2];
            //send position to character
            Character.SendPosition(position, save.Minute,save.Second, save.Items);          
        }
        else
        {
            
            sf.GetComponent<Text>().text = "Load failed,empty database.";
            StartCoroutine(wait());
        }
    }

    private void save()
    {   //get data from ESC.save
        //string path = System.Environment.CurrentDirectory;
        string path = Application.dataPath+ "\\Resources\\ESC.save";
        //path = path + "\\Assets\\ESC.save";
        file = File.Open(path, FileMode.Open);
        if (File.Exists(path))
        {   //get game data(character current position and scene)
            BinaryFormatter bf = new BinaryFormatter();         
            Save save = (Save)bf.Deserialize(file);           
            Save save1 =save;
            //save game           
            string path1 = Application.dataPath + "\\Resources\\Save.save";
            //path1 = path1 + "\\Assets\\GAME.save";
            BinaryFormatter bf1 = new BinaryFormatter();
            FileStream file1 = File.Create(path1);
            bf.Serialize(file1, save);
            file1.Close();
            file.Close();
            sf.GetComponent<Text>().text= "Game save succeed.";
            StartCoroutine(wait());
        }
        else
        {
            
            sf.GetComponent<Text>().text = "Game save Failed.";
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        sf.GetComponent<Text>().text = "";
    }
}
