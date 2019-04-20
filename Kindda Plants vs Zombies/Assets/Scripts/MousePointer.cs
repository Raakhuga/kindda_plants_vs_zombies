using UnityEngine;

public class MousePointer : MonoBehaviour
{

    Shader stdShader, selectedShader, hoverShader;
    private Renderer currentTile;

    void Start()
    {
        stdShader = Shader.Find("Standard");
        selectedShader = Shader.Find("Custom/SelectedObject");
        hoverShader = Shader.Find("Custom/HoverObject");
        currentTile = null;
    }

    private void setTileShaders(GameObject initTile, Shader sh)
    {
        int x = initTile.GetComponent<TileParams>().x;
        int y = initTile.GetComponent<TileParams>().y;

        for (int i = 0; i < 9; i++)
        {
            Renderer tile = GameObject.Find("tile " + i + " " + y).GetComponent<Renderer>();
            tile.material.shader = sh;
        }

        for (int j = 0; j < 5; j++)
        {
            Renderer tile = GameObject.Find("tile " + x + " " + j).GetComponent<Renderer>();
            tile.material.shader = sh;
        }
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.magenta);

        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0F);

        // reset currentTile (if we hit nothing, clean last tile selected)
        if (currentTile)
        {
            setTileShaders(currentTile.gameObject, stdShader);
        }
        currentTile = null;
        // Search if we hit a tile
        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.transform.GetComponent<Renderer>();
            if (rend.CompareTag("tile"))
            {
                if (currentTile)
                {
                    setTileShaders(currentTile.gameObject, stdShader);
                }
                currentTile = rend;
                setTileShaders(currentTile.gameObject, hoverShader);
                currentTile.material.shader = selectedShader;

                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            }
        }
        Debug.Log(currentTile == null ? "No tile selected."
            : "Tile " + currentTile.gameObject.name + " selected");
    }

}
