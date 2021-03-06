﻿using UnityEngine;
using System.Collections;
using System;

public class MusicPlayerScript : MonoBehaviour {

    public AudioClip levelIntro;
	public AudioClip levelLoop;

    public AudioClip bossIntro;
    public AudioClip bossLoop;

	public AudioClip VictoryTheme;

    private AudioSource source;

    private delegate void NextTrack();

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
        source.ignoreListenerVolume = true;

        LevelThemeStart();
	}

    private void LevelThemeStart()
    {
        if (source.clip != levelIntro && source.clip != levelLoop)
        {
            if (levelIntro != null)
            {
                source.clip = levelIntro;
                source.Play();
                StartCoroutine(PlayNextTrack(StartLevelLoop));
            }
        }
    }
    private void StartLevelLoop()
    {
        if (levelLoop != null)
        {
            source.clip = levelLoop;
            source.Play();
        }
    }


    private void BossThemeStart()
    {
        if (source.clip != bossIntro && source.clip != bossLoop)
        {
            if (bossIntro != null)
            {
                source.clip = bossIntro;
                source.Play();

                StartCoroutine(PlayNextTrack(StartBossLoop));
            }
        }
    }

    private void StartBossLoop()
    {
        if (bossLoop != null)
        {
            source.clip = bossLoop;
            source.Play();
        }
    }

	public void VictoryThemeStart()
	{
		if (source.clip != VictoryTheme) {
			source.clip = VictoryTheme;
			source.Play ();
		}
	}

    public void BossEnter()
    {
        BossThemeStart();
    }
    public void BossExit()
    {
        LevelThemeStart();
    }

    IEnumerator PlayNextTrack(NextTrack playThisNext)
    {
        yield return new WaitForSeconds(source.clip.length);
        playThisNext();
    }
}
