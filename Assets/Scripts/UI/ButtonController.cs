using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum ButtonType
    {
        LevelUp,
        Upgrade1,
        Upgrade2
    }

    [RequireComponent(typeof(Button))]
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private ButtonType _type;

        public ButtonType Type => _type;
    }
}