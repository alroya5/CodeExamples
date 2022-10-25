using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    //prefab enemigo
    public Transform soldierPrefab;
    //localizaci�n del spawn
    public Transform spawnLocation;

    //tiempo de espera entre oleadas
    public float waveTimer=5f;
    private float countDown=2f;

    private int waveIndex=1;

    private void Update()
    {
        if (countDown <= 0f)
        {
            SpawnWave();
            countDown = waveTimer;
        }

        countDown -= Time.deltaTime;
       
    }

    /// <summary>
    /// m�todo que inicia la oleada y llama a spawnEnemy
    /// </summary>
    private void SpawnWave()
    {


        for (int i = 0; i < waveIndex; i++)
        {
            spawnEnemy();
            waveIndex = 0;
        }   
        waveIndex++;

    }

    /// <summary>
    /// instancia un enemigo en la posici�n y rotaci�n del spawn
    /// </summary>
    private void spawnEnemy()
    {
        Instantiate(soldierPrefab,spawnLocation.position,spawnLocation.rotation);
    }

}
