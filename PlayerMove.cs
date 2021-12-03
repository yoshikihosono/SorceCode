using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Playerの移動のコードです

public class PlayerMove : MonoBehaviour
{
    Vector3 PlayerPos;  //PlayerのPosition
    public float Hori;  //スティックの横の傾きを取得する変数
    public float Vert;  //スティックの縦の傾きを取得する変数
    float RunVelocity;  //走る速度
    Vector3 translation;    //positionに変換するようの変数

    public bool running = false;   //trueのときは走っている
    private bool cantran = false;   //trueのときは走れない
    public Stamina stamina;    //スタミナスクリプトの取得用

    // Start is called before the first frame update
    void Start()
    {
        //playerのポジションをTransformから取得します
        PlayerPos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Hori = Input.GetAxis("Horizontal");
        Vert = Input.GetAxis("Vertical");
        //速度は基本1.0f
        RunVelocity = 1.0f;
        //走ってるフラグをfalseに
        running = false;

        //スタミナがなくなったら走れなくなる
        if (stamina.nowStamina <= 0)    
            cantran = true;
        //4割回復したら走れる
        else if (stamina.nowStamina > 0.4)  
            cantran = false;

        //走れるときに
        if (!cantran)
        {
            //ダッシュボタンを押して
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //今のスタミナが0より多いなら
                if (stamina.nowStamina > 0)
                {
                    running = true;
                    RunVelocity = 3.0f;
                }
            }
            //ダッシュボタンを押していないなら
            else
            {
                running = false;
                RunVelocity = 1.5f;
            }
        }
        //走れないなら
        else
        {
            running = false;
            RunVelocity = 1.0f;
        }

        translation.x = Hori * RunVelocity * Time.deltaTime;
        translation.z = Vert * RunVelocity * Time.deltaTime;

        transform.position += translation;

        if (running)    //走っていて
            if (Hori != 0 || Vert != 0) //移動しているなら
                stamina.ConStamina();   //スタミナを減らす
    }
}
