using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PointEffect : MonoBehaviour
{
    //スコア表示の枠
    [SerializeField] Text text = default;

    //スコアを表示する
    public void Show(int score)
    {
        text.text = score.ToString();
        StartCoroutine(MoveUP()); //上にあげる
    }

    //スコアの表示を上にあげる
    IEnumerator MoveUP()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(0, 0.1f, 0);          
        }
        Destroy(gameObject, 0.2f);
    }


}
