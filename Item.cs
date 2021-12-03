using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//アイテムの管理とアイテム関連のUIを行います
//石と爆弾を投げるのもここで行います
//ランタンのON,OFFはRantan.csで行っています

public class Item : MonoBehaviour
{
    public GameObject stone;    //投げる時に作る石のオブジェクト
    public GameObject bomb;     //投げる時に作る爆弾のオブジェクト
    public GameObject rightHand;    //右手の位置
    public GameObject throwDirection;   //投げる方向
    public GameObject rantan;   //ランタンを取得したときに出現するランタン
    public float throwSpeed;    //投げるスピードです
    Vector3 pos;    //爆弾||石を投げる時にそれらを出現させる場所です
    public int[] itemBox;   //アイテムの所持状況です
    const int Space = 0;    //何も持っていないときはSpaceを入れます
    const int Stone = 1;    //石を持っている場合はStoneを入れます
    const int Bomb = 2;     //爆弾を持っている場合はbombを入れます
    public Image stoneImage1;   //UIの上の段の石の画像です
    public Image stoneImage2;   //UIの下の段の石の画像です
    public Image bombImage1;    //UIの上の段の爆弾の画像です
    public Image bombImage2;    //UIの下の段の爆弾の画像です
    public GameObject rantanGauge;  //ランタンゲージの下の画像と上の画像をまとめたもの
    public Image rantanGaugeImage;  //ランタンゲージの上の画像です
    public Light playerLight;       //プレイヤーの光
    public Light rantanLight;       //ランタン取得時にランタンの光としてActiveにする

    // Start is called before the first frame update
    void Start()
    {
        //開始時にアイテムは持っていない状態
        itemBox[0] = Space;
        itemBox[1] = Space;
    }

    // Update is called once per frame
    void Update()
    {
        /*アイテムを投げる処理*/
        /*上のアイテム欄のアイテムを投げる処理*/
        if (Input.GetButtonUp("Item1") || Input.GetKey(KeyCode.X))
        {
            //上のアイテム欄の状態をみます
            switch (itemBox[0])
            {
                //石を持っているなら
                case Stone:

                    //石を投げます
                    ThrowItem(stone);
                    //アイテム欄を空にします
                    itemBox[0] = Space;
                    //UIも空にします
                    stoneImage1.gameObject.SetActive(false);

                    break;

                //爆弾を持っているなら
                case Bomb:

                    //爆弾を投げます
                    ThrowItem(bomb);
                    //アイテム欄を空にします
                    itemBox[0] = Space;
                    //UIも空にします
                    bombImage1.gameObject.SetActive(false);

                    break;
            }
        }
        /*上のアイテム欄のアイテムを投げる処理*/

        /*下のアイテム欄のアイテムを投げる処理*/
        if (Input.GetButtonUp("Item2") || Input.GetKey(KeyCode.C))
        {
            //下のアイテム欄の状態をみます
            switch (itemBox[1])
            {
                //石を持っているなら
                case Stone:

                    //石を投げます
                    ThrowItem(stone);
                    //アイテム欄を空にします
                    itemBox[1] = Space;
                    //UIも消します
                    stoneImage2.gameObject.SetActive(false);

                    break;

                //爆弾を持っているなら
                case Bomb:

                    //ボムを投げます
                    ThrowItem(bomb);
                    //アイテム欄を空にします
                    itemBox[1] = Space;
                    //UIも消します
                    bombImage2.gameObject.SetActive(false);

                    break;
            }
        }
        /*下のアイテム欄のアイテムを投げる処理*/
        /*アイテムを投げる処理*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        //当たったのが石なら
        if (collision.gameObject.tag == "stone")
        {
            //上のアイテム欄が空か見ます
            if (CheckItemBox1())
            {
                itemBox[0] = Stone;
                //Destroy(collision.gameObject);
                stoneImage1.gameObject.SetActive(true);
            }
            //空じゃなかったら
            else
            {
                //下のアイテム欄が空か見ます
                if (CheckItemBox2())
                {
                    itemBox[1] = Stone;
                  //  Destroy(collision.gameObject);
                    stoneImage2.gameObject.SetActive(true);
                }
            }
        }

        //当たったのが爆弾なら
        if (collision.gameObject.tag == "bomb")
        {
            //上のアイテム欄が空か見ます
            if (CheckItemBox1())
            {
                itemBox[0] = Bomb;
                Destroy(collision.gameObject);
                bombImage1.gameObject.SetActive(true);
            }
            //空じゃなかったら
            else
            {
                //下のアイテム欄が空か見ます
                if (CheckItemBox2())
                {
                    itemBox[1] = Bomb;
                    Destroy(collision.gameObject);
                    bombImage2.gameObject.SetActive(true);
                }
            }
        }

        //当たったのがランタンなら
        if (collision.transform.tag == "rantan")
        {
            //当たったランタンを消します
            Destroy(collision.gameObject);

            //ランタンを持っていないなら
            if (rantan.gameObject.active == false)
            {
                //ランタンを持ちます
                rantan.gameObject.SetActive(true);
                //UIを表示します
                rantanGauge.gameObject.SetActive(true);
                //ランタン取得状態のライトをtrueにします
                rantanLight.gameObject.SetActive(true);
                //通常時のライトをfalseにします
                playerLight.gameObject.SetActive(false);
            }

            //すでにランタンを持っていたら
            if (rantanGaugeImage.fillAmount <= 1)
            {
                //ランタンゲージを回復させます
                rantanGaugeImage.fillAmount += 0.5f;
            }
        }
    }

    //上のアイテム欄が空か調べる関数
    bool CheckItemBox1()
    {
        //空だったら
        if (itemBox[0] != Space)
        {
            return false;
        }

        return true;
    }

    //下のアイテム欄が空か調べる関数
    bool CheckItemBox2()
    {
        //空だったら
        if (itemBox[1] != Space)
        {
            return false;
        }

        return true;
    }

    //投げる関数
    //引数として渡されたオブジェクトを作ります
    void ThrowItem(GameObject itemModel)
    {
        //右手の位置を取得
        pos = rightHand.transform.position;
        /*投げる方向を決めます*/
        float throwDirectionX = throwDirection.transform.position.x;
        float throwDirectionY = throwDirection.transform.position.y;
        float throwDirectionZ = throwDirection.transform.position.z;
        float velocityX = throwDirectionX - rightHand.transform.position.x;
        float velocityY = throwDirectionY - rightHand.transform.position.y;
        float velocityZ = throwDirectionZ - rightHand.transform.position.z;
        /*投げる方向を決めます*/

        //引数のアイテムを作ります
        GameObject obj = Instantiate(itemModel, pos, Quaternion.identity);
        obj.SetActive(true);
        Vector3 v3 = new Vector3(velocityX * throwSpeed, velocityY * throwSpeed, velocityZ * throwSpeed);

        obj.GetComponent<Rigidbody>().velocity = v3;
    }
}
