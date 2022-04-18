using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using Random = UnityEngine.Random;

public class FileReader : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomInSpeed;
    [SerializeField] private float zoomOutSpeed;
    [SerializeField] private float zoomFactor;
    [SerializeField] private float unitMoveFactor;
    // read data every 3 seconds
    private WaitForSeconds waitForSeconds = new WaitForSeconds(3f);
    private bool isDataRead;
    private Vector3 cameraOriginPos;
    private float cameraOriginFOV;
    private float tmp_CameraFOV;

    private void Awake()
    {
        // file name to read from the directory
        // fileName = "test.txt";
        cameraOriginPos = Camera.main.transform.position;
        cameraOriginFOV = Camera.main.fieldOfView;
        tmp_CameraFOV = cameraOriginFOV;
    }

    private enum CameraDirection
    {
        Up, Down, Left, Right, ZoomIn, ZoomOut, None
    }

    private CameraDirection cameraDirection = CameraDirection.None;

    void Start()
    {
        // test writing data
        //StartCoroutine(WriteDataToFileForTest());

        // Read data from file every waitForSeconds
        StartCoroutine(ReadDataFromFile());
    }

    void Update()
    {
        if (isDataRead)
        {
            switch (cameraDirection)
            {
                case CameraDirection.Left:
                    CameraMoveLeft();
                    break;
                case CameraDirection.Right:
                    CameraMoveRight();
                    break;
                case CameraDirection.Up:
                    CameraMoveUp();
                    break;
                case CameraDirection.Down:
                    CameraMoveDown();
                    break;
                case CameraDirection.ZoomIn:
                    CameraZoomIn();
                    break;
                case CameraDirection.ZoomOut:
                    CameraZoomOut();
                    break;
                case CameraDirection.None:
                    break;
            }

            isDataRead = false;
        }
    }

    private void CameraMoveLeft()
    {
        Camera.main.transform.position =
            Vector3.Slerp(Camera.main.transform.position, Camera.main.transform.position - new Vector3(unitMoveFactor, 0, 0),
                moveSpeed * Time.deltaTime);
    }

    private void CameraMoveRight()
    {
        Camera.main.transform.position =
            Vector3.Slerp(Camera.main.transform.position, Camera.main.transform.position + new Vector3(unitMoveFactor, 0, 0),
                moveSpeed * Time.deltaTime);
    }

    private void CameraMoveUp()
    {
        Camera.main.transform.position =
            Vector3.Slerp(Camera.main.transform.position, Camera.main.transform.position - new Vector3(0, unitMoveFactor, 0),
                moveSpeed * Time.deltaTime);
    }

    private void CameraMoveDown()
    {
        Camera.main.transform.position =
            Vector3.Slerp(Camera.main.transform.position, Camera.main.transform.position + new Vector3(0, unitMoveFactor, 0),
                moveSpeed * Time.deltaTime);
    }

    private void CameraZoomIn()
    {
        StartCoroutine(ZoomIn());
    }

    private void CameraZoomOut()
    {
        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomIn()
    {
        if (tmp_CameraFOV - zoomFactor < 10f)
        {
            yield break;
        }
        for (float i = 0; i <= 1f; i += zoomInSpeed * Time.deltaTime)
        {
            float tmp_RefCameraFOV = 0f;
            tmp_CameraFOV -= zoomFactor;
            Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView,
                tmp_CameraFOV,
                ref tmp_RefCameraFOV,
                Time.deltaTime );
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        if (tmp_CameraFOV + zoomFactor >= cameraOriginFOV)
        {
            RestoreCameraPos();
            yield break;
        }
        for (float i = 0; i <= 1f; i += zoomOutSpeed * Time.deltaTime)
        {
            float tmp_RefCameraFOV = 0f;
            tmp_CameraFOV += zoomFactor;
            Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView,
                tmp_CameraFOV,
                ref tmp_RefCameraFOV,
                Time.deltaTime);
            yield return null;
        }
    }

    private void RestoreCameraPos()
    {
        Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, cameraOriginPos, moveSpeed * Time.deltaTime);
        Camera.main.fieldOfView = cameraOriginFOV;
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

    // C:\Users\abcdefg\AppData\LocalLow\DefaultCompany\CSE 5546\ + fileName
    private void ReadData()
    {
        string path = Application.persistentDataPath + "/" + fileName;
        string[] lines = File.ReadAllLines(path);
        if (lines.Length == 0) return;
        Debug.Log(lines[lines.Length - 1]);
        isDataRead = Enum.TryParse(lines[lines.Length - 1], out CameraDirection cameraDirection);
    }

    // test write 
    private void WriteData()
    {
        string path = Application.persistentDataPath + "/" + fileName;
        //Write some text to the test.txt file, if append mode is false then rewrite data
        StreamWriter writer = new StreamWriter(path, true);
        cameraDirection = (CameraDirection)Random.Range(0, System.Enum.GetValues(typeof(CameraDirection)).Length);
        writer.WriteLine(cameraDirection.ToString());
        writer.Close();
    }

    private IEnumerator WriteDataToFileForTest()
    {
        for (; ; )
        {
            WriteData();
            yield return waitForSeconds;
        }
    }

    private IEnumerator ReadDataFromFile()
    {
        for (; ; )
        {
            ReadData();
            yield return waitForSeconds;
        }
    }
}
