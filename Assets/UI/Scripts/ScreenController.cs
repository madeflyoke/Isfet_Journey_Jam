using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Scripts
{
    public class ScreenController : MonoBehaviour
    { 
        public event Action OnTutorClosed;
        [SerializeField] private VisualTreeAsset TutorialScreen;
        [SerializeField] private VisualTreeAsset GameplayScreen;
        [SerializeField] private VisualTreeAsset FadeScreen;
        private VisualElement _root;
        private VisualElement _fade;
        private VisualElement _tutor;
        private int _fadeAnimationDuration;
        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            
            _tutor = TutorialScreen.CloneTree().Q<VisualElement>(nameof(UIElementsType.TutorScreen));
            _fade = FadeScreen.CloneTree().Q<VisualElement>(nameof(UIElementsType.Fade));
            _root.Add(_fade);
            _root.Add(_tutor);
            _tutor.AddToClassList(nameof(StyleClasses.TutorHide));
            _fadeAnimationDuration =  1000;
            
        }
        
        public async void EnableFade(Action action= null)
        {
            _fade.RemoveFromClassList(nameof(StyleClasses.HalfFade));
            _fade.AddToClassList(nameof(StyleClasses.FullFade));
            await UniTask.Delay(_fadeAnimationDuration);
            action?.Invoke();
        }
        
        public async void SetFadeHalf(Action action= null)
        {
            _fade.RemoveFromClassList(nameof(StyleClasses.FullFade));
            _fade.AddToClassList(nameof(StyleClasses.HalfFade));
            await UniTask.Delay(_fadeAnimationDuration);
            action?.Invoke();
        }
        
        public async void DisableFade(Action action= null)
        {
            _fade.RemoveFromClassList(nameof(StyleClasses.FullFade));
            _fade.RemoveFromClassList(nameof(StyleClasses.HalfFade));
            await UniTask.Delay(_fadeAnimationDuration);
            action?.Invoke();
        }
        public void ShowTutor()
        {
           
            _tutor.Q<Button>(nameof(StyleClasses.CloseTutorBTN)).RegisterCallback<ClickEvent>(evt => CloseTutor());
            _tutor.RemoveFromClassList(nameof(StyleClasses.TutorHide));
        }
        
        public void CloseTutor()
        {
            _tutor.Q<Button>(nameof(StyleClasses.CloseTutorBTN)).clickable = new Clickable(()=>{});
            _tutor.AddToClassList(nameof(StyleClasses.TutorHide));
            DisableFade();
            OnTutorClosed?.Invoke();
        }
    }
    
    public enum StyleClasses{
       FullFade,
       HalfFade,
       TutorHide,
       CloseTutorBTN
    }
    
    public enum UIElementsType{
        TutorScreen,
        Fade
    }
}