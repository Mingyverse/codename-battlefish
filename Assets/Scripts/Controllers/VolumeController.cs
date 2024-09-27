using System;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public string volumeName = default!;
    private VCA _vca;
    private Slider slider;

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat(volumeName, 1f);
        slider.value = volume;
        SetVolume(volume);
    }

    private void Awake()
    {
        Assert.IsNotNull(volumeName);
        
        FMODUnity.RuntimeManager.StudioSystem.getVCA(volumeName, out _vca);
        Assert.IsTrue(_vca.isValid());

        slider = GetComponent<Slider>();
        Assert.IsNotNull(slider);
    }

    public float GetVolume()
    {
        _vca.getVolume(out float volume);
        return volume;
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat(volumeName, volume);
        _vca.setVolume(volume);
    }
}