using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Stairs : MonoBehaviour
{
    [SerializeField] [Tooltip("The child sprite containing the top step of the stairs")] 
                      Transform topChild;
    [SerializeField] [Tooltip("The child sprite containing the bottom step of the stairs")]
                      Transform bottomChild;

    Vector2 topPos;
    Vector2 bottomPos;

    // Start is called before the first frame update
    void Start()
    {
        // set stairs to be on correct layer, needed for layerMasks in charaController
        gameObject.layer = LayerMask.NameToLayer("Stairs");

        SetStairEnds();
    }

    /// <summary>
    /// Sets the topPos and bottomPos variables,
    /// by checking postions and size of all children making up the stairs.
    /// </summary>
    private void SetStairEnds()
    {
        /* -- decided to serialise these.
        Transform topChild = transform;
        Transform bottomChild = transform;

        foreach (Transform child in transform)  // check each child of Stair component
        {
            if(child.transform.position.y >= topChild.transform.position.y )
            {
                topChild = child;
            }
            if (child.transform.position.y <= bottomChild.transform.position.y)
            {
                bottomChild = child;
            }
        }
        */

        float topY = topChild.transform.position.y +
                     topChild.GetComponent<SpriteRenderer>().bounds.extents.y;

        float bottomY = bottomChild.transform.position.y -
                        bottomChild.GetComponent<SpriteRenderer>().bounds.extents.y;

        topPos = new Vector2(topChild.transform.position.x,
                             topY);
        bottomPos = new Vector2(bottomChild.transform.position.x,
                                bottomY);
    }

    public Vector2 TopPosition { get { return topPos; } }
    public Vector2 BottomPosition { get { return bottomPos; } }


}
