using UnityEngine;

public class GameInteractionController : MonoBehaviour
{
    private Shader stdShader, selectedShader, hoverShader;
    private GameObject currentTile;
    private bool buttonDown;

    public GameObject Archer;
    private GameObject newArcher;

    private int nrows, ncols;
    private ResourcesController resources;
    private BoardController board;

    public void initGameInteraction()
    {
        stdShader = Shader.Find("Standard");
        selectedShader = Shader.Find("Custom/SelectedObject");
        hoverShader = Shader.Find("Custom/HoverObject");

        currentTile = null;
        buttonDown = false;

        board = GameManager.instance.board;
        nrows = board.nrows;
        ncols = board.ncols;

        resources = GameManager.instance.resources;
    }

    public void Update()
    {

        hoverTiles();

        if (Input.GetMouseButtonDown(0))
        {
            buttonDown = true;
        }

        if (currentTile != null && Input.GetMouseButtonUp(0) && buttonDown
            && !currentTile.GetComponent<TileParams>().activeUnit)
        {
            buttonDown = false;
            if (resources.resources >= Archer.GetComponent<Stats>().gold)
            {
                currentTile.GetComponent<TileParams>().activeUnit = true;
                resources.resources -= Archer.GetComponent<Stats>().gold;
                Vector3 pos = currentTile.transform.position;
                pos.y = 0.06f;
                newArcher = Instantiate(Archer, pos, Archer.transform.rotation);
                newArcher.tag = "ally";
                newArcher.GetComponent<UnitTile>().tile = currentTile;
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
