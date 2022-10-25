using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    /// <summary>
    /// Vida de la barrera, el daño que le hace un enemigo, cada cuanto le hace ese daño y 
    /// daño que la barrera le hace al enemigo al morir.
    /// </summary>
    public float BarrierHp = 10f;
    public float damage = 2.5f;
    float timeRemaining;
    public float Timer = 2f;
    bool viva = true;
    public float damageEnemy = 20;

    BuildManager buildManager;
    HealthModule Hm;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = FindObjectOfType<BuildManager>();
        Hm = FindObjectOfType<HealthModule>();
        timeRemaining = Timer;    }

    // Update is called once per frame
    void Update()
    {
        if (BarrierHp <= 0)
        {
            Destroy(gameObject);
            Hm.currentHealth = Hm.currentHealth - damageEnemy;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Soldier")
        {
            TimeLeft();
        }
    }
    void TimeLeft()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining <= 0)
        {
            damageOT();
            timeRemaining = Timer;
        }
    }

    void damageOT()
    {
        BarrierHp = BarrierHp - damage;
    }
}
