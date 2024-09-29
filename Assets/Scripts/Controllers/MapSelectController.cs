using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class MapSelectController : MonoBehaviour
{
    public static MapSelectController instance = default!;
    public MapInfo mapInfo = default!;
    public RectTransform map = default!;
    public RectTransform anchorTo = default!;

    private IEnumerator? _tween; 
        
    private void Awake()
    {
        instance = this;
        
        Assert.IsNotNull(mapInfo);
        Assert.IsNotNull(map);
    }

    public void OpenMapInfo(MapSelector mapSelector)
    {
        if (_tween != null)
            StopCoroutine(_tween);
        
        _tween = TweenMapToX(0.5f, anchorTo.anchoredPosition.x - mapSelector.GetComponent<RectTransform>().anchoredPosition.x);
        StartCoroutine(_tween);
        mapInfo.StageData = mapSelector.stageData;
    }

    public void CloseMapInfo()
    {
        mapInfo.gameObject.SetActive(false);

        if (_tween != null)
            StopCoroutine(_tween);
        
        _tween = TweenMapToX(0.5f, 0);
        StartCoroutine(_tween);
    }

    public IEnumerator TweenMapToX(float duration, float finalX)
    {
        float stopAt = Time.time + duration;
        float initialX = map.anchoredPosition.x;
        Vector2 pos = map.anchoredPosition;
        while (Time.time < stopAt)
        {
            pos.x = Mathf.Lerp(finalX, initialX, (stopAt - Time.time) / duration);
            map.anchoredPosition = pos;
            yield return null;
        }
        pos.x = finalX;
        map.anchoredPosition = pos;
    }
}