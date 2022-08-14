using UnityEngine;


[SelectionBase]
public class Moth : Insect
{
    [Header("Moth Setup")]
    [SerializeField] ParticleSystem healParticles;
    [SerializeField] float healDelay = 1f;
    [SerializeField] float healAmount = 2f;

    float timer = 0f;


    private void Update()
    {
        HealAbility();
    }

    void HealAbility()
    {
        timer += Time.deltaTime;
        if(timer >= healDelay)
        {
            var insects = FindObjectsOfType<Health>();
            foreach (var insect in insects)
            {
                if(Vector3.Distance(transform.position, insect.transform.position) <= auraRadius)
                {
                    healParticles.Play();
                    insect.ProcessHeal(healAmount);
                }
            }
            timer = 0f;
        }
    }

    
}
