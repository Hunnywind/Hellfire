using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonCollection : MonoBehaviour {
    public void GameStart()
    {
        Debug.Log("click");
        SceneManager.LoadScene("PlayTest");
    }
}
