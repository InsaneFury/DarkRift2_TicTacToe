using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoBehaviour
{
    [SerializeField]
    private GameObject slateSample;

    void Start()
    {
        Vector3[] positions = new Vector3[]
        {
        new Vector3(-1.0f,1.0f),
        new Vector3(0f,1.0f),
        new Vector3(1.0f,1.0f),

        new Vector3(-1.0f,0.0f),
        new Vector3(0f,0.0f),
        new Vector3(1.0f,0.0f),

        new Vector3(-1.0f,-1.0f),
        new Vector3(0f,-1.0f),
        new Vector3(1.0f,-1.0f),
        };

        for (int i = 0; i < 9; i++)
        {
            GameObject slate = Instantiate(slateSample);
            slate.transform.parent = gameObject.transform;
            slate.transform.position = positions[i];
            BoardSlateControl slateControl = slate.GetComponent<BoardSlateControl>();
            if (slateControl)
            {
                slateControl.index = i;
            }
        }
    }

    public void SlateCliked(int slateIndex)
    {
        Debug.Log("slate clicked " + slateIndex);
    }
}
