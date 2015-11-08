using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour
{
    public Sprite fireSprite;
    public Sprite greenSprite;
    public Sprite deadSprite;

    SpriteRenderer _spriteRenderer;
    public GameObject FireParticals;
    public float LifeTime = 10f;

    bool Alive = true;
    
        
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
       // if (Random.value < .25f) Message_SetFire();
    }

    public void Message_SetFire()
    {
        _spriteRenderer.sprite = fireSprite;
        FireParticals.SetActive(true);
    }

    public void Message_Splash()
    {
        if (!Alive)
        {
            return;
        }

        _spriteRenderer.sprite = greenSprite;
        FireParticals.SetActive(false);
    }


}
