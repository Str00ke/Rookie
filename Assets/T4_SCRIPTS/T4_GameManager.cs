using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class T4_GameManager : MonoBehaviour
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
    public GameObject stuff;
    public GameObject loosePanel;
    public GameObject winPanel;
    public GameObject lifeContainer;
    public GameObject charaContainer;
    GameObject UI;
    public GameObject PausePanel;
    public GameObject loseScore;
    public GameObject winScore;
    Text scoreTxt;


    public GameObject wavesHolder;
    int wave = 0;
    int score = 0;

    public bool isPaused;

    private void Awake()
    {
        UI = GameObject.Find("_UI");
        //PausePanel = UI.gameObject.transform.GetChild(0).gameObject;
    }

    void Start()
    {
        PausePanel.SetActive(false);
        /*GameObject childObject = Instantiate(YourObject) as GameObject;
        childObject.transform.parent = parentObject.transform;*/
        player = player.gameObject;
        coeffEnemiesInRound = round;
        //CreateRound();
        CreateWave();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonUp("Fire2"))
        {
            EndRound();
        }*/

        //Debug.Log(test);
        test += 1 / test * 0.75f;
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isPaused = !isPaused;

            Pause(isPaused);
        }

        
        if (wavesHolder.transform.GetChild(wave).gameObject.transform.childCount == 0)
        {
            EndWave();
        }


    }

    void Pause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        } else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
        }
    }


    public void AddScore(int value)
    {
        score += value * (wave + 1);
        Debug.Log(score);
    }

    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CreateWave()
    {
       Debug.Log(wave);
       GameObject waveToCreate = wavesHolder.transform.GetChild(wave).gameObject;
       waveToCreate.SetActive(true);
    }


    void EndWave()
    {
        if (wave >= 4)
        {
            Win();
        } else
        {
            wave++;
            CreateWave();
        }
    }

    void Win()
    {
        winScore.GetComponent<Text>().text = score.ToString();
        stuff.SetActive(false);
        lifeContainer.SetActive(false);
        charaContainer.SetActive(false);
        winPanel.SetActive(true);
    }

    public void Lose() 
    {
        loseScore.GetComponent<Text>().text = score.ToString();
        stuff.SetActive(false);
        lifeContainer.SetActive(false);
        charaContainer.SetActive(false);
        loosePanel.SetActive(true);
    }

    /*void FirstRound()
    {

    }

    void CreateRound()
    {
        
        ///*if (round == 0)
        {
            enemiesToSpawn = new GameObject[1];
            enemiesToSpawn[0] = enemiesPool[0];
            StartRound();
            return;
        } else if (round == 1)
        {
            coeffEnemiesInRound = round;
        //}


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
    }*/

}
