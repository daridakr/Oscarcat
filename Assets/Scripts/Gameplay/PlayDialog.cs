using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayDialog : MonoBehaviour
{
    public Dialog.DialogType DialogType;
    public DialogWin Dialog;

    private void Awake()
    {
        DOTween.Init();
       
    }

    public void Start()
    {
        //Dialog.StartDialog(DialogType);
    }

    public void StartDialog()
    {
        
    }

    public void EndDialog()
    {
        GetComponent<Button>().enabled = true;
    }
}
