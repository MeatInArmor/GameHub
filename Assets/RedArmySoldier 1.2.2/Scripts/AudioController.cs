using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    [SerializeField] public Scrollbar[] VolumeScrollBar;

    [SerializeField] public AudioSource[] volume;

    [SerializeField] public float[] maxVolume;

    [SerializeField] public bool[] volumeIsOn;



    private void Start()
    {
        VolumeScrollBar[0].onValueChanged.AddListener(SetVolume0);
        VolumeScrollBar[1].onValueChanged.AddListener(SetVolume1);

        volumeIsOn[0] = true;
        volumeIsOn[1] = true;

        maxVolume[0] = 1f;
        maxVolume[1] = 1f;
    }

    public void setClip(AudioClip clip, int clipType)
    {
        volume[clipType].clip = clip;

        if(clipType == 0)
        {
            volume[clipType].loop = true;
            volume[clipType].volume = 0f;
        }
        else
        {
            volume[clipType].loop = false;
            volume[clipType].volume = maxVolume[1];
        }
        if (clipType == 0)
        {
            volume[clipType].Play();
            StartCoroutine(MusicChanger(3f));
        }
        else if (volumeIsOn[1])
        {
            volume[clipType].Play();
        }
    }

    private void SetVolume0(float newVolume)
    {
        maxVolume[0] = newVolume;

        if (volumeIsOn[0])
            volume[0].volume = maxVolume[0];
        else
            volume[0].volume = 0;
    }
    private void SetVolume1(float newVolume)
    {
        maxVolume[1] = newVolume;

        if (volumeIsOn[1])
            volume[1].volume = maxVolume[1];
        else
            volume[1].volume = 0;
    }

    public void SetVolumeType(int volumeType)
    {
        volumeIsOn[volumeType] = !volumeIsOn[volumeType];

        if (volumeType == 0)
            SetVolume0(maxVolume[0]);
        else
            SetVolume1(maxVolume[0]);

    }

    private IEnumerator MusicChanger(float duration)
    {
        float volumeDelta = 1f;
        while (volume[0].volume < maxVolume[0])
        {
            if (!volumeIsOn[0])
                yield break;
            volume[0].volume += volumeDelta * Time.deltaTime / duration;
            yield return null;
        }
        volume[0].volume = maxVolume[0];
    }
}
