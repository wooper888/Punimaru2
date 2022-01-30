using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    //スタートボタンを押下したらゲームシーンに遷移
    public void OnStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
