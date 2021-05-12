using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Camera mapCamera;
    public GameObject mapWindow;
    public RenderTexture fullMapWindow;
    public Texture fullMapWindow2;
    private RawImage size;
    private RectTransform rt;
    private bool fullMap = false;
    private Vector3 mapPosition;
    // Start is called before the first frame update
    void Start()
    {
        size = mapWindow.GetComponent<RawImage>();
        rt = mapWindow.GetComponent<RectTransform>();
        mapPosition = rt.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Zoom in") && mapCamera.orthographicSize > 0) // zoom in and out
        {
            mapCamera.orthographicSize = mapCamera.orthographicSize - 5.0f;
        }

        if (Input.GetButton("Zoom out"))
        {
            mapCamera.orthographicSize = mapCamera.orthographicSize + 5.0f;
        }

        if (Input.GetButtonDown("FullMap") && fullMap == false) // change size existing minimap to 1920x1080 and position to the middle of the screen
        {
            rt.sizeDelta = new Vector2(1920, 1080);
            //size.SetNativeSize();
            rt.localPosition = new Vector3(0, 0, 0);
            //mapCamera.targetTexture = fullMapWindow;
            //size.texture = fullMapWindow2;
            fullMap = true;
        }
        else if (Input.GetButtonDown("FullMap") && fullMap == true)
        {
            rt.sizeDelta = new Vector2(640, 360); // change size existing minimap to 640x360 and position to the previous position
            //size.SetNativeSize();
            rt.localPosition = mapPosition;
            //mapCamera.targetTexture = fullMapWindow;
            //size.texture = fullMapWindow2;
            fullMap = false;
        }
    }
}
