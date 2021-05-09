//import statements
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 


/// <summary>
/// Ensures continuity of score val and health val throughout all levels
/// </summary>
public class GameSession : MonoBehaviour
{
    //serializeable fields: editable in the inspector
    [SerializeField] int currentScore = 0; //keep track of current score
    [SerializeField] TextMeshProUGUI scoreText = default; //used to display the score 
    [SerializeField] HealthBar healthBar = default; //used to refer healthbar in the scene

    //cache ref: only needs one call to access the object rather than using update
    SceneLoader sceneLoader;

    //variables to keep track of player health    
    int maxHealth = 100;
    int currentHealth;

    /// <summary>
    /// ensures continuity of one instance by destroying new instance in the next scene
    /// </summary>
    private void Awake()
    {
        //counts how many instances are active
        int gameSessionObjCount = FindObjectsOfType<GameSession>().Length;
        if (gameSessionObjCount > 1)
        {
            //destroy the new instance so that score and health are saved after new scene is loaded. 
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            //destroy health bar on the "win" scene. This was plainly a matter of preference. I did not want the health bar there on the "You won" scene.
            if (SceneManager.GetActiveScene().buildIndex == 11)
            {
                Destroy(healthBar); 
            }
        }
    }//end Awake method. 

    /// <summary>
    /// sets health to max and finds SceneLoader object present in the scene
    /// </summary>
    private void Start()
    {
        currentScore = 0; 
        scoreText.text = currentScore.ToString();
        currentHealth = maxHealth;
        healthBar.SetMax(currentHealth);
        sceneLoader = FindObjectOfType<SceneLoader>(); 
    }//end Start method. 


    /// <summary>
    /// updates score by adding points via passed value
    /// </summary>
    /// <param name="val">points being added to score</param>
    public void AddToScore(int val)
    {
        currentScore += val;
        scoreText.text = currentScore.ToString(); //updates text component so that new score can be seen
    }//end AddToScore method. 


    /// <summary>
    /// Ensures that when player hits retry, old scores/health are all destroyed.
    /// </summary>
    public void ResetScore()
    {
        Destroy(gameObject); 
    }//end ResetScore method. 


    /// <summary>
    /// changes health according to the dmg val passed in the function
    /// </summary>
    /// <param name="dmg">val being subtracted from current health</param>
    public void ChangeHealth(int dmg)
    {
        currentHealth -= dmg;

        //if health is 0, load "try again" scene. 
        if(currentHealth <= 0)
        {
            healthBar.SetHealth(currentHealth);
            sceneLoader.LoadScene(12); 
        }
        else
        {
            healthBar.SetHealth(currentHealth);
        }
        
    }//end ChangeHealth method. 


    /// <summary>
    /// if player collides, set health to 0. 
    /// </summary>
    public void CollisionChange()
    {
        healthBar.SetHealth(0);
    }//end CollisionChange method.

}//end GameSession method. 

