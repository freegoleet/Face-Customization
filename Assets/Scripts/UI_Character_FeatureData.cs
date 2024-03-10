using UnityEngine;

public class UI_Character_FeatureData
{
    public Color Color { get; set; } = Color.white;
    public int Index { get; set; } = -1;
    public UI_Character_Create_FeatureButton ButtonRef { get; set; } = null;
    public UI_Character_Create_Grid Grid { get; set; } = null;
}
