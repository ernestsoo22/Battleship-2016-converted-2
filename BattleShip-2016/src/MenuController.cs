using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// The menu controller handles the drawing and user interactions
/// from the menus in the game. These include the main menu, game
/// menu and the settings m,enu.
/// </summary>

static class MenuController
{

	/// <summary>
	/// The menu structure for the game.
	/// </summary>
	/// <remarks>
	/// These are the text captions for the menu items.
	/// </remarks>
	private static readonly string [] [] _menuStructure = {
		new string[] {
			"PLAY",
			"SETUP",
			"SCORES",
			"QUIT",
			// Added Function: Change Music Function 
			// Author: Ernest Soo 
			"MUSIC",
			// Added Function: Change Background(Credits) Function // Author: Jacky Ten
			"CREDITS",
			// Added Function: Help Function
			// Author: Ernest Soo 
			"HELP"

		},
		new string[] {
			"RETURN",
			"SURRENDER",
			"QUIT"
		},
		new string[] {
			"EASY",
			"MEDIUM",
			"HARD"
		},
		// Added Function: Change Music Function 
		// Author: Ernest Soo 
		new string[] {
			"HORROR",
			"BATTLE",
			"DRUMS",
			"MUTE"
		},
		// Added Function: Credits Option Function 
		// Author: Jacky Ten 
		new string[] {
			"Credit1",
			"Credit2",
		},
		// Added Function: Help Function
		// Author: Ernest Soo 
		new string[] {
			"PREPARE",
			"BATTLE"
		}

	};
	private const int MENU_TOP = 575;
	private const int MENU_LEFT = 30;
	private const int MENU_GAP = 0;
	private const int BUTTON_WIDTH = 75;
	private const int BUTTON_HEIGHT = 15;
	private const int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;

	private const int TEXT_OFFSET = 0;
	private const int MAIN_MENU = 0;
	private const int GAME_MENU = 1;

	private const int SETUP_MENU = 2;
	// Added Function: Change Music Function 
	// Author: Ernest Soo 
	private const int MUSIC_MENU = 3;
	// Added Function: Change Background(Credit) Function 
	// Author: Jacky Ten  
	private const int B_MENU = 4;
	// Added Function: Help Function
	// Author: Ernest Soo 
	private const int HELP_MENU = 5;
	private const int MAIN_MENU_PLAY_BUTTON = 0;
	private const int MAIN_MENU_SETUP_BUTTON = 1;
	private const int MAIN_MENU_TOP_SCORES_BUTTON = 2;

	private const int MAIN_MENU_QUIT_BUTTON = 3;
	// Added Function: Change Music Function 
	// Author: Ernest Soo 
	private const int MAIN_MENU_MUSIC_BUTTON = 4;
	// Added Function: Credit Function 
	// Author: Jacky Ten 
	private const int MAIN_MENU_B_BUTTON = 5;
	// Added Function: Help Function
	// Author: Ernest Soo
	private const int MAIN_MENU_HELP_BUTTON = 6;

	private const int SETUP_MENU_EASY_BUTTON = 0;
	private const int SETUP_MENU_MEDIUM_BUTTON = 1;
	private const int SETUP_MENU_HARD_BUTTON = 2;

	private const int SETUP_MENU_EXIT_BUTTON = 3;

	// Added Function: Change Music Function 
	// Author: Ernest Soo 
	private const int MUSIC_MENU_MUSIC1_BUTTON = 0;
	private const int MUSIC_MENU_MUSIC2_BUTTON = 1;
	private const int MUSIC_MENU_MUSIC3_BUTTON = 2;
	private const int MUSIC_MENU_MUSIC4_BUTTON = 3;
	private const int MUSIC_MENU_EXIT_BUTTON = 4;

	// Added Function: Credit Function 
	// Author: Jacky Ten
	private const int B_MENU_B1 = 0;
	private const int B_MENU_B2 = 1;

	// Added Function: Help Function
	// Author: Ernest Soo 
	//	private const int HELP_MENU_PREPARE_BUTTON = 0;
	//	private const int HELP_MENU_BATTLE_BUTTON = 1;
	//	private const int HELP_MENU_EXIT_BUTTON = 2;

	private const int GAME_MENU_RETURN_BUTTON = 0;
	private const int GAME_MENU_SURRENDER_BUTTON = 1;

	private const int GAME_MENU_QUIT_BUTTON = 2;
	private static readonly Color MENU_COLOR = SwinGame.RGBAColor (2, 167, 252, 255);

	private static readonly Color HIGHLIGHT_COLOR = SwinGame.RGBAColor (1, 57, 86, 255);
	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>


	public static int CREDIT1 {
		get {
			return B_MENU_B1;
		}
	}
	public static int CREDIT2 {
		get {
			return B_MENU_B2;
		}
	}

	public static void HandleMainMenuInput ()
	{
		HandleMenuInput (MAIN_MENU, 0, 0);
	}

	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleSetupMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (SETUP_MENU, 1, 1);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}
	}

	// Added Function: Change Music Function 
	// Author: Ernest Soo
	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleMusicMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (MUSIC_MENU, 1, 3);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}

	}

	// Added Function: Credit Function 
	// Author: Jacky Ten
	public static void HandleBackgroundMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (B_MENU, 1, 4);

		if (!handled) {
			HandleMenuInput (B_MENU, 0, 0);
		}
	}

	// Added Function: Help Function 
	// Author: Ernest Soo
	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleHelpMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (HELP_MENU, 1, 6);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}

	}


	/// <summary>
	/// Handle input in the game menu.
	/// </summary>
	/// <remarks>
	/// Player can return to the game, surrender, or quit entirely
	/// </remarks>
	public static void HandleGameMenuInput ()
	{
		HandleMenuInput (GAME_MENU, 0, 0);
	}

	/// <summary>
	/// Handles input for the specified menu.
	/// </summary>
	/// <param name="menu">the identifier of the menu being processed</param>
	/// <param name="level">the vertical level of the menu</param>
	/// <param name="xOffset">the xoffset of the menu</param>
	/// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
	private static bool HandleMenuInput (int menu, int level, int xOffset)
	{
		if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE)) {
			GameController.EndCurrentState ();
			return true;
		}

		if (SwinGame.MouseClicked (MouseButton.LeftButton)) {
			int i = 0;
			for (i = 0; i <= _menuStructure [menu].Length - 1; i++) {
				//IsMouseOver the i'th button of the menu
				if (IsMouseOverMenu (i, level, xOffset)) {
					PerformMenuAction (menu, i);
					return true;
				}
			}

			if (level > 0) {
				//none clicked - so end this sub menu
				GameController.EndCurrentState ();
			}
		}

		return false;
	}

	/// <summary>
	/// Draws the main menu to the screen.
	/// </summary>
	public static void DrawMainMenu ()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Main Menu", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons (MAIN_MENU);
	}

	/// <summary>
	/// Draws the Game menu to the screen
	/// </summary>
	public static void DrawGameMenu ()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Paused", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons (GAME_MENU);
	}

	/// <summary>
	/// Draws the settings menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawSettings ()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Settings", Color.White, GameFont("ArialLarge"), 50, 50)

		DrawButtons (MAIN_MENU);
		DrawButtons (SETUP_MENU, 1, 1);
	}

	// Added Function: Change Music Function 
	// Author: Ernest Soo 
	/// <summary>
	/// Draws the music menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawMusic ()
	{
		DrawButtons (MAIN_MENU);
		DrawButtons (MUSIC_MENU, 1, 3);
	}

	// Added Function: Change Background Function // Author: Jacky Ten
	public static void DrawBackground ()
	{
		DrawButtons (MAIN_MENU);
		DrawButtons (B_MENU, 1, 4);
	}

	// Added Function: Help Function 
	// Author: Ernest Soo 
	/// <summary>
	/// Draws the help menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	/*public static void DrawHelp ()
	{
		DrawButtons (MAIN_MENU);
		DrawButtons (HELP_MENU, 1, 6);
	}*/

	/// <summary>
	/// Draw the buttons associated with a top level menu.
	/// </summary>
	/// <param name="menu">the index of the menu to draw</param>
	private static void DrawButtons (int menu)
	{
		DrawButtons (menu, 0, 0);
	}

	/// <summary>
	/// Draws the menu at the indicated level.
	/// </summary>
	/// <param name="menu">the menu to draw</param>
	/// <param name="level">the level (height) of the menu</param>
	/// <param name="xOffset">the offset of the menu</param>
	/// <remarks>
	/// The menu text comes from the _menuStructure field. The level indicates the height
	/// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
	/// to allow the submenus to be positioned correctly.
	/// </remarks>
	private static void DrawButtons (int menu, int level, int xOffset)
	{
		int btnTop = 0;

		btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int i = 0;
		for (i = 0; i <= _menuStructure [menu].Length - 1; i++) {
			int btnLeft = 0;
			btnLeft = MENU_LEFT + BUTTON_SEP * (i + xOffset);
			//SwinGame.FillRectangle(Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT)
			SwinGame.DrawTextLines (_menuStructure [menu] [i], MENU_COLOR, Color.Black, GameResources.GameFont ("Menu"), FontAlignment.AlignCenter, btnLeft + TEXT_OFFSET, btnTop + TEXT_OFFSET, BUTTON_WIDTH, BUTTON_HEIGHT);

			if (SwinGame.MouseDown (MouseButton.LeftButton) & IsMouseOverMenu (i, level, xOffset)) {
				SwinGame.DrawRectangle (HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}
	}

	/// <summary>
	/// Determined if the mouse is over one of the button in the main menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <returns>true if the mouse is over that button</returns>
	private static bool IsMouseOverButton (int button)
	{
		return IsMouseOverMenu (button, 0, 0);
	}

	/// <summary>
	/// Checks if the mouse is over one of the buttons in a menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <param name="level">the level of the menu</param>
	/// <param name="xOffset">the xOffset of the menu</param>
	/// <returns>true if the mouse is over the button</returns>
	private static bool IsMouseOverMenu (int button, int level, int xOffset)
	{
		int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
		int btnLeft = MENU_LEFT + BUTTON_SEP * (button + xOffset);

		return UtilityFunctions.IsMouseInRectangle (btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
	}

	/// <summary>
	/// A button has been clicked, perform the associated action.
	/// </summary>
	/// <param name="menu">the menu that has been clicked</param>
	/// <param name="button">the index of the button that was clicked</param>
	private static void PerformMenuAction (int menu, int button)
	{
		switch (menu) {
		case MAIN_MENU:
			PerformMainMenuAction (button);
			break;
		case SETUP_MENU:
			PerformSetupMenuAction (button);
			break;
		case GAME_MENU:
			PerformGameMenuAction (button);
			break;
		// Added Function: Change Music Function 
		// Author: Ernest Soo 
		case MUSIC_MENU:
			PerformChangeMusicAction (button);
			break;
		// Added Function: Change Background Function
		// Author: Jacky Ten
		case B_MENU:
			PerformChangeBackgroundAction (button);
			break;

		}
	}

	/// <summary>
	/// The main menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformMainMenuAction (int button)
	{
		switch (button) {
		case MAIN_MENU_PLAY_BUTTON:
			GameController.StartGame ();
			break;
		case MAIN_MENU_SETUP_BUTTON:
			GameController.AddNewState (GameState.AlteringSettings);
			break;
		case MAIN_MENU_TOP_SCORES_BUTTON:
			GameController.AddNewState (GameState.ViewingHighScores);
			break;
		// Added Function: Change Music Function 
		// Author: Ernest Soo 
		case MAIN_MENU_MUSIC_BUTTON:
			GameController.AddNewState (GameState.AlteringMusic);
			break;
		// Added Function: Help Function
		// Author: Ernest Soo 
		case MAIN_MENU_HELP_BUTTON:
			GameController.AddNewState (GameState.ViewingHelp);
			break;
		// Added Function: Change Background Function 
		// Author: Jacky Ten
		case MAIN_MENU_B_BUTTON:
			GameController.AddNewState (GameState.AlteringBackground);
			break;
		case MAIN_MENU_QUIT_BUTTON:
			GameController.EndCurrentState ();
			break;




		}

	}

	/// <summary>
	/// The setup menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSetupMenuAction (int button)
	{
		switch (button) {
		case SETUP_MENU_EASY_BUTTON:
			GameController.SetDifficulty (AIOption.Hard);
			break;
		case SETUP_MENU_MEDIUM_BUTTON:
			GameController.SetDifficulty (AIOption.Hard);
			break;
		case SETUP_MENU_HARD_BUTTON:
			GameController.SetDifficulty (AIOption.Hard);
			break;
		}
		//Always end state - handles exit button as well
		GameController.EndCurrentState ();
	}

	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformGameMenuAction (int button)
	{
		switch (button) {
		case GAME_MENU_RETURN_BUTTON:
			GameController.EndCurrentState ();
			break;
		case GAME_MENU_SURRENDER_BUTTON:
			GameController.EndCurrentState ();
			//StopWatch.stopWatch.Stop ();
			//StopWatch.stopWatch.Reset ();
			//end game menu
			GameController.EndCurrentState ();
			//end game
			break;
		case GAME_MENU_QUIT_BUTTON:
			GameController.AddNewState (GameState.Quitting);
			break;
		}

	}

	// Added Function: Change Music Function 
	// Author: Ernest Soo 
	/// <summary>
	/// The music menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformChangeMusicAction (int button)
	{
		switch (button) {
		case MUSIC_MENU_MUSIC1_BUTTON:
			SwinGame.PlayMusic ("Horrordrone.mp3");
			break;
		case MUSIC_MENU_MUSIC2_BUTTON:
			SwinGame.PlayMusic ("Battle.mp3");
			break;
		case MUSIC_MENU_MUSIC3_BUTTON:
			SwinGame.PlayMusic ("Drums.mp3");
			break;
		case MUSIC_MENU_MUSIC4_BUTTON:
			SwinGame.StopMusic ();
			break;

		}

		//Always end state - handles exit button as well
		GameController.EndCurrentState ();
	}

	// Added Function: Change Background Function 
	// Author: Jacky Ten 
	/// <summary>
	/// The background menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	public static void PerformChangeBackgroundAction (int button)
	{
		switch (button) {
		case B_MENU_B1:
			SwinGame.DrawBitmap (GameResources.GameImage ("Credit1"), 0, 0);
			GameResources.ChangeBackground0 ();

			SwinGame.DrawText ("Hello", Color.White, GameResources.GameFont ("Courier"), 20, 20);
			break;
		case B_MENU_B2:
			SwinGame.DrawBitmap (GameResources.GameImage ("Credit2"), 0, 0);
			GameResources.ChangeBackground1 ();
			break;
		}

	}



}