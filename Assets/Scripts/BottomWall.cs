//import statements
using UnityEngine;

/// <summary>
/// Handles lose condition when player fails to destroy block 
/// </summary>
public class BottomWall : MonoBehaviour
{
    //cached ref: esnures only one call needed to access these objects in an active scene
    Level lvl;
    GameSession gameSession; 

    /// <summary>
    /// Finding the objects these variables refer in the active scene
    /// </summary>
    public void Start()
    {
        lvl = FindObjectOfType<Level>();
        gameSession = FindObjectOfType<GameSession>(); 
    }//end Start method. 

    /// <summary>
    /// Changes health according to the dmg val of the block that went through the bottom of the screen 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if object colliding is a block, destroy it. 
        if (collision.gameObject.CompareTag("Block"))
        {
            //change health based on dmg val of block, reduce number of blocks, destroy the block colliding 
            gameSession.ChangeHealth(collision.gameObject.GetComponent<Block>().dmgDone);
            Destroy(collision.gameObject);
            lvl.BlocksDestroyed();
        }
            
    }//end OnTriggerEnter2D method. 
}//end BottomWall class. 
