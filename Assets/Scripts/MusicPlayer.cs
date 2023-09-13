using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        KeepMusicPlayer();
    }

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {

    }

    void KeepMusicPlayer()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
