using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cameraZoom;
    public GameObject appOpen;
    public SpriteRenderer touchApp;
    public Animator panel;
    public GameObject panelBack;
    Animator appAnim;
    Animator cameraAnim;

    public GameObject UI;
    
    void Start()
    {
        UI.SetActive(true);
        panelBack.SetActive(false);
        appAnim = appOpen.GetComponent<Animator>();
        cameraAnim = cameraZoom.GetComponent<Animator>();
        touchApp = touchApp.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }



    IEnumerator animationStart()
    {
        

        for (float i = 0; i < 50; i++)
        {
            touchApp.color = new Color(touchApp.color.r, touchApp.color.g, touchApp.color.b, i / 100);
            yield return new WaitForSeconds(0.01f);
        }
        for (float i = 50; i > 0; i -= 1.5f)
        {
            touchApp.color = new Color(touchApp.color.r, touchApp.color.g, touchApp.color.b, i / 100);
            yield return new WaitForSeconds(0.01f);

            

        }

        UI.SetActive(false);

        cameraAnim.SetTrigger("Zoom");
        yield return new WaitForSeconds(1.0f);
        appAnim.SetTrigger("Open");
        yield return new WaitForSeconds(3.0f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("T4_Level");

    }

    public void Play()
    {
        StartCoroutine(animationStart());
    }

    public void Quit()
    {
        panel.SetBool("isPanelOn", true);
        panelBack.SetActive(true);
        panelBack.GetComponent<Animator>().SetBool("isPanelOn", true);
        
    }

    public void Saved()
    {
        panel.SetBool("isPanelOn", false);
        panelBack.GetComponent<Animator>().SetBool("isPanelOn", false);
        panelBack.SetActive(false);
    }

    public void DefinitiveQuit()
    {
        Application.Quit();
    }
}
