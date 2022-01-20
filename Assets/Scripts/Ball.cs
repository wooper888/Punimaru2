using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //ボールの種類を判別するためのID
    public int id;

    //爆破エフェクトのPrefabを格納する枠
    [SerializeField] GameObject explosionPrefab = default;


    //爆破エフェクト
    public void Explosion()
    {
        //ボールの破壊
        Destroy(gameObject);

        //エフェクトの生成
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        //エフェクトの破壊
        Destroy(explosion, 0.2f); //0.2秒後に破壊する
    }
}
