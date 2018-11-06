using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpPage : MonoBehaviour {
    public Button Back;
    void Start () {
        Back.onClick.AddListener(mainmenu);
        Cursor.visible = true;
    }

    private void mainmenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}
