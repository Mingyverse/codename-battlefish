using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class FishGuidePreviews : MonoBehaviour
{
    public PreviewBox[] previewBoxes = Array.Empty<PreviewBox>();
    public PageIndicators indicator;
    public NavigationArrow navigation;
    public TMP_Dropdown waterTypeFilterDropdown = default!;
    
    public WaterType filterWaterType
    {
        get
        {
            switch (waterTypeFilterDropdown?.value)
            {
                case 1:
                    return WaterType.Freshwater;
                case 2:
                    return WaterType.Saltwater;
                default:
                    return WaterType.All;
            }
        }
    }

    public int numberOfFishes => GetFilteredFishes().Count();
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

    private IEnumerable<BattleFishData> GetFilteredFishes()
    {
        IEnumerable<BattleFishData> enumerable = BattleFishData.GetData();
        if (filterWaterType != WaterType.All)
            enumerable = enumerable.Where(fish => fish.waterType == filterWaterType);
        return enumerable;
    }

    private IEnumerable<BattleFishData> GetPaginatedFishes()
    {
        return GetFilteredFishes()
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize);
    }

    public void ReloadPage()
    {
        for (int i = 0; i < _pageIndicators.Count; i++)
            Destroy(_pageIndicators[i].gameObject);
        _pageIndicators = new List<Image>();
        
        currentPage = Math.Min(currentPage, maxPage);
        Start();
    }

    public void LoadPage()
    {
        IEnumerator<BattleFishData> data = GetPaginatedFishes().GetEnumerator();

        float delay = 0.05f;
        foreach (PreviewBox previewBox in previewBoxes)
        {
            if (!data.MoveNext())
                previewBox.BattleFishData = null;
            else
                previewBox.BattleFishData = data.Current;
                
            StartCoroutine(previewBox.AnimateFadeIn(0.5f, delay));
            delay += 0.05f;
        }
        data.Dispose();
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