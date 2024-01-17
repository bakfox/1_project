using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class loadManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Image ProgressBar;
    float mid = 0f;

    public TextMeshProUGUI ProText;
    //��ǥ  0~1270����
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadBar());
    }
    //�ε�â ����
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("load_sc");
    }
    //�ε�â ��ŵ
    public static void LoadScene_fast(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadBar()
    {
        mid += Time.deltaTime * 0.8f;
        AsyncOperation Operation = SceneManager.LoadSceneAsync(nextScene);
        Operation.allowSceneActivation = false;
        while (!Operation.isDone)
        {
            ProgressBar.fillAmount = Mathf.Lerp(ProgressBar.fillAmount, Operation.progress / 0.1f, mid);
            if (ProgressBar.fillAmount >= 0.97f)
            {
                Operation.allowSceneActivation = true; ;
            }
            ProText.text = Mathf.Round(ProgressBar.fillAmount * 100f) + "%";
            yield return null;
        }
    }
}
