using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IglesiaController : MonoBehaviour
{
    public float HP = 100f;
    public GameController Game;
    public AgentController Enemy;

    /// <summary>
    /// Se le pasa un script tipo gamecontroller y uno agent controller. Si el HP de la iglesia llega a 0 llama a la funcion lose del
    /// game controller que termina el juego, y se usa el agent controller para pasarle el damage que hacen los enemigos a la iglesia
    /// </summary>
    void Update()
    {
        if (HP <= 0)
        {
            Game.Lose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Soldier")
        {
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
            HP = HP - other.gameObject.GetComponent<AgentController>().soldierDamage;
        }

    }
}