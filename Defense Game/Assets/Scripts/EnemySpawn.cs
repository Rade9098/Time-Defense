using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour 
{

    public GameObject enemySatyr;
    public GameObject enemyHyrda;
    public GameObject enemyPegasus;
    GameObject enemyInstance;
    public GameObject levelCompleteScreen;
    UnityEngine.UI.Image levelTimerRender;
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
                wave = Mathf.Pow(levelTimer, 1.1f) / 30 + 1;
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
            spawnRNG = Random.Range(0,3);
            if(spawnRNG<1)
            {
                
                spawnList.Enqueue(1);
                spawnedStrength = spawnedStrength + enemySatyr.GetComponent<EnemyUpdate>().threatValue;
                //Debug.Log(spawnedStrength);
            }
            else if (spawnRNG < 2)
            {

                spawnList.Enqueue(2);
                spawnedStrength = spawnedStrength + enemyHyrda.GetComponent<EnemyUpdate>().threatValue;
                //Debug.Log(spawnedStrength);
            }
            else if (spawnRNG < 3)
            {

                spawnList.Enqueue(3);
                spawnedStrength = spawnedStrength + enemyPegasus.GetComponent<EnemyUpdate>().threatValue;
                //Debug.Log(spawnedStrength);
            }
        }
    }
    void Spawn()
    {
        if (spawnList.Count > 0)
        {
            int enemyType = (int)spawnList.Dequeue();
            if (enemyType == 1)
            {
                enemyInstance = Instantiate(enemySatyr, new Vector3(-5, Random.value * -2), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<EnemyUpdate>().levelCompleteScreen = levelCompleteScreen;
            }
            if (enemyType == 2)
            {
                enemyInstance = Instantiate(enemyHyrda, new Vector3(-5, Random.value * -2), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<EnemyUpdate>().levelCompleteScreen = levelCompleteScreen;
            }
            if (enemyType == 3)
            {
                enemyInstance = Instantiate(enemyPegasus, new Vector3(-5, Random.value * -2 +1.3f), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                enemyInstance.GetComponent<EnemyUpdate>().levelCompleteScreen = levelCompleteScreen;
            }
        }
    }

}

