using UnityEngine;

public class GameInteractionController : MonoBehaviour
{
    private Shader stdShader, selectedShader, hoverShader;
    private GameObject currentTile;

    public GameObject Priest;
    public GameObject Archer;
    public GameObject Paladin;
    public GameObject Barricade;

    private Transform allies;
    private GameObject[] units = new GameObject[4];
    public int selectedUnitIdx;
    private GameObject newUnit;
    public string selectedUnitName;

    private int nrows, ncols;
    private ResourcesController resources;
    private BoardController board;
    private GoldGen goldGen;

    public void initGameInteraction()
    {
        stdShader = Shader.Find("Standard");
        selectedShader = Shader.Find("Custom/SelectedObject");
        hoverShader = Shader.Find("Custom/HoverObject");

        currentTile = null;

        board = GameManager.instance.board;
        nrows = board.nrows;
        ncols = board.ncols;

        resources = GameManager.instance.resources;
        allies = new GameObject("allies").transform;
        units[0] = Priest;
        units[0].name = "priest";
        units[1] = Archer;
        units[1].name = "archer";
        units[2] = Paladin;
        units[2].name = "paladin";
        units[3] = Barricade;
        units[3].name = "barricade";
        selectedUnitIdx = 1;
        selectedUnitName = units[selectedUnitIdx].name;

        goldGen = GameManager.instance.goldGenerator;
    }

    public void Update()
    {

        hoverTiles();
        keyboardInput();
        mouseInput();

        selectedUnitName = units[selectedUnitIdx].name;
    }

    public void selectUnit(int unitId)
    {
        selectedUnitIdx = (unitId - 1) % units.Length;
    }

    private void keyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedUnitIdx = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedUnitIdx = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedUnitIdx = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedUnitIdx = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            selectedUnitIdx = (selectedUnitIdx + 1) % units.Length;
        }
    }

    private void mouseInput()
    {

        if (Input.GetMouseButtonUp(0))
        {
            if (currentTile != null)
            {
                GameObject unit = currentTile.GetComponent<TileParams>().tileUnit;
                GameObject goldBag = currentTile.GetComponent<TileParams>().tileGoldBag;

                if (unit == null)
                {
                    if (resources.resources >= units[selectedUnitIdx].GetComponent<Stats>().gold && goldBag == null)
                    {
                        resources.resources -= units[selectedUnitIdx].GetComponent<Stats>().gold;
                        Vector3 pos = currentTile.transform.position;
                        pos.y = 0.06f;
                        newUnit = Instantiate(units[selectedUnitIdx], pos, units[selectedUnitIdx].transform.rotation, allies) as GameObject;
                        newUnit.tag = "ally";
                        newUnit.name = units[selectedUnitIdx].name;
                        newUnit.GetComponent<UnitTile>().tile = currentTile;
                        currentTile.GetComponent<TileParams>().tileUnit = newUnit;
                    }
                }
                else
                {                    
                    if (unit != null && unit.name == "priest")
                    {
                        if (unit.GetComponent<PriestController>().currentGold == unit.GetComponent<PriestController>().maxGold)
                        {
                            unit.GetComponent<PriestController>().takeGold();
                        }
                    }                    
                }

                if (goldBag != null)
                {
                    resources.resources += goldBag.GetComponent<MoneyBagController>().gold;
                    Destroy(goldBag);
                    goldGen.numBags -= 1;
                }
            }
        }
    }

    private void setTileShaders(GameObject initTile, Shader sh)
    {
        int pi = initTile.GetComponent<TileParams>().pi;
        int pj = initTile.GetComponent<TileParams>().pj;

        for (int i = 0; i < nrows; i++)
        {
            Renderer tile = board.board[i * ncols + pj].GetComponent<Renderer>();
            tile.material.shader = sh;
        }

        for (int j = 0; j < ncols; j++)
        {
            Renderer tile = board.board[pi * ncols + j].GetComponent<Renderer>();
            tile.material.shader = sh;
        }
    }

    // Changes shader of Tiles on hover.
    void hoverTiles()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.magenta);

        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0F);

        // reset currentTile (if we hit nothing, clean last tile selected)
        if (currentTile != null)
        {
            setTileShaders(currentTile, stdShader);
            currentTile = null;
        }
        // Search if we hit a tile
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("tile"))
            {
                GameObject hitObj = hit.transform.gameObject;
                if (currentTile != null)
                {
                    setTileShaders(currentTile.gameObject, stdShader);
                }
                currentTile = hitObj;
                setTileShaders(currentTile.gameObject, hoverShader);
                currentTile.GetComponent<Renderer>().material.shader = selectedShader;

                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                break;
            }
        }
    }

}
