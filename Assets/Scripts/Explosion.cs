using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    const float RADIUS = 2.5f;
    const float FORCE  = 10f;
    const float TREE_FORCE = 0.5f;

    void Awake()
    {
        StartCoroutine(waitAndKill());
    }

    void Start()
    {
        var castHits = Physics2D.CircleCastAll(transform.position.AsVector2(), RADIUS, Vector2.zero);
        foreach (var hit in castHits) {
            if (hit.rigidbody) {
                blowAway(hit.rigidbody, hit.rigidbody.transform.position - transform.position);
            }
        }
    }

    void blowAway(Rigidbody2D rb, Vector2 dir)
    {
        var force = FORCE * dir;
        var tree = rb.GetComponent<TreeController>();

        if (tree) {
            force = TREE_FORCE * dir;
            tree.SendMessage(Messages.SetFire, SendMessageOptions.DontRequireReceiver);
        }

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.localScale = Vector3.one * (transform.localScale.x - 0.1f);
    }

    IEnumerator waitAndKill()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
