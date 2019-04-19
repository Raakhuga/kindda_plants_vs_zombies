using UnityEngine;

public class RayCast : MonoBehaviour
{

    Shader shader;
    Shader hoverShader;

    void Start()
    {
        shader = Shader.Find("Standard");
        hoverShader = Shader.Find("Custom/HoverObject");
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.magenta);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit) == true)
        {
            if (hit.transform.gameObject.CompareTag("tile"))
            {
                hit.transform.gameObject.GetComponent<Renderer>().material.shader = hoverShader;
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            }
            Debug.Log("Hit " + hit.transform.gameObject.tag);
        }

    }
}
