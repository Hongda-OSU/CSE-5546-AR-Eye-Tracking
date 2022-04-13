using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    [SerializeField] private string fileName;
    private string direction;
    private enum CameraDirection
    {
        Up, Down, Left, Right, ZoomIn, ZoomOut, None
    }

    private CameraDirection cameraDirection;

    void Update()
    {
        //StartCoroutine(ReadDataFromFile());
    }

    [MenuItem("Tools/Read file")]
    public static void ReadString()
    {
        string path = Application.persistentDataPath + "/test.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    // C:\Users\linho\AppData\LocalLow\DefaultCompany\CSE 5546\ + fileName
    private void ReadData()
    {
        string path = Application.persistentDataPath + "/" + fileName;
    }

    // 
    private IEnumerator ReadDataFromFile()
    {
        yield break;
    }
}
