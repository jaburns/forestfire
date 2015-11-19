using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    public GameObject TreePrefab;
    public Vector2 SpawnRange;
    public int SpawnCount;
    public float SmallSize;
    public float LargeSize;
    public Vector2 KeepOutWindow;
    

    void Awake()
    {
        for (int i = 0; i < SpawnCount; ++i) {
            
            float x = (.5f - Random.value) * SpawnRange.x;
            float y = y = (.5f - Random.value) * SpawnRange.y;
            while (Mathf.Abs(x) < KeepOutWindow.x && Mathf.Abs(y) < KeepOutWindow.y)
            {
                x = (.5f - Random.value) * SpawnRange.x;
                y = y = (.5f - Random.value) * SpawnRange.y;
            }

            spawnTree(new Vector2 (x, y));
        }
    }

    TreeController spawnTree(Vector2 pos)
    {
        var treeObj = Instantiate(TreePrefab, pos.AsVector3(), Quaternion.identity) as GameObject;
        treeObj.transform.localScale *= Random.value * (LargeSize - SmallSize) + SmallSize;
        treeObj.transform.parent = transform;
   //     treeObj.transform.rotation = Quaternion.EulerAngles(0, 0, Random.value * 2000);

        var treeSpring = treeObj.GetComponent<SpringJoint2D>();
        treeSpring.connectedAnchor = treeObj.transform.position.AsVector2();

        return treeObj.GetComponent<TreeController>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SpawnRange.AsVector3());
    }
}
