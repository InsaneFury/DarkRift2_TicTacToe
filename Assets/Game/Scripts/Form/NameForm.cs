using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Scripts.Networking;
using UnityEngine.SceneManagement;

public class NameForm : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInput;
    private bool IsSubmiting = false;
    private bool LoadingScene = false;
    void Start()
    {
        if (!nameInput)
            throw new System.Exception("Missing name input");
    }

    private void Update()
    {
        if(LoadingScene == false && NetworkingManager.Instance.GotMatch)
        {
            LoadingScene = true;
            SceneManager.LoadScene("Match");
        }
    }

    public void OnSubmitClick()
    {
        if (IsSubmiting) return;
        string name = nameInput.text.Trim();

        if (name == string.Empty) return;

        if (NetworkingManager.Instance.ConnectionState == DarkRift.ConnectionState.Connected || NetworkingManager.Instance.Connect())
        {
            IsSubmiting = true;
            NetworkingManager.Instance.MessageNameToServer(name);
        }
    }
}
