using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    private static GameObject soundManager;
    private static GameObject soundManagerPrefab;

    public string defaultSong;
    public bool playMusicOnStart = true;

    public List<AudioClip> fxClips = new List<AudioClip>();
    public List<AudioClip> musicList = new List<AudioClip>();

    private AudioSource music; //Audio source for music
    private AudioSource fx; //Audio source for fx

    private static bool isPlayingBackgroundMusic;

    public static SoundManagerScript getInstance()
    {
        if (soundManager == null)
        {
            soundManagerPrefab = Resources.Load<GameObject>("Prefabs/SoundManager") as GameObject;
            soundManager = Instantiate(soundManagerPrefab);
            isPlayingBackgroundMusic = false;
        }

        return soundManager.GetComponent<SoundManagerScript>();
    }

    private void Awake()
    {
        if(soundManager == null)
        {
            soundManager = gameObject;
            DontDestroyOnLoad(soundManager);
        }
        

        //Grabs all audio sources on the gameobject
        var audioSources = GetComponents<AudioSource>();
        music = audioSources[0];
        fx = audioSources[1];
    }

    // Use this for initialization
    void Start () {

        if (!isPlayingBackgroundMusic)
        {
            setSong(defaultSong);

            music.loop = true;

            if (playMusicOnStart)
            {
                music.Play();
            }

            isPlayingBackgroundMusic = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Requires the songList to be populated
    public void setSong(string songName)
    {
        music.clip = findMusic(songName);
    }

    //Requires the songList to be populated
    private AudioClip findMusic(string key)
    {
        foreach(AudioClip song in musicList)
        {
            if (song.name == key)
                return song;
        }

        Debug.Log("Song " + key + " not found, playing default track " + musicList[0].name);
        return musicList[0];
    }

    //Requires the fxClips to be populated
    public void playFx(string fxName)
    {
        AudioClip newFx = findFx(fxName);

        if (newFx == null) //Handles errors
            return;

        fx.clip = newFx;
        fx.Play();
    }

    //Requires the fxClips to be populated
    private AudioClip findFx(string key)
    {
        foreach (AudioClip fx in fxClips)
        {
            if (fx.name == key)
                return fx;
        }

        Debug.Log("Fx " + key + " not found");
        return null;
    }
}
