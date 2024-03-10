using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Face Features", menuName = "ScriptableObjects/Entities/Character Face Features", order = 1)]
public class SO_Character_FaceFeatures : ScriptableObject
{
    [SerializeField]
    private List<Sprite> m_Chin = null;
    [SerializeField]
    private List<Sprite> m_Hairstyle = null;
    [SerializeField]
    private List<Sprite> m_Eyes = null;
    [SerializeField]
    private List<Sprite> m_Ears = null;
    [SerializeField]
    private List<Sprite> m_Mouth = null;
    [SerializeField]
    private List<Sprite> m_Eyebrows = null;
    [SerializeField]
    private List<Sprite> m_Nose = null;

    public List<Sprite> Chin { get => m_Chin; }
    public List<Sprite> Hairstyle { get => m_Hairstyle; }
    public List<Sprite> Eyes { get => m_Eyes; }
    public List<Sprite> Ears { get => m_Ears; }
    public List<Sprite> Mouth { get => m_Mouth; }
    public List<Sprite> Eyebrows { get => m_Eyebrows; }
    public List<Sprite> Nose { get => m_Nose; }

    public Dictionary<FaceFeature, List<Sprite>> GetFaceFeatures()
    {
        Dictionary<FaceFeature, List<Sprite>> faceFeatures = new()
        {
            { FaceFeature.Hair, Hairstyle },
            { FaceFeature.Eyes, Eyes },
            { FaceFeature.Eyebrows, Eyebrows },
            { FaceFeature.Ears, Ears },
            { FaceFeature.Nose, Nose },
            { FaceFeature.Mouth, Mouth },
            { FaceFeature.Chin, Chin }
        };

        return faceFeatures;
    }
}
