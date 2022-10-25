using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthModule : MonoBehaviour
{
    /// <summary>
    /// Puntos que da al jugador al morir
    /// </summary>
    public int enemyPoints = 50;
    public int enemyMoney = 50;
   
    private TowerController twr;
    private GameController gameController;
    /// <summary>
    /// Salud máxima del personaje
    /// </summary>
    public float MAX_HEALTH = 100;

    /// <summary>
    /// Salud actual del personaje
    /// </summary>
    public float currentHealth;

    /// <summary>
    /// Estado del sujeto según su salud
    /// </summary>
    public enum HealthState { live, death }
    public HealthState healthState;

    [HideInInspector]
    /// <summary>
    /// Spawn que ha generado a este personaje
    /// </summary>
    public GameObject mySpawner;

    // Start is called before the first frame update
    void Start()
    {
        twr = FindObjectOfType<TowerController>();
        gameController = FindObjectOfType<GameController>();
        Reset();
    }

    public void Reset()
    {
        //Set Health to max
        currentHealth = MAX_HEALTH;

        //El sujeto está vivo
        healthState = HealthState.live;
    }


    /// <summary>
    /// Método invocado por Unity
    /// cunado se produce una colisión de una partícula
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {

        TowerController tower = other.GetComponent<TowerController>();

        //Si la prtícula procede de un GUnTower
        if (tower != null)
        {
            //Se descuentan los puntos de salud correspondientes
            currentHealth -= tower.GetComponent<TowerController>().damage;

            //Se limita el valor de salud
            currentHealth = Mathf.Clamp(currentHealth, 0, MAX_HEALTH);

        }
        currentHealth = currentHealth- twr.damage;

        if (currentHealth <= 0)
        {
            gameController.SumarPuntos(enemyPoints, enemyMoney);
            //El sujeto perece
            gameObject.SetActive(false);
            //Informa al spawner que ha muerto
            healthState = HealthState.death;
            print("c murió");
            //mySpawner.SubjectDeath();
        }
    }
   
}