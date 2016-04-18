using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour 
{

    public GameObject enemySatyr;
    public GameObject enemyGorgon;
    public GameObject enemyPegasus;
    public GameObject enemyMermaid;
    public GameObject enemyHydra;
    public GameObject enemyWerewolf;
    public GameObject enemyVampire;
    public GameObject enemyCyclops;
    public GameObject enemyBanshee;
    public GameObject enemyMinotaur;
    public GameObject enemyTroll;
    public GameObject enemyDragon;
    List<GameObject> enemyList;
    GameObject enemyInstance;
    public GameObject levelCompleteScreen;
    UnityEngine.UI.Image levelTimerRender;
    public int spawnRange;
    public int currentLevel;
    float waveTimer;
    float spawnTimer;
    float levelTimer;
    //public EnemyUpdate target;
    float wave;
    float initialTimerWidth;
    bool tutorial2IsSpawned;
    Queue spawnList = new Queue();
	// Use this for initialization
	void Start () 
        {
        enemyList = new List<GameObject>();
        enemyList.Add(enemySatyr);
        enemyList.Add(enemyGorgon);
        enemyList.Add(enemyPegasus);
        enemyList.Add(enemyMermaid);
        enemyList.Add(enemyHydra);
        enemyList.Add(enemyWerewolf);
        enemyList.Add(enemyVampire);
        enemyList.Add(enemyCyclops);
        enemyList.Add(enemyBanshee);
        enemyList.Add(enemyMinotaur);
        enemyList.Add(enemyTroll);
        enemyList.Add(enemyDragon);
        Time.timeScale = 1;
        waveTimer=0;
        spawnTimer = 0;
        levelTimer = 0;
        levelTimerRender = GameObject.FindGameObjectWithTag("WaveTimerRender").GetComponent<UnityEngine.UI.Image>();
        //initialTimerWidth = levelTimerRender.rect.width;
        tutorial2IsSpawned = false;
        //Time.timeScale = 5;
         //EnemyInstance = Instantiate(Enemy, new Vector3(-5, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        if (GlobalDataScript.globalData.tutorialState == 3)
        {
            wave = 1;
            AddToSpawn(wave);
        }
        else
        {
            enemyInstance = Instantiate(enemySatyr, new Vector3(-3, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
            enemyInstance = Instantiate(enemySatyr, new Vector3(-3.5f, -1), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
            enemyInstance = Instantiate(enemySatyr, new Vector3(-3.5f, -2), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
        }
	
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if(Input.GetKeyDown(KeyCode.F))
        //{
        //    wave = wave + 10;
        //}
        if (GlobalDataScript.globalData.tutorialState == 3)
        {
            levelTimer = levelTimer + Time.deltaTime;

            levelTimerRender.fillAmount = 1-(levelTimer / 112f); //(-initialTimerWidth + (initialTimerWidth*(112f-levelTimer))/112f);
            if (levelTimer >= 112)
            {
                GlobalDataScript.globalData.ResetBuffs();
                GlobalDataScript.globalData.completedLevels[currentLevel - 1] = true;
                levelCompleteScreen.SetActive(true);
                levelCompleteScreen.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Level Complete!";
                Time.timeScale = 0;
                //Application.LoadLevel("Overworld Map");
            }
            waveTimer = waveTimer + Time.deltaTime;
            spawnTimer = spawnTimer + Time.deltaTime;
            if (waveTimer >= Random.value * 2 + 6)
            {
                waveTimer = 0;
                //EnemyInstance = Instantiate(Enemy, new Vector3(-5, Random.value*-2), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                AddToSpawn(wave);
                switch (currentLevel)
                {
                    case 1:
                        wave = Mathf.Pow(levelTimer, 1.1f) /30 + 1;
                        break;
                    case 2:
                        wave = Mathf.Pow(levelTimer, 1.2f) + 1;
                        break;
                    case 3:
                        wave = Mathf.Pow(levelTimer, 1.3f) + 1;
                        break;
                    case 4:
                        wave = Mathf.Pow(levelTimer, 1.4f) + 1;
                        break;
                    case 5:
                        wave = Mathf.Pow(levelTimer, 1.5f) + 1;
                        break;
                    case 6:
                        wave = Mathf.Pow(levelTimer, 1.6f) + 1;
                        break;
                    case 7:
                        wave = Mathf.Pow(levelTimer, 1.7f) + 1;
                        break;
                    case 8:
                        wave = Mathf.Pow(levelTimer, 1.8f) + 1;
                        break;
                    case 9:
                        wave = Mathf.Pow(levelTimer, 1.9f) + 1;
                        break;
                    case 10:
                        wave = Mathf.Pow(levelTimer, 2f) + 1;
                        break;
                }
                
            }
            if (spawnTimer >= Random.value + .1)
            {
                spawnTimer = 0;
                Spawn();
            }
        }
        else if(GlobalDataScript.globalData.tutorialState == 1)
        {
            if(GameObject.FindWithTag("Enemy")==null)
            {
                GlobalDataScript.globalData.tutorialState = 2;
            }
        }
        else if(GlobalDataScript.globalData.tutorialState == 2)
        {
            if (!tutorial2IsSpawned)
            {
                enemyInstance = Instantiate(enemySatyr, new Vector3(-2, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-2.5f, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-3, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-3.5f, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-4, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-1.5f, -1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-2, -.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-2.5f, -.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-3, -.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-3.5f, -.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-4, -.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                enemyInstance = Instantiate(enemySatyr, new Vector3(-1.5f, -.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<Rigidbody2D>().drag = 1000;
                tutorial2IsSpawned = true;
            }
            if (GameObject.FindWithTag("Enemy") == null)
            {
                GlobalDataScript.globalData.tutorialState = 3;
            }
        }
	}

    void AddToSpawn(float waveStrength)
    {
        int spawnedStrength = 0;        
        float spawnRNG;
        while(spawnedStrength < waveStrength)
        {
            spawnRNG = Random.Range(0,spawnRange+1);
            if(spawnRNG>=spawnRange)
            {
                spawnList.Enqueue(spawnRange-1);
                spawnedStrength += enemyList[spawnRange - 1].GetComponent<EnemyUpdate>().threatValue;

            }
            else
            {
                
                spawnList.Enqueue((int)Mathf.Floor(spawnRNG));
                spawnedStrength = spawnedStrength + enemyList[(int)Mathf.Floor(spawnRNG)].GetComponent<EnemyUpdate>().threatValue;
                //Debug.Log(spawnedStrength);
            }
            //else if (spawnRNG < 2)
            //{

            //    spawnList.Enqueue(1);
            //    spawnedStrength = spawnedStrength + enemyList[1].GetComponent<EnemyUpdate>().threatValue;
            //    //Debug.Log(spawnedStrength);
            //}
            //else if (spawnRNG < 3)
            //{

            //    spawnList.Enqueue(2);
            //    spawnedStrength = spawnedStrength + enemyList[2].GetComponent<EnemyUpdate>().threatValue;
            //    //Debug.Log(spawnedStrength);
            //}
        }
    }
    void Spawn()
    {
        if (spawnList.Count > 0)
        {
            int enemyType = (int)spawnList.Dequeue();
            if (enemyList[enemyType].GetComponent<EnemyUpdate>().isFlier)
            {
                enemyInstance = Instantiate(enemyList[enemyType], new Vector3(-5, Random.value * -1.8f + 1.5f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<EnemyUpdate>().levelCompleteScreen = levelCompleteScreen;
            }
            else
            {
                enemyInstance = Instantiate(enemyList[enemyType], new Vector3(-5, Random.value * -1.8f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<EnemyUpdate>().levelCompleteScreen = levelCompleteScreen;
            }
            //if (enemyType == 1)
            //{
                
            //}
            //if (enemyType == 2)
            //{
            //    enemyInstance = Instantiate(enemyGorgon, new Vector3(-5, Random.value * -2), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            //    enemyInstance.GetComponent<EnemyUpdate>().levelCompleteScreen = levelCompleteScreen;
            //}
            //if (enemyType == 3)
            //{
            //    
            //}
        }
    }

}

