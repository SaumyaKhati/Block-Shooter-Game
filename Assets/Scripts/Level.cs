//import statments
using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// manages blocks per level data and loads next level if player destroys all blocks 
/// </summary>
public class Level : MonoBehaviour
{

    //cache reference: ensures only one call needed to access these game objects in an active scene
    SceneLoader sceneLoader;
    Scene scene;
    Level level;

    //important fields to keep track of block data
    int numBlocks;
    int[] blocksInLevels = { 5, 5, 5, 8, 8, 12, 10, 12, 12, 30}; //stores number of blocks per level

    // Start is called before the first frame update
    private void Start()
    {
        //finding the appropriate objects present in the scene so that their methods can be used for additonal purposes
        level = FindObjectOfType<Level>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        level.GetBlockNum(); 
    }//end Start method

    /// <summary>
    /// handles destruction of block 
    /// </summary>
    public void BlocksDestroyed()
    {
        numBlocks--; //reduces number of blocks left
        if (numBlocks <= 0)
        {
            //if all blocks destroyed, call next scene and get the numbers for that scene
            sceneLoader.NextScene();
            level.GetBlockNum();
        }
    }//end BlocksDestroyed method. 

    /// <summary>
    /// get number of blocks for current level/scene
    /// </summary>
    /// <returns></returns>
    public int GetBlockNum()
    {
        scene = SceneManager.GetActiveScene(); //gettting the name of the current scene 

        //switch statement returns number of blocks from array based on active scene/level
        switch (scene.name)
        {
            case "Level 1":
                return numBlocks = blocksInLevels[0];
                
            case "Level 2": 
                return numBlocks = blocksInLevels[1];

            case "Level 3":
                return numBlocks = blocksInLevels[2];

            case "Level 4":
                return numBlocks = blocksInLevels[3];

            case "Level 5":
                return numBlocks = blocksInLevels[4];

            case "Level 6":
                return numBlocks = blocksInLevels[5];

            case "Level 7":
                return numBlocks = blocksInLevels[6];

            case "Level 8":
                return numBlocks = blocksInLevels[7];

            case "Level 9":
                return numBlocks = blocksInLevels[8];

            case "Level 10":
                return numBlocks = blocksInLevels[9];

            default:
                return 0;
        }
    }//end GetBlockNum method. 
}//end Level class