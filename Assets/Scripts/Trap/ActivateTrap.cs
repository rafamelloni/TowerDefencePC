using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateTrap : MonoBehaviour
{
    public Transform player;
    public GameObject ray;
    public Canvas trapCanvas;
    public Button trapBtn;

    public bool trapActivated;
    public AudioClip flameSoundClip;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position )<3f)
        {
            trapCanvas.gameObject.SetActive(true);
        }
        else
        {
            trapCanvas.gameObject.SetActive(false);
        }
    }

    public void onButtonClick()
    {
        if(GameManager.Instance.coins >= 30)
        {
            GameManager.Instance.DecreaseCoins(30);
            StartCoroutine(activateTrap());
            trapBtn.gameObject.SetActive(false);
        }
        else
        {
            GameManager.Instance.InsufficientFunds();
        }

    }

    public IEnumerator activateTrap()
    {
        trapActivated = true;
        ray.gameObject.SetActive(true);
        SoundFXManager.instance.PlaySoundFXClip(flameSoundClip, transform, 1f);
        yield return new WaitForSeconds(8f);
        ray.gameObject.SetActive(false);
        trapActivated = false;
        trapBtn.gameObject.SetActive(true);
    }

}
