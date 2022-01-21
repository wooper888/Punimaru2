using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //スコア
    int score;

    //スコアを入れる枠
    [SerializeField] Text scoreText = default;

    //ポイントエフェクトのPrefabを入れる枠
    [SerializeField] GameObject pointEffectPrefab = default;

    //タイマーテキストを入れるための枠
    [SerializeField] Text timerText = default;

    //タイマー
    int timeCount;

    //リザルトパネルを表示する枠
    [SerializeField] GameObject resultPanel = default;




    void Start()
    {
        timeCount = 5; //タイマーの初期値
        score = 0; //スコアの初期化
        AddScore(0); //スコアの表示
        StartCoroutine(ballGenerator.Spawns(ParamsSO.Entity.initBallCount)); //初期のボール生成
        StartCoroutine(CountDown()); //カウントダウンタイマー
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
            //ヒットしたボールをリストに追加する
            Ball ball = hit.collider.GetComponent<Ball>();

            //ボールの種類を判定
            if (ball.IsBomb())
            {
                //ボムなら爆破
                BombExplosion(ball);
            }
            else
            {
                //ボムじゃなければリストに追加
                AddRemoveBall(ball);
                isDragging = true;
            }
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
        int removeCount = removeBalls.Count; //リストに追加したボールの数を数えるc

        //リストに追加したボールが３個以上なら削除する
        if (removeCount >= 3)
        {
            for (int i = 0; i < removeCount; i++)
            {
                removeBalls[i].Explosion(); //爆破エフェクトとボールの破壊
            }
            StartCoroutine(ballGenerator.Spawns(removeCount)); //消した分だけボールを生成する
            int score = removeCount * 100; //スコアを計算
            AddScore(score); //スコアを表示
            SpawnPointEffect(removeBalls[removeBalls.Count-1].transform.position, score); //ポイントエフェクトの表示
        }

        //リストに追加したボールのサイズと色を元に戻す
        for (int i = 0; i < removeCount; i++)
        {
            removeBalls[i].transform.localScale = Vector3.one * 3;
            removeBalls[i].GetComponent<SpriteRenderer>().color = Color.white;
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
            ball.transform.localScale = Vector3.one * 3 * 1.4f;　//ボールの大きさを大きくする
            ball.GetComponent<SpriteRenderer>().color = Color.yellow;　//ボールの色を変える
            removeBalls.Add(ball);
        }
    }

    //スコアの表示
    void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    //ボムによる爆破
    void BombExplosion(Ball bomb)
    {
        //爆破リストの作成
        List<Ball> explosionList = new List<Ball>();

        //ボムを中心に爆破するボールを集める
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(bomb.transform.position, ParamsSO.Entity.bombRange);

        for (int i = 0; i < hitObj.Length; i++)
        {
            //ボールかどうか判定する
            Ball ball = hitObj[i].GetComponent<Ball>();
            if (ball)
            {
                explosionList.Add(ball); //ボールなら爆破リストに追加する
            }
        }
        //爆破する
        int removeCount = explosionList.Count; //リストに追加したボールの数を数える

        for (int i = 0; i < removeCount; i++)
        {
            explosionList[i].Explosion(); //爆破
        }
        StartCoroutine(ballGenerator.Spawns(removeCount)); //消した分だけボールを生成する
        int score = removeCount * 100; //スコアの計算
        AddScore(score); //スコアを表示
        SpawnPointEffect(bomb.transform.position, score); //ポイントエフェクトの表示
    }

    //ポイントエフェクトを生成する
    void SpawnPointEffect(Vector2 position, int score)
    {
        //ポイントエフェクトを生成して変数に入れる
        GameObject effectObj =  Instantiate(pointEffectPrefab, position, Quaternion.identity);
        //エフェクトのクラスを取得
        PointEffect pointEffect = effectObj.GetComponent<PointEffect>();
        //エフェクトの取得
        pointEffect.Show(score);
    }

    //カウントダウンタイマー
    IEnumerator CountDown()
    {
        //１秒ごとにタイマーの表示を減らしていく
        while (timeCount > 0)
        {
            yield return new WaitForSeconds(1);
            timeCount--;
            timerText.text = timeCount.ToString();
        }
        Debug.Log("タイムアップ");

        //リザルトパネルを表示する
        resultPanel.SetActive(true);
    }

    //リトライ機能
    public void OnRetryButton()
    {
        //同じシーンを再読み込みする
        SceneManager.LoadScene("Gamescene");
    }



}
