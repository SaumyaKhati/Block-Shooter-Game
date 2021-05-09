//import statements
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// regulates the behaviour of the player object 
/// </summary>
public class Player : MonoBehaviour
{
    //config params: editable in the Inspector
    [SerializeField] float speed = 10f; //player ship speed
    [SerializeField] float padding = 0.15f; //min. distance from corners of screen, so ship is fully visible
    [SerializeField] GameObject laser = null; //laser object that will be instantiated when firing
    [SerializeField] float laserSpeed = 12f; //speed of laser
    [SerializeField] private float firingPeriod = 0.1f; //time bet. laser fired. 
    [SerializeField] private AudioClip destroyedSound = default;

    //variables to keep track of movement 
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    //cache ref
    GameSession gameSession; //will be used to refer to GameSession in the scene

    //variable to regulate laser firing coroutine 
    Coroutine shootCoroutine;

    /// <summary>
    ///  Start is called before the first frame update.
    ///  Calls SetMoveBoundary method. 
    /// </summary>
    void Start()
    {
        SetMoveBoundary(); //call set move boundary method.
        gameSession = FindObjectOfType<GameSession>(); //finds the object present in the scene. 

    }//end Start method. 


    /// <summary>
    ///  Update is called once per frame.
    ///  Calls Move and Shoot methods. 
    /// </summary>
    void Update()
    {
        //calling move and shoot methods in update since player is constantly shooting and firing. 
        Move();
        Shoot();
    }//end Update method. 

    /// <summary>
    /// Start and stop coroutine when user presses and releases the space bar. 
    /// </summary>
    private void Shoot()
    {
        //"Fire1" is the button name for the space bar. 
        if (Input.GetButtonDown("Fire1"))
        {
            shootCoroutine = StartCoroutine(FireContiunously()); 
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(shootCoroutine); //Once user releases space bar, stop loop. 
        }
    }//end Shoot method. 

    /// <summary>
    /// Continously instantiates laser objects
    /// </summary>
    /// <returns></returns>
    IEnumerator FireContiunously()
    {
        //loop will always run while coroutine is running 
        while (true)
        {
            //Instantiating laser as a gameobject so that getComponent method can be accessed 
            GameObject l = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
            //Assigning velocity to rigidbody compoent of the laser. 
            l.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            GetComponent<AudioSource>().Play();

            //coroutine stops until condition is true, ie. firing period has passed. 
            yield return new WaitForSeconds(firingPeriod); 
        }
    }//end FireContinuosly coroutine. 

    /// <summary>
    /// Controls the player's movement
    /// </summary>
    private void Move()
    {
        //variables store change in position when player presses left/right/up/down buttons 
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //ensures the positions are within the restrictions of the set boundary using Mathf.clamp 
        var updatedXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        var updatedYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        //updates the position of the object to where user moved it 
        transform.position = new Vector2(updatedXPos, updatedYPos);
    }//end Move method. 

    /// <summary>
    /// Sets the restrictions on where player's position can be in game. 
    /// </summary>
    private void SetMoveBoundary()
    {
        //creating camera object. 
        Camera camera = Camera.main;

        //establishing min and max x positions as the corners of the game camera view with slight "padding"
        //so that entire player ship remains within view
        minX = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        //establishing min and max y positions as the bottom and top half of the game camera view with slight "padding"
        //so that entire player ship remains within view
        minY = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = camera.ViewportToWorldPoint(new Vector3(0, 0.45f, 0)).y; 
    }//end SetMoveBoundary method. 

    /// <summary>
    /// destroys player ship if it collides with a falling block 
    /// </summary>
    /// <param name="collision"> detects which object collided with player</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if player object colliders with block, destroy player object. 
        if (collision.gameObject.CompareTag("Block"))
        {
            AudioSource.PlayClipAtPoint(destroyedSound, Camera.main.transform.position, 0.5f); //player ship destroyed sound is played. 
            gameSession.CollisionChange();//calls for GameSession class's collision change method that resets the score. 
            SceneManager.LoadScene("Game Failed"); //calls the try again scene. 
            Destroy(gameObject); //destroys player ship.
        }
    }//end OnCollisionEnter2D method.
}//end Player class.