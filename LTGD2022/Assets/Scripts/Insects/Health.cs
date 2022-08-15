using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [Header("Health Setup")]
    [SerializeField] Image healthBar;
    [SerializeField] float maxHealth = 10;
    [SerializeField] float defense = 0;

    public float Defense => defense;
    float currentHealth;
    float defaultDefense;

    private void Awake()
    {
        defaultDefense = defense;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void ProcessHeal(float amt)
    {
        currentHealth += amt;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void ProcessHit(int dmg)
    {
        float finalDmg = dmg - defense;
        if(finalDmg < 1) finalDmg = 0;
        currentHealth -= finalDmg;

        if (currentHealth <= 0)
            ProcessDeath();
    }

    void ProcessDeath()
    {
        Destroy(gameObject);
    }

    public void ActivateDefenseBuff(float amt, float duration) => StartCoroutine(ProcessDefenseBuff(amt, duration));

    IEnumerator ProcessDefenseBuff(float amt, float dur)
    {
        var origDef = defense;
        defense += amt;
        yield return new WaitForSeconds(dur);
        defense = origDef;
    }
}
