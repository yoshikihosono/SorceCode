using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Playerの移動の制御を行います

public class Player : MonoBehaviour
{
    public float moveSpeed;  //移動速度
    private bool running = false;   //trueのときは走っている
    private bool cantran = false;   //trueのときは走れない
    float hori; //スティックの傾きを取得するようの変数
    float vert; //スティックの傾きを取得するようの変数
    Stamina stamina;    //スタミナスクリプトの取得用

    private Animator animator;

    private string runstr = "Running";
    private string walkstr = "Walking";

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        stamina = GetComponent<Stamina>();  //スタミナスクリプト取得
    }

    // Update is called once per frame
    void Update()
    {
        hori = Input.GetAxis("Horizontal"); //スティックの傾きを取得
        vert = Input.GetAxis("Vertical");   //スティックの傾きを取得
        running = false;

        if (stamina.nowStamina <= 0)    //スタミナがなくなったら走れなくなる
            cantran = true;
        else if (stamina.nowStamina > 0.4)  //4割回復したら走れる
            cantran = false;

        //走れるときに
        if (!cantran)   
        {
            //ダッシュボタンを押して
            if (Input.GetButton("Dash") || Input.GetKey(KeyCode.LeftShift))
            {
                //今のスタミナが0より多いなら
                if (stamina.nowStamina > 0)
                {
                    running = true;
                    moveSpeed = 0.1f;
                }
            }
            //ダッシュボタンを押していないなら
            else
            {
                running = false;
                moveSpeed = 0.05f;
            }
        }
        //走れないなら
        else
        {
            running = false;
            moveSpeed = 0.05f;
        }
    }

    //万が一ステージ外に落ちた場合の処理
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "underGround")
        {
            Scene loadScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadScene.name);
        }
    }

    //走っているかどうか返す
    public bool isRunning()
    {
        return running && hori != 0.0f && vert != 0.0f;
    }
}
