using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public float RADIUS = 5f;
    public float FORCE  = 10f;
    public float TREE_FORCE = 0.5f;
    public float TREE_RADIUS = 2.5f;

    void Awake()
    {
        StartCoroutine(waitAndKill());
    }

    void Start()
    {
        Camera.main.gameObject.SendMessage(Messages.StartCameraShake);

        var castHits = Physics2D.CircleCastAll(transform.position.AsVector2(), RADIUS, Vector2.zero);
        foreach (var hit in castHits) {
            if (hit.rigidbody && hit.rigidbody.gameObject.GetComponent<TreeController>() == null) {
                blowAway(hit.rigidbody, hit.rigidbody.transform.position - transform.position, FORCE);
            }
        }

        castHits = Physics2D.CircleCastAll(transform.position.AsVector2(), TREE_RADIUS, Vector2.zero);
        foreach (var hit in castHits) {
            if (hit.rigidbody && hit.rigidbody.gameObject.GetComponent<TreeController>()) {
                blowAway(hit.rigidbody, hit.rigidbody.transform.position - transform.position, TREE_FORCE);
            }
        }

        Debug.DrawLine(transform.position  + Vector3.right * RADIUS, transform.position  - Vector3.right * RADIUS, Color.red, 1);
        Debug.DrawLine(transform.position  + Vector3.up * RADIUS, transform.position  - Vector3.up * RADIUS, Color.red, 1);
    }

    void blowAway(Rigidbody2D rb, Vector2 dir, float forceMag)
    {
        dir = dir.normalized;
        var force = forceMag * dir;
        rb.gameObject.SendMessage(Messages.SetFire, SendMessageOptions.DontRequireReceiver);
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
