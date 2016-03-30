using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    // Use this for initialization
    //public AudioClip startSound;
    //public AudioClip hoverSound;
    //public AudioClip smallTick;
    //public AudioClip largeTick;
    //public AudioClip backSound;
    public AudioClip menuMusic;
    public AudioClip levelMusic;
    public GameObject oneShotPrefab;
    public static SoundManager singleton;
    //bool newLoadedLevel;
    GameObject prefabInstance;
    AudioSource loopSource;
    AudioSource persistentSource;

    void Awake()
    {
        Time.timeScale = 1f;
    }


    void Start()
    {
        //newLoadedLevel = true;
        //persistentSource = new AudioSource();
        //persistentSource = this.gameObject.GetComponents<AudioSource>()[1];
        //Debug.Log("persistance source assigned: " + persistentSource);
        if (singleton != this)
        {
            if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(this.gameObject);
                loopSource = new AudioSource();
                persistentSource = new AudioSource();
                loopSource = this.gameObject.GetComponents<AudioSource>()[0];
                //singleton.persistentSource = this.gameObject.GetComponents<AudioSource>()[1];
                persistentSource = this.gameObject.GetComponents<AudioSource>()[1];
                DontDestroyOnLoad(persistentSource);
                switch (Application.loadedLevel)
                {
                    case 0:
                        if (singleton.loopSource.clip != menuMusic)
                        {
                            singleton.playSustainedSound(menuMusic);
                        }
                        break;
                    case 1:
                        if (singleton.loopSource.clip != menuMusic)
                        {
                            singleton.playSustainedSound(menuMusic);
                        }
                        break;
                    case 2:
                        if (singleton.loopSource.clip != levelMusic)
                        {
                            singleton.playSustainedSound(levelMusic);
                        }
                        break;
                }
                //Debug.Log(loopSource);
                //Debug.Log(loopSource.clip);

            }
            else
            {
                switch (Application.loadedLevel)
                {
                    case 0:
                        if (singleton.loopSource.clip != menuMusic)
                        {
                            singleton.playSustainedSound(menuMusic);
                        }
                        break;
                    case 1:
                        if (singleton.loopSource.clip != menuMusic)
                        {
                            singleton.playSustainedSound(menuMusic);
                        }
                        break;
                    case 2:
                        if (singleton.loopSource.clip != levelMusic)
                        {
                            singleton.playSustainedSound(levelMusic);
                        }
                        break;
                }
                Destroy(this.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playStandardSound(AudioClip sound)
    {
        oneShotPrefab.GetComponent<AudioSource>().clip = sound;
        oneShotPrefab.GetComponent<AudioSource>().volume = 1.0f;
        oneShotPrefab.GetComponent<AudioSource>().pitch = 1;
        Instantiate(oneShotPrefab, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //oneShotPrefab.GetComponent<AudioSource>().pitch = Random.Range(90, 110);
        //AudioSource.PlayClipAtPoint(sound, this.transform.position);
        //Debug.Log(sound + " played.");
    }

    public void playSoundAtVolume(AudioClip sound, float volume)
    {
        oneShotPrefab.GetComponent<AudioSource>().clip = sound;
        oneShotPrefab.GetComponent<AudioSource>().volume = volume;
        oneShotPrefab.GetComponent<AudioSource>().pitch = 1;
        if (oneShotPrefab != null)
        {
            Instantiate(oneShotPrefab, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }//oneShotPrefab.GetComponent<AudioSource>().pitch = Random.Range(90, 110);
        //AudioSource.PlayClipAtPoint(sound, this.transform.position);
        //Debug.Log(sound + " played.");
    }

    public void playModulatedSound(AudioClip sound, float volume)
    {

        oneShotPrefab.GetComponent<AudioSource>().clip = sound;
        oneShotPrefab.GetComponent<AudioSource>().pitch = (Random.Range(80, 120)) / 100f;
        Random.seed = Random.seed + (int)Random.value * 100;
        oneShotPrefab.GetComponent<AudioSource>().volume = volume;
        Instantiate(oneShotPrefab, this.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        //AudioSource.PlayClipAtPoint(sound, this.transform.position, volume);
        //AudioSource.PlayClipAtPoint()
        //Debug.Log(sound + " played.");
    }

    public void playSustainedSound(AudioClip sound)
    {
        //loopSource.transform.position = this.transform.position;
        loopSource.clip = sound;
        loopSource.loop = true;
        loopSource.Play();
        //Debug.Log(sound + " played. sus");
    }

    public void playPersistentSound(AudioClip sound)
    {
        if (this != singleton)
        {
            singleton.playPersistentSound(sound);
            return;
        }
        persistentSource.clip = sound;
        persistentSource.Play();
        //Debug.Log(sound + " played. pers");
    }
}
