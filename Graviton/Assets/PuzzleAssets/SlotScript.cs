using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{

    public int id1;
    public int id2;
    public int id3;

    public void OnDrop(PointerEventData eventData)
    {
      if ( eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id1 == id1 || eventData.pointerDrag.GetComponent<DragAndDrop>().id2 == id2 || eventData.pointerDrag.GetComponent<DragAndDrop>().id3 == id3)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                GameObject.Find("PointHandler").GetComponent<WinText>().Addpoints();
            }
            else
            {
                eventData.pointerDrag.GetComponent<DragAndDrop>().ResetPos();
            }
            
        }
    }
}




