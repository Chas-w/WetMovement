using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class OxygenPostProcessManager : MonoBehaviour
{
    public SwimOxygen swimOxygen;
    [SerializeField] Volume m_Volume;
    [SerializeField] Vignette vignette;


    public bool overrideVignette;

    [SerializeField] float maxOxy;
    [SerializeField] float currentOxy;

    void Start()
    {
        //swim oxygen values
        maxOxy = swimOxygen.OxygenMax;
        currentOxy = swimOxygen.currentOxygen;

        //find swimOxygen script
        if (swimOxygen == null)
        {
            swimOxygen = GetComponent<SwimOxygen>();
        }

        if (vignette == null)
        {
            // Get the Vignette component from the Volume
            if (m_Volume.profile.TryGet<Vignette>(out vignette))
            {
                vignette = m_Volume.profile.Add<Vignette>(false);
            }
            else
            {
                Debug.LogError("Vignette component not found in the Volume profile.");
            }
        }
        
        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0.34f;                

    }

    void Update()
    {
        vignette.intensity.overrideState = true;

        // Update currentOxy from swimOxygen
        currentOxy = swimOxygen.currentOxygen;

        float vignetteLevel = currentOxy / maxOxy;
        //update vignette value with the vignette level
        if (vignette.intensity.value <= 0.34f)
        {
            vignette.intensity.value = 0.34f;
        }
        else if (vignetteLevel >= 0.34f)
        {
            vignette.intensity.value = 1 - (vignetteLevel + .34f);
        }
    }
}
