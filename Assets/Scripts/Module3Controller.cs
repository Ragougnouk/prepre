using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Module3Controller : MonoBehaviour
{
    public carnet_anim ca;
    public carnet_fill cf;

    private TMP_InputField[] inputFields;
    public MessagesList messageFile;
    public moduleSequencer modSeq;
    public module4Controller mod4;

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

    public bool actif = false;

    public GameObject light;
    public GameObject canvasMod3Up;

    public Sprite val;
    public Color correctColor;

    public bool on = false;

    public flicker flckr;
    public breakerController bc;

    public bool randomTarget = false;

    //private List<char> absentCharacters = new List<char>();
    private String noLetterString;
    public TMP_Text noLetters;
    public GameObject noLettersObj;
    public GameObject stopSign;
    private int errors;

    private Dictionary<char, int> absentCharactersCount = new Dictionary<char, int>();

    //public List<TMP_InputField> inputFields = new List<TMP_InputField>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(actif && !ca.actif && on)
        {

            
            
             // Vérifiez si l'utilisateur appuie sur backspace.
            if (Input.GetKeyDown(KeyCode.Backspace))
            {

                if(AreAllFieldsFilled())
                {
                    FocusInputField(inputFields[inputFields.Length-1]);
                }

                for (int i = 0; i < inputFields.Length; i++)
                {
                    if (inputFields[i].isFocused && i > 0 && inputFields[i].text.Length == 0)
                    {
                        int prevIndex = FindPreviousInteractableIndex(i);
                        //print("prev = " + prevIndex);
                        if (prevIndex != -1) // Assurez-vous qu'un champ interactif suivant existe.
                        {
                            EventSystem.current.SetSelectedGameObject(null);
                            FocusInputField(inputFields[prevIndex]);
                        }
                        // Focus sur le champ précédent si actuel est vide.
                        //FocusInputField(inputFields[i - 1]);
                    }
                }
            }

            /*if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //EventSystem.current.SetSelectedGameObject(null);
                for (int i = 0; i < inputFields.Length; i++)
                {
                    if (inputFields[i].isFocused && i > 0)
                    {
                        // Focus sur le champ précédent si actuel est vide.
                        int prevIndex = FindPreviousInteractableIndex(i);
                        print("prev = " + prevIndex);
                        if (prevIndex != -1) // Assurez-vous qu'un champ interactif suivant existe.
                        {
                            EventSystem.current.SetSelectedGameObject(null);
                            FocusInputField(inputFields[prevIndex]);
                        }
                    }
                }    
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                //EventSystem.current.SetSelectedGameObject(null);
                for (int i = 0; i < inputFields.Length; i++)
                {
                    if (inputFields[i].isFocused && i < inputFields.Length)
                    {
                        // Focus sur le champ précédent si actuel est vide.
                        int nextIndex = FindNextInteractableIndex(i+1);
                        print("next = " + nextIndex);
                        if (nextIndex != -1) // Assurez-vous qu'un champ interactif suivant existe.
                        {
                            EventSystem.current.SetSelectedGameObject(null);
                            FocusInputField(inputFields[nextIndex]);
                        }
                    }
                }    
            }*/

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

        if(!actif && on && !randomTarget)
        {
            randomTarget = true;
        }        if(randomTarget && (actif || !on))
        {
            randomTarget = false;
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
        if (currentField.text.Length == 1 && currentIndex < inputFields.Length)
        {
            // Trouver le prochain champ interactif.
            int nextIndex = FindNextInteractableIndex(currentIndex + 1);
            if (nextIndex != -1) // Assurez-vous qu'un champ interactif suivant existe.
            {
                FocusInputField(inputFields[nextIndex]);

            }
            else
            {
                UnFocusInputField(inputFields[currentIndex]);
            }
        }
    }

    void FocusInputField(TMP_InputField field)
    {
        field.Select();
        field.ActivateInputField();
    }

    void UnFocusInputField(TMP_InputField field)
    {
        field.DeactivateInputField();
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

    int FindPreviousInteractableIndex(int startIndex)
    {
        for (int i = 1;i < startIndex + 1; i++)
        {
            if (!inputFields[startIndex - i].readOnly)
            {
                return startIndex - i;
            }
        }
        return -1;
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
        //absentCharacters.Clear();

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
                testLetters(inputFields[i]);
                StartCoroutine(FadeOutInputField(inputFields[i]));
                inputFields[i].readOnly = false; // Assurez-vous que le champ n'est pas en readOnly si incorrect
                isAllCorrect = false;
                //audioSource.PlayOneShot(incorrectSound); // Jouez le son d'erreur.
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
            audioSource.PlayOneShot(incorrectSound);
            ls.loseHP();
            errors += 1;
            FocusInputField(firstIncorrectField);

            // d'autres actions pour gérer un champ incorrect
        }

        /*if(absentCharacters.Count != 0)
        {
            noLettersObj.SetActive(true);
            stopSign.SetActive(true);

            if(noLetterString == null)
            {
                noLetterString += absentCharacters[0];
                noLetters.SetText(noLetterString);
            }
            else if(!noLetterString.Contains(absentCharacters[0]) && errors)
            {
                print("test  " + (noLetterString.Length +1) % 2);
                noLetterString += absentCharacters[0];
                noLetters.SetText(noLetterString); 
            }
            
        }*/
        int maxCount = 0;

        foreach (var entry in absentCharactersCount)
        {
            if (entry.Value > (message.Length/2)-1)
            {
                //print("yes");
                StartCoroutine(delayHint(entry.Key));
                /*mod4.loading.SetActive(false);
                mod4.loadingBarSize(0);
                noLettersObj.SetActive(true);
                //stopSign.SetActive(true);
                  
                if(noLetterString == null)
                {
                    noLetterString += entry.Key;
                }
                else if (!noLetterString.Contains(entry.Key) && noLetterString.Length < 10)
                {
                    noLetterString += entry.Key;
                }
            noLetters.SetText(noLetterString);*/
            }
            if(noLetterString == null)
            {
                
                if (entry.Value > maxCount)
                {
                    maxCount = entry.Value;
                    if(maxCount > (message.Length/2) - 1)
                    {
                        maxCount = (message.Length/2);
                    }
                }

                //print("loading "+ (100*maxCount/(message.Length/2.0f)));
                //mod4.loading.SetActive(true);
                //mod4.loadingBarSize((maxCount*94)/message.Length);
            }
            mod4.loadingBarSize((maxCount*94)/(message.Length/2));
        }

        if (isAllCorrect)
        {
            //audioSource.PlayOneShot(CorrectSound);
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
        yield return new WaitForSeconds(0.2f); // Attendez une seconde.

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
        TMP_InputField firstEmptyField = FindFirstEmptyField();
        if(ls.healthPoints > 0 && ls.healthPoints < 10)
        {
            FocusInputField(firstEmptyField);
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
        if(!(noLetterString == null))
        {
            mod4.hintDisplay = true;
        }
    }

    public void turnOff()
    {
        absentCharactersCount.Clear();
        //noLettersObj.SetActive(false);
        //stopSign.SetActive(false);
        //noLetters.SetText("");
        on = false;
        light.SetActive(false);
        flckr.enabled = false;
        canvasMod3Up.SetActive(false);
        reInit();
    }

    public void active()
    {
        nextStep = false;
        actif = true;
        if(!on)
        {
            bc.flickOn(4,5);
        }
        //mod4.stopSign.SetActive(true);
        mod4.hint.SetActive(true);
        if(!(noLetterString == null))
        {
           //mod4.loading.SetActive(true); 
        }
        
        //mod4.lineHint.SetActive(true);
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
        light.GetComponent<SpriteRenderer>().enabled = true;
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
        absentCharactersCount.Clear();
        mod4.hintDisplay = false;
        //noLettersObj.SetActive(false);
        //stopSign.SetActive(false);
        noLetterString = null;
        noLetters.SetText("");
        
        audioSource.PlayOneShot(CorrectSound);
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(CorrectSound);
        actif = false;
        yield return new WaitForSeconds(0.0f);
        StartCoroutine(modSeq.winMod3());
    }

    private void testLetters(TMP_InputField wif)
    {
        char inputChar = wif.text[0];

        /*if(noLetterString != null)
        {
            if (!message.Contains(inputChar) && !noLetterString.Contains(inputChar))
            {
                absentCharacters.Add(inputChar);
            }
        }
        else
        {
            if (!message.Contains(inputChar))
            {
                absentCharacters.Add(inputChar);
            }
        }*/

        if (!message.Contains(inputChar))
        {
                    // Incrémentez le compteur pour ce caractère
            if (absentCharactersCount.ContainsKey(inputChar))
            {
                absentCharactersCount[inputChar]++;
            }
            else
            {
                absentCharactersCount[inputChar] = 1;
            }
        }
        
    }
    private IEnumerator delayHint(char entryChar)
    {
        yield return new WaitForSeconds(0.5f);

        if(nextStep)
        {
            yield break;
        }

        mod4.loading.SetActive(false);
        mod4.loadingBarSize(0);
        noLettersObj.SetActive(true);
        


        if(noLetterString == null)
        {
            noLetterString += entryChar;
        }
        else if (!noLetterString.Contains(entryChar) && noLetterString.Length < 10)
        {
            noLetterString += entryChar;
        }
        noLetters.SetText(noLetterString);
    }
}
