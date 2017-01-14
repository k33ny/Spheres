using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TooltipController : MonoBehaviour {

    private Transform tooltip;
    private Transform infoPanel;   
    private bool isActive = false;

    
    private void Awake()
    {
        tooltip = transform.GetChild(transform.childCount - 1);
        tooltip.gameObject.SetActive(isActive);
        infoPanel = tooltip.GetChild(0);
    }

    private void Start()
    {
        tooltip.FindChild("Name").GetComponent<Text>().text = transform.FindChild("Name").GetComponent<Text>().text;
        if (infoPanel.childCount <= 1) return;
        TurretController prefab = GetComponent<BuilderBtn>().prefab.GetComponent<TurretController>();
        infoPanel.FindChild("Damage").GetComponent<Text>().text = "Damage: " + prefab.damage;
        infoPanel.FindChild("FireRate").GetComponent<Text>().text = "FireRate: " + prefab.fireRate;
        infoPanel.FindChild("Range").GetComponent<Text>().text = "Range: " + prefab.range;
        infoPanel.FindChild("Misc").GetComponent<Text>().text = prefab.description;
    }


    public void SetActive(bool state)
    {
        isActive = state;
        tooltip.gameObject.SetActive(state);
    }

    public void StartFollowing()
    {
        StartCoroutine(FollowMouse());
    }

    IEnumerator FollowMouse()
    {
        while (isActive)
        {
            tooltip.position = Input.mousePosition;
            yield return null;
        }
    }
}
