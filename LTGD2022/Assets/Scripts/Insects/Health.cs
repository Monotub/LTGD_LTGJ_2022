using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;


public class Health : MonoBehaviour
{
    [Header("Health Setup")]
    [SerializeField] Image healthBar;
    [SerializeField] float maxHealth = 10;
    [SerializeField] float defense = 0;
    [SerializeField] GameObject defenseVFX;

    public bool isDead { get; private set; }
    public float Defense => defense;
    public static event Action<InsectStatsSO> InsectDied;

    float currentHealth;
    float origDef;



    private void Awake()
    {
        currentHealth = maxHealth;
        origDef = defense;
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        MonitorDefense();
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
        {
            isDead = true;
            ProcessDeath();
        }
    }

    void ProcessDeath()
    {
        Insect insect = gameObject.GetComponent<Insect>();
        insect.OnDeath();
        InsectDied?.Invoke(insect.Stats);
        Destroy(gameObject, 2f);
    }

    public void ActivateDefenseBuff(float amt, float duration)
    {
        StartCoroutine(ProcessDefenseBuff(amt, duration));
    }

    IEnumerator ProcessDefenseBuff(float amt, float dur)
    {
        defense += amt;
        yield return new WaitForSeconds(dur);
        defense = origDef;
    }

    void MonitorDefense()
    {
        if(defense > origDef)
            defenseVFX.SetActive(true);
        else
            defenseVFX.SetActive(false);
    }
}
