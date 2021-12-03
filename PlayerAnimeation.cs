using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Playerのアニメーションを管理します

public class PlayerAnimeation : MonoBehaviour
{
    private Animator animator;

    private string runstr = "Running";      //走りアニメーション
    private string walkstr = "Walking";     //歩きアニメーション
    private string rantanstr = "IsRantan";  //ランタン所持時のアニメーション
    private string throwstr = "Throwing";   //投げアニメーション
    public GameObject Box;
    PlayerMove move;

    // Start is called before the first frame update
    void Start()
    {
        //Animatorを取得
        this.animator = GetComponent<Animator>();
        //BoxのPlayerMoveコンポーネントを取得
        move = Box.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動キーが入力されていたら
        if (move.Vert != 0 || move.Hori != 0)
        {
            //歩きアニメーションのフラグをtrueにする
            this.animator.SetBool(walkstr, true);
            //走っていたら
            if (move.running)
            {
                //走りアニメーションのフラグをtrueに
                this.animator.SetBool(runstr, true);
            }
        }
        //入力されていなかったら
        else
        {
            //歩き、走りアニメーションのフラグをfalseに
            this.animator.SetBool(walkstr, false);
            this.animator.SetBool(runstr, false);
        }

        //走っていなかったら
        if (!move.running)
            this.animator.SetBool(runstr, false);

        //投げるキー、ボタンが入力されたら
        if (Input.GetButtonUp("Throw") || Input.GetKeyUp(KeyCode.Space) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.C))
        {
            //投げアニメーションのフラグをtrueに
            this.animator.SetBool(throwstr, true);
        }
        else
        {
            this.animator.SetBool(throwstr, false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ランタンに当たったら
        if (collision.gameObject.tag == "rantan")
        {
            //ランタン用アニメーションのフラグをtrueに
            this.animator.SetBool(rantanstr, true);
        }
    }
}
