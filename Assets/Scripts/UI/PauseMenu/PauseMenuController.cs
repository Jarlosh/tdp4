using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseMenuController : MonoBehaviour
{
    [SerializeReference] InputActionReference pauseInput;
    [SerializeReference] InputActionReference mouseInput;
    [SerializeReference] InputActionAsset playeActionsMap;

    [SerializeReference] GameObject pauseMenuView;
    [SerializeReference] GameObject bottle;
    [SerializeReference] GameObject credits;
    [SerializeReference] Slider musicSlider;
    [SerializeReference] Slider mouseSlider;

    public UnityAction OnPauseUpdated;
    
    private float _actualSoundVolume = 1;
    private float _actualMouseSence = 1;
    
    public bool IsPaused { get; private set; }

    
    public void Continue()
    {
        Unpause();
    }

    public void Credits()
    {
        credits.SetActive(!credits.activeSelf);
    }

    public void Exit()
    {
        Application.Quit(0);
    }

    private void Awake()
    {
        bottle.SetActive(false);
        Cursor.visible = false;
        pauseInput.action.Enable();
        pauseInput.action.performed += (_) => UpdatePause();

        IsPaused = false;
        pauseMenuView.SetActive(false);

        musicSlider.onValueChanged.AddListener(SetSoundVolumeByView);
        mouseSlider.onValueChanged.AddListener(SetMouseSenseByView);

        SetSoundViewInitial(_actualSoundVolume);
        SetMouseViewInitial(_actualMouseSence);
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(SetSoundVolumeByView);
        mouseSlider.onValueChanged.RemoveListener(SetMouseSenseByView);
    }

    private void UpdatePause()
    {
        if (IsPaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        bottle.SetActive(true);
        pauseMenuView.SetActive(true);
        IsPaused = true;
        playeActionsMap.FindActionMap("Player").Disable();
        OnPauseUpdated?.Invoke();
    }

    private void Unpause()
    {
        bottle.SetActive(false);
        pauseMenuView.SetActive(false);
        IsPaused = false;
        playeActionsMap.FindActionMap("Player").Enable();
        OnPauseUpdated?.Invoke();
    }

    private void SetSoundVolumeByView(float volume)
    {
        _actualSoundVolume = volume;
        FMODUnity.RuntimeManager.GetVCA("vca:/Master").setVolume(volume);
    }

    private void SetMouseSenseByView(float sense)
    {
        _actualMouseSence = sense;
        mouseInput.action.ApplyParameterOverride("x", sense);
        mouseInput.action.ApplyParameterOverride("y", sense);
    }

    private void SetSoundViewInitial(float volume)
    {
        musicSlider.SetValueWithoutNotify(volume);
    }

    private void SetMouseViewInitial(float sense)
    {
        mouseSlider.SetValueWithoutNotify(sense);
    }
}