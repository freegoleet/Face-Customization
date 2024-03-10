using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Character_Create_FeatureIndexButton : MonoBehaviour
{
    [SerializeField]
    private Image m_ImgFeature = null;
    [SerializeField]
    private Image m_ImgBg = null;
    [SerializeField]
    private Image m_ImgHighlight = null;

    private FaceFeature Feature { get; set; } = FaceFeature.None;

    public Action<FaceFeature> OnButtonPressed = null;

    public void SetupColors(SO_UI_Colors colors)
    {
        m_ImgHighlight.color = colors.BgButtonHighlight;
        m_ImgBg.color = colors.BgButton;
    }

    public void SetFeatureImage(Sprite sprite)
    {
        m_ImgFeature.sprite = sprite;
    }

    public void SetFeatureType(FaceFeature feature)
    {
        Feature = feature;
    }

    public void SetHighlight(bool highlight)
    {
        m_ImgHighlight.gameObject.SetActive(highlight);
    }

    public void ButtonPressed()
    {
        OnButtonPressed?.Invoke(Feature);
    }
}
