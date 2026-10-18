using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XController : MonoBehaviour
{


    /*
     아이템을 먹다보면 발사 효과가 너무 좋아서 죽을일이 없게됨 (밸런스 문제)
아이템 나오는 확률이 높음
죽었을떄 적을 몇마리 죽였는지 아니면 몇초 살아남았는지 스코어 만들기

보스 3마리 잡고 다음 스테이지로 넘어가게 하는거
     
     
     
     
     
     */
    public List<GameObject> Xitems;
    public PlaneCollider player;

    // Start is called before the first frame update
    void Start()
    {
        UpdateX(player.x);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateX(player.x);
    }
    public void UpdateX(int x)
    {
        for (int i = 0; i < Xitems.Count; i++)
        {
            if (i < x)
            {
                Xitems[i].SetActive(true);
            }
            else
            {
                Xitems[i].SetActive(false);
            }
        }
    }
}