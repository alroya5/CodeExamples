using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu_Controller : MonoBehaviour
{
    public Canvas C_MainMenu;

    public Button B_Play;
    public Button B_Quit;

    // Start is called before the first frame update
    void Start()
    {
        C_MainMenu.gameObject.GetComponent<Canvas>().enabled = true;

        Button btn = B_Play.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        Button btn1 = B_Quit.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick1);
    }

    /// <summary>
    /// Carga la escena principal del juego.
    /// </summary>
    void TaskOnClick()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Cierra el juego.
    /// </summary>
    void TaskOnClick1()
    {
        Application.Quit();
    }
}
