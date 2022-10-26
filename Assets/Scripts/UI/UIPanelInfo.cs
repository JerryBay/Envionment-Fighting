using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelInfo : UIPanelBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject productionObj;
    [SerializeField] private Text productionNameText;
    [SerializeField] private Text productionDescText;
    [SerializeField] private Text productionText;
    [SerializeField] private Text pollutionText;
    [SerializeField] private GameObject defenseObj;
    [SerializeField] private Text defenseNameText;
    [SerializeField] private Text defenseDescText;
    [SerializeField] private Text defenseAttackText;
    [SerializeField] private Text defenseAttackAreaText;
    [SerializeField] private GameObject monsterObj;

    private BaseBuilding building;

    /// <summary>
    /// 展示建筑信息
    /// </summary>
    public void ShowBuildingInfo(BaseBuilding building)
    {
        this.building = building;
        monsterObj.SetActive(false);
        if (building is ProductionBuilding) // 更新生产建筑展示信息
        {
            productionObj.SetActive(true);
            defenseObj.SetActive(false);

            var config = (building as ProductionBuilding).productionBuildingConfig;
            productionNameText.text = config.name;
            productionDescText.text = config.desc;
            productionText.text = config.productionRate.ToString();
            pollutionText.text = config.polluteRate.ToString();
        }
        else if (building is DefenseTower) // 更新防御建筑展示信息
        {
            productionObj.SetActive(false);
            defenseObj.SetActive(true);

            var config = (building as DefenseTower).defenseBuildingConfig;
            defenseNameText.text = config.name;
            defenseNameText.text = config.desc;
            defenseAttackText.text = config.damage.ToString();
            defenseAttackAreaText.text = config.attackArea.ToString();
        }
        else
        {
            Debug.LogError("error");
        }
    }

    public void OnBuildingUpgradeButtonClick()
    {
        if (building != null)
            EventManager.Dispath(GameEvent.UI_BuildingUpgrade, building);
    }

    public void OnBuildingRemoveButtonClick()
    {
        if (building != null)
            EventManager.Dispath(GameEvent.UI_BuildingRemove, building);
    }

    public void AnimEvent_Hide()
    {
        Hide();
    }
}