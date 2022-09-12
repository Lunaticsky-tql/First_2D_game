using UnityEngine;
using UnityEngine.UI;

public class OptionAudioSetPanel : ViewBase
{


    #region Fields

    public Slider sliderMusic;
    public Slider sliderSound;

    #endregion

    #region Action Listeners

    public void OnMusicValueChange( float f ) {
        PlayerPrefs.SetFloat(AudioConst.MusicVolume, f);
        // AudioManager.Instance.ChangeMusicVolume(f);
    }


    public void OnSoundValueChange(float f) {
      
        PlayerPrefs.SetFloat(AudioConst.SoundVolume, f);
   
        // AudioManager.Instance.ChangeSoundVolume(f);

    }
    #endregion

    #region Override Methods

    public override void Show()
    {
        base.Show();
        sliderMusic.value = PlayerPrefs.GetFloat(AudioConst.MusicVolume, 0);
        sliderSound.value = PlayerPrefs.GetFloat(AudioConst.SoundVolume, 0);

    }

    #endregion


}