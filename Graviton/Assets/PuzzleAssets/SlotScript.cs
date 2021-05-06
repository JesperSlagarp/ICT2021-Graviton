using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{

    public int id;

    public void OnDrop(PointerEventData eventData)
    {
      if ( eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
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




