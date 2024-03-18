using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum FaceFeature
{
    Hair,
    Eyebrows,
    Eyes,
    Ears,
    Nose,
    Mouth,
    Chin,
    None
}

public class UI_Character_Create_Panel : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private UI_Character_Face m_Face = null;
    [SerializeField]
    private HSVPicker.ColorPicker m_ColorPicker = null;

    [Header("Feature Buttons")]
    [SerializeField]
    private UI_Character_Create_FeatureButton m_Button = null;
    [SerializeField]
    private UI_Character_Create_Grid m_Grid = null;
    [SerializeField]
    private RectTransform m_Content = null;

    [Header("Feature Index Buttons")]
    [SerializeField]
    private RectTransform m_FeatureIndexContent = null;
    [SerializeField]
    private UI_Character_Create_FeatureIndexButton m_FeatureIndexButton = null;

    [Header("Serialized Objects")]
    [SerializeField]
    private SO_UI_Colors m_UIColors = null;
    [SerializeField]
    private SO_Character_FaceFeatures m_FaceFeatures = null;

    // Private variables
    private UI_Character_Create_FeatureButton m_CurrentlySelectedButton = null;
    private UI_Character_Create_Grid m_CurrentGrid = null;
    private List<UI_Character_Create_FeatureButton> m_Buttons = new();
    private Dictionary<FaceFeature, List<Sprite>> m_CharacterFeatures = null;
    private Tuple<FaceFeature, int> m_CurrentlySelectedFeature = new(FaceFeature.None, -1);
    private Dictionary<FaceFeature, UI_Character_Create_FeatureIndexButton> m_FeatureIndexButtons = new();

    // Public Variables
    public Dictionary<FaceFeature, FaceFeatureData> NewFaceData { get; set; }
    public Dictionary<FaceFeature, UI_Character_FeatureData> CurrentlyChosenFeatures { get; private set; } = new();
    public Dictionary<FaceFeature, List<UI_Character_Create_FeatureButton>> ButtonsByFeature { get; private set; } = new() {
            { FaceFeature.Hair, new() },
            { FaceFeature.Eyebrows, new() },
            { FaceFeature.Eyes, new() },
            { FaceFeature.Ears, new() },
            { FaceFeature.Nose, new() },
            { FaceFeature.Mouth, new() },
            { FaceFeature.Chin, new() },
        };

    public void Start()
    {
        m_CharacterFeatures = m_FaceFeatures.GetFaceFeatures();
        m_Face.CharacterFeatures = m_CharacterFeatures;

        m_ColorPicker.onValueChanged.AddListener(SetColor);

        NewFaceData = new Dictionary<FaceFeature, FaceFeatureData>();

        var features = Enum.GetValues(typeof(FaceFeature)).Cast<FaceFeature>().ToList();
        for (int i = 0; i < features.Count - 1; i++)
        {
            NewFaceData.Add(features[i], new FaceFeatureData { Color = Color.white, Index = 0 });
        }

        SetupButtons();
        RandomizeLook();
        GotoFeature(FaceFeature.Hair);
    }

    private void SetupButtons()
    {
        foreach (var kvp in m_CharacterFeatures)
        {
            var chosenFeatureData = new UI_Character_FeatureData();

            var grid = Instantiate(m_Grid, m_Content);
            grid.SetTitleText(kvp.Key.ToString());
            grid.SetFeature(kvp.Key);
            grid.gameObject.SetActive(false);
            chosenFeatureData.Grid = grid;

            float yPos = -1f;
            float xPos = -1f;

            for (int i = 0; i < kvp.Value.Count; i++)
            {
                var button = Instantiate(m_Button, grid.Grid);
                button.SetFeature(kvp.Key, kvp.Value[i]);
                button.SetIndex(i);
                button.SetupColors(m_UIColors);
                button.OnButtonPressed += SelectFeature;
                button.ImgBorder.gameObject.SetActive(false);

                if (button.RectTransform.position.y != yPos)
                {
                    grid.Rows++;
                }

                if (button.RectTransform.position.y != xPos)
                {
                    grid.Cols++;
                }

                yPos = button.transform.localPosition.y;
                xPos = button.transform.localPosition.x;

                m_Buttons.Add(button);
                ButtonsByFeature[kvp.Key].Add(button);
            }

            var indexButton = Instantiate(m_FeatureIndexButton, m_FeatureIndexContent);
            indexButton.SetFeatureType(kvp.Key);
            indexButton.SetFeatureImage(kvp.Value[0]);
            indexButton.SetupColors(m_UIColors);
            indexButton.OnButtonPressed += GotoFeature;
            m_FeatureIndexButtons.Add(kvp.Key, indexButton);

            chosenFeatureData.ButtonRef = ButtonsByFeature[kvp.Key][0];
            chosenFeatureData.Color = Color.white;
            chosenFeatureData.Index = 0;
            CurrentlyChosenFeatures.Add(kvp.Key, chosenFeatureData);

        }
    }

    /// <summary>
    /// Call when a feature button is pressed.
    /// </summary>
    /// <param name="button"> A reference to the feature button that was pressed. </param>
    public void SelectFeature(UI_Character_Create_FeatureButton button)
    {
        if (button == m_CurrentlySelectedButton)
        {
            return;
        }

        if(m_CurrentlySelectedButton == null)
        {
            m_CurrentlySelectedButton = button;
        }
        else
        {
            m_CurrentlySelectedButton.SetSelected(false);
            m_CurrentlySelectedButton = button;
            button.SetSelected(true);
        }

        SetFeature(button);
    }

    /// <summary>
    /// Call when a feature was selected without directly pressing a button.
    /// </summary>
    public void SelectFeature(FaceFeature feature, int index)
    {
        var button = ButtonsByFeature[feature][index];
        SelectFeature(button);
    }

    private void SetFeature(UI_Character_Create_FeatureButton button)
    {
        var previousFeature = m_CurrentlySelectedFeature;
        m_CurrentlySelectedFeature = new Tuple<FaceFeature, int>(button.Feature, button.FeatureIndex);
        
        m_Face.SetFeature(button.Feature, button.FeatureIndex);

        if (button.Feature != previousFeature.Item1)
        {
            m_ColorPicker.CurrentColor = CurrentlyChosenFeatures[button.Feature].Color;
        }

        CurrentlyChosenFeatures[button.Feature].Index = button.FeatureIndex;
        CurrentlyChosenFeatures[button.Feature].ButtonRef = button;
        NewFaceData[button.Feature].Index = button.FeatureIndex;
    }

    private void SetColorOfFeatureType(FaceFeature feature, Color color)
    {
        m_Face.SetFeatureColor(feature, color);

        for (int i = 0; i < ButtonsByFeature[feature].Count; i++)
        {
            ButtonsByFeature[feature][i].SetFeatureColor(color);
            NewFaceData[feature].Color = color;
        }
    }

    private void SetColor(Color color)
    {
        if (m_CurrentlySelectedFeature.Item1 == FaceFeature.None)
        {
            return;
        }

        switch (m_CurrentlySelectedFeature.Item1)
        {
            case FaceFeature.Hair:
                break;
            case FaceFeature.Eyebrows:
                break;
            case FaceFeature.Eyes:
                break;
            case FaceFeature.Ears:
                SetupColor(FaceFeature.Chin, color);
                break;
            case FaceFeature.Nose:
                break;
            case FaceFeature.Mouth:
                break;
            case FaceFeature.Chin:
                SetupColor(FaceFeature.Ears, color);
                break;
            case FaceFeature.None:
                break;
            default:
                break;
        }

        SetupColor(m_CurrentlySelectedFeature.Item1, color);
    }

    private void SetupColor(FaceFeature feature, Color color)
    {
        if (CurrentlyChosenFeatures[feature].Color == color)
        {
            return;
        }

        CurrentlyChosenFeatures[feature].Color = color;
        SetColorOfFeatureType(feature, color);
    }

    /// <summary>
    /// Randomize every feature of the face.
    /// </summary>
    public void RandomizeLook()
    {
        for (int i = 0; i < Enum.GetNames(typeof(FaceFeature)).Length; i++)
        {
            var feature = (FaceFeature)Enum.GetValues(typeof(FaceFeature)).GetValue(i);
            if (feature == FaceFeature.None)
            {
                continue;
            }

            int index = UnityEngine.Random.Range(0, m_CharacterFeatures[feature].Count);
            m_Face.SetFeature(feature, index);
            SetupColor(feature, UnityEngine.Random.ColorHSV());

            SelectFeature(feature, index);
        }

        GotoFeature(m_CurrentlySelectedFeature.Item1);
    }

    private void GotoFeature(FaceFeature feature)
    {
        if (m_CurrentGrid != null)
        {
            if (feature == m_CurrentGrid.Feature)
            {
                return;
            }

            m_FeatureIndexButtons[m_CurrentGrid.Feature].SetHighlight(false);
            m_CurrentGrid.gameObject.SetActive(false);
        }

        m_CurrentGrid = CurrentlyChosenFeatures[feature].Grid;
        CurrentlyChosenFeatures[feature].Grid.gameObject.SetActive(true);
        m_FeatureIndexButtons[feature].SetHighlight(true);

        SelectFeature(CurrentlyChosenFeatures[feature].ButtonRef);
    }
}
