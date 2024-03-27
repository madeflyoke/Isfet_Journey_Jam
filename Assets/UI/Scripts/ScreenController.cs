using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Scripts
{
    public class ScreenController : MonoBehaviour
    {
        [SerializeField] private VisualTreeAsset TutorialScreen;
        [SerializeField] private VisualTreeAsset WinScreen;
        [SerializeField] private VisualTreeAsset LoseScreen;
        [SerializeField] private VisualTreeAsset GameplayScreen;
    }
}