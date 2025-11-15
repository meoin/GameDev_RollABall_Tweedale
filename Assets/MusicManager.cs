using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicPlayer;
    public List<AudioClip> songList;
    private int songIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        songIndex = Random.Range(0, songList.Count-1);
        musicPlayer.clip = songList[songIndex];
        musicPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (musicPlayer.isPlaying && musicPlayer.time >= musicPlayer.clip.length - 1f)
        {
            PlayNewSong();
        }
    }

    private void PlayNewSong() 
    {
        while (true) 
        {
            int newSong = Random.Range(0, songList.Count - 1);
            if (newSong != songIndex) 
            {
                songIndex = newSong;
                break;
            }
        }

        musicPlayer.clip = songList[songIndex];
        musicPlayer.Play();
    }
}
