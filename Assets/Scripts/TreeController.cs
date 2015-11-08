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
    public int SplashCount = 5;
    int _splashes = 0;
    bool _Alive = true;
    bool _Burning = false;
    Coroutine burningCoroutine = null;
        
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Message_SetFire()
    {
        if (!_Alive)
            return;
        _spriteRenderer.sprite = fireSprite;
        FireParticals.SetActive(true);
        _Burning = true;
        if(burningCoroutine == null)
            burningCoroutine = StartCoroutine(Burning());
    }

    public void Message_Splash()
    {
        if (!_Alive)
        {
            return;
        }
        else if (_Burning)
        {
            _splashes += 1;
            if (_splashes >= SplashCount)
            {
                if(burningCoroutine != null)
                StopCoroutine(burningCoroutine);
                _Burning = false;

            }
        }
        else
        {
            _spriteRenderer.sprite = greenSprite;
            _splashes = 0;
        }
        
        FireParticals.SetActive(false);
    }

    IEnumerator Burning()
    {
        yield return new WaitForSeconds(LifeTime);
        _Alive = false;
        _spriteRenderer.sprite = deadSprite;
        burningCoroutine = null;
        FireParticals.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
