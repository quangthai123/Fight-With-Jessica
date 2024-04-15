using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [SerializeField] private List<AudioSource> sfx;
    [SerializeField] private List<AudioSource> bgm;
    private Transform SFX;
    private Transform BGM;
    public bool playBgm = true;
    public int bgmIndex;
    private void Reset()
    {
        LoadSFXPrefabs();
        LoadBGMPrefabs();
    }
    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;
        LoadSFXPrefabs();
        LoadBGMPrefabs();
    }
    private void LoadSFXPrefabs()
    {
        if (sfx.Count > 0) return;
        SFX = transform.Find("SFX");
        foreach(Transform sfx in SFX)
        {
            AudioSource audioSource = sfx.GetComponent<AudioSource>();
            this.sfx.Add(audioSource);
        }
    }
    private void LoadBGMPrefabs()
    {
        if (bgm.Count > 0) return;
        BGM = transform.Find("BGM");
        foreach (Transform bgm in BGM)
        {
            AudioSource audioSource = bgm.GetComponent<AudioSource>();
            this.bgm.Add(audioSource);
        }
    }
    void Update()
    {
        if(!playBgm)
            StopAllBgm();
        else
        {
            if (!bgm[bgmIndex].isPlaying)
                PlayBgm(bgmIndex);
        }
    }
    public void PlaySfx(int _index)
    {
        if(_index < sfx.Count)
        {
            sfx[_index].Play();
        }
    }
    public void StopSfx(int _index) => sfx[_index].Stop(); 
    public void PlayBgm(int _index)
    {
        if(_index < bgm.Count)
        {
            bgmIndex = _index;
            StopAllBgm();
            bgm[bgmIndex].Play();
        }
    }
    private void StopAllBgm()
    {
        for(int i=0; i<bgm.Count; i++)
        {
            bgm[i].Stop();
        }
    }
}
