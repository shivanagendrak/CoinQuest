using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{


    public GameObject prefab;

    public List<GameObject> list;
    public List<GameObject> list2;


    [ContextMenu("spwanPlayer")]
    public void Spwan()
    {
        for (int i = 0; i < list.Count; i++)
        {

            //GameObject spwanObject = Instantiate(prefab, list[i].transform.position, list[i].transform.rotation);
            //spwanObject.name = list[i].name;

            list2[i].transform.position = list[i].transform.position;
            list2[i].transform.eulerAngles = list[i].transform.eulerAngles;
            list2[i].name= list[i].name;
        }
    }
}
