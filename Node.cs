using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    
    /// <summary>
    /// referencia a gameController
    /// </summary>
    public GameObject gameController;
    /// <summary>
    /// canvas torres
    /// </summary>
    public Canvas towerCanvas;

    
    /// <summary>
    /// color al que cambiar cambiar
    /// </summary>
    public Color hoverColor;

    /// <summary>
    /// posición de una torreta
    /// </summary>
    public  GameObject turretHolder;
    /// <summary>
    /// gameobject de torreta
    /// </summary>
    public  GameObject turret;
    /// <summary>
    /// renderer
    /// </summary>
    private Renderer rend;

    /// <summary>
    /// color inicial
    /// </summary>
    private Color startColor;

    /// <summary>
    /// Confirmador
    /// </summary>
    public bool HayTorre=false;
    /// <summary>
    /// barrierIndex
    /// </summary>
    public int index;

    private void Start()
    {
        
        towerCanvas.gameObject.GetComponent<Canvas>().enabled = false;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    private void Update()
    {
        
        if (this.turret != null)
        {
            //Debug.Log("This turret cannot be placed");
            HayTorre = true;
            
        }
        else
        {
            HayTorre = false;
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach(RaycastHit hit in hits)
        {
            if (hit.transform.gameObject == gameObject)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    MouseDown();
                }
                else
                {
                    MouseEnter();
                }
                break;
            }
            else
                MouseExit();
        }



    }
    /// <summary>
    /// al pasar el ratón por encima cambia a el color designado
    /// </summary>
    void MouseEnter()
    {
            rend.material.color = hoverColor;
    }
    /// <summary>
    /// al quitar el ratón de encima se vuelve al color original
    /// </summary>
     void MouseExit()
    {
        rend.material.color = startColor;
    }

    /// <summary>
    /// al hacer click en un cubo se crea una torreta si no se ha creado ya
    /// </summary>
    private void MouseDown()
    {
        if (HayTorre == true)
        {
            Debug.Log("This turret cannot be placed");
            
            return;
        }
        else
        {
            if (towerCanvas.gameObject.GetComponent<Canvas>().enabled == false)
            {
                gameController.GetComponent<BuildManager>().setNode(turretHolder, gameObject);
                towerCanvas.gameObject.GetComponent<Canvas>().enabled = true;

                for (int i = 0; i < 7; i++)
                {
                    gameController.GetComponent<GameController>().confirmationTowerButtons[i].gameObject.SetActive(false);
                }
            }
            else
            {
                towerCanvas.gameObject.GetComponent<Canvas>().enabled = false;
                gameController.GetComponent<BuildManager>().setNode(null, null);
            }
        }
    }
}
