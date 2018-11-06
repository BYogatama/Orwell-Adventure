using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _proceduralGeneration : MonoBehaviour {

    //Level Chuck
    //EntryLevel & EndLevel Always Same
    //MidLevel Randomly Chosen from Array
    [Header("Level Pattern")]
    public GameObject[] entryLevel;
    public GameObject[] midLevel;
    public GameObject[] endLevel;

    /*Different Every Area
    [Header("Decoration")]
    public GameObject[] grassDecor;
    public GameObject[] treeDecor;
    */

    [Header("Level Length")]
    public int minLevel;
    public int maxLevel; //maxLevel should not > midLevel Length

    private GameObject[] groundTop;
    private GameObject Environment;

    private int levelLength;
    private int[] seedLevel;
    private int chunkLength;

    private bool EntryIsReady;
    private bool LevelIsReady;

    private void Start()
    {
        CreateSeed();
        GenerateLevel();
        GenerateTerrain();
        GenerateEnemy();
    }

    void CreateSeed()
    {
        //Randomize Level Length
        /*if (maxLevel > midLevel.Length)
        {
            maxLevel = midLevel.Length;
            if (minLevel > maxLevel)
            {
                minLevel = midLevel.Length-5;
            }
        }*/
        levelLength = Random.Range(minLevel, maxLevel);


        seedLevel = new int[levelLength - 2];
        chunkLength = midLevel.Length-1;

        //List<int> usedChunk = new List<int>();
        int indexLevel = seedLevel.Length - 1;
        for (int i = 0; i <= indexLevel; i++)
        {
            seedLevel[i] = Random.Range(0, chunkLength);
            //usedChunk.Add(seedLevel[i]);
            if (i != 0)
            {
                while(seedLevel[i] == seedLevel[i - 1])
                {
                    seedLevel[i] = Random.Range(0, chunkLength);
                }

                /*while (usedChunk.Contains(seedLevel[i]))
                {
                    seedLevel[i] = Random.Range(0, chunkLength);
                }
                usedChunk.Add(seedLevel[i]);*/
            }
        }
    }

    void GenerateLevel()
    {

        //Check if Entry Level Is Ready/Created
        if (!EntryIsReady)
        {
            //Create Entry Level
            int entryIndex = Random.Range(0, entryLevel.Length);
            Instantiate(entryLevel[entryIndex], transform.position, Quaternion.identity, transform);

            //Create End Level
            int endIndex = Random.Range(0, entryLevel.Length);
            Vector3 position = new Vector3(transform.position.x + ((seedLevel.Length + 1) * 5), transform.position.y, transform.position.z);
            Instantiate(endLevel[endIndex], position, Quaternion.identity, transform);

            EntryIsReady = true;
        }

        //If Entry Level Created, Create Mid Level
        if (EntryIsReady && !LevelIsReady)
        {

            for (int i = 0; i <= seedLevel.Length; i++)
            {
                if (i != seedLevel.Length)
                {
                    Vector3 position = new Vector3(transform.position.x + ((i + 1) * 5), transform.position.y, transform.position.z);
                    Instantiate(midLevel[seedLevel[i]], position, Quaternion.identity, transform);
                }
            }
            LevelIsReady = true;
        }
    }

    void GenerateTerrain()
    {
        if (LevelIsReady)
        {
            Environment = new GameObject("Environment");
            Environment.transform.SetParent(transform);
            groundTop = GameObject.FindGameObjectsWithTag("Ground Top");

            /*for (int i = 0; i < groundTop.Length; i++)
            {
                int index = Random.Range(0, grassDecor.Length);
                Vector3 postition = new Vector3(groundTop[i].transform.position.x, groundTop[i].transform.position.y + 1, groundTop[i].transform.position.z);
                Instantiate(grassDecor[index], postition, Quaternion.identity, Environment.transform);

            }

            int Rand = Random.Range(3, 10);
            for (int i = 0; i < groundTop.Length; i += Rand)
            {
                int index = Random.Range(0, grassDecor.Length);
                Vector3 postition = new Vector3(groundTop[i].transform.position.x, groundTop[i].transform.position.y + 1, groundTop[i].transform.position.z);
                Instantiate(treeDecor[index], postition, Quaternion.identity, Environment.transform);
            }*/
        }
    }

    void GenerateEnemy()
    {
        throw new System.NotImplementedException();
    }
}
