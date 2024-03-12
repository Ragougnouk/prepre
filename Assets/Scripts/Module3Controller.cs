using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Module3Controller : MonoBehaviour
{
    public carnet_anim ca;
    public carnet_fill cf;

    private TMP_InputField[] inputFields;
    public MessagesList messageFile;
    public moduleSequencer modSeq;

    public life_system ls;

    private string message;
    private string[] messageLetters;

    public AudioClip CorrectSound;
    public AudioClip ErrorSound; // ce bruit survient quand on veut valider alors que certains espaces sont vacants.
    public AudioClip incorrectSound; //son quand il reste au moins une lettre à deviner
    public AudioClip buttonPressSound; // Son à jouer quand le bouton ou entrée est pressé.
    public AudioClip[] randomKeySounds; // Tableau pour stocker les bruits de clavier.
    public AudioSource audioSource;
    public Button validateButton;        // Bouton pour valider la saisie.
    public Color incorrectColor = Color.red; // Couleur pour les caractères incorrects.
    private Color originalTextColor;

    public bool nextStep = false;

    private bool actif = false;

    public GameObject light;
    public GameObject canvasMod3Up;

    public Sprite val;
    public Color correctColor;

    public bool on = false;

    public flicker flckr;

    //public List<TMP_InputField> inputFields = new List<TMP_InputField>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(actif && !ca.actif)
        {
            
             // Vérifiez si l'utilisateur appuie sur backspace.
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                for (int i = 0; i < inputFields.Length; i++)
                {
                    if (inputFields[i].isFocused && i > 0 && inputFields[i].text.Length == 0)
                    {
                        // Focus sur le champ précédent si actuel est vide.
                        FocusInputField(inputFields[i - 1]);
                    }
                }
            }
            // Vérifier si la touche "Entrée" est appuyée.
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ValidateTranslation(); // Appelle la fonction de validation lorsque "Entrée" est appuyée.
            }
            else if (Input.anyKeyDown) // Vérifie si n'importe quelle touche est pressée.
            {
                PlayRandomKeySound();
            }

            if(!AreAllFieldsFilled() && !Input.GetKeyDown(KeyCode.Backspace))
            {
                reFocus();
            }
        }
        

    }

    void OnEnable()
    {
        /*validateButton.onClick.AddListener(ValidateTranslation);
        messageLettersFill(modSeq.loopNumber);
        if (inputFields.Length > 0)
        {
            // Focus automatique sur le premier InputField.
            FocusInputField(inputFields[0]);
        }

        // Abonnez chaque InputField à un événement qui vérifie la longueur de son contenu.
        foreach (var field in inputFields)
        {
            field.onValueChanged.AddListener(delegate { CheckAutoTab(field); });
        }*/
    }





    void CheckAutoTab(TMP_InputField currentField)
    {
        int currentIndex = System.Array.IndexOf(inputFields, currentField);

        // Limiter la saisie à un seul caractère pour les champs qui ne sont pas en readOnly.
        if (currentField.text.Length > 1 && !currentField.readOnly)
        {
            currentField.text = currentField.text.Substring(0, 1);
        }

        // Sauter les champs qui sont en readOnly.
        if (currentField.text.Length == 1 && currentIndex < inputFields.Length - 1)
        {
            // Trouver le prochain champ interactif.
            int nextIndex = FindNextInteractableIndex(currentIndex + 1);
            if (nextIndex != -1) // Assurez-vous qu'un champ interactif suivant existe.
            {
                FocusInputField(inputFields[nextIndex]);
            }
        }
    }

    void FocusInputField(TMP_InputField field)
    {
        field.Select();
        field.ActivateInputField();
    }

    int FindNextInteractableIndex(int startIndex)
    {
        for (int i = startIndex; i < inputFields.Length; i++)
        {
            if (!inputFields[i].readOnly)
            {
                return i;
            }
        }
        return -1; // Retourne -1 s'il n'y a pas de champs interactifs suivants.
    }

    public void ValidateTranslation()
    {
        // Vérifier si toutes les zones de saisie sont remplies.
        bool allFieldsFilled = AreAllFieldsFilled();

        // Si au moins un champ est vide, jouer le son d'erreur et ne rien faire de plus.
        if (!allFieldsFilled)
        {
            audioSource.PlayOneShot(ErrorSound);
            TMP_InputField firstEmptyField = FindFirstEmptyField();
            if (firstEmptyField != null)
            {
                FocusInputField(firstEmptyField);
            }
            return;
        }

        //Jouer le son du bouton pressé.
        audioSource.PlayOneShot(buttonPressSound);

        bool isAllCorrect = true;

        TMP_InputField firstIncorrectField = null; // Garder une référence au premier champ incorrect.

        for (int i = 0; i < messageLetters.Length; i++)
        {
            // On vérifie si le texte est égal, sans tenir compte de la casse.
            if (string.Equals(inputFields[i].text, messageLetters[i], StringComparison.OrdinalIgnoreCase))
            {
                // Si c'est correct, mettre le champ en readOnly.
                inputFields[i].GetComponent<Image>().sprite = val;
                inputFields[i].textComponent.color = correctColor;
                inputFields[i].readOnly = true;
                inputFields[i].interactable = false; // Désactiver le champ pour clarifier visuellement qu'il ne peut plus être modifié.
            }
            else
            {
                // Si c'est incorrect, on change la couleur en rouge et on planifie la disparition.
                //inputFields[i].textComponent.color = incorrectColor;
                StartCoroutine(FadeOutInputField(inputFields[i]));
                inputFields[i].readOnly = false; // Assurez-vous que le champ n'est pas en readOnly si incorrect
                isAllCorrect = false;
                audioSource.PlayOneShot(incorrectSound); // Jouez le son d'erreur.
                // Si c'est le premier champ incorrect, on le garde en mémoire.
                if (firstIncorrectField == null)
                {
                    firstIncorrectField = inputFields[i];
                    
                }
            }
        }

        // Si tout n'est pas correct, on met le focus sur le premier champ incorrect.
        if (!isAllCorrect && firstIncorrectField != null)
        {
            ls.loseHP();
            FocusInputField(firstIncorrectField);
            // d'autres actions pour gérer un champ incorrect
        }

        if (isAllCorrect)
        {
            audioSource.PlayOneShot(CorrectSound);
            nextStep = true;
            StartCoroutine(success());
            // Autres actions à effectuer si tout est correct..
        }
    }

    private bool AreAllFieldsFilled()
    {
        foreach (var field in inputFields)
        {
            if (string.IsNullOrWhiteSpace(field.text))
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator FadeOutInputField(TMP_InputField inputField)
    {
        Color originalColor = inputField.textComponent.color; // Sauvegardez la couleur originale du texte.
        inputField.textComponent.color = incorrectColor; // Changez la couleur en rouge pour indiquer une erreur.
        yield return new WaitForSeconds(1); // Attendez une seconde.

        if (inputField != null) // Vérifiez si l'InputField n'est pas détruit.
        {
            inputField.text = ""; // Effacez le texte.
            inputField.textComponent.color = originalColor; // Rétablissez la couleur originale.

            // Réactivez le champ si ce n'est pas le dernier ou si c'est le dernier mais incorrect.
            if (inputField == inputFields[inputFields.Length - 1] || !inputField.readOnly)
            {
                inputField.readOnly = false;
                // Réactiver le champ de saisie pour la saisie et lui redonner le focus si nécessaire.
                if (inputField.text.Length == 0)
                {
                    //FocusInputField(inputField);
                }
            }
        }
    }

    private TMP_InputField FindFirstEmptyField()
    {
        foreach (var field in inputFields)
        {
            if (string.IsNullOrWhiteSpace(field.text))
            {
                return field;
            }
        }
        return null;
    }



    private void PlayRandomKeySound()
        {
            // Assurez-vous qu'il y a au moins un son dans le tableau et que la touche Entrée n'est pas celle pressée.
            if (randomKeySounds.Length > 0 && !(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            {
                int index = UnityEngine.Random.Range(0, randomKeySounds.Length); // Sélection aléatoire d'un index.
               audioSource.PlayOneShot(randomKeySounds[index]); // Jouer le son au hasard.
            }
        }





    public void IFlistFill(List<TMP_InputField> IFList)
    {
        inputFields = IFList.ToArray();
    }

    private void messageLettersFill(int lNb)
    {

        List<string> letters = new List<string>(); 
        message = messageFile.stringList[lNb];
        for(int i = 0; i < message.Length; i++)

        {

            if (message[i] != ' ')
            {

                string currentLetter = message[i].ToString();
                letters.Add(currentLetter);

            }

        }

        messageLetters = letters.ToArray();

    }

    public void reInit()
    {   
        if (inputFields != null && inputFields.Length != 0)
        {
            foreach(TMP_InputField inF in inputFields)
            {
                inF.readOnly = true;
            }
            inputFields = new TMP_InputField[0];
        }
    }

    public void turnOn()
    {
        on = true;
        light.SetActive(true);
        canvasMod3Up.SetActive(true);
    }

    public void turnOff()
    {
        on = false;
        light.SetActive(false);
        flckr.enabled = false;
        canvasMod3Up.SetActive(false);
        reInit();
    }

    public void active()
    {
        actif = true;
        flckr.enabled = true;
        validateButton.onClick.AddListener(ValidateTranslation);
        messageLettersFill(modSeq.loopNumber);
        if (inputFields.Length > 0)
        {
            // Focus automatique sur le premier InputField.
            FocusInputField(inputFields[0]);
        }

        // Abonnez chaque InputField à un événement qui vérifie la longueur de son contenu.
        foreach (var field in inputFields)
        {
            field.onValueChanged.AddListener(delegate { CheckAutoTab(field); });
        }
    }

    public void inactive()
    {
        actif = false;
        flckr.enabled = false;
    }

    void reFocus()
    {
        // Check if any input field is currently focused
        bool anyInputFieldFocused = false;
        foreach (var inputField in inputFields)
        {
            if (inputField.isFocused)
            {
                anyInputFieldFocused = true;
                break;
            }
        }

        // If no input field is focused, focus the first one
        if (!anyInputFieldFocused)
        {
            TMP_InputField firstEmptyField = FindFirstEmptyField();
            FocusInputField(firstEmptyField);
        }
    }

    private IEnumerator success()
    {
        actif = false;
        yield return new WaitForSeconds(1);
        StartCoroutine(modSeq.winMod3());
    }
}
