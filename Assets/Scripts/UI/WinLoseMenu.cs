using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class WinLoseMenu : MonoBehaviour
{

    public GameObject winUI = default!;
    public GameObject loseUI = default!;

    public string playerTeamTag = "Team1";
    public string enemyTeamTag = "Team2";

    private int _playerFishCount;
    private int _enemyFishCount;

    private void Awake()
    {
        Assert.IsNotNull(winUI);
        Assert.IsNotNull(loseUI);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        foreach (var fish in FindObjectsByType<BattleFish>(FindObjectsSortMode.None))
        {
            TallyFish(fish);
            fish.onDeath += OnFishDead;
        }
    }

    public void TallyFish(BattleFish fish)
    {
        if (fish.CompareTag(playerTeamTag))
            _playerFishCount++;
        else if (fish.CompareTag(enemyTeamTag))
            _enemyFishCount++;
    }

    public void OnFishDead(BattleFish fish, BattleFish attacker)
    {
        if (fish.CompareTag(playerTeamTag))
            _playerFishCount--;
        else if (fish.CompareTag(enemyTeamTag))
            _enemyFishCount--;
        
        CheckWin();
        fish.onDeath -= OnFishDead;
    }

    public void CheckWin()
    {
        if (_playerFishCount == 0)
            StartCoroutine(Wait(loseUI));
        else if (_enemyFishCount == 0)
        {
            StartCoroutine(Wait(winUI));
            PlayerDataController.instance.completedBattles.Add(StageController.instance.stageData.id);
        }
        
    }

    IEnumerator Wait(GameObject scene)
    {
        yield return new WaitForSeconds(1.25f);
        scene.SetActive(true);
        Time.timeScale = 0f;
    }
}
