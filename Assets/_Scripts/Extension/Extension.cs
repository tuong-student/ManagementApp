using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NOOD.Extension
{
    public static class Extension 
    {
        public static void RenameKey<TKey, TVale>(this Dictionary<TKey, TVale> dic, TKey oldKey, TKey newKey)
        {
            TVale value = dic[oldKey];
            dic.Remove(oldKey);
            dic.Add(newKey, value);
        }
    }

}
