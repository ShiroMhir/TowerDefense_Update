using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public GameObject ShopMenu;
    public Animator shopAnimator;

    [Header("Turrets")]
    public TurretBlueprints StandardTurret;
    public TurretBlueprints MissileTurret;
    public TurretBlueprints SecondaryTurret;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(StandardTurret);
    }
    
    public void SelectMissileTurret()
    {
        buildManager.SelectTurretToBuild(MissileTurret);
    }

    public void SelectSecondaryTurret()
    {
        buildManager.SelectTurretToBuild(SecondaryTurret);
    }

    public void ToogleShop()
    {
        shopAnimator.SetBool("ShopSelected", !shopAnimator.GetBool("ShopSelected"));
        
        
        ShopMenu.SetActive(!ShopMenu.activeSelf);
        return;
    }
}
