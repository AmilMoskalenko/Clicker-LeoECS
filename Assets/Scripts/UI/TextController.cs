using TMPro;
using UnityEngine;

namespace UI
{
    public enum TextType
    {
        Name,
        Level,
        Income,
        LevelUp,
        Upgrade1,
        Upgrade2
    }

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextController : MonoBehaviour
    {
        [SerializeField] private TextType _type;

        public TextType Type => _type;
    }
}