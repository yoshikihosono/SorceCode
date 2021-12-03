using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rantan : MonoBehaviour
{
    public Image rantanGauge;   //ランタンのゲージです
    public Light rantaLight;    //ランタンのライトです
    public Light playerLight;   //プレイヤーのライトです
    public float burningSpeed;  //燃焼速度です

    // Update is called once per frame
    void Update()
    {
        /*ランタンのON,OFFの切り替え処理*/
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (rantaLight.gameObject.active)
            {
                rantaLight.gameObject.SetActive(false);
                playerLight.gameObject.SetActive(true);
            }
            else
            {
                //ランタンをOFFからONにしたらちょっとゲージを減らします
                rantaLight.gameObject.SetActive(true);
                playerLight.gameObject.SetActive(false);
                rantanGauge.fillAmount -= 0.05f;
            }
        }

        //ランタンが付いていたら
        if (rantaLight.gameObject.active)
        {
            //ゲージを減らしていきます
            rantanGauge.fillAmount -= burningSpeed;
        }
        /*ランタンのON,OFFの切り替え処理*/

        //ランタンゲージが0になったらランタンを消します
        if (rantanGauge.fillAmount <= 0)
        {
            rantaLight.gameObject.SetActive(false);
            playerLight.gameObject.SetActive(true);
        }
    }
}
