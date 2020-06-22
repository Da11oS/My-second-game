using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 originPosition = Testing.Instance.transform.position;
        Pathfinding.Instance.GetGrid().GetXY(transform.position + originPosition, out int x, out int y);
        Testing.Instance.GetComponent<Testing>().SetIsUnwalkable(transform.position);
    }

}
