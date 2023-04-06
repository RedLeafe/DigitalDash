using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public static bool Clicked = false;
	public GameObject Light;
	public GameObject ExclamationMark;
	public GameObject Player;
	public GameObject SetttingsButton;
	public GameObject Dim;
	public GameObject Credits;
	public Animator animator;
	public AudioMixer audioMixer;
	public Slider SoundSlider;
	private float MusicVolume;
	public int multiplier = 12;
	private bool Once;

	void Start()
	{
		Clicked = false;
        Once = true;
		if (PlayerPrefs.HasKey("Volume") == false)
		{
			PlayerPrefs.SetFloat("Volume", -30f);
		}
		MusicVolume = PlayerPrefs.GetFloat("Volume");
		audioMixer.SetFloat("Volume", MusicVolume);
		SoundSlider.value = MusicVolume;
	}

    private void Update()
    {
		if (Clicked == true & Light.transform.position.x >= -12)
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Loads the next scene in the build index
		}
		else if (Clicked == true)
        {
			Light.transform.Translate((0.01f * multiplier), 0, 0);
			if (Light.transform.position.x >= -15.1)
            {
				Player.transform.Translate((0.01f * multiplier), 0, 0);
			}
		}
		if (Clicked == true & Light.transform.position.x >= -15 & Once == true)
		{
			ExclamationMark.SetActive(false);
			Player.transform.Rotate(0, 180, 0);
			animator.SetBool("Running", true);
			Once = false;
		}
	}
    public void playGame()
	{
		if (Dim.activeInHierarchy == false && Credits.activeInHierarchy == false) { 
		Clicked = true;
		ExclamationMark.SetActive(true);
		SetttingsButton.SetActive(false);
		}
	}

	public void settingsMenu(){
		if (Dim.activeInHierarchy == false)
		{
			Dim.SetActive(true);
		}
		//SceneManager.LoadScene("SettingsMenu");
	}

	public void creditsMenu()
	{
		Dim.SetActive(false);
		if (Credits.activeInHierarchy == false)
		{
			Credits.SetActive(true);
		}
		//SceneManager.LoadScene("SettingsMenu");
	}

	public void backButton()
	{
		if (Dim.activeInHierarchy == true)
		{
			Dim.SetActive(false);
		}
		//SceneManager.LoadScene("SettingsMenu");
	}
	public void creditsBackButton()
	{
		if (Credits.activeInHierarchy == true)
		{
			Credits.SetActive(false);
		}
		Dim.SetActive(true);
		//SceneManager.LoadScene("SettingsMenu");
	}

	public void SetVolume(float decimalVolume)
	{		
		/*var dbVolume = Mathf.Log10(decimalVolume) * 20;
		if (decimalVolume == 0.0f)
		{
			dbVolume = -80.0f;
		}	
		*/
		MusicVolume = decimalVolume;
		audioMixer.SetFloat("Volume", MusicVolume);
		PlayerPrefs.SetFloat("Volume", MusicVolume);
		PlayerPrefs.Save();
	}

	public void storeMenu(){
		SceneManager.LoadScene("StoreMenu");
	}
}