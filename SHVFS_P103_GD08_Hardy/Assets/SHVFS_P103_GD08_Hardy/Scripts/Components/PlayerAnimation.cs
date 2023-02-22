using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private int IswalkingID = Animator.StringToHash("Iswalking");
    private Animator anim;
    private Transform myTran;
    private Vector3 lastUpd, NowUpd;

    public enum MyState
    {
        none,
        stand,
        run,
    
        
    };
    MyState myState;
    // Start is called before the first frame update

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        myTran = gameObject.GetComponent<Transform>();
        NowUpd = lastUpd = myTran.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        NowUpd = myTran.position;
        CheckRun(lastUpd, NowUpd);
        lastUpd = myTran.position;

    }

    void CheckRun(Vector3 i, Vector3 r)
    {
        if(i==r || Time.time <= 0.02f)
        {
            if(myState!= MyState.stand)
            {
                anim.SetBool(IswalkingID,false);
                myState = MyState.stand;
            }
        }
        else
        {
            if(myState!=MyState.run)
            {
                anim.SetBool(IswalkingID,true);
                myState = MyState.run;
            }
        }

    }
}
