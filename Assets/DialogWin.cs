using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogWin : MonoBehaviour
{
    public Text SpeakerName, SpeakerPhrase;
    public Image SpeakerFace;

    int MessageCooldown, CurrentMessageNum;

    Dialog CurrentDialog;

    public void StartDialog(Dialog.DialogType type)
    {
        CurrentMessageNum = 0;
        CurrentDialog = DialogManager.Dialogs.Find(x => x.Type == type);
        transform.DOLocalMoveY(28.3f, 0.15f);
        ShowMessage();
    }

    void ShowMessage()
    {
        StopCoroutine("Print Message");
        DialogPhrase currentPhrase;
        if (CurrentDialog.Phrases.Count > CurrentMessageNum) currentPhrase = CurrentDialog.Phrases[CurrentMessageNum];
        else
        {
            EndDialog();
            return;
        }

        MessageCooldown = 3;
        SpeakerFace.sprite = currentPhrase.Speaker.Face;
        SpeakerName.text = currentPhrase.Speaker.Name;
        SpeakerPhrase.text = "";
        StartCoroutine(PrintMessage(currentPhrase.Message));
        CurrentMessageNum++;
    }

    IEnumerator PrintMessage(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            SpeakerPhrase.text += message[i];
            if (i == message.Length - 1) StartCoroutine(NextMessage());
            yield return new WaitForSeconds(.0001f);
        }
    }

    IEnumerator NextMessage()
    {
        while (MessageCooldown > 0)
        {
            MessageCooldown--;
            yield return new WaitForSeconds(1);
        }

        ShowMessage();
    }

    void EndDialog()
    {
        transform.DOLocalMoveY(-187, 0.15f);
    }
}
