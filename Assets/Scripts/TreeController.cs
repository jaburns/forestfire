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
    Coroutine burningCoroutine = null;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Message_SetFire()
    {
        if (burningCoroutine != null) {
            return;
        }

        _spriteRenderer.sprite = fireSprite;
        FireParticals.SetActive(true);
        burningCoroutine = StartCoroutine(Burning());
    }

    public void Message_Splash()
    {
        if (burningCoroutine != null) {
            _splashes += 1;
            if (_splashes >= SplashCount) {
                StopCoroutine(burningCoroutine);
                burningCoroutine = null;
                FireParticals.SetActive(false);
            }
        } else {
            _spriteRenderer.sprite = greenSprite;
            _splashes = 0;
        }
    }

    IEnumerator Burning()
    {
        yield return new WaitForSeconds(LifeTime);
        _spriteRenderer.sprite = deadSprite;
        burningCoroutine = null;
        FireParticals.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        Destroy(this);
    }
}
