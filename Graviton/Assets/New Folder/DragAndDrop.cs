using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private RectTransform reactTrans;
    public Canvas myCanvas;
    private CanvasGroup canvasGroup;
    public int id;
    private Vector3 initial;

    void Start()
    {
        StorePosition();
    }
    public void Awake()
    {
        
        reactTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
       

    }

    public void StorePosition()
    {
        initial = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("BeginDrag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
         
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On");
        reactTrans.anchoredPosition += eventData.delta / myCanvas.scaleFactor; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("EndDrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("CLICK");
    }

    public void ResetPos()
    {
        transform.position = initial;
    }
   




    /**
        private float startX;
        private float startY;
        private Vector3 reset;
        

        public GameObject correctFormat;
        private bool correct; 
        private bool ismoving;
        
        // Start is called before the first frame update
        void Start()
        {
            reset = this.transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if (correct == false)
            {
                if (ismoving)
                {
                    Vector3 mousepos;
                    mousepos = Input.mousePosition;
                    mousepos = Camera.main.ScreenToWorldPoint(mousepos);

                    this.gameObject.transform.localPosition = new Vector3(mousepos.x - startX, mousepos.y - startY, this.gameObject.transform.localPosition.z);
                }
            }
        }

        private void OnMouseDown()     
        {
            
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousepos;
                    mousepos = Input.mousePosition;
                    mousepos = Camera.main.ScreenToWorldPoint(mousepos);

                    startX = mousepos.x - this.transform.localPosition.x;
                    startY = mousepos.y - this.transform.localPosition.y;

                    ismoving = true;

                }
            
          
        }
        private void OnMouseUp()
        {
            ismoving = false;
            if (Mathf.Abs(this.transform.localPosition.x - correctFormat.transform.localPosition.x) <= 0.5f &&
                Mathf.Abs(this.transform.localPosition.x - correctFormat.transform.localPosition.x) <= 0.5f)
            {
                this.transform.localPosition = new Vector3(correctFormat.transform.localPosition.x, correctFormat.transform.localPosition.y, correctFormat.transform.localPosition.z);
                correct = true;
            }
            else
            {
                this.transform.localPosition = new Vector3(reset.x, reset.y, reset.z);
            }


        }

        */

}
 