using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class NodeController : MonoBehaviour {
    public Color selectColor;
    public Color pressedColor;
    public GameObject turret = null;      

    private Color defaultColor;
    private Renderer r;    
    public static GameObject selectedNode = null;

    void Start()
    {
        r = GetComponent<Renderer>();
        defaultColor = r.material.color;
        r.sortingOrder = 1;       
    }   

    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; 
        SelectNode();
        if (turret == null) MenuController.controll.ChangeMenu(0);
        else MenuController.controll.ChangeMenu(turret.GetComponent<TurretController>().index);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (selectedNode != transform.gameObject) r.material.color = selectColor;
    }

    void OnMouseExit()
    {        
        if (transform.gameObject != selectedNode) r.material.color = defaultColor;
    }

    void SelectNode()
    {
        if (selectedNode != null) selectedNode.GetComponent<Renderer>().material.color = defaultColor;
        selectedNode = transform.gameObject;
        selectedNode.GetComponent<Renderer>().material.color = pressedColor;
    }

    public void BuildByType(string type)
    {
        Destroy(turret);
        if (type != null)
        {
            GameObject prefab = (GameObject)Resources.Load(type);
            Vector3 offset = prefab.GetComponent<TurretController>().offset;
            turret = (GameObject)Instantiate(prefab, transform.position + offset, transform.rotation);
        }
    }
}
