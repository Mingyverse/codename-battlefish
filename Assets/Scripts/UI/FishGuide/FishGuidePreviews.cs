using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class FishGuidePreviews : MonoBehaviour
{
    public PreviewBox[] previewBoxes = Array.Empty<PreviewBox>();
    public PageIndicators indicator;
    public NavigationArrow navigation;
    
    public int numberOfFishes => BattleFishData.GetData().Length;
    public int pageSize => previewBoxes.Length;
    public int maxPage => previewBoxes.Length > 0 ?  (int)Math.Ceiling((double) numberOfFishes / pageSize) : 0;
    [NonSerialized] public int currentPage = 1;

    [Serializable]
    public struct PageIndicators
    {
        public Transform indicatorParent;
        public Image indicatorPrefab;
        public Sprite defaultSprite;
        public Sprite onPageSprite;
        public float gap;
    }
    
    private List<Image> _pageIndicators = new List<Image>();

    [Serializable]
    public struct NavigationArrow
    {
        public Image previousPageImage;
        public Image nextPageImage;
    }

    private void Awake()
    {
        Assert.IsNotNull(indicator.indicatorParent, this + ": IndicatorParent is missing");
        Assert.IsNotNull(indicator.indicatorPrefab, this + ": IndicatorPrefab is missing");
        Assert.IsNotNull(indicator.defaultSprite, this + ": DefaultSprite is missing");
        Assert.IsNotNull(indicator.onPageSprite, this + ": OnPageSprite is missing");
        Assert.IsNotNull(navigation.previousPageImage, this + ": PreviousPageImage is missing");
        Assert.IsNotNull(navigation.nextPageImage, this + ": NextPageImage is missing");
    }

    private void Start()
    {
        CreatePageIndicators();
        ArrangePageIndicators();
        
        UpdateNavigationArrowVisibility();
        LoadPage();
        SetCurrentOnPageIndicator();
    }

    private void CreatePageIndicators()
    {
        for (int i = 0; i < maxPage; i++)
            _pageIndicators.Add(Instantiate(indicator.indicatorPrefab, indicator.indicatorParent));
    }

    private void ArrangePageIndicators()
    {
        float xOffset = 0;
        foreach (Image pageIndicator in _pageIndicators)
        {
            Vector2 position = pageIndicator.rectTransform.anchoredPosition;
            position.x = xOffset;
            pageIndicator.rectTransform.anchoredPosition = position;
            
            xOffset += pageIndicator.rectTransform.sizeDelta.x + indicator.gap;
        }

        xOffset -= indicator.gap;
        xOffset /= 2;
        
        foreach (Image pageIndicator in _pageIndicators)
        {
            Vector2 position = pageIndicator.rectTransform.anchoredPosition;
            position.x -= xOffset;
            pageIndicator.rectTransform.anchoredPosition = position;
        }
    }

    private void SetCurrentDefaultIndicator()
    {
        _pageIndicators[currentPage - 1].sprite = indicator.defaultSprite;
    }

    private void SetCurrentOnPageIndicator()
    {
        _pageIndicators[currentPage - 1].sprite = indicator.onPageSprite;
    }

    private void UpdateNavigationArrowVisibility()
    {
        navigation.nextPageImage.gameObject.SetActive(currentPage < maxPage);
        navigation.previousPageImage.gameObject.SetActive(currentPage > 1);
    }

    public void LoadPage()
    {
        IEnumerator<BattleFishData> data = BattleFishData.GetData()
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .GetEnumerator();
        foreach (PreviewBox previewBox in previewBoxes)
        {
            if (!data.MoveNext())
                previewBox.BattleFishData = null;
            else
                previewBox.BattleFishData = data.Current;
        }
    }

    public void NextPage()
    {
        if (currentPage >= maxPage)
            return;

        SetCurrentDefaultIndicator();
            
        currentPage++;
        
        UpdateNavigationArrowVisibility();
        LoadPage();
        SetCurrentOnPageIndicator();
    }

    public void PreviousPage()
    {
        if (currentPage <= 1)
            return;

        SetCurrentDefaultIndicator();
        
        currentPage--;
        
        UpdateNavigationArrowVisibility();
        LoadPage();
        SetCurrentOnPageIndicator();
    }
}