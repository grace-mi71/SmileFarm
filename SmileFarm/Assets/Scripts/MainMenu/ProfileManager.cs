using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    public static string NameText = "";
    public TMP_InputField inputField1;
    public static string FarmNameText = "";
    public TMP_InputField inputField2;
    public static string FarmInfoText = "";
    public TMP_InputField inputField3;

    void OnDisable()
    {
        NameText = inputField1.text;
        FarmNameText = inputField2.text;
        FarmInfoText = inputField3.text;
    }

    void OnEnable()
    {
        inputField1.text = NameText;
        inputField2.text = FarmNameText;
        inputField3.text = FarmInfoText;
    }
}
