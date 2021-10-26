using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 posOffset;


    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    [Header("Optional")]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprints turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;


    void Start()
    {
        rend = GetComponent<Renderer> ();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + posOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        
        BuildTurret(buildManager.GetTurretToBuild());
    }

    // Building Turret
    void BuildTurret(TurretBlueprints blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money!");
            return;
        }
        // Torrent building
        PlayerStats.Money -= blueprint.cost;
        turretBlueprint = blueprint;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
               

        TextMesh text = buildManager.BuildEffect.GetComponentInChildren<TextMesh>();
        text.text = "-$" + blueprint.cost.ToString();
        GameObject effect = (GameObject)Instantiate(buildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    // Upgrade Turret
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade!");
            return;
        }
        // Torrent building
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        // Destroy existing turret
        Destroy(turret);

        // Building upgraded turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        TextMesh text = buildManager.BuildEffect.GetComponentInChildren<TextMesh>();
        text.text = "-$" + turretBlueprint.upgradeCost.ToString();
        GameObject effect = (GameObject)Instantiate(buildManager.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    // Sell Turret
    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.sellPrice();
        
        // Sell Effect
        TextMesh text = buildManager.SellEffect.GetComponentInChildren<TextMesh>();
        text.text = "+$" + turretBlueprint.sellPrice().ToString();
        GameObject effect = (GameObject)Instantiate(buildManager.SellEffect, GetBuildPosition(), Quaternion.identity);

        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        } else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}