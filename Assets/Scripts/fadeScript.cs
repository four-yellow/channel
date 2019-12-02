using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeScript : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("goal").GetComponent<ToNextLevel>().readyToTransition)
        {
            StartCoroutine("PlayAnimation");
        }
    }

    IEnumerator PlayAnimation()
    {
        FadeToLevel();
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(GameObject.FindWithTag("goal").GetComponent<ToNextLevel>().levelName);
    }
}
