using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaceFeatureData
{
    public int Index;
    public Color Color;
}

public class UI_Character_Face : MonoBehaviour
{
    [Header("Image References")]
    [SerializeField]
    private Image m_ImgHair = null;
    [SerializeField]
    private Image m_ImgEyebrows = null;
    [SerializeField]
    private Image m_ImgEyes = null;
    [SerializeField]
    private Image m_ImgNose = null;
    [SerializeField]
    private Image m_ImgEars = null;
    [SerializeField]
    private Image m_ImgMouth = null;
    [SerializeField]
    private Image m_ImgChin = null;

    private Dictionary<FaceFeature, Image> m_FeatureImages = null;

    public Dictionary<FaceFeature, List<Sprite>> CharacterFeatures { get; set; }
    public Image ImgHair { get => m_ImgHair; }
    public Image ImgEyebrows { get => m_ImgEyebrows; }
    public Image ImgEyes { get => m_ImgEyes; }
    public Image ImgNose { get => m_ImgNose; }
    public Image ImgEars { get => m_ImgEars; }
    public Image ImgMouth { get => m_ImgMouth; }
    public Image ImgChin { get => m_ImgChin; }

    public void Awake()
    {
        m_FeatureImages = new()
        {
            { FaceFeature.Hair, ImgHair },
            { FaceFeature.Eyes, ImgEyes },
            { FaceFeature.Eyebrows, ImgEyebrows },
            { FaceFeature.Ears, ImgEars },
            { FaceFeature.Nose, ImgNose },
            { FaceFeature.Mouth, ImgMouth },
            { FaceFeature.Chin, ImgChin }
        };
    }

    public void SetFeature(FaceFeature feature, int index)
    {
        m_FeatureImages[feature].sprite = CharacterFeatures[feature][index];
    }

    public void SetFeatures(Dictionary<FaceFeature, FaceFeatureData> featureData)
    {
        foreach (var item in featureData)
        {
            SetFeature(item.Key, item.Value.Index);
            SetFeatureColor(item.Key, item.Value.Color);
        }
    }

    public void SetFeatureColor(FaceFeature feature, Color color)
    {
        m_FeatureImages[feature].color = color;
    }
}
