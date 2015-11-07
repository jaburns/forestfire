using UnityEngine;
using System.Collections;

public class FireGuyController : MonoBehaviour
{
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
}
