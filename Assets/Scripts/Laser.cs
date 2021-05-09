//import statements
using UnityEngine;

/// <summary>
/// Controls laser object's behaviour when interacting with block objects
/// </summary>
public class Laser : MonoBehaviour
{
    /// <summary>
    /// Controls behaviour of laser object when colliding with another object (ie. Block)
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Shredder"))
        {
            //destroys laser object upon collision. 
            Destroy(gameObject);
        }    
    }//end OnCollisionEnter2D method.
}//end Laser class. 
