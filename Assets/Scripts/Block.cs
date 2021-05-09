//import statement
using UnityEngine;

/// <summary>
/// Controls behaviour of block objects. 
/// </summary>
public class Block : MonoBehaviour
{

    //Config param: editable in the Inspector
    [SerializeField] float fallspeed = default; //fall speed of block 
    [SerializeField] AudioClip breakSound = default; //sound played when block is destroyed
    [SerializeField] int health = default; //health, ie. number of hits the block can handle
    [SerializeField] public int dmgDone = default; //dmg to health if block is not destroyed and falls through
    [SerializeField] int pointVal = default; //points added when block is destroyed
    [SerializeField] GameObject destroyedVFX = default; //plays particle effect

    //cache ref: esnures only one call needed to access these objects in an active scene. 
    Level lvl;
    GameSession gameSession; 

    /// <summary>
    /// Finds the appropriate objects in active scene based on variable reference 
    /// </summary>
    private void Start()
    {
        lvl = FindObjectOfType<Level>();
        gameSession = FindObjectOfType<GameSession>(); 
    }//end Start method. 

    /// <summary>
    ///  Update is called once per frame.
    ///  Calls Fall method. 
    /// </summary>
    private void Update()
    {
        Fall(); //ensures block is falling. 
    }//end Update method.   


    /// <summary>
    /// Regulates fall of block objects. 
    /// </summary>
    private void Fall()
    {
        var deltaY = Time.deltaTime * fallspeed; //storing change in y pos. 
        var newYPos = transform.position.y - deltaY; //subtracting since its falling in "negative" direction.
        transform.position = new Vector3(transform.position.x, newYPos); //setting new postion of block. 
    }//end Fall method. 


    /// <summary>
    /// handles hit by laser
    /// </summary>
    private void HandleHit()
    {
        health--; //reduces health by one per laser

        //destroy health is its less than 0
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 0.25f); //play sound
            lvl.BlocksDestroyed(); //reduce total number of blocks by one
            gameSession.AddToScore(pointVal); //add and update score
            TriggerSparkleVFX(); //particle effects activated
            Destroy(gameObject); //destroys block object.
        }
    }//end HandleHit method. 


    /// <summary>
    /// Destroys block upon any sort of collision. 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Laser")){
            HandleHit(); ///handles hit by laser
        }
            
    }//end OnCollisionEnter2D method.


    /// <summary>
    /// triggers particle effect when block is destroyed. 
    /// </summary>
    private void TriggerSparkleVFX()
    {
        //creating sparkle effect right where block being destroyed was located. 
        GameObject sparkle = Instantiate(destroyedVFX, transform.position, transform.rotation);
        Destroy(sparkle, 1f); //you do not want to create numerous clones in hierarchy, it is very inefficient. 
    }//end TriggerSparkleVFX method. 

}//end Block class. 
