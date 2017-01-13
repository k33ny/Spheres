using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuilderBtn : MonoBehaviour {    
    [Header("Turret Prefabs")]
    public GameObject prefab;   
    public Button button;
    public Text nameText, valueText;   
    
    private int price;
    private StatMaster stats;  

    void Start()
    {
        stats = StatMaster.controll;
        nameText.text = prefab.GetComponent<TurretController>().name;
        price = prefab.GetComponent<TurretController>().price;        
        valueText.text = (-price).ToString() + "$";
        button.image.sprite = prefab.GetComponent<TurretController>().sprite;
    }

    public void BuildTurret()
    {
        if (stats.CanAfford(price))
        {            
            if (NodeController.selectedNode.GetComponent<NodeController>().turret != null)
            {
                GameObject oldTurret = NodeController.selectedNode.GetComponent<NodeController>().turret;
                Destroy(oldTurret);
            }            
            Vector3 offset = prefab.GetComponent<TurretController>().offset;
            GameObject turret = (GameObject)Instantiate(prefab, NodeController.selectedNode.transform.position + offset, NodeController.selectedNode.transform.rotation);
            NodeController.selectedNode.GetComponent<NodeController>().turret = turret;
            MenuController.controll.ChangeMenu(turret.GetComponent<TurretController>().index);
            stats.AddGold(-price);         
        }
    }     
}
