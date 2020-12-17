using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlateControl : MonoBehaviour
{
    public ushort index;
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(transform.parent != null)
            {
                BoardControl board = transform.parent.gameObject.GetComponent<BoardControl>();
                if (board)
                {
                    board.SlateCliked(index);
                }
            }
        }
    }
}
