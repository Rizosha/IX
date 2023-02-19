using UnityEngine;
using UnityEngine.UI;

namespace GameControllerScripts
{
    public class NameInput : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        public void Submit() {
            TimerSaveHandler.instance.SaveName(inputField.text.ToUpper());
        }
    }
}
