using UnityEngine;

public class SwimOxygen : MonoBehaviour
{
    [Header("oxygen data")]
    public float OxygenMax;
    [SerializeField] float currentOxygen; //unserialize after UI is functional
    public float oxygenLost = 2;
    [SerializeField] float oxygenInjureAmount = 10; //amount health goes down if no O2


    [Header("breathing data")]
    [SerializeField] float addedBreatheTime = 6; 
    [SerializeField] float breatheTimerMax; //acceptable time in between taking breaths
    [SerializeField] float breathInjureAmount = 2; //amount health goes down if not breathing
    [SerializeField] float breatheTimer; //unserialize after UI is functional

    bool resetBreath; 

    [Header("Health Data")]
    public Health health; 

    private void Start()
    {
        breatheTimer = breatheTimerMax;
        currentOxygen = OxygenMax; 
    }

    public void PlayerBreathe(bool breathing) //room to add systems like needing to breathe more when sprinting or performing intensive actions
    {

        if (breatheTimer > 0 )
        {
            resetBreath = false;
            if (breatheTimer >= breatheTimerMax)
            {
                breatheTimer = breatheTimerMax;
            } //cap the breath timer to breath timer max, clamp isn't working for some reason

            breatheTimer -=Time.deltaTime;

            if (breathing)
            {
                breatheTimer += addedBreatheTime;
                currentOxygen -= oxygenLost; 
            }
        }  else if (breatheTimer <= 0)
        {
            if (breathing)
            {
                breatheTimer += addedBreatheTime/2;
            }
            health.Injure(breathInjureAmount);
            if (!resetBreath)
            {
                breatheTimer = 0;
                resetBreath = true;
            }
        }



        if (currentOxygen <= 0) 
        {
            health.Injure(oxygenInjureAmount);
        }
    }



}
