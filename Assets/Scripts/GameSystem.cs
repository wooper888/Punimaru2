using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{

    //BallGeneratorスクリプトを取得する
    [SerializeField] BallGenerator ballGenerator = default;

    //ドラッグ中かどうかの判定に使用
    bool isDragging;

    //Ballスクリプトを取得してボールをリストに格納する
    [SerializeField] List<Ball> removeBalls = new List<Ball>();

    //触れているボール(最後に追加したボール)
    Ball currentDraggingBall;


    void Start()
    {
        StartCoroutine(ballGenerator.Spawns(40));
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //右クリックを押し込んだ時
            OnDragBegin();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //右クリックを離した時
            OnDragEnd();
        }
        else if (isDragging)
        {
            //ドラッグ中の時
            OnDragging();
        }
    }

    //ドラッグ開始
    void OnDragBegin()
    {
        //Rayによるオブジェクトの当たり判定
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit && hit.collider.GetComponent<Ball>())
        {
            Debug.Log("オブジェクトにヒットしたよ！");
            //ヒットしたボールをリストに追加する
            Ball ball = hit.collider.GetComponent<Ball>();
            AddRemoveBall(ball);
            isDragging = true;
        }
    }

    //ドラッグ中
    void OnDragging()
    {
        //Rayによるオブジェクトの当たり判定
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit && hit.collider.GetComponent<Ball>())
        {
            //ヒットしたボールをリストに追加する
            Ball ball = hit.collider.GetComponent<Ball>();

            //最後に追加したボールのidと同じ、且つ距離が近かったら追加する
            if (ball.id == currentDraggingBall.id)
            {
                float distance = Vector2.Distance(ball.transform.position, currentDraggingBall.transform.position);
                if (distance < 1.0)
                {
                    AddRemoveBall(ball);
                }
            }
        }
    }

    //ドラッグ終了
    void OnDragEnd()
    {
        //リストに追加したボールを削除する
        int removeCount = removeBalls.Count; //リストに追加したボールの数を数える

        //リストに追加したボールが３個以上なら削除する
        if (removeCount >= 3)
        {
            for (int i = 0; i < removeCount; i++)
            {
                Destroy(removeBalls[i].gameObject);
            }
            StartCoroutine(ballGenerator.Spawns(removeCount));
        }
        removeBalls.Clear();
        isDragging = false;

    }

    //すでにドラッグしているボールを再追加しないようにする処理
    void AddRemoveBall(Ball ball)
    {
        //現在触れているボール(最後に追加したボール)
        currentDraggingBall = ball;

        //未追加のボールであれば追加する
        if (removeBalls.Contains(ball) == false)
        {
            removeBalls.Add(ball);
        }
    }
}
