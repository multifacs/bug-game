using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroControl : MonoBehaviour
{
    private float speed = 0.8f;
    private bool isRun = true;
    TextMeshProUGUI textInfo;
    LoadSituations loadSituations = new LoadSituations();
    Configuration config = new Configuration();
    //    Plane tracePlane;
    public static int sceneCounter = 0;
    public static int attemptCounter = 1;
    GameObject wasp;
    GameObject cherry;
    GameObject mainCamera;
    GameObject lbBody;
    GameObject lbHead;

    GameObject rawImage;

    //    Vector3 cameraPosition;
    //    Quaternion cameraRotation;
    Vector3 bugSize;
    BoxCollider bugCollider;
    Renderer render;
    public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        textInfo = GameObject.Find("TextInfo").GetComponent<TextMeshProUGUI>();
        //       tracePlane = GameObject.Find("TracePlane").GetComponent<Plane>();

        loadSituations.Load();
        config.Load();

        //       render = GetComponent<Renderer>();
        bugCollider = GetComponent<BoxCollider>();
        wasp = GameObject.Find("FantasyBee");
        cherry = GameObject.Find("Cherry");
        mainCamera = GameObject.Find("Main Camera");
        lbBody = GameObject.Find("lb_body");
        lbHead = GameObject.Find("lb_head");
        transform.position = Configuration.bugInitPosition;
        mainCamera.transform.position = transform.position + Configuration.cameraOffset;
        bugSize = transform.localScale;

        rawImage = GameObject.Find("RawImage");

        Debug.Log("israwloaded", rawImage);


        if (Configuration.cameraMode == 0)
        {
            mainCamera.transform.position = transform.position + Configuration.cameraOffset;
            mainCamera.transform.rotation = transform.rotation;
            mainCamera.transform.Rotate(0f, -90f, 0f);

            rawImage.SetActive(true);
            //lbBody.SetActive(false);
            //lbHead.SetActive(false);
            //            render.enabled = false;
            //Debug.Log("before");
            //Debug.Log(bugCollider.size);
            //transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            //bugCollider.size.Scale(new Vector3(1000f, 1000f, 1000f));
            //Debug.Log("after");
            //Debug.Log(bugCollider.size);
        }
        else
        {
            mainCamera.transform.position = new Vector3(0f, 50f, Configuration.xOffset);
            mainCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            rawImage.SetActive(false);

            //lbBody.SetActive(true);
            //lbHead.SetActive(true);
            //            render.enabled = true;
            //transform.localScale = bugSize;
            //bugCollider.size = new Vector3(2f, 3f, 2f);
        }


        Debug.Log("wasp scale: " + wasp.transform.localScale);

        cherry.transform.localScale = cherry.transform.localScale * Configuration.cherry_size;
        wasp.transform.localScale = wasp.transform.localScale * Configuration.wasp_size;
        Debug.Log("wasp scale after: " + wasp.transform.localScale);

        //cameraPosition = mainCamera.transform.position;
        //cameraRotation = mainCamera.transform.rotation;

        Debug.Log("camera position: " + mainCamera.transform.position);
        Debug.Log("camera rotation: " + mainCamera.transform.rotation);

        speed *= Configuration.bug_rotate_speed;

        //cherry.transform.localScale = new Vector3(8.48f, 8.48f, 8.48f);
        //wasp.transform.localScale = new Vector3(3.22f, 3.22f, 3.22f);

        // поле 350 на 450
        wasp.transform.position = new Vector3(LoadSituations.datas[sceneCounter].waspX / 10.0f - Configuration.xOffset + 2.5f, wasp.transform.position.y, 50 - LoadSituations.datas[sceneCounter].waspY / 10.0f - 1.8f);
        cherry.transform.position = new Vector3(LoadSituations.datas[sceneCounter].cherryX / 10.0f - Configuration.xOffset + 2.5f, cherry.transform.position.y, 50 - LoadSituations.datas[sceneCounter].cherryY / 10.0f - 2.5f);

        cherry.GetComponent<CherryControl>().dx = LoadSituations.datas[sceneCounter].cherryDx * Configuration.cherry_speed;
        cherry.GetComponent<CherryControl>().dy = LoadSituations.datas[sceneCounter].cherryDy * Configuration.cherry_speed;
        wasp.GetComponent<WaspControl>().dx = LoadSituations.datas[sceneCounter].waspDx * Configuration.wasp_speed;
        wasp.GetComponent<WaspControl>().dy = LoadSituations.datas[sceneCounter].waspDy * Configuration.wasp_speed;

        string res = "road" + (LoadSituations.datas[sceneCounter].scene);

        Texture txt = (Texture)Resources.Load(res);

        if (txt == null)
        {
            txt = (Texture)Resources.Load("road0");
        }

        GameObject.Find("TracePlane").GetComponent<Renderer>().material.mainTexture = txt;

        wasp.SetActive(LoadSituations.datas[sceneCounter].waspVisible);

        GameObject.Find("Trees").SetActive(LoadSituations.showTrees);
        GameObject.Find("Hills").SetActive(LoadSituations.showHills);

        //      StartCoroutine(Countdown());
        InvokeRepeating("InvokeTimer", 0, 0.02f);

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
            //            Application.Quit();
        }
        else if (Input.GetKeyDown("f1"))
        {
            Debug.Log("F1 pressed");
            if (++Configuration.cameraMode > 1)
            {
                Configuration.cameraMode = 0;
            }

            if (Configuration.cameraMode == 0)
            {
                mainCamera.transform.position = transform.position + Configuration.cameraOffset;
                mainCamera.transform.rotation = transform.rotation;
                mainCamera.transform.Rotate(0f, -90f, 0f);
                //lbBody.SetActive(false);
                //lbHead.SetActive(false);
                rawImage.SetActive(true);
                //                render.enabled = false;

                //                transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                //                bugCollider.size = new Vector3(1000f, 1500f, 1000f);
                //                bugCollider.size.Scale(new Vector3(1000f, 1000f, 1000f));
            }
            else
            {
                mainCamera.transform.position = new Vector3(0f, 50f, Configuration.xOffset);
                mainCamera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                //               render.enabled = true;
                rawImage.SetActive(false);
                //lbBody.SetActive(true);
                //lbHead.SetActive(true);
                //                transform.localScale = bugSize;
                //                bugCollider.size = new Vector3(2f, 3f, 2f);
            }
        }




        if (isRun)
        {
            //            textInfo.text = "scene:" + LoadSituations.datas[sceneCounter].scene + " attempt:" + attemptCounter + " x:" + (int)((transform.position.x + 22.5f) * 10) + " y:" + (int)((transform.position.z) * 10);
            textInfo.text = "Сцена: " + (sceneCounter + 1) + " из " + LoadSituations.datas.Count + "\n\nПопытка: " + attemptCounter;
        }
    }


    void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > Configuration.start_pause)
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

        if (isRun)
        {
            //transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * 0.01f);
            if (Time.timeSinceLevelLoad > Configuration.start_pause)
            {
                float xLimit = Configuration.xOffset - 2;

                transform.Translate(Vector3.left * Time.deltaTime * 5.0f * Configuration.bug_speed);
                if (transform.position.x > xLimit)
                {
                    transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
                }
                if (transform.position.x < -xLimit)
                {
                    transform.position = new Vector3(-xLimit, transform.position.y, transform.position.z);
                }
                if (transform.position.z > 50f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 50f);
                }
                if (transform.position.z < 0f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                }
                transform.Rotate(0.0f, Input.GetAxis("Horizontal") * speed, 0.0f);


                if (Configuration.cameraMode == 0)
                {
                    mainCamera.transform.Translate(Vector3.forward * Time.deltaTime * 5.0f * Configuration.bug_speed);
                    if (mainCamera.transform.position.x > xLimit)
                    {
                        mainCamera.transform.position = new Vector3(xLimit, mainCamera.transform.position.y, transform.position.z);
                    }
                    if (mainCamera.transform.position.x < -xLimit)
                    {
                        mainCamera.transform.position = new Vector3(-xLimit, mainCamera.transform.position.y, transform.position.z);
                    }
                    if (mainCamera.transform.position.z > 50f)
                    {
                        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, 50f);
                    }
                    if (mainCamera.transform.position.z < 0f)
                    {
                        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, 0f);
                    }
                    mainCamera.transform.Rotate(0.0f, Input.GetAxis("Horizontal") * speed, 0.0f);
                }
            }
        }
    }


    //   private IEnumerator Countdown()
    private void InvokeTimer()
    {
        //       long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        //       while (true)
        //      {
        if (Time.timeSinceLevelLoad > Configuration.start_pause && isRun)
        {

            LoadSituations.WriteLog(GetX(transform.position.x), GetY(transform.position.z),
                GetX(wasp.transform.position.x), GetY(wasp.transform.position.z),
                LoadSituations.datas[sceneCounter].waspDx, LoadSituations.datas[sceneCounter].waspDy,
                LoadSituations.datas[sceneCounter].waspVx, LoadSituations.datas[sceneCounter].waspVy,
                GetX(cherry.transform.position.x), GetY(cherry.transform.position.z),
                LoadSituations.datas[sceneCounter].cherryDx, LoadSituations.datas[sceneCounter].cherryDy,
                0, score, LoadSituations.datas[sceneCounter].scene, attemptCounter,
                -10000, -10000
                );
        }
        //           long timeDiff = 20 - (DateTimeOffset.Now.ToUnixTimeMilliseconds() - time);
        //           yield return new WaitForSecondsRealtime(timeDiff / 1000.0f); //wait 2 seconds
        //           time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        //      }
    }

    void OnTriggerEnter(Collider collider)
    {

        isRun = false;
        Debug.Log("Hero Trigger tag:" + collider.tag);
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
            //if (sceneCounter >= LoadSituations.datas.Count)
            //{
            //    sceneCounter--;
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            //    return;
            //}
        }
        if (collider.tag == "")
        {
            //set velocity 0
            //adjust the object position (the object may overlap with the block)
        }
        //        Application.LoadLevel(Application.loadedLevel);
        //       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private IEnumerator coroutine;

    private void OnApplicationQuit()
    {
        LoadSituations.CloseLog();
    }


    IEnumerator WaitOnCollision(Boolean isCherry)
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(0.1f);
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

}
