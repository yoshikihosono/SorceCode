using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラの動きを管理します

public class CameraMove : MonoBehaviour
{
    public GameObject objTarget;    //注視するGameObject
    public Vector3 offset;          //どこからみるか
    public float Hori;              //スティックの横の傾きを取得する変数
    public float Vert;              //スティックの縦の傾きを取得する変数

    public float MoveSpeed = 0.01f; //カメラの移動速度

    void Start()
    {
        updatePostion();
    }

    private void Update()
    {
        Vert = Input.GetAxis("Vertical");   //スティックの傾きを取得

        /*上下移動時の処理*/
        if (Vert < 0)
        {
            //一定値上に行ったら
            if (offset.z > -2.7f)
                //それ以上カメラが移動しないようにする
                offset.z -= MoveSpeed;
        }
        else if (Vert > 0)
        {
            //一定値下に行ったら
            if (offset.z < -1.3f)
                //それ以上カメラが移動しないようにする
                offset.z += MoveSpeed;
        }
        /*上下移動時の処理*/
    }

    void LateUpdate()
    {
        updatePostion();
    }

    void updatePostion()
    {
        //プレイヤーの座標を取得します
        Vector3 pos = objTarget.transform.localPosition;

        //カメラの位置を設定
        transform.localPosition = pos + offset;
    }

    //下移動の時
    public void cameraDown()
    {
        if (offset.z > -2.7f)
        {
            offset.z = -2.7f;
        }
    }

    //上移動の時
    public void cameraUp()
    {
        if (offset.z < -1.3f)
        {
            offset.z = -1.3f;
        }
    }
}
