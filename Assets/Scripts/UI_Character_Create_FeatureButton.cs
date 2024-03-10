using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Character_Create_FeatureButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TxtIndex = null;
    [SerializeField]
    private Image m_ImgBg = null;
    [SerializeField]
    private Image m_ImgHighlight = null;
    [SerializeField]
    private Image m_ImgFeature = null;
    [SerializeField]
    private Image m_ImgBorder = null;
    [SerializeField]
    private RectTransform m_RectTransform = null;

    public FaceFeature Feature { get; private set; } = FaceFeature.None;
    public int FeatureIndex { get; private set; } = -1;
    public Image ImgBorder { get => m_ImgBorder; }
    public RectTransform RectTransform { get => m_RectTransform; }

    public Action<UI_Character_Create_FeatureButton> OnButtonPressed = null;

    public void SetupColors(SO_UI_Colors colors)
    {
        m_ImgBg.color = colors.BgButton;
        m_ImgHighlight.color = colors.BgButtonHighlight;
        m_ImgBorder.color = colors.ButtonBorderSelected;

        m_ImgHighlight.gameObject.SetActive(false);
        m_ImgBorder.gameObject.SetActive(false);
    }

    public void SetIndex(int index)
    {
        m_TxtIndex.text = index.ToString();
        FeatureIndex = index;
    }

    public void SetFeature(FaceFeature faceFeature, Sprite sprite)
    {
        Feature = faceFeature;
        m_ImgFeature.sprite = sprite;
    }

    public void SetFeatureColor(Color color)
    {
        m_ImgFeature.color = color;
    }

    public void SetSelected(bool selected)
    {
        m_ImgBorder.gameObject.SetActive(selected);
        m_ImgHighlight.gameObject.SetActive(selected);
    }

    public void ButtonPressed()
    {
        OnButtonPressed?.Invoke(this);
    }
}