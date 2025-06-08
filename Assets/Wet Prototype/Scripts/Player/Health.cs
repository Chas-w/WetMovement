using UnityEngine;

public class Health : MonoBehaviour
{
    public float healthMax;
    [SerializeField] float currentHealth; //unserialize after UI added
    bool dead;

    private void Start()
    {
        currentHealth = healthMax; 
    }

    public void Injure(float injureAmount)
    {
        currentHealth -= injureAmount * Time.deltaTime;
    }

    public void Death()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            dead = true; 
        }
    }
}
