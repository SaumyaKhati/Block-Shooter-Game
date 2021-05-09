//import statements
using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// loads various scenes using SceneManager and object methods
/// </summary>
public class SceneLoader : MonoBehaviour
{
    //cache ref: ensures only one call needed to access game object in active scene 
    GameSession gameSession; 

    /// <summary>
    /// refer GameSession object present in the scene using FindObjectOfType method 
    /// </summary>
    public void Start()
    {
        gameSession = FindObjectOfType<GameSession>(); //refers GameSession object in the scene
    }//end Start method. 

    /// <summary>
    /// loads next scene based on project build settings and index
    /// </summary>
    public void NextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex; //get current active scene's index
        SceneManager.LoadScene(currentIndex + 1); //load next scene
    }//end NextScene method. 

    /// <summary>
    /// loads scene based on build value
    /// </summary>
    /// <param name="v"> provides build scene index</param>
    public void LoadScene(int v)
    {
        SceneManager.LoadScene(v); //loads scene that has the passed build index value
    }//end LoadScene method. 

    /// <summary>
    /// restarts game, ie. loads main menu scene. 
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0); //load manin menu
        gameSession.ResetScore(); //reset game session therefore, destroy score + healthbar objects 
    }//end Restart method. 

    /// <summary>
    /// Quit the application when in full play mode 
    /// </summary>
    public void Quit()
    {
        Application.Quit(); 
    }//end Quit method. 

    /// <summary>
    /// loads instructions page
    /// </summary>
    public void LoadInstructions()
    {
        SceneManager.LoadScene(13); //load instructions page using load scene method
    }//end LoadInstructions method. 

    /// <summary>
    /// loads first level of the game 
    /// </summary>
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1); //loads first level using SceneManager object's method. 
    }//end LoadFirstLevel method. 
}//end SceneLoader class. 
