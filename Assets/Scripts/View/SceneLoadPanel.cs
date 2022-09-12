using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadPanel : SingletonView<SceneLoadPanel>
{
    #region Fields

    Slider sliderProcess;
    AsyncOperation currentLoadScene;  
    #endregion

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sliderProcess = transform.Find("bg/Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLoadScene !=null)
        {
            UpdateProcess(currentLoadScene.progress);
        }
    }


    public void UpdateProcess(float process)
    {
        this.Show();
        this.sliderProcess.value = process;

        if (process >= 1) // load scene complete
        {
            //hide the loading panel after 2 seconds
            Invoke("Hide", 2);
        }

    }

    public void UpdateProcess(AsyncOperation asyncOperation) {
        this.Show();
        currentLoadScene = asyncOperation;
    }

    public override void Hide()
    {
        base.Hide();
        currentLoadScene = null;
    }

}
