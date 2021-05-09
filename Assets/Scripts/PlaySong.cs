//import statement
using UnityEngine;

/// <summary>
/// plays the theme song of the game
/// </summary>
public class PlaySong : MonoBehaviour
{
    //components present in the inspector. 
    public AudioClip theme; 
    public AudioSource player; 

    /// <summary>
    /// ensures that the song does not restart once next scene is loaded. Song will play through without interruptions. 
    /// </summary>
    private void Awake()
    {

        int objcount = FindObjectsOfType<PlaySong>().Length; //counts number of PlaySong objects
        if (objcount > 1)
        {
            gameObject.SetActive(false); //ensures that two instances of the play song object is not present per scene. 
            Destroy(gameObject); //ensures that the song continues throught all scenes. 
        }
        else
        {
            DontDestroyOnLoad(gameObject); //keeps the same song playing throught the levels
        }
    }//end Awake method. 
}//end PlaySong method. 
