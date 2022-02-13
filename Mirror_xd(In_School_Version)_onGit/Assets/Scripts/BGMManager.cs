using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public float bgmVolume = 0.1f;

    public AudioClip clip;
    public float DesktopBgm = 0f;
    public float MythBgm = 0f;
    public float LibraryBgm = 0f;
    public float FishingBgm = 0f;
    public float FolderBgm = 0f;
    public float CmailBgm = 0f;

    public AudioSource defaultSource;
    public AudioSource BGMSource;

    // Chapter 2
    public AudioClip[] Chapter2Bgm;
    public float DickichuRaccoonBgm = 0f;
    public float BigDickichuBgm = 0f;
    public float RaccoonGameBgm = 0f;
    public float FirstHorseBgm = 0f;
    public float SecondHorseBgm = 0f;
    public float BreakSafeBgm = 0f;
    public float HorseCGBgm = 0f;
    public float MythCGBgm = 0f;
    public float GLITCHBgm = 0f;
    public float CH2MythBackBgm = 0f;
    // Record BGM string
    public string NowBGMState;

    // Start is called before the first frame update
    void Start()
    {
        defaultSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBgmVolume(float vol)
    {
        bgmVolume = vol;
    }

    public float GetBgmVolume()
    {
        return bgmVolume;
    }

    private void PlayAudio(float strength = 1f)
    {
        defaultSource.volume = bgmVolume * strength;
        defaultSource.loop = true;
        defaultSource.clip = clip;
        defaultSource.Play();
    }

    private void PlayBgm(AudioClip bgmClip, float time)
    {
        BGMSource = this.gameObject.AddComponent<AudioSource>();
        BGMSource.clip = bgmClip;
        BGMSource.volume = bgmVolume;
        BGMSource.loop = true;
        BGMSource.time = time;
        BGMSource.Play();
    }

    public void PauseBgm()
    {
        if(BGMSource != null)
        {
            BGMSource.Pause();
        }
        if(defaultSource != null)
        {
            defaultSource.Pause();
        }
    }

    public void ResumeBgm()
    {
        if (BGMSource != null)
        {
            BGMSource.UnPause();
        }
        if(defaultSource != null)
        {
            defaultSource.UnPause();
        }
    }

    public void StopBackBgm(string stage)
    {
        switch (stage)
        {
            case "Desktop":
                DesktopBgm = BGMSource.time;
                break;
            case "Myth":
                MythBgm = BGMSource.time;
                break;
            case "Library":
                LibraryBgm = BGMSource.time;
                break;
            case "Fishing":
                FishingBgm = BGMSource.time;
                break;
            case "Folder":
                FolderBgm = BGMSource.time;
                break;
            case "Cmail":
                CmailBgm = BGMSource.time;
                break;
            case "DickichuRaccoon":
                DickichuRaccoonBgm = BGMSource.time;
                break;
            case "BigDickichu":
                BigDickichuBgm = BGMSource.time;
                break;
            case "RaccoonGame":
                RaccoonGameBgm = BGMSource.time;
                break;
            case "FirstHorse":
                FirstHorseBgm = BGMSource.time;
                break;
            case "SecondHorse":
                SecondHorseBgm = BGMSource.time;
                break;
            case "BreakSafe":
                BreakSafeBgm = BGMSource.time;
                break;
            case "HorseCG":
                HorseCGBgm = BGMSource.time;
                break;
            case "MythCG":
                MythCGBgm = BGMSource.time;
                break;
            case "GLITCH":
                GLITCHBgm = BGMSource.time;
                break;
            case "CH2MythBack":
                CH2MythBackBgm = BGMSource.time;
                break;
        }
        Destroy(BGMSource);
    }

    public void StopCH2BGM()
    {
        if(BGMSource != null)
        {
            StopBackBgm(NowBGMState);
        }
    }

    public void StopBGM()
    {
        if (this.GetComponent<AudioSource>() != null)
        {
            this.GetComponent<AudioSource>().Stop();
        }    
    }

    public void DinasorBGM()
    {
        clip = (AudioClip)Resources.Load("audio/bgm/menu");
        PlayAudio(1.3f);
    }

    public void MoraBGM()
    {
        clip = (AudioClip)Resources.Load("audio/bgm/Power_of_Bravery_Loop");
        PlayAudio(0.5f);
    }

    // =======================================
    // Play BGM
    // 第一關
    public void PlayDesktopBgm()
    {
        PlayBgm((AudioClip)Resources.Load("audio/bgm/Respite"), DesktopBgm);
    }
    public void PlayMythBgm()
    {
        PlayBgm((AudioClip)Resources.Load("audio/bgm/Neon"), MythBgm);
    }
    public void PlayLibraryBgm()
    {
        PlayBgm((AudioClip)Resources.Load("audio/bgm/Space-Time-Lapse"), LibraryBgm);
    }
    public void PlayFishingBgm()
    {
        PlayBgm((AudioClip)Resources.Load("audio/bgm/Underwater Ambient"), FishingBgm);
    }
    public void PlayFolderBgm()
    {
        PlayBgm((AudioClip)Resources.Load("audio/bgm/Take Off"), FolderBgm);
    }
    public void PlayCmailBgm()
    {
        PlayBgm((AudioClip)Resources.Load("audio/bgm/Sights of Voyager"), CmailBgm);
    }
    // 第二關
    public void PlayDickichuRaccoonBgm()
    {
        PlayBgm(Chapter2Bgm[0], DickichuRaccoonBgm);
        NowBGMState = "DickichuRaccoon";
    }
    public void PlayBigDickichuBgm()
    {
        PlayBgm(Chapter2Bgm[1], BigDickichuBgm);
        NowBGMState = "BigDickichu";
    }
    public void PlayRaccoonGameBgm()
    {
        PlayBgm(Chapter2Bgm[2], RaccoonGameBgm);
        NowBGMState = "RaccoonGame";
    }
    public void PlayFirstHorseBgm()
    {
        PlayBgm(Chapter2Bgm[3], FirstHorseBgm);
        NowBGMState = "FirstHorse";
    }
    public void PlaySecondHorseBgm()
    {
        PlayBgm(Chapter2Bgm[4], SecondHorseBgm);
        NowBGMState = "SecondHorse";
    }
    public void PlayBreakSafeBgm()
    {
        PlayBgm(Chapter2Bgm[5], BreakSafeBgm);
        NowBGMState = "BreakSafe";
    }
    public void PlayHorseCGBgm()
    {
        PlayBgm(Chapter2Bgm[6], HorseCGBgm);
        NowBGMState = "HorseCG";
    }
    public void PlayMythCGBgm()
    {
        PlayBgm(Chapter2Bgm[7], MythCGBgm);
        NowBGMState = "MythCG";
    }
    public void PlayGLITCHBgm()
    {
        PlayBgm(Chapter2Bgm[8], GLITCHBgm);
        NowBGMState = "GLITCH";
    }
    public void PlayCH2MythBackBgm()
    {
        PlayBgm(Chapter2Bgm[9], CH2MythBackBgm);
        NowBGMState = "CH2MythBack";
    }

}
