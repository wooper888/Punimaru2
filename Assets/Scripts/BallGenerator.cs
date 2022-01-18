using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    //BallPrefabを入れる枠
    [SerializeField] GameObject ballPrefab = default;

    //Ballの画像を設定する
    [SerializeField] Sprite[] ballSprites = default;


    //Ballを自動生成する
    public IEnumerator Spawns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-0.2f, 0.2f), 8); //Ballが生成される範囲
            //生成したボールを変数に格納
            GameObject ball = Instantiate(ballPrefab, pos, Quaternion.identity);
            //ballに画像を設定
            int ballID = Random.Range(0, ballSprites.Length);
            ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballID];
            ball.GetComponent<Ball>().id = ballID;
            
            yield return new WaitForSeconds(0.04f);
        }
        
    }







}

