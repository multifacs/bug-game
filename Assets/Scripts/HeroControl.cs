using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroControl : MonoBehaviour
{
    [Header("Player Config")]
    [Range(0f, 10f)]
    public float RotationSpeed = 0.9f;

    private bool isRun = true;
    TextMeshProUGUI textInfo;
    //LoadSituations loadSituations = new();
    //Configuration config = new();
    //    Plane tracePlane;
    public static int sceneCounter = 0;
    public static int attemptCounter = 1;
    GameObject wasp;
    GameObject cherry;
    GameObject mainCamera;
    GameObject topCamera;
    //GameObject rawImage;

    //    Vector3 cameraPosition;
    //    Quaternion cameraRotation;
    Vector3 bugSize;
    BoxCollider bugCollider;
    Renderer render;
    public static int score = 0;

    private bool _hasStartedMoving = false;

    EEGControl eegControl;

    // Start is called before the first frame update
    void Start()
    {
        textInfo = GameObject.Find("TextInfo").GetComponent<TextMeshProUGUI>();
        LoadSituations.Load();
        Configuration.Load();

        bugCollider = GetComponent<BoxCollider>();
        wasp = GameObject.Find("FantasyBee");
        cherry = GameObject.Find("Cherry");
        mainCamera = GameObject.Find("Main Camera");

        eegControl = GameObject.Find("EEGCircle").GetComponent<EEGControl>();

        transform.position = Configuration.bugInitPosition;
        mainCamera.transform.position = transform.position + Configuration.cameraOffset;
        bugSize = transform.localScale;

        topCamera = GameObject.Find("TopCameraPlayer");
        topCamera.SetActive(false);
        //rawImage = GameObject.Find("RawImage");

        UpdateCameras();

        Debug.Log("wasp scale: " + wasp.transform.localScale);

        cherry.transform.localScale = cherry.transform.localScale * Configuration.cherry_size;
        wasp.transform.localScale = wasp.transform.localScale * Configuration.wasp_size;
        Debug.Log("wasp scale after: " + wasp.transform.localScale);

        Debug.Log("camera position: " + mainCamera.transform.position);
        Debug.Log("camera rotation: " + mainCamera.transform.rotation);

        RotationSpeed *= Configuration.bug_rotate_speed;

        // поле 350 на 450
        wasp.transform.position = new Vector3(LoadSituations.datas[sceneCounter].waspX / 10.0f - Configuration.xOffset + 2.5f, wasp.transform.position.y, 50 - LoadSituations.datas[sceneCounter].waspY / 10.0f - 1.8f);
        cherry.transform.position = new Vector3(LoadSituations.datas[sceneCounter].cherryX / 10.0f - Configuration.xOffset + 2.5f, cherry.transform.position.y, 50 - LoadSituations.datas[sceneCounter].cherryY / 10.0f - 2.5f);

        cherry.GetComponent<CherryControl>().dx = LoadSituations.datas[sceneCounter].cherryDx * Configuration.cherry_speed * Configuration.speed_mult;
        cherry.GetComponent<CherryControl>().dy = LoadSituations.datas[sceneCounter].cherryDy * Configuration.cherry_speed * Configuration.speed_mult;
        wasp.GetComponent<WaspControl>().dx = LoadSituations.datas[sceneCounter].waspDx * Configuration.wasp_speed * Configuration.speed_mult;
        wasp.GetComponent<WaspControl>().dy = LoadSituations.datas[sceneCounter].waspDy * Configuration.wasp_speed * Configuration.speed_mult;

        string res = "Roads/road" + (LoadSituations.datas[sceneCounter].scene);

        Texture txt = (Texture)Resources.Load(res);

        if (txt == null)
        {
            txt = (Texture)Resources.Load("Roads/road0");
        }

        GameObject.Find("TracePlane").GetComponent<Renderer>().material.mainTexture = txt;

        wasp.SetActive(LoadSituations.datas[sceneCounter].waspVisible);

        GameObject.Find("Trees").SetActive(LoadSituations.showTrees);
        GameObject.Find("Hills").SetActive(LoadSituations.showHills);

        GameObject.Find("Hills").SetActive(LoadSituations.showHills);

        LoadSituations.InitLog();
    }

    private void UpdateCameras()
    {
        if (Configuration.cameraMode == 0)
        {
            topCamera.SetActive(false);
            mainCamera.SetActive(true);
            //rawImage.SetActive(true);
        }
        else
        {
            mainCamera.SetActive(false);
            topCamera.SetActive(true);
            //rawImage.SetActive(false);
        }
    }

    private float GetX(float x)
    {
        return (x + Configuration.xOffset) * 10;
    }
    private float GetY(float y)
    {
        return y * 10;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            LoadSituations.CloseLog();
        }
        else if (Input.GetKeyDown("f1"))
        {
            Debug.Log("F1 pressed");
            if (++Configuration.cameraMode > 1)
            {
                Configuration.cameraMode = 0;
            }

            UpdateCameras();
        }

        if (isRun)
        {
            textInfo.text = "Сцена: " + (sceneCounter + 1) + " из " + LoadSituations.datas.Count + "\n\nПопытка: " + attemptCounter;

            if (Time.timeSinceLevelLoad > Configuration.start_pause)
            {
                if (!_hasStartedMoving)
                {
                    _hasStartedMoving = true;
                    eegControl.BlinkOnce();
                }

                float xLimit = Configuration.xOffset - 2;
                float moveSpeed = 5.0f * Configuration.bug_speed * Configuration.speed_mult;

                // Плавное движение с deltaTime
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);

                // Ограничения
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, -xLimit, xLimit),
                    transform.position.y,
                    Mathf.Clamp(transform.position.z, 0f, 50f)
                );

                // Плавный поворот
                transform.Rotate(0.0f, Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime * 60 * Configuration.speed_mult, 0.0f);
            }
        }
    }

    private float logTimer = 0f;
    [field: SerializeField]
    [Header("Movement Logging")]
    private float logFrequency = Configuration.log_frequency; // по-умолч. 10 записей в секунду
    void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > Configuration.start_pause)
        {
            logTimer += Time.fixedDeltaTime;

            float logInterval = 1f / logFrequency;
            if (logTimer >= logInterval)
            {
                logTimer = 0f;
                WriteMovementLog();
            }
        }
    }

    private void WriteMovementLog()
    {
        LoadSituations.WriteLog(GetX(transform.position.x), GetY(transform.position.z),
            GetX(wasp.transform.position.x), GetY(wasp.transform.position.z),
            LoadSituations.datas[sceneCounter].waspDx, LoadSituations.datas[sceneCounter].waspDy,
            LoadSituations.datas[sceneCounter].waspVx, LoadSituations.datas[sceneCounter].waspVy,
            GetX(cherry.transform.position.x), GetY(cherry.transform.position.z),
            LoadSituations.datas[sceneCounter].cherryDx, LoadSituations.datas[sceneCounter].cherryDy,
            0, sceneCounter * 10, sceneCounter + 1, LoadSituations.datas[sceneCounter].scene,
            -10000, -10000
            );
    }

    void OnTriggerEnter(Collider collider)
    {
        isRun = false;
        Debug.Log("Hero Trigger tag:" + collider.tag);

        eegControl.BlinkTwice();

        if (collider.tag == "FantasyBee")
        {
            attemptCounter++;
            score -= 5;
            coroutine = WaitOnCollision(false);
            StartCoroutine(coroutine);
        }
        if (collider.tag == "Cherry")
        {
            score += 10;
            coroutine = WaitOnCollision(true);
            Debug.Log("TIMER before");
            StartCoroutine(coroutine);
            Debug.Log("TIMER after");

            attemptCounter++;
            sceneCounter++;
        }
        if (collider.tag == "")
        {
            //set velocity 0
            //adjust the object position (the object may overlap with the block)
        }
    }

    private IEnumerator coroutine;

    private void OnApplicationQuit()
    {
        LoadSituations.CloseLog();
    }


    IEnumerator WaitOnCollision(Boolean isCherry)
    {
        // suspend execution for circle_black_time * 4 (black -> white -> black -> white)
        yield return new WaitForSeconds(Configuration.circle_black_time * 4);
        if (sceneCounter >= LoadSituations.datas.Count && isCherry)
        {
            sceneCounter--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        Application.targetFrameRate = 60;
    }
}
