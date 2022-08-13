using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [Header("Health Setup")]
    [SerializeField] float maxHealth = 10;
    [SerializeField] Image healthBar;

    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void ProcessHit(int dmg)
    {

        currentHealth -= dmg;

        if (currentHealth <= 0)
            ProcessDeath();
    }

    void ProcessDeath()
    {
        Destroy(gameObject);
    }
}
