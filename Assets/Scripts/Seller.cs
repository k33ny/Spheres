using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Seller : MonoBehaviour {
    public Button button;
    public Text nameText, valueText;
    public Sprite sprite;
    private TurretController currentStructure;
    public static float sellRatio = 0.8f;

    private int value;

    void OnEnable()
    {
        currentStructure = NodeController.selectedNode.GetComponent<NodeController>().turret.GetComponent<TurretController>();
        button.image.sprite = sprite;
        nameText.text = name;
        value = (int)(currentStructure.price * sellRatio);
        valueText.text = "+" + value.ToString() + "$";
    }

    public void Sell()
    {        
        GameObject soldTurret = NodeController.selectedNode.GetComponent<NodeController>().turret;
        NodeController.selectedNode.GetComponent<NodeController>().turret = null;       
        Destroy(soldTurret);
        StatMaster.controll.AddGold(value);
        MenuController.controll.ChangeMenu(0);
    }

}
