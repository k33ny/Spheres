using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [Header("Mouse/Keyboard Properties")]
    public bool toggleMove = true;
    public float panSpeed = 30f;
    public float panOffset = 10f;
    public float maxVer = 20f;
    public float minVer = -20f;
    public float maxHor = 20f;
    public float minHor = -20f;
    [Header("Scroll Wheel Properties")]
    public float scrollSpeed = 5f;   
    public float minHeight = 10f;
    public float maxHeight = 80f;

    private Camera camera;    

    void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        
        if (!toggleMove) return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panOffset)
        {
            Vector3 pan = Vector3.forward * panSpeed * Time.deltaTime;
            if ((transform.position + pan).z <= maxVer && (transform.position + pan).z >= minVer) transform.Translate(pan, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panOffset)
        {
            Vector3 pan = Vector3.back * panSpeed * Time.deltaTime;            
            if ((transform.position + pan).z <= maxVer && (transform.position + pan).z >= minVer) transform.Translate(pan, Space.World);            
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panOffset)
        {
            Vector3 pan = Vector3.left * panSpeed * Time.deltaTime;
            if ((transform.position + pan).x <= maxHor && (transform.position + pan).x >= minHor) transform.Translate(pan, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panOffset)
        {
            Vector3 pan = Vector3.right * panSpeed * Time.deltaTime;
            if ((transform.position + pan).x <= maxHor && (transform.position + pan).x >= minHor) transform.Translate(pan, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float zoom = camera.fieldOfView - scroll * scrollSpeed * Time.deltaTime * 1000;
        camera.fieldOfView = Mathf.Clamp(zoom, minHeight, maxHeight);             
    }
}
