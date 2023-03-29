using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
	[SerializeField] AudioMixer myAudio;

	public void SetVolume (float volume)
	{
		myAudio.SetFloat("volume", volume);
	}

	public void SetQuality(int qualityIndex)
	{
		switch(qualityIndex)
		{
            case 0:
            {
                QualitySettings.SetQualityLevel(0);
                break;
            }
            case 1:
            {
                QualitySettings.SetQualityLevel(3);
                break;
            }
            case 2:
            {
                QualitySettings.SetQualityLevel(5);
                break;
            }
		}
	}
}
