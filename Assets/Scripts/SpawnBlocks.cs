//import statements
using UnityEngine;

/// <summary>
/// class spawnblocks instantiates a set number of block objects at the top of the screen at repeated intervals
/// </summary>
public class SpawnBlocks : MonoBehaviour
{
    //config parameters to be manipulated in the inspector
    [SerializeField] float delay = 1f; //time between blocks instantiated. 
    [SerializeField] Block[] blockArray = default; //stores all versions of blocks needed per level

    //cache ref: ensures only one call needed to access object in active scene the variables refer to
    private Level level; //Level object to refer to the one in a particular scene.a

    //important fields to keep track of block data
    private int blockNum; //num of blocks to be produced per level
    private int blockChoice = 0; //alternating between different version of blocks from the blockArray
    private float timer = 0f; //needed to keep track for block spawn rate


    /// <summary>
    ///  refers variable to level object and blockNum value in the scene. 
    /// </summary>
    public void Start()
    {
        level = FindObjectOfType<Level>(); //find level object in active scene
        blockNum = level.GetBlockNum(); //find num of blocks for instantiation in the active scene/level
    }//end Start method. 

    /// <summary>
    ///  Update is called once per frame.
    ///  Method spawns blocks at the top of the screen. 
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime; //keeps track of time passed from last frame loaded. 

            // if time between frames passed is greater than delay, and block num has not reached limit, then instantiates a block
            if (timer > delay && blockNum > 0)
            {
                //reset the choice at the beginning of the array so that choice is always valid. 
                if(blockChoice >= blockArray.Length)
                {
                    blockChoice = 0;
                }

                //instantiates a block within a range of x val which essentially cover the game screen. 
                Instantiate(blockArray[blockChoice], new Vector3(Random.Range(2.25f, 13.75f), 12), Quaternion.identity);
                timer -= delay; //resets timer
                blockNum--; //reducing total number of blocks since one was just created.
                blockChoice++; //allows for next block to be a different type if possible based on the block array. 
            }
    }//end Update method. 
}//end SpawnBlocks class. 