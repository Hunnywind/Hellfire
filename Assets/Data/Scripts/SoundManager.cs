using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance;

    public AudioSource BGMSource;
    public AudioClip [] sceneBGM;

    public AudioSource SFXSource;
    public AudioClip [] screamClip;

    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake () 
    {
        if (SoundManager.instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }            
    }

    void Start()
    {
        BGMSource.clip = sceneBGM[0];
        BGMSource.Stop();
        BGMSource.Play();
    }

    public void ScreamSound()
    {
        int num = Random.Range(0, screamClip.Length);
        SFXSource.PlayOneShot(screamClip[num]);
    }       
}
