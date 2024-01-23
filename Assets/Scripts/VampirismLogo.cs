using TMPro;
using UnityEngine;

public class VampirismLogo : MonoBehaviour
{
    [SerializeField] private TMP_Text _logo;

    private void Awake()
    {
        SetActiveFalse();
    }

    public void SetActiveFalse()
    {
        _logo.alpha = 0f;
    }

    public void SetActiveTrue()
    {
        _logo.alpha = 100f;
    }
}
