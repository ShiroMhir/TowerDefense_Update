using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More that one BuildManager!");
            return;
        }
        instance = this;
    }

    public GameObject BuildEffect;
    public GameObject SellEffect;
    public NodeUI nodeUI;

    [Header("Turrets")]
    public GameObject StandardTurretPrefab;
    public GameObject MissileTurretPrefab;
    public GameObject SecondaryTurretPrefab;

    private TurretBlueprints turretToBuild;
    private Node selectedNode;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    
    public void SelectTurretToBuild(TurretBlueprints turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null!;
        nodeUI.Hide();
    }

    public TurretBlueprints GetTurretToBuild()
    {
        return turretToBuild;
    }
}
