using Scripts.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoBehaviour
{
    [SerializeField]
    private GameObject slateSample;
    [SerializeField]
    private GameObject player1Pawn;
    [SerializeField]
    private GameObject player2Pawn;

    private GameObject[] slateGameObjects;

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

        slateGameObjects = new GameObject[9];

        for (int i = 0; i < 9; i++)
        {
            GameObject slate = Instantiate(slateSample);

            slateGameObjects[i] = slate;

            slate.transform.parent = gameObject.transform;
            slate.transform.position = positions[i];
            BoardSlateControl slateControl = slate.GetComponent<BoardSlateControl>();
            if (slateControl)
            {
                slateControl.index = (ushort)i;
            }
        }

        MatchModel.currentMatch.OnBoardChange.AddListener(OnBoardChanged);
        

    }
    private ushort SlateIndex;
    private MatchModel.SlateStatus SlateStatus = MatchModel.SlateStatus.NONE;
    private void OnBoardChanged(ushort slateIndex, MatchModel.SlateStatus slateStatus)
    {
        Debug.Log("board changed " + slateIndex + " changed to " + slateStatus);

        SlateIndex = slateIndex;
        SlateStatus = slateStatus;

        
    }

    public void SlateCliked(ushort slateIndex)
    {
        MatchModel.currentMatch.ReportSlateTaken(slateIndex);
    }

    private void Update()
    {
        if (SlateStatus != MatchModel.SlateStatus.NONE)
        {
            GameObject pawn;
            if (SlateStatus == MatchModel.SlateStatus.MINE)
            {
                pawn = Instantiate(player1Pawn);
            }
            else
            {
                pawn = Instantiate(player2Pawn);
            }

            GameObject slate = slateGameObjects[SlateIndex];
            pawn.transform.parent = slate.transform;
            pawn.transform.localPosition = new Vector3(0f,0f,0.8f);

            SlateStatus = MatchModel.SlateStatus.NONE;
        }
        
    }
}
