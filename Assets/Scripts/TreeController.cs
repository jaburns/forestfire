using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour
{
    public Sprite fireSprite;
    public Sprite greenSprite;
    SpriteRenderer _spriteRenderer;
    public GameObject FireParticals;


    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (Random.value < .25f) Message_SetFire();
    }

    public void Message_SetFire()
    {
        _spriteRenderer.sprite = fireSprite;
        FireParticals.SetActive(true);
    }

    public void Message_Splash()
    {
        _spriteRenderer.sprite = greenSprite;
        FireParticals.SetActive(false);
    }
}
