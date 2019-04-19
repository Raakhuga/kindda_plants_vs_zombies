using UnityEngine;

public class mouseOver : MonoBehaviour
{
    Shader shader;
    Shader hoverShader;
    bool hover = false;

    void Start()
    {
        shader = Shader.Find("Standard");
        hoverShader = Shader.Find("Custom/HoverObject");
    }

    void OnMouseOver()
    {
        //hover = true;
        //if (GetComponent<Renderer>().CompareTag("tile"))
        //{
        //    GetComponent<Renderer>().material.shader = hoverShader;
        //    Debug.Log("Mouse is over a tile.");
        //}
    }

    void OnMouseExit()
    {
        //hover = false;
        //if (GetComponent<Renderer>().CompareTag("tile"))
        //{
        //    GetComponent<Renderer>().material.shader = shader;
        //    Debug.Log("Mouse is no longer on tile.");
        //}
    }
}
