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
        Assert.IsNotNull(previewImage, this + ": previewImage is missing.");
        Assert.IsNotNull(previewText, this + " : previewText is missing.");
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
}