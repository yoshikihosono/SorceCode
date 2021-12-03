using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //爆発エフェクト格納用
    public GameObject Explosion;

    //当たったら
    private void OnCollisionEnter(Collision collision)
    {
        //このオブジェクトを消します
        Destroy(this.gameObject);
        //爆発エフェクトをだします
        Instantiate(Explosion, this.transform.position, Quaternion.identity);
    }
}
