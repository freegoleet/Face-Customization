using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Character_Create_ColorPicker : MonoBehaviour
{
    //[SerializeField]
    //private Texture2D m_ColorChart = null;
    //[SerializeField]
    //private GameObject m_Chart = null;
    //[SerializeField]
    //private RectTransform m_Cursor = null;
    //[SerializeField]
    //private Image m_Button = null;
    //[SerializeField]
    //private Image m_CursorColor = null;

    //[SerializeField]
    //private Image m_TargetImage = null;

    //public UnityEvent<Color> ColorPickerEvent = null;

    //public void PickColor(BaseEventData data)
    //{
    //    PointerEventData pointer = data as PointerEventData;
    //    m_Cursor.position = pointer.position;

    //    var rect = transform.GetChild(0).GetComponent<RectTransform>().rect;

    //    Color pickedColor = m_ColorChart.GetPixel(
    //        (int)(m_Cursor.localPosition.x * ( m_ColorChart.height / rect.width)), 
    //        (int)(m_Cursor.localPosition.y * (m_ColorChart.height / rect.height))
    //        );

    //    m_Button.color = pickedColor;
    //    m_CursorColor.color = pickedColor;
    //    ColorPickerEvent.Invoke(pickedColor);
    //}

    //public void SetColor(Color color)
    //{
    //    m_TargetImage.color = color;
    //}
}
