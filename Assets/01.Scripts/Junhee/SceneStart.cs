using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        SceneManager.LoadScene("Hanul",LoadSceneMode.Additive);
        SceneManager.LoadScene("Youngseo",LoadSceneMode.Additive);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
