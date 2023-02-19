using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        /// <summary>
        /// Health slider and health updates 
        /// </summary>
        public Slider slider;
   
        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(float health)
        {
            slider.value = health;
        }
    }
}
