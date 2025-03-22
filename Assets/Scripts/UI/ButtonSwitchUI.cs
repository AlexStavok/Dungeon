using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitchUI : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject secondButton;


    private void Start()
    {
        Player.Instance.OnInteractReady += Player_OnInteractReady;
        Player.Instance.OnAttackReady += Player_OnAttackReady;
    }

    private void Player_OnAttackReady(object sender, System.EventArgs e)
    {
        firstButton.SetActive(true);
        secondButton.SetActive(false);
    }

    private void Player_OnInteractReady(object sender, System.EventArgs e)
    {
        firstButton.SetActive(false);
        secondButton.SetActive(true);
    }
}
