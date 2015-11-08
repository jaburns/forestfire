using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour
{
    public Material FireMaterial;

    Renderer _renderer;
    Material _originalMaterial;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.material;

        //if (Random.value < .5f) Message_SetFire();
    }

    public void Message_SetFire()
    {
        _renderer.material = FireMaterial;
    }

    public void Message_Splash()
    {
        _renderer.material = _originalMaterial;
    }
}
