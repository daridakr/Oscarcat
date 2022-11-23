using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogPerson
{
    public Sprite Face;
    public string Name;

    public DialogPerson(string name, string facePath)
    {
        Name = name;
        Face = Resources.Load<Sprite>(facePath);
    }
}

public class DialogPhrase
{
    public DialogPerson Speaker;
    public string Message;

    public DialogPhrase(DialogPerson person, string message)
    {
        Speaker = person;
        Message = message;
    }
}

public class Dialog
{
    public enum DialogType
    {
        FIRST_DIALOG
    }
    public DialogType Type;
    public List<DialogPhrase> Phrases = new List<DialogPhrase>();
    public Dialog(DialogType type)
    {
        Type = type;
    }
    public void AddPhrase(DialogPhrase phrase)
    {
        Phrases.Add(phrase);
    }
}

public static class DialogManager
{
    public static List<Dialog> Dialogs = new List<Dialog>();
    public static void AddDialog(Dialog dialog)
    {
        Dialogs.Add(dialog);
    }
}


public class DialogScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DialogPerson Oscar = new DialogPerson("Оскар", "oscarFace");
        DialogPerson OscarMother = new DialogPerson("Мама", "oscarMotherFace");
        Dialog firstDialog = new Dialog(Dialog.DialogType.FIRST_DIALOG);
        firstDialog.AddPhrase(new DialogPhrase(Oscar, "С меня хватит! Сегодня я выберусь на волю и буду сам добывать себе еду. Я уже взрослый..."));
        DialogManager.AddDialog(firstDialog);
    }
}
