using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Button NewGame;
    public Button LoadGame;
    public Button Help;
    public Button GameExit;
    public string load;




    public FileStream file;

    void Start () {
        load = Application.dataPath + "\\Resources\\Save.save";
        Cursor.visible = true;
        NewGame.onClick.AddListener(begin);     
        Help.onClick.AddListener(help);
        GameExit.onClick.AddListener(Exit);
        LoadGame.onClick.AddListener(Load);    
    }
    private void Update()
    {
         
        if (!File.Exists(load))
        {
            LoadGame.GetComponent<Button>().enabled = false;
        }
        else
        {
            LoadGame.GetComponent<Button>().enabled = true;
        }
    }

    private void Exit()
    {         
        Application.Quit();
    }
    private void help()
    {   
        SceneManager.LoadScene("HelpPage");
    }
    private void Load()
    {   //1. read date and load to the last location
        //+ "\\Assets\\GAME.save"
        if (File.Exists(load))
        {
            
            //get game data(character current position and scene)
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(load, FileMode.Open);//+ "\\Assets\\GAME.save"
            Save save = (Save)bf.Deserialize(file);
            file.Close();
            //go to scene
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
    private void begin()
    {   
        SceneManager.LoadScene("Game1");     
    }
}
