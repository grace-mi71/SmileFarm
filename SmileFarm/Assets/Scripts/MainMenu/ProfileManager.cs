// Owner: Lee Haejun
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Manages the profile settings UI and persists input field values across enable/disable cycles
public class ProfileManager : MonoBehaviour
{
    public static string NameText = "";        // Stores the player's name
    public TMP_InputField inputField1;         // Input field for player name

    public static string FarmNameText = "";    // Stores the farm name
    public TMP_InputField inputField2;         // Input field for farm name

    public static string FarmInfoText = "";    // Stores the farm description
    public TMP_InputField inputField3;         // Input field for farm description

    // Save input field values to static variables when the object is disabled
    void OnDisable()
    {
        NameText = inputField1.text;
        FarmNameText = inputField2.text;
        FarmInfoText = inputField3.text;
    }

    // Restore saved values back into input fields when the object is enabled
    void OnEnable()
    {
        inputField1.text = NameText;
        inputField2.text = FarmNameText;
        inputField3.text = FarmInfoText;
    }
}