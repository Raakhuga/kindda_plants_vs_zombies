using UnityEngine;

public class MousePointer : MonoBehaviour
{

    Shader shader;
    Shader hoverShader;
    private Renderer currentTile;

    void Start()
    {
        shader = Shader.Find("Standard");
        hoverShader = Shader.Find("Custom/HoverObject");
        currentTile = null;
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.magenta);

        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0F);

        // Search if we hit a tile
        if (currentTile) currentTile.material.shader = shader; // reset currentTile (if we hit nothing, clean last tile selected)
        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.transform.GetComponent<Renderer>();
            if (rend.CompareTag("tile"))
            {
                if (currentTile) currentTile.material.shader = shader;
                currentTile = rend;

                rend.material.shader = hoverShader;

                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            }
        }
    }
}
