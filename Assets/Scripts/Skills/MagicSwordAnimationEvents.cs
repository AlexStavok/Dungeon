using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSwordAnimationEvents : MonoBehaviour
{
    [SerializeField] private MagicSword magicSword;
     
    public void StartFlying()
    {
        magicSword.StartFlying();
    }
    public void DestroySword()
    {
        magicSword.DestroySword();
    }
}
