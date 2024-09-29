using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CineCamSwap : MonoBehaviour
{
    public KeyCode swapKey = KeyCode.Space;
    public Transform[] objects = Array.Empty<Transform>();

    private CinemachineVirtualCamera _cineCamera = default!;
    protected int nextIndex;
    
    private void Awake()
    {
        _cineCamera = GetComponent<CinemachineVirtualCamera>();
    }
    
    private void Update()
    {
        if (Input.GetKeyUp(swapKey))
            _cineCamera.Follow = GetNext();
    }
    
    private Transform GetNext()
    {
        nextIndex++;
        nextIndex %= objects.Length;
        Transform ret = objects[nextIndex];
        return ret;
    }
}