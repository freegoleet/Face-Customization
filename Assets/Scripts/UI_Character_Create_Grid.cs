using TMPro;
using UnityEngine;

public class UI_Character_Create_Grid : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TxtTitle = null;
    [SerializeField]
    private RectTransform m_ImgTitleLine = null;
    [SerializeField]
    private RectTransform m_Grid = null;
    [SerializeField]
    private float m_PaddingBottom = 20f;

    public FaceFeature Feature { get; private set; } = FaceFeature.None;

    private int m_Cols = 0;
    private int m_Rows = 0;

    public TextMeshProUGUI TxtTitle { get => m_TxtTitle; }
    public RectTransform Grid { get => m_Grid; }
    public RectTransform ImgTitleLine { get => m_ImgTitleLine; }
    public int Cols { get => m_Cols; set => m_Cols = value; }
    public int Rows { get => m_Rows; set => m_Rows = value; }

    public float UpdateHeight()
    {
        float height = Grid.rect.height;
        height += TxtTitle.rectTransform.rect.height;
        height += ImgTitleLine.rect.height;
        height += Mathf.Abs(ImgTitleLine.localPosition.y - Grid.localPosition.y);
        height += m_PaddingBottom;

        var rectT = GetComponent<RectTransform>();

        rectT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

        return height;
    }

    public void SetTitleText(string text)
    {
        TxtTitle.text = text;
    }

    public void SetFeature(FaceFeature feature)
    {
        Feature = feature;
    }
}
