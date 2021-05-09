//import statements
using UnityEngine;
using UnityEngine.UI; 

/// <summary>
/// controls behaviour of the health bar present in the levels
/// </summary>
public class HealthBar : MonoBehaviour
{

    //objects to refer to components of the health bar object
    public Slider slider;
    public Gradient gradient;
    public Image fill; 

    /// <summary>
    /// set max health of the health bar (100) 
    /// </summary>
    /// <param name="health">max possible for the health bar</param>
    public void SetMax(int health)
    {
        //setting max value of the health bar both numerically via slider and the actual image itself
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f); 
    }//end SetMax method. 

    /// <summary>
    /// sets current in the health bar with the slider and fill image
    /// </summary>
    /// <param name="heatlh">represents current health</param>
    public void SetHealth(int heatlh)
    {
        //equating current health with the heatlh bar so that it is updated appropriately
        slider.value = heatlh;
        fill.color = gradient.Evaluate(slider.normalizedValue); 
    }//end SetHealth method. 
}//end HealthBar class. 
