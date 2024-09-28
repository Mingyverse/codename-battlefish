using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PreviewBox : MonoBehaviour
{
    public Image previewImage = default!;
    public TextMeshProUGUI previewText = default!;
    
    [SerializeField] private BattleFishData? battleFishData;
    public bool autoRender = true;

    private CanvasGroup _canvasGroup = default!;
    private FishInfo _fishInfo = default!;
    
    public BattleFishData? BattleFishData
    {
        get => battleFishData;
        set
        {
            battleFishData = value;
            if (autoRender)
                Render();
        }
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        FishInfo[] fishInfo = Resources.FindObjectsOfTypeAll<FishInfo>();
        
        Assert.IsNotNull(previewImage, this + ": previewImage is missing.");
        Assert.IsNotNull(previewText, this + " : previewText is missing.");
        Assert.IsNotNull(_canvasGroup, this + ": canvasGroup is missing");
        Assert.IsFalse(fishInfo.Length == 0, this + ": fishInfo is missing");
        
        _fishInfo = fishInfo[0];
    }

    public void Render()
    {
        if (battleFishData == null)
        {
            #pragma warning disable
            previewImage.sprite = null;
            #pragma warning restore
            previewText.text = "";
        }
        else
        {
            previewImage.sprite = battleFishData.previewSprite;
            previewText.text = battleFishData.fishName;
        }
    }

    public IEnumerator AnimateFadeIn(float duration, float delaySeconds)
    {
        _canvasGroup.alpha = 0f;
        yield return new WaitForSeconds(delaySeconds);

        float stopAt = Time.time + duration;
        while (Time.time < stopAt)
        {
            _canvasGroup.alpha = Mathf.Lerp(1, 0, (stopAt - Time.time) / duration);
            yield return null;
        }
    }

    public void OpenFishInfoMenu()
    {
        _fishInfo.BattleFishData = battleFishData;
    }
}