using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    float coeffEnemiesInRound;
    float coeffEnemyDifficulty;
    float coefficientScore;
    float round = 1;
    float test = 1;
    int enemyNbrsInRound;
    public float spawnDistanceToPlayer;
    public float ringWidth;
    public float ringHeight;
    public GameObject player;
    GameObject[] enemiesToSpawn;
    public GameObject enemiesHolder;
    public GameObject[] enemiesPool;

    void Start()
    {
        /*GameObject childObject = Instantiate(YourObject) as GameObject;
        childObject.transform.parent = parentObject.transform;*/
        player = player.gameObject;
        coeffEnemiesInRound = round;
        //CreateRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire2"))
        {
            EndRound();
        }

        //Debug.Log(test);
        test += 1 / test * 0.75f;
        

    }

    void FirstRound()
    {

    }

    void CreateRound()
    {
        
        /*if (round == 0)
        {
            enemiesToSpawn = new GameObject[1];
            enemiesToSpawn[0] = enemiesPool[0];
            StartRound();
            return;
        } else if (round == 1)
        {
            coeffEnemiesInRound = round;
        }*/


        //coeffEnemiesInRound = round;
        coeffEnemiesInRound += 1 / coeffEnemiesInRound * 0.75f;
        //Debug.Log(coeffEnemiesInRound);
        coeffEnemyDifficulty = coeffEnemiesInRound % 1;
        enemyNbrsInRound = Mathf.FloorToInt(coeffEnemiesInRound);
        //Debug.Log("int: " + enemyNbrsInRound);
        //Debug.Log("enemieNbr: " + enemyNbrsInRound + "  coeffDifficulty: " + coeffEnemyDifficulty);
        enemiesToSpawn = new GameObject[enemyNbrsInRound];


        for (int i = 0; i < enemyNbrsInRound; i++)
        {
            int numPool = Random.Range(1, 3);
            //Debug.Log("numPool: " + (numPool));
            float rand = Random.value - coeffEnemyDifficulty / numPool;
            //Debug.Log("randomFinal: " + rand);
            if (rand <= 0)
            {
                enemiesToSpawn[i] = enemiesPool[numPool];
                //Debug.Log("got");
            }
            else
            {
                enemiesToSpawn[i] = enemiesPool[numPool - 1];
                //Debug.Log("NOTgot");
            }

            //Debug.Log("Enemy num " + enemiesToSpawn[i]);
        }

        StartRound();
        
        
    }

    void StartRound()
    {
        //instantiate ennemies
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            GameObject enemy;

            Vector3 spawnCoords = spawnRand();            
            enemy = Instantiate(enemiesToSpawn[i]) as GameObject;
            enemy.transform.position = spawnCoords;
            //Debug.Log(spawnCoords);
            enemy.transform.parent = enemiesHolder.transform;
            enemy.SetActive(true);
        }
    }

    Vector3 spawnRand()
    {
        Vector3 spawn = new Vector3(Random.Range(-(ringWidth), ringWidth) , 0, Random.Range(-(ringHeight ), ringHeight));

        float distance = Vector3.Distance(spawn, player.transform.position);

        if (distance < spawnDistanceToPlayer)
        {
            spawnRand();
        }

        return spawn;



    }

    void Round()
    {

    }

    void EndRound()
    {
        //Debug.Log(enemiesHolder.transform.childCount);
        int t = enemiesHolder.transform.childCount;
        for (int i = 0; i < t; i++)
        {
            Destroy(enemiesHolder.transform.GetChild(i).gameObject);
            //Debug.Log(enemiesHolder.transform.childCount);
        }
        round++;
        CreateRound();
    }

}
