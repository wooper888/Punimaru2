using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    //BallPrefabを入れる枠
    [SerializeField] GameObject ballPrefab = default;

    //Ballの画像を設定する
    [SerializeField] Sprite[] ballSprites = default;

    //ボムの画像をを設定する
    [SerializeField] Sprite bombSprite = default;

    //Ballを自動生成する
    public IEnumerator Spawns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-0.2f, 0.2f), 8); //Ballが生成される範囲
            //ボールを生成して変数に格納
            GameObject ball = Instantiate(ballPrefab, pos, Quaternion.identity);

            //ballに画像を設定
            int ballID = Random.Range(0, ballSprites.Length);

            //ボムの生成
            if (Random.Range(0, 100) < 5)　//何％の確率でボムを生成するか
            {
                ballID = -1;
                ball.GetComponent<SpriteRenderer>().sprite = bombSprite; //ボムの画像を表示
            }
            else
            {
                ball.GetComponent<SpriteRenderer>().sprite = ballSprites[ballID]; //ボム以外は通常のボールを表示
            }
            
            ball.GetComponent<Ball>().id = ballID;
            yield return new WaitForSeconds(0.04f);
        }
        
    }







}

