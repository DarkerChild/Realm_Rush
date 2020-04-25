//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class MusicPlayer : MonoBehaviour
{
    
    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;        //if more than music player in scene the ndestroy ourselves
        if (numMusicPlayers>1) {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
