using FMOD.Studio;
using UnityEngine;
using UnityEngine.Assertions;

public class VolumeController : MonoBehaviour
{
    public string volumeName = default!;

    private VCA _vca;

    private void Awake()
    {
        Assert.IsNotNull(volumeName);
        
        FMODUnity.RuntimeManager.StudioSystem.getVCA(volumeName, out _vca);
        Assert.IsTrue(_vca.isValid());
    }

    public float GetVolume()
    {
        _vca.getVolume(out float volume);
        return volume;
    }

    public void SetVolume(float volume)
    {
        _vca.setVolume(volume);
    }
}