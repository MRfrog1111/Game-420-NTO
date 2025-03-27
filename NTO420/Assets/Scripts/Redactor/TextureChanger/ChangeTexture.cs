using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ChangeTexture : MonoBehaviour
{
    public GameObject[] updatedObject;
    public TMP_Dropdown dropdown;
    public TMP_InputField inputField;
    // Start is called before the first frame update
   public void Upply()
    {
        string filename = inputField.text;
        /*string [] fileEntries = Directory.GetFiles(Application.dataPath + "/", "*.png");
        print(fileEntries[0]);*/
      /* var t2d = Resources.Load<Texture2D>( Application.dataPath + "/"+filename+ ".png");
        Rect rectPixels = new Rect(0,0,t2d.width,t2d.height);
        Vector2 pivotUV = Vector2.one / 2;
        Sprite sprite = Sprite.Create(t2d, rectPixels, pivotUV);*/
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<IMG2Sprite>().LoadNewSprite(Application.dataPath + "/"+filename);;
        if (dropdown.value == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                updatedObject[i].GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<IMG2Sprite>().LoadNewSprite(Application.dataPath + "/"+filename);
            }
        }
        else if (dropdown.value == 3)
        {
            for (int i = 6; i < 8; i++)
            {
                updatedObject[i].GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<IMG2Sprite>().LoadNewSprite(Application.dataPath + "/"+filename);
            }
        }
        else
        {
            updatedObject[dropdown.value+3].GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<IMG2Sprite>().LoadNewSprite(Application.dataPath + "/"+filename);
        }
        
    }
    //public Texture2D t2d;

 /*   void Start ()
    {
        Rect rectPixels = new Rect(50, 50, t2d.width - 100,t2d.height - 100);

        Vector2 pivotUV = Vector2.one / 2;

        Sprite sprite = Sprite.Create( t2d, rectPixels, pivotUV);

        #if UNITY_EDITOR
        AssetDatabase.CreateAsset( sprite, Application.dataPath + "/test.png");
        #endif

        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;     
    }*/
}
