using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    //遊び方パネルを表示する枠
    [SerializeField] GameObject houToPanel = default;

    //あそびかた１を表示する枠
    [SerializeField] GameObject houTo1Image = default;

    //あそびかた２を表示する枠
    [SerializeField] GameObject houTo2Image = default;

    //あそびかた３を表示する枠
    [SerializeField] GameObject houTo3Image = default;

    //あそびかた４を表示する枠
    [SerializeField] GameObject houTo4Image = default;

    //スタートボタンを押下したらゲームシーンに遷移
    public void OnStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    //あそびかたボタンを押下したらHowToPanelを表示する
    public void OnHowToButton()
    {
        houToPanel.SetActive(true);
        houTo1Image.SetActive(true);
        houTo2Image.SetActive(false);
        houTo3Image.SetActive(false);
        houTo4Image.SetActive(false);
    }

    //NextButton1を押下した時
    public void OnNextButton1()
    {
        houTo1Image.SetActive(false);
        houTo2Image.SetActive(true);
    }

    //NextButton2を押下した時
    public void OnNextButton2()
    {
        houTo2Image.SetActive(false);
        houTo3Image.SetActive(true);
    }

    //BackButton2を押下した時
    public void OnBackButton2()
    {
        houTo1Image.SetActive(true);
        houTo2Image.SetActive(false);
    }

    //NextButton3を押下した時
    public void OnNextButton3()
    {
        houTo3Image.SetActive(false);
        houTo4Image.SetActive(true);
    }

    //BackButton3を押下した時
    public void OnBackButton3()
    {
        houTo2Image.SetActive(true);
        houTo3Image.SetActive(false);
    }

    //BackButton4を押下した時
    public void OnBackButton4()
    {
        houTo3Image.SetActive(true);
        houTo4Image.SetActive(false);
    }

    //とじるボタンを押下したらHowToPanelを閉じる
    public void OnCloseButton()
    {
        houToPanel.SetActive(false);
    }
}
