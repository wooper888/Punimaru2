using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] //おまじない
public class ParamsSO : ScriptableObject
{
    [Header("初期のボールの数")]
    public int initBallCount;

    [Header("ボムの範囲")]
    public float bombRange;


    //以下ParamsSOの便利設定
    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ParamsSO";

    //MyScriptableObjectの実体
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }
}
