using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceUtility {

    public static void GetInterfaces<T>(out List<T> resultList, GameObject objectToSearch) where T : class {
        MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
        resultList = new List<T>();
        foreach (MonoBehaviour mono in list) {
            if (mono is T) {
                resultList.Add((T) (System.Object)mono);
            }
        }
    }
    
}