using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ImagimonTheGame
{
public class BattleDialogBox : MonoBehaviour
{
    //[SerializeField] int lettersPerSecond;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] Button attack1Button;
    [SerializeField] TMP_Text attack1ButtonText;

    [SerializeField] Button attack2Button;
    [SerializeField] TMP_Text attack2ButtonText;

    [SerializeField] Button attack3Button;
    [SerializeField] TMP_Text attack3ButtonText;

    [SerializeField] Button attack4Button;
    [SerializeField] TMP_Text attack4ButtonText;

    [SerializeField] Button switchButton;
    [SerializeField] TMP_Text switchButtonText;

    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> moveTexts;

    [SerializeField] TMP_Text pp_Text;
    [SerializeField] TMP_Text typeText;

    public IEnumerator SetDialog(string dialog)
    {
        dialogText.text = dialog;
        yield return new WaitForSeconds(1f);
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableMoveSelector(bool enabled)
    {
        moveDetails.SetActive(enabled);
    }
    
    public void SetMoveNames(List<Move> moves)
    {
        attack1ButtonText.text = moves[0].Base.Name;
        attack2ButtonText.text = moves[1].Base.Name;
        attack3ButtonText.text = moves[2].Base.Name;
        attack4ButtonText.text = moves[3].Base.Name;

    }

    /*
    public IEnumerator TypeDialog(string dialog)
    {
        dialogText = "";
        foreach (var letter in dialog.ToCharArray()) { 
            dialogText += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
    */

}
}
