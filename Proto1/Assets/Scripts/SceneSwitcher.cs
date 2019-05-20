using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    GameObject LoadingBar;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Slider slider;

    private GameObject loadingBarPrivet;
    private GameObject ProgressBar;
    


    private static SceneSwitcher instance;
    public static SceneSwitcher Instance
    {
        get { return instance; }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        this.gameObject.SetActive(false);
    }
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AsynchronousLoadStart(string scene)
    {
        //coinList.Clear();
        this.gameObject.SetActive(true);
        InitLoadingBar();
        IEnumerator coroutine;
        //niet vergeten die unsubscribe te doen enzo
        SceneManager.sceneLoaded += SceneIsLoaded;
        coroutine = AsynchronousLoad(scene);
        StartCoroutine(coroutine);

    }
    //deze methode is speciaal bedoeld voor het overgaan van een level naar de victoryscreen
    //hij accepteert een nieuwe variable en laat geen loading screen zien
    internal void AsynchronousLoadStartNoLoadingBar(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void SceneIsLoaded(Scene arg0, LoadSceneMode arg1)
    {
        this.gameObject.SetActive(false);
        //InitScene(arg0.name);
    }

    IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            //Debug.Log("Loading progress: " + (progress * 100) + "%");
            slider.value = (progress * 100);
            //ProgressBar.transform.localScale = new Vector3(progress, 0.12f, 1f);
            // Loading completed
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
                
            }
            yield return null;
        }
    }
    internal void InitLoadingBar()
    {
        
    }
    internal void UpdateLoadingBar()
    {

    }
}
