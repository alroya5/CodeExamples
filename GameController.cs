using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    public Canvas C_Pause;
    public Canvas C_Options;
    public Canvas C_Lose;
    //public Canvas C_Win;

    public Button Continue;
    public Button Options;
    public Button Back;
    public Button Exit;
    public Button MainMenu;

    public Death death;


    /// <summary>
    /// Cuenta los puntos de la partida y dinero.
    /// </summary>
    public int pointCounter = 0;
    public int moneyCounter = 100;

    /// <summary>
    /// Cuenta la oleada actual
    /// </summary>
    float waveCounter;

    /// <summary>
    /// referencia a texto de puntos
    /// </summary>
    public TextMeshProUGUI points;

    public TextMeshProUGUI hp;

    [HideInInspector]
    /// <summary>
    /// index de torre
    /// </summary>
    public int towerSelector;

    /// <summary>
    /// 
    /// </summary>
    public Button[] confirmationTowerButtons;

    private IglesiaController iglesiaController;

    /// <summary>
    /// Inicializa todo
    /// </summary>
    void Start()
    {
        iglesiaController = FindObjectOfType<IglesiaController>();
        Invoke("SumarPuntosPorSegundo", 1f);

        C_Pause.gameObject.GetComponent<Canvas>().enabled = false;

        C_Options.gameObject.GetComponent<Canvas>().enabled = false;

        C_Lose.gameObject.GetComponent<Canvas>().enabled = false;

        //C_Win.gameObject.GetComponent<Canvas>().enabled = false;

        Button btn = Continue.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        Button btn1 = Options.GetComponent<Button>();
        btn1.onClick.AddListener(TaskOnClick1);

        Button btn2 = Back.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClick2);

        Button btn3 = Exit.GetComponent<Button>();
        btn3.onClick.AddListener(TaskOnClick3);

        Button btn4 = MainMenu.GetComponent<Button>();
        btn4.onClick.AddListener(TaskOnClick3);

        Time.timeScale = 1;
    }

    /// <summary>
    /// Resume el juego al usar el boton continue
    /// </summary>
    void TaskOnClick()
    {
        C_Pause.gameObject.GetComponent<Canvas>().enabled = false;
       
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    /// <summary>
    /// Boton Options que desplega las opciones del juego.
    /// </summary>
    void TaskOnClick1()
    {
        C_Pause.gameObject.GetComponent<Canvas>().enabled = false;
        C_Options.gameObject.GetComponent<Canvas>().enabled = true;
        
    }

    /// <summary>
    /// Boton Back que te devuelve al menu pausa.
    /// </summary>
    void TaskOnClick2()
    {
        C_Pause.gameObject.GetComponent<Canvas>().enabled = true;
        C_Options.gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Boton Exit que te devuelve al menu principal.
    /// Tambien se usa este codigo al perder la partida.
    /// </summary>
    void TaskOnClick3()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        C_Lose.gameObject.GetComponent<Canvas>().enabled = true;
    }
    
    /*void Win()
    {
        if(pointCounter >=10000)
            Time.timeScale = 0f;
            C_Win.gameObject.GetComponent<Canvas>().enabled = true;
    }*/
    
    public static bool gameIsPaused;

    void Update()
    {
        //actualiza la vida de la iglesia
        hp.text = "HP: " + iglesiaController.HP;
        //actualiza los puntos y el dinero en cada frame
        points.text = "Puntos: " + pointCounter + "\nDinero: " + moneyCounter;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
            TurnOffCanvas();
        }
        //Win();
    }

    /// <summary>
    /// Pausa el juego, activa el canvas de pausa y desactiva el canvas Game.
    /// </summary>
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            C_Pause.gameObject.GetComponent<Canvas>().enabled = true;
           
        }
        else
        {
            Time.timeScale = 1;
            C_Pause.gameObject.GetComponent<Canvas>().enabled = false;
            
        }
    }

    /// <summary>
    /// Desactiva el options canvas.
    /// </summary>
    void TurnOffCanvas()
    {
        C_Options.gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// selecciona el tipo de torre
    /// </summary>
    /// <param name="index"></param>
    public void TowerSelect(int index)
    {
        towerSelector = index;
    }

    /// <summary>
    /// Muestra y oculta los botones de confirmación de torres
    /// </summary>
    public void ShowConfirmationButton()
    {
        for (int i = 0; i < 7; i++)
        {
            //si está desactivado lo activa y sino lo desactiva
            if (i == towerSelector && confirmationTowerButtons[i].gameObject.activeInHierarchy == false)
            {
                confirmationTowerButtons[i].gameObject.SetActive(true);
            }
            else if (i == towerSelector && confirmationTowerButtons[i].gameObject.activeInHierarchy == true)
            {
                confirmationTowerButtons[i].gameObject.SetActive(false);
            }
           
        }
    }

    /// <summary>
    /// Gana 5 puntos por segundo
    /// </summary>
    private void SumarPuntosPorSegundo()
    {
        moneyCounter = moneyCounter + 5;
        Invoke("SumarPuntosPorSegundo", 1f);
    }
    /// <summary>
    /// Suma puntos al morir
    /// </summary>
    /// <param name="p"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public void SumarPuntos(int p, int i)
    {
        moneyCounter +=  p;
        pointCounter += i;
        Debug.Log("se sumaron puntos");
    }
}
   

