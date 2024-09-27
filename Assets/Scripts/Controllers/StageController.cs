using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [NonSerialized] public GameObject? player; 
    [NonSerialized] public BattleFish[] wildFishes = Array.Empty<BattleFish>();
    
    public static StageController instance = default!;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wildFishes = FindObjectsOfType<BattleFish>();
    }
    
    public Transform? GetClosestFish(Transform position, Predicate<BattleFish> predicate)
    {
        Transform? bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        
        foreach(BattleFish fish in wildFishes.Where(fish => predicate(fish)))
        {
            // calc distance
            Transform target = fish.transform;
            Vector3 directionToTarget = target.position - currentPosition;
            float distanceSqrToTarget = directionToTarget.sqrMagnitude;
            
            if (distanceSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToTarget;
                bestTarget = target;
            }
        }
     
        return bestTarget;
    }
    
    public Transform? GetClosestFish(Transform position, Predicate<BattleFish> predicate, float maxDistance)
    {
        Transform? bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        
        foreach(BattleFish fish in wildFishes.Where(fish => predicate(fish)))
        {
            // calc distance
            Transform target = fish.transform;
            Vector3 directionToTarget = target.position - currentPosition;
            float distanceSqrToTarget = directionToTarget.sqrMagnitude;

            if (distanceSqrToTarget > maxDistance)
                continue;
            
            if (distanceSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToTarget;
                bestTarget = target;
            }
        }
     
        return bestTarget;
    }
}