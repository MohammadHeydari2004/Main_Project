using UnityEngine;
using UnityEngine.SceneManagement;
public class seaneMeneger : MonoBehaviour
{
    public string sceneName; // نام Scene مقصد
    public float delay = 5f; // زمان تاخیر به ثانیه

    void Start()
    {
        Invoke("LoadNextScene", delay);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
