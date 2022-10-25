using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildManager : MonoBehaviour
{
    /// <summary>
    /// crea una instancia estática y pública a otros scripts de Buildmanager
    /// </summary>
    public static BuildManager instance;

    /// <summary>
    /// Nodo de posición de la torreta
    /// </summary>
    public GameObject node;
    public GameObject fatherNode;
    public GameObject turretToBuild;

    /// <summary>
    /// la torreta a crear
    /// </summary>
    public GameObject[] turretPrefabs;

    int[] costs = { 50, 100, 100, 200, 200, 50 };

    /// <summary>
    /// devuelve la torreta a construir
    /// </summary>
    /// <returns></returns>
    public void BuildTurret()
    {
        
        if (costs[gameObject.GetComponent<GameController>().towerSelector - 1 ] <= gameObject.GetComponent<GameController>().moneyCounter)
        {
            gameObject.GetComponent<GameController>().moneyCounter = gameObject.GetComponent<GameController>().moneyCounter - costs[gameObject.GetComponent<GameController>().towerSelector - 1];
            //coge la torreta a construir de buildManager
            
            turretToBuild = turretPrefabs[gameObject.GetComponent<GameController>().towerSelector];
            
            
            fatherNode.GetComponent<Node>().turret = turretToBuild;
            //instancia una torreta en la posición de turretHolder
            Instantiate(turretToBuild, node.transform.position, node.transform.rotation);
            gameObject.GetComponent<GameController>().confirmationTowerButtons[gameObject.GetComponent<GameController>().towerSelector - 1].gameObject.SetActive(false);
            fatherNode.GetComponent<Node>().towerCanvas.enabled = false;
        }
        //else
        //{
        //    Debug.Log("Not enough points");
        //}

    }

    public void setNode(GameObject node, GameObject fatherNode)
    {
        this.node = node;
        this.fatherNode = fatherNode;
    }
    

    
}
