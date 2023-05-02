using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShapeSelector : MonoBehaviour
{

    public static int shapeIndex;


    public void ShapeIndex(int index)
    {
        shapeIndex = index;
        Debug.Log(index);
        Debug.Log(shapeIndex);
    }


}
