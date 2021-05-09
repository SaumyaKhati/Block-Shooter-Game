//import statements
using UnityEngine;

/// <summary>
/// destroys laser objects after they go past camera view 
/// </summary>
public class Shredder : MonoBehaviour
{
    /// <summary>
    /// triggers destruction of laser object when it passes through the shredder 
    /// </summary>
    /// <param name="collision"> keep track of what object collides with shredder </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if colliding object is a laser, then destroy the laser game object. 
        if(collision.gameObject.CompareTag("Laser"))
        {
            Destroy(collision.gameObject); //destroys laser at the top of the screen so that numerous objects do not overload scene. 
        }
    }//end OnTriggerEnter2D method. 
}//end Shredder class. 
