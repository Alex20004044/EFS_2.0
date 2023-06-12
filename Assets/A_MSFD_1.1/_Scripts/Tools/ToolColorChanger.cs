using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolColorChanger : MonoBehaviour
{
    [SerializeField]
    Transform root;
    [TableList]
    [SerializeField]
    List<ColorPair> colorPairs = new List<ColorPair>();
    [Button]
    public void ActivateColorChange()
    {
        /*foreach(var x in FindComponents<Image>(root))
        {
            x.color = GetChangedColor(x.color);
        }
        foreach(var x in FindComponents<TMP_Text>(root))
        {
            x.color = GetChangedColor(x.color);
        }*/
        foreach (var x in FindComponents<MaskableGraphic>(root))
        {
            x.color = GetChangedColor(x.color);
        }
    }

    Color GetChangedColor(Color currentColor)
    {
        ColorPair colorPair = colorPairs.Find((x) => x.source == currentColor);
        if(colorPair != null)
        {
            return colorPair.destination; 
        }
        return currentColor;
    }
    List<T> FindComponents<T>(Transform root)
    {
        List<T> list = new List<T>();
        list.AddRange(root.GetComponents<T>());
        list.AddRange(root.GetComponentsInChildren<T>());
        return list;
    }

    [System.Serializable]
    public class ColorPair
    {
        //[HorizontalGroup]
        public Color source;
        //[HorizontalGroup]
        public Color destination;
    }
}
