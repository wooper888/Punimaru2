using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab = default;

    private void Start()
    {
        Spawns();
    }



    //Ballを自動生成する
    public void Spawns()
    {
        for (int i = 0; i < 10; i++)
        {
        
            Vector2 pos = new Vector2(Random.Range(-0.2f, 0.2f), 8); //Ballが生成される範囲
            Instantiate(ballPrefab, pos, Quaternion.identity);
        }
        
    }







}

