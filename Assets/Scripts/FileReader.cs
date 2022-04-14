using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class FileReader : MonoBehaviour
{
    [SerializeField] private string fileName;
    private string direction;
    private int counter = 0;

    private void Awake()
    {
        fileName = "test.txt";
    }

    private enum CameraDirection
    {
        Up, Down, Left, Right, ZoomIn, ZoomOut, None
    }

    private CameraDirection cameraDirection;

    void Update()
    {
        StartCoroutine(WriteDataToFileForTest());
        StartCoroutine(ReadDataFromFile());
    }

    [MenuItem("Tools/Read file")]
    public static void TestRead()
    {
        string path = Application.persistentDataPath + "/test.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    [MenuItem("Tools/Write file")]
    static void TestWrite()
    {
        string path = Application.persistentDataPath + "/test.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();
    }

    // C:\Users\linho\AppData\LocalLow\DefaultCompany\CSE 5546\ + fileName
    private void ReadData()
    {
        string path = Application.persistentDataPath + "/" + fileName;
        string[] lines = File.ReadAllLines(path);
        //StreamReader reader = new StreamReader(path);
        //reader.Close();
        Debug.Log(lines[lines.Length - 1]);
    }

    private void WriteData()
    {
        string path = Application.persistentDataPath + "/" + fileName;
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        if (counter == 0)
        {
            cameraDirection = CameraDirection.Up;
        }
        else if (counter == 1)
        {
            cameraDirection = CameraDirection.Down;
        }
        else if (counter == 2)
        {
            cameraDirection = CameraDirection.Left;
        }
        else if (counter == 3)
        {
            cameraDirection = CameraDirection.Right;
        }
        else if (counter == 4)
        {
            cameraDirection = CameraDirection.ZoomIn;
        }
        else if (counter == 5)
        {
            cameraDirection = CameraDirection.ZoomOut;
        }
        else if (counter == 6)
        {
            cameraDirection = CameraDirection.None;
        }

        if (counter == 6)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }
        writer.WriteLine(cameraDirection.ToString());
        writer.Close();
    }

    private IEnumerator WriteDataToFileForTest()
    {
        for (; ; )
        {
            // execute block of code here
            WriteData();
            yield return new WaitForSeconds(10f);
        }
    }

    // 
    private IEnumerator ReadDataFromFile()
    {
        for (; ; )
        {
            // execute block of code here
            ReadData();
            yield return new WaitForSeconds(10f);
        }
    }
}
