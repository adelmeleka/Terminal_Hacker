using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hacker : MonoBehaviour
{

    // Member variables - available everywhere
    const string menuHint = "You may type menu at any time.";
    // Game configuration data
    string[] level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] level3Passwords = { "starfield", "telescope", "environment", "exploration", "astronauts" };
    //game state
    int level;
    //used for menu items
    enum Screen { MainMenu, Password, Win};
    Screen currentScreen;
    string userPassword;
    string correctPassword;

    void ShowMainMenu()
    {
        //write to unity console
        //print("Hello Console");
        currentScreen = Screen.MainMenu;

        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?\n");

        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for NASA");

        Terminal.WriteLine("\nEnter your selection:");
        
    }

    // return user input after pressing return button
    // prepared by terminal hacker package
    //this only decide who to handle user input, not actually do it 
    void OnUserInput(string input)
    {
        //we can always go direct to main menu in any state
        if (input == "menu") { ShowMainMenu();}
        else if (currentScreen == Screen.MainMenu) { RunMainMenu(input); }
        else if (currentScreen == Screen.Password) { RunPasswordMenu(input); }
        //else if (currentScreen == Screen.Win) { }
    }

    void RunPasswordMenu(string input)
    {
        userPassword = input;
        GameResult();
    }

    void RunMainMenu(string input)
    {
        switch (input)
        {
            case "1":
            case "2":
            case "3":
                level = Int32.Parse(input);
                AskForPassword();
                break;
            default:
                Terminal.WriteLine("Invlaid choice, try again!");
                //ShowMainMenu();
                break;
        }
    }

    private void AskForPassword()
    {
        Terminal.WriteLine("You choose level " + level);
        currentScreen = Screen.Password;
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + correctPassword.Anagram());

    }

    private void SetRandomPassword()
    {
        //set randomly a game password based upon level difficulty
        //r.Next(min,max) -> min inclusive, max is exclusive
        System.Random r = new System.Random();
        switch (level)
        {
            case (1):
                correctPassword = level1Passwords[r.Next(0, level1Passwords.Length)];
                print("selected pass: " + correctPassword);
                break;
            case (2):
                correctPassword = level2Passwords[r.Next(0, level2Passwords.Length)];
                print("selected pass: " + correctPassword);
                break;
            case (3):
                correctPassword = level3Passwords[r.Next(0, level3Passwords.Length)];
                print("selected pass: " + correctPassword);
                break;
            default:
                Debug.LogError("Invalid level number!");
                break;
        }
    }

    void GameResult() 
    {
        if (correctPassword == userPassword)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
        //currentScreen = Screen.Win;
    }
    
    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    _______
   /      //
  /      //
 /_____ //
(______(/           
"
                );
                break;
            case 2:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine(@"
 __
/0 \_______
\__/-=' = '         
"
                );
                break;
            case 3:
                Terminal.WriteLine("Welcome to NASA's internal system!");
                Terminal.WriteLine(@"
 _ __   __ _ ___  __ _
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___)\__,_|
"
                ); 
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
    }


    // Start is called before the first frame update
    // Get executed when we press start from unity engine 
    void Start()
    {
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
