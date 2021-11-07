using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
	[Header("UI")]
	public Text messageText;
	public InputField emailInput;
	public InputField passwordInput;

	public void RegisterButton()
	{
		if (passwordInput.text.Length < 6)
		{
			messageText.text = "Password too short";
			return;
		}

		var request = new RegisterPlayFabUserRequest
		{
			Email = emailInput.text,
			Password = passwordInput.text,
			RequireBothUsernameAndEmail = false
		};
		PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
	}

	void OnRegisterSuccess(RegisterPlayFabUserResult result)
	{
		messageText.text = "Registered, please log in.";
	}

	public void LoginButton()
	{
		var request = new LoginWithEmailAddressRequest
		{
			Email = emailInput.text,
			Password = passwordInput.text
		};
		PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
	}

	public void ResetPasswordButton()
	{
		var request = new SendAccountRecoveryEmailRequest
		{
			Email = emailInput.text,
			TitleId = "DB19D"               //TO BE ADDED
		};
		PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
	}

	void OnPasswordReset(SendAccountRecoveryEmailResult result)
	{
		messageText.text = "Password reset mail sent";
	}
	
												//HERE1

	void start()
    {
		Login();
    }

	void Login()
    {
		var request = new LoginWithCustomIDRequest
		{
			CustomId = SystemInfo.deviceUniqueIdentifier,
			CreateAccount = true
		};
		PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

	void OnLoginSuccess(LoginResult result)
	{
		messageText.text = "";
		Debug.Log("Successful login/account created");
	}

											//JSON DO I NEED THIS?

	void OnError(PlayFabError error)
	{
		messageText.text = error.ErrorMessage;
		Debug.Log(error.GenerateErrorReport());
	}
























}
