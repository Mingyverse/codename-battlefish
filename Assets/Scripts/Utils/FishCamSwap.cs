using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FishCamSwap : MonoBehaviour
{
    public KeyCode swapKey = KeyCode.Space;
    public List<BattleFish> objects = new List<BattleFish>();

    private CinemachineVirtualCamera _cineCamera = default!;
    protected int nextIndex = -1;
    
    private void Awake()
    {
        _cineCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        foreach (BattleFish fish in objects)
            fish.onDeath += OnDeath;
        _cineCamera.Follow = GetNext();
    }

    private void Update()
    {
        if (Input.GetKeyUp(swapKey))
            _cineCamera.Follow = GetNext();
    }
    
    private Transform GetNext()
    {
        nextIndex++;
        nextIndex %= objects.Count;
        Transform ret = objects[nextIndex].transform;
        return ret;
    }

    private void OnDeath(BattleFish fish, BattleFish attacker)
    {
        objects.Remove(fish);
        fish.onDeath -= OnDeath;
        _cineCamera.Follow = GetNext();
    }
}