using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {


	public void LoadLevel0()
	{
		Application.LoadLevel("Level0");		
	}
	public void LoadMainMenu()
	{
		Application.LoadLevel("Menu");		
	}
}
