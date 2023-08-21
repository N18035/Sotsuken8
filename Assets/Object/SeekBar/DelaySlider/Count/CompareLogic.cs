using System;
using System.Collections.Generic;


public class CompareLogic:IComparer<TB>
{
    //xがyより小さいときはマイナスの数、大きいときはプラスの数、同じときは0を返す
    public int Compare(TB x, TB y)
    {
        if(x.Time < y.Time){
            return -1;
        }else if(x.Time > y.Time){
            return 1;
        }else{
            return 0;
        }
    }
}


