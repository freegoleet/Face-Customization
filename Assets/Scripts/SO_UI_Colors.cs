using UnityEngine;

[CreateAssetMenu(fileName = "UI Colors", menuName = "ScriptableObjects/Misc/UI Colors", order = 1)]
public class SO_UI_Colors : ScriptableObject
{
    [SerializeField]
    private Color m_BgButton = Color.white;
    [SerializeField]
    private Color m_BgButtonSelected = Color.white;
    [SerializeField]
    private Color m_ButtonBorderSelected = Color.white;

    public Color BgButton { get => m_BgButton; }
    public Color BgButtonHighlight { get => m_BgButtonSelected; }
    public Color ButtonBorderSelected { get => m_ButtonBorderSelected; }
}
