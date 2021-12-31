/**************************
 * Copyleft (L) 2021 CENG - All Rights Not Reserved
 * You may use, distribute and modify this code.
 **************************/

/**
 * @file ce103-hw5-snake-dll
 * @author Bedirhan OZCELIK
 * @date 31 December 2021
 *
 * @brief <b> HW-5 Functions </b>
 *
 * HW-5 Sample Lib Functions
 *
 * @see http://bilgisayar.mmf.erdogan.edu.tr/en/
 *
 */
using ce103_hw5_snake_app.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ce103_hw5_snake_dll
{
    public class Class1
    {
        public const int SNAKE_ARRAY_SIZE = 310;
        public const int UP_ARROW = 38;
        public const int LEFT_ARROW = 37;
        public const int RIGHT_ARROW = 39;
        public const int DOWN_ARROW = 40;
        public const int ENTER_KEY = 13;
        public const int EXIT_BUTTON = 27;
        public const int PAUSE_BUTTON = 112;
        const char SNAKE_HEAD = (char)177;
        const char SNAKE_BODY = (char)178;
        const char WALL = (char)219;
        const char FOOD = (char)254;
        const char BLANK = ' ';

        /**
	    *
	    *	  @name clsrcr
	    *
	    *	  Calculates the fibonacci number in the given index and return as output
	    *	  
	    *     Function to clear the console.
	    **/
        public void clrscr()
        {
            //clear the function
            Console.Clear();
            return;
        }

        /**
	    *
	    *	  @name   waitForAnyKey
	    *     Waiting for a random keypress in the function. It acts as a key.
	    *
	    *	  @param  [in] pressed [\b ConsoleKey]  
	    *
	    *	  @retval [\b ConsoleKey] 
	    **/
        public ConsoleKey waitForAnyKey()
        {
            ConsoleKey pressed;

            while (!Console.KeyAvailable) ;
            pressed = Console.ReadKey(false).Key;
            return pressed;
        }

        /**
	    *
	    *	  @name  getGameSpeed
	    *     It is the function that adjusts the game speed in each of the numbers from 1 to 9 when you press them.
	    *     
	    *	  @param  [in] speed [\b int]  
	    *
	    *	  @retval [\b int] 
	    **/
        public int getGameSpeed()
        {
            //speed is equal to 50
            int speed = 50;
            //clear the function
            clrscr();
            Console.SetCursorPosition(10, 5);
            Console.Write("Select The game speed between 1 and 9, and press enter.\n");
            //Press a number between 1 and 9 to start the game.
            //Because Convert.ToInt ie integer is used.
            int pressed = Convert.ToInt32(Console.ReadLine());
            switch (pressed)
            {
                case 1:
                    //When you press the number 1, the speed is 100ms.
                    speed = 100;
                    break;
                case 2:
                    //When you press the number 2, the speed is 90ms.
                    speed = 90;
                    break;
                case 3:
                    //When you press the number 3, the speed is 80ms.
                    speed = 80;
                    break;
                case 4:
                    //When you press the number 4, the speed is 70ms.
                    speed = 70;
                    break;
                case 5:
                    //When you press the number 5, the speed is 60ms.
                    speed = 60;
                    break;
                case 6:
                    //When you press the number 6, the speed is 50ms.
                    speed = 50;
                    break;
                case 7:
                    //When you press the number 7, the speed is 40ms.
                    speed = 40;
                    break;
                case 8:
                    //When you press the number 8, the speed is 30ms.
                    speed = 30;
                    break;
                case 9:
                    //When you press the number 9, the speed is 20ms.
                    speed = 20;
                    break;
            }
            return speed;
        }

        /**
	    *
	    *	  @name pauseMenu
	    *
	    *	  This function is the function that allows us to pause the game while playing the game.
	    **/
        public void pauseMenu()
        {


            Console.SetCursorPosition(28, 23);
            Console.WriteLine("**Paused**");

            waitForAnyKey();
            Console.SetCursorPosition(28, 23);
            Console.WriteLine("            ");

            return;
        }

        /**
	    *
		@name  checkKeysPressed
				
		@param [out] direction	[\b ConsoleKey] 

		This function controls the functions such as up, down, left and right, stop, exit, which we use while playing games, with ReadKey.

		@retval [\b ConsoleKey] 
	    **/
        public ConsoleKey checkKeysPressed(ConsoleKey direction)
        {

            ConsoleKey pressed;
            //ConsoleKey Downkey = ConsoleKey.DownArrow;
            if (Console.KeyAvailable) //If a key has been pressed
            {
                pressed = Console.ReadKey(false).Key;
                if (direction != pressed)
                {
                    if (pressed == ConsoleKey.DownArrow && direction != ConsoleKey.UpArrow)
                        direction = pressed;
                    else if (pressed == ConsoleKey.UpArrow && direction != ConsoleKey.DownArrow)
                        direction = pressed;
                    else if (pressed == ConsoleKey.LeftArrow && direction != ConsoleKey.RightArrow)
                        direction = pressed;
                    else if (pressed == ConsoleKey.RightArrow && direction != ConsoleKey.LeftArrow)
                        direction = pressed;
                    else if (pressed == ConsoleKey.P)
                        pauseMenu();
                }
            }
            return (direction);
        }

        /**
        * @name  collisionSnake
        * 
        * @param [in]  x [\b int]
        * 
        * @param [in]  y [\b int]
        * 
        * @param [in] snakeLength [\b int]
        * 
        * @param [in] snakeXY [\b int[,]]
        * 
        * @param [in] detect [\b int]
        * 
        * This is the function that shows the collisions made by the snake.
        **/
        public bool collisionSnake(int x, int y, int[,] snakeXY, int snakeLength, int detect)
        {
            int i;
            for (i = detect; i < snakeLength; i++) //Checks if the snake collided with itself
            {
                if (x == snakeXY[0, i] && y == snakeXY[1, i])
                    return true;
            }
            return false;
        }

        /**
        * @name  generateFood
        * 
        * @param [in] foodXY [\b int[]]
        * 
        * @param [in] width [\b int]
        * 
        * @param [in] snakeLength [\b int]
        * 
        * @param [in] snakeXY [\b int[,]]
        * 
        * @param [in] height [\b int]
        * 
        * This function allows the snake to produce food.
        **/
        public int generateFood(int[] foodXY, int width, int height, int[,] snakeXY, int snakeLength)
        {
            do
            {
                Random rnd = new Random();
                foodXY[0] = rnd.Next() % (width - 2) + 2;
                foodXY[1] = rnd.Next() % (height - 6) + 2;
            } while (collisionSnake(foodXY[0], foodXY[1], snakeXY, snakeLength, 0)); //This should prevent the "Food" from being created on top of the snake. - However the food has a chance to be created ontop of the snake, in which case the snake should eat it...

            Console.SetCursorPosition(foodXY[0], foodXY[1]);
            Console.WriteLine(FOOD);

            return (0);
        }

        /**
        * @name  moveSnakeArray
        * 
        * @param [in] direction [\b ConsoleKey]
        * 
        * @param [in] snakeLength [\b int]
        * 
        * @param [in] snakeXY [\b int[,]]
        *         
        *This function is the one that controls the movements of the snake.
        **/
        public void moveSnakeArray(int[,] snakeXY, int snakeLength, ConsoleKey direction)
        {
            int i;
            for (i = snakeLength - 1; i >= 1; i--)
            {
                snakeXY[0, i] = snakeXY[0, i - 1];
                snakeXY[1, i] = snakeXY[1, i - 1];
            }

            /*
            because we dont actually know the new snakes head x y, 
            we have to check the direction and add or take from it depending on the direction.
            */
            switch (direction)
            {
                case ConsoleKey.DownArrow:
                    snakeXY[1, 0]++;
                    break;
                case ConsoleKey.RightArrow:
                    snakeXY[0, 0]++;
                    break;
                case ConsoleKey.UpArrow:
                    snakeXY[1, 0]--;
                    break;
                case ConsoleKey.LeftArrow:
                    snakeXY[0, 0]--;
                    break;
            }

            return;
        }

        /**
        * @name  move
        * 
        * @param [in] direction [\b ConsoleKey]
        * 
        * @param [in] snakeLength [\b int]
        * 
        * @param [in] snakeXY [\b int[,]]
        *         
        *This function is related to the size of the snake's body and head.
        **/
        public void move(int[,] snakeXY, int snakeLength, ConsoleKey direction)
        {
            int x;
            int y;

            //Remove the tail ( HAS TO BE DONE BEFORE THE ARRAY IS MOVED!!!!! )
            x = snakeXY[0, snakeLength - 1];
            y = snakeXY[1, snakeLength - 1];

            Console.SetCursorPosition(x, y);
            Console.WriteLine(BLANK);

            //Changes the head of the snake to a body part
            Console.SetCursorPosition(snakeXY[0, 0], snakeXY[1, 0]);
            Console.WriteLine(SNAKE_BODY);

            moveSnakeArray(snakeXY, snakeLength, direction);

            Console.SetCursorPosition(snakeXY[0, 0], snakeXY[1, 0]);
            Console.WriteLine(SNAKE_HEAD);

            Console.SetCursorPosition(1, 1); //Gets rid of the darn flashing underscore.

            return;
        }

        /**
        * @name eatFood
        * 
        * @param [in] foodXY [\b int[]]
        * 
        * @param [in] snakeXY [\b int[,]]
        *         
        *This is the function of the snake eating. It controls the food that the snake eats.
        **/
        public bool eatFood(int[,] snakeXY, int[] foodXY)
        {
            //This function checks if the snakes head his on top of the food, if it is then it'll generate some more food...
            if (snakeXY[0, 0] == foodXY[0] && snakeXY[1, 0] == foodXY[1])
            {
                foodXY[0] = 0;
                foodXY[1] = 0; //This should prevent a nasty bug (loops) need to check if the bug still exists...

                return true;
            }

            return false;
        }

        /**
        * @name  collisionDetection
        * 
        * @param [in] consoleWidth [\b int]
        * 
        * @param [in] consoleHeight [\b int]
        * 
        * @param [in] snakeLength [\b int]
        * 
        * @param [in] snakeXY [\b int[,]]
        *         
        *This function is the function to detect the collisions made by the snake.
        **/
        public int collisionDetection(int[,] snakeXY, int consoleWidth, int consoleHeight, int snakeLength) //Need to Clean this up a bit
        {
            int colision = 0;
            if ((snakeXY[0, 0] == 1) || (snakeXY[1, 0] == 1) || (snakeXY[0, 0] == consoleWidth) || (snakeXY[1, 0] == consoleHeight - 4)) //Checks if the snake collided wit the wall or it's self
                colision = 1;
            else
                if (collisionSnake(snakeXY[0, 0], snakeXY[1, 0], snakeXY, snakeLength, 1)) //If the snake collided with the wall, theres no point in checking if it collided with itself.
                colision = 1;

            return (colision);
        }

        /**
        * @name refreshInfoBar
        * 
        * @param [in] score [\b int]
        * 
        * @param [in] speed [\b int]
        * 
        *This is the function that shows the game version, author, game speed and total score.
        **/
        public void refreshInfoBar(int score, int speed)
        {
            Console.SetCursorPosition(5, 23);
            Console.WriteLine($"Score: {score}");

            Console.SetCursorPosition(5, 24);
            Console.WriteLine($"Speed: {speed}");

            Console.SetCursorPosition(52, 23);
            Console.WriteLine("Coder: Bedirhan OZCELIK");

            Console.SetCursorPosition(52, 24);
            Console.WriteLine("Version: 0.5");

            return;
        }

        /**
	    *
	    * @name createHighScores
	    * This function is the function that creates the total score at the end of the game into an empty .txt file.
	    **/
        public void createHighScores()
        {
            //Let's define the name of our txt file in string.
            string path = "highscores.txt";
            //creates the file
            List<Scores> list = new List<Scores>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Scores
                {
                    Name = "EMPTY",
                    Score = 0,
                });
            }
            using (StreamWriter writer = File.CreateText(path))
            {
                writer.Write(JsonConvert.SerializeObject(list));
            }
            //if the file we defined in our path is out of the path
            if (!File.Exists(path))
            {
                Console.WriteLine("FAILED TO CREATE HIGHSCORES!!! EXITING!");
                Environment.Exit(0);
            }


        }

        /**
        *
        * @name getLowestScore
        * This function creates the lowest score you get in .txt format and reads the lowest score to the screen.
        **/
        public int getLowestScore()
        {
            List<Scores> list = new List<Scores>();
            string path = "highscores.txt";

            if (!File.Exists(path))
            {
                //Create the file, then try open it again.. if it fails this time exit.
                createHighScores(); //This should create a highscores file (If there isn't one)
                if (!File.Exists(path))
                    Environment.Exit(1);
            }
            //JsonDeserialize is reading the resulting txt file.
            list = JsonConvert.DeserializeObject<List<Scores>>(File.ReadAllText(path));
            if (list != null)
            {

                return (list.Min(x => x.Score));
            }
            else
            {
                return 0;
            }

        }

        /**
        *
        * @name inputScore
        * @param [in] score [\b int]
        * This function reads the score formed in our txt file. It writes the total score at the end of the game into the high score file and lists these scores.
        **/
        void inputScore(int score) //This seriously needs to be cleaned up
        {

            List<Scores> list = new List<Scores>();
            string name;
            string path = "highscores.txt";

            clrscr(); //clear the console

            if (!File.Exists(path))
            {
                //Create the file, then try open it again.. if it fails this time exit.
                createHighScores(); //This should create a highscores file (If there isn't one)
                if (!File.Exists(path))
                    Environment.Exit(1);
            }
            Console.SetCursorPosition(10, 5);
            Console.WriteLine("Your Score made it into the top 5!!!");
            Console.SetCursorPosition(10, 6);
            Console.WriteLine("Please enter your name: ");
            name = Console.ReadLine();



            //JsonDeserialize is reading the resulting txt file.
            var y = JsonConvert.DeserializeObject<List<Scores>>(File.ReadAllText(path));
            if (y != null)
            {
                list = y;
            }
            list.Add(new Scores
            {
                Name = name,
                Score = score,
            });
            //If the 6th data we add to our score list is higher than the other data,
            if (list.Count == 6)
            {
                //the smallest of the 5 previously entered values is deleted
                list.Remove(list.Where(x => x.Score == list.Min(t => t.Score)).FirstOrDefault());

            }
            //Text is written into our resulting text file.
            using (StreamWriter writer = File.CreateText(path))
            {
                writer.Write(JsonConvert.SerializeObject(list));

            }
            return;
        }

        /**
        *
        * @name displayHighScores
        * This function creates.txt file. It opens the file and reads the highest scores to the screen.
        **/
        public void displayHighScores() //NEED TO CHECK THIS CODE!!!
        {
            int rank = 1;
            int y = 5;
            List<Scores> list = new List<Scores>();
            string path = "highscores.txt";
            clrscr(); //clear the console
            if (!File.Exists(path))
            {
                //Create the file, then try open it again.. if it fails this time exit.
                createHighScores(); //This should create a highscores file (If there isn't one)
                if (!File.Exists(path))
                    Environment.Exit(1);
            }

            Console.SetCursorPosition(10, y++);
            Console.WriteLine("High Scores");
            Console.SetCursorPosition(10, y++);
            Console.WriteLine("Rank\tScore\t\t\tName");
            list = JsonConvert.DeserializeObject<List<Scores>>(File.ReadAllText(path));

            if (list != null)
            {
                list = list.OrderByDescending(x => x.Score).ToList();

                foreach (var item in list)
                {
                    Console.SetCursorPosition(10, y++);
                    Console.WriteLine(rank + "\t" + item.Score + "\t\t\t" + item.Name);
                    rank++;
                }

            }



            Console.SetCursorPosition(10, y++);

            Console.WriteLine("Press any key to continue...");
            waitForAnyKey();
            return;
        }

        /**
        * @name youWinScreen
        * This is the function that displays "YOU WIN" when you win the game.
        **/
        public void youWinScreen()
        {
            int x = 6, y = 7;
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("'##:::'##::'#######::'##::::'##::::'##:::::'##:'####:'##::: ##:'####:");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(". ##:'##::'##.... ##: ##:::: ##:::: ##:'##: ##:. ##:: ###:: ##: ####:");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(":. ####::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ####: ##: ####:");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("::. ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ## ## ##:: ##::");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("::: ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ##. ####::..:::");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("::: ##:::: ##:::: ##: ##:::: ##:::: ##: ##: ##:: ##:: ##:. ###:'####:");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("::: ##::::. #######::. #######:::::. ###. ###::'####: ##::. ##: ####:");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(":::..::::::.......::::.......:::::::...::...:::....::..::::..::....::");
            Console.SetCursorPosition(x, y++);

            waitForAnyKey();
            clrscr(); //clear the console
            return;
        }

        /**
        * @name gameOverScreen
        * This is the function that displays "GAME OVER" when you lose the game.
        **/
        public void gameOverScreen()
        {
            int x = 17, y = 3;

            Console.SetCursorPosition(x, y++);
            Console.WriteLine(":'######::::::'###::::'##::::'##:'########:\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("'##... ##::::'## ##::: ###::'###: ##.....::\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(" ##:::..::::'##:. ##:: ####'####: ##:::::::\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(" ##::'####:'##:::. ##: ## ### ##: ######:::\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(" ##::: ##:: #########: ##. #: ##: ##...::::\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(" ##::: ##:: ##.... ##: ##:.:: ##: ##:::::::\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(". ######::: ##:::: ##: ##:::: ##: ########:\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(":......::::..:::::..::..:::::..::........::\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(":'#######::'##::::'##:'########:'########::'####:\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("'##.... ##: ##:::: ##: ##.....:: ##.... ##: ####:\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(" ##:::: ##: ##:::: ##: ##::::::: ##:::: ##: ####:\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(" ##:::: ##: ##:::: ##: ######::: ########::: ##::\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(" ##:::: ##:. ##:: ##:: ##...:::: ##.. ##::::..:::\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(" ##:::: ##::. ## ##::: ##::::::: ##::. ##::'####:\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(". #######::::. ###:::: ########: ##:::. ##: ####:\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine(":.......::::::...:::::........::..:::::..::....::\n");

            waitForAnyKey();
            clrscr(); //clear the console
            return;
        }

        /**
        * @name startGame
        * 
        * @param [in] consoleWidth [\b int]
        * 
        * @param [in] consoleHeight [\b int]
        * 
        * @param [in] snakeLength [\b int]
        * 
        * @param [in] snakeXY [\b int[,]]
        *    
        * @param [in] score [\b int]
        * 
        * @param [in] speed [\b int]
        * 
        * @param [in] foodXY [\b int[]]
        * 
        * @param [in] direction [\b ConsoleKey]
        * 
        *This function is the function to start the game.
        **/
        public void startGame(int[,] snakeXY, int[] foodXY, int consoleWidth, int consoleHeight, int snakeLength, ConsoleKey direction, int score, int speed)
        {
            int gameOver = 0;

            ConsoleKey oldDirection = 0;
            bool canChangeDirection = true;


            //endWait = clock() + waitMili;

            do
            {
                if (canChangeDirection)
                {
                    oldDirection = direction;
                    direction = checkKeysPressed(direction);
                }

                if (oldDirection != direction)//Temp fix to prevent the snake from colliding with itself
                    canChangeDirection = false;

                if (true) //haha, it moves according to how fast the computer running it is...
                {
                    //gotoxy(1,1);
                    //printf("%d - %d",clock() , endWait);
                    move(snakeXY, snakeLength, direction);
                    canChangeDirection = true;

                    if (eatFood(snakeXY, foodXY))
                    {
                        generateFood(foodXY, consoleWidth, consoleHeight, snakeXY, snakeLength); //Generate More Food
                        snakeLength++;
                        switch (speed)
                        {
                            case 20: score += 100; break;
                            case 30: score += 90; break;
                            case 40: score += 80; break;
                            case 50: score += 70; break;
                            case 60: score += 60; break;
                            case 70: score += 50; break;
                            case 80: score += 40; break;
                            case 90: score += 30; break;
                            case 100: score += 20; break;

                        }


                        refreshInfoBar(score, speed);
                    }


                    Thread.Sleep(speed);
                }

                gameOver = collisionDetection(snakeXY, consoleWidth, consoleHeight, snakeLength);

                if (snakeLength >= SNAKE_ARRAY_SIZE - 5) //Just to make sure it doesn't get longer then the array size & crash
                {
                    gameOver = 2;//You Win! <- doesn't seem to work - NEED TO FIX/TEST THIS
                    score += 1500; //When you win you get an extra 1500 points!!!
                }

            } while (gameOver != 1);

            switch (gameOver)
            {
                case 1:
                    gameOverScreen();
                    break;
                case 2:
                    youWinScreen();
                    break;
            }
            if (score >= getLowestScore() && score != 0)
            {
                inputScore(score);
                displayHighScores();
            }
            return;
        }

        /**
        * @name loadEnvironment
        * 
        * @param [in] consoleWidth [\b int]
        * 
        * @param [in] consoleHeight [\b int]
        * 
        *This is the function that controls what happens around the game. For example, walls.
        **/
        public void loadEnviroment(int consoleWidth, int consoleHeight)//This can be done in a better way... FIX ME!!!! Also i think it doesn't work properly in ubuntu <- Fixed
        {

            int x = 1, y = 1;
            int rectangleHeight = consoleHeight - 4;
            clrscr(); //clear the console

            Console.SetCursorPosition(x, y); //Top left corner

            for (; y < rectangleHeight; y++)
            {
                Console.SetCursorPosition(x, y); //Left Wall 
                Console.WriteLine(WALL);

                Console.SetCursorPosition(consoleWidth, y); //Right Wall
                Console.WriteLine(WALL);
            }

            y = 1;
            for (; x < consoleWidth + 1; x++)
            {
                Console.SetCursorPosition(x, y); //Left Wall 
                Console.WriteLine(WALL);

                Console.SetCursorPosition(x, rectangleHeight); //Right Wall
                Console.WriteLine(WALL);
            }
            return;
        }

        /**
        * @name loadSnake
        * 
        * @param [in] snakeLength [\b int]
        * 
        * @param [in] snakeXY [\b int[,]]
        * 
        *This function is the function to control the loads on the snake's body.
        **/
        public void loadSnake(int[,] snakeXY, int snakeLength)
        {
            int i;
            /*
			First off, The snake doesn't actually have enough XY coordinates (only 1 - the starting location), thus we use
			these XY coordinates to "create" the other coordinates. For this we can actually use the function used to move the snake.
			This helps create a "whole" snake instead of one "dot", when someone starts a game.
			*/
            //moveSnakeArray(snakeXY, snakeLength); //One thing to note ATM, the snake starts of one coordinate to whatever direction it's pointing...

            //This should print out a snake :P
            for (i = 0; i < snakeLength; i++)
            {
                Console.SetCursorPosition(snakeXY[0, i], snakeXY[1, i]);
                Console.WriteLine(SNAKE_BODY); //Meh, at some point I should make it so the snake starts off with a head...
            }

            return;
        }

        /* NOTE, This function will only work if the snakes starting direction is left!!!! 
		Well it will work, but the results wont be the ones expected.. I need to fix this at some point.. */

        /**
        * @name prepairSnakeArray
        * 
        * @param [in] snakeLength [\b int]
        * 
        * @param [in] snakeXY [\b int[,]]
        * 
        *This function is the function that prepares the snake array, that is, creates it.
        **/
        public void prepairSnakeArray(int[,] snakeXY, int snakeLength)
        {
            int i;
            int snakeX = snakeXY[0, 0];
            int snakeY = snakeXY[1, 0];

            for (i = 1; i <= snakeLength; i++)
            {
                snakeXY[0, i] = snakeX + i;
                snakeXY[1, i] = snakeY;
            }

            return;
        }

        //This function loads the enviroment, snake, etc

        /**
        * @name loadGame
        * This function is the one that loads all the data in the game into it.
        **/
        public void loadGame()
        {
            int[,] snakeXY = new int[2, SNAKE_ARRAY_SIZE]; //Two Dimentional Array, the first array is for the X coordinates and the second array for the Y coordinates

            int snakeLength = 4; //Starting Length

            ConsoleKey direction = ConsoleKey.LeftArrow; //DO NOT CHANGE THIS TO RIGHT ARROW, THE GAME WILL INSTANTLY BE OVER IF YOU DO!!! <- Unless the prepairSnakeArray function is changed to take into account the direction....

            int[] foodXY = { 5, 5 };// Stores the location of the food

            int score = 0;
            //int level = 1;

            //Window Width * Height - at some point find a way to get the actual dimensions of the console... <- Also somethings that display dont take this dimentions into account.. need to fix this...
            int consoleWidth = 80;
            int consoleHeight = 25;

            int speed = getGameSpeed();

            //The starting location of the snake
            snakeXY[0, 0] = 40;
            snakeXY[1, 0] = 10;

            loadEnviroment(consoleWidth, consoleHeight); //borders
            prepairSnakeArray(snakeXY, snakeLength);
            loadSnake(snakeXY, snakeLength);
            generateFood(foodXY, consoleWidth, consoleHeight, snakeXY, snakeLength);
            refreshInfoBar(score, speed); //Bottom info bar. Score, Level etc
            startGame(snakeXY, foodXY, consoleWidth, consoleHeight, snakeLength, direction, score, speed);

            return;
        }

        /**
        * @name menuSelector
        * 
        * @param [in] i [\b int]
        * 
        * @param [in] x [\b int]
        * 
        * @param [in] y [\b int]
        * 
        * @param [in] yStart [\b int]
        * 
        * @retval [\b int
        * 
        * This function is the function that allows us to use keys such as up and down on the buttons in the main menu of the game.
        **/
        public int menuSelector(int x, int y, int yStart)
        {
            ConsoleKey key;
            int i = 0;
            x = x - 2;
            Console.SetCursorPosition(x, yStart);

            Console.WriteLine(">");

            Console.SetCursorPosition(1, 1);

            do
            {
                key = waitForAnyKey();
                //printf("%c %d", key, (int)key);
                if (key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(x, yStart + i);
                    Console.WriteLine(" ");

                    if (yStart >= yStart + i)
                        i = y - yStart - 2;
                    else
                        i--;
                    Console.SetCursorPosition(x, yStart + i);
                    Console.WriteLine(">");
                }
                else
                    if (key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(x, yStart + i);
                    Console.WriteLine(" ");

                    if (i + 2 >= y - yStart)
                        i = 0;
                    else
                        i++;
                    Console.SetCursorPosition(x, yStart + i);
                    Console.WriteLine(">");
                }
                //gotoxy(1,1);
                //printf("%d", key);
            } while (key != ConsoleKey.Enter); //While doesn't equal enter... (13 ASCII code for enter) - note ubuntu is 10
            return (i);
        }

        /**
        *
        * @name welcomeArt
        * 
        * This function is the function that creates the snake picture that appears on the first opening screen of the game.
        **/
        public void welcomeArt()
        {
            clrscr(); //clear the console
                      //Ascii art reference: http://www.chris.com/ascii/index.php?art=animals/reptiles/snakes
            Console.WriteLine("\n");
            Console.WriteLine("\t\t    _________         _________ 			\n");
            Console.WriteLine("\t\t   /         \\       /         \\ 			\n");
            Console.WriteLine("\t\t  /  /~~~~~\\  \\     /  /~~~~~\\  \\ 			\n");
            Console.WriteLine("\t\t  |  |     |  |     |  |     |  | 			\n");
            Console.WriteLine("\t\t  |  |     |  |     |  |     |  | 			\n");
            Console.WriteLine("\t\t  |  |     |  |     |  |     |  |         /	\n");
            Console.WriteLine("\t\t  |  |     |  |     |  |     |  |       //	\n");
            Console.WriteLine("\t\t (o  o)    \\  \\_____/  /     \\  \\_____/ / 	\n");
            Console.WriteLine("\t\t  \\__/      \\         /       \\        / 	\n");
            Console.WriteLine("\t\t    |        ~~~~~~~~~         ~~~~~~~~ 		\n");
            Console.WriteLine("\t\t    ^											\n");
            Console.WriteLine("\t		Welcome To The Snake Game!			\n");
            Console.WriteLine("\t		Press Any Key To Continue...	\n");
            Console.WriteLine("\n");

            waitForAnyKey();
            return;
        }

        /**
        *
        * @name controls
        * 
        * This function is the one that makes the controls in the game work properly.
        **/
        public void controls()
        {
            int x = 10, y = 5;
            clrscr(); //clear the console
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Controls\n");
            Console.SetCursorPosition(x++, y++);
            Console.WriteLine("Use the following arrow keys to direct the snake to the food: ");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Right Arrow");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Left Arrow");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Top Arrow");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Bottom Arrow");
            Console.SetCursorPosition(x, y++);
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("P & Esc pauses the game.");
            Console.SetCursorPosition(x, y++);
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Press any key to continue...");
            waitForAnyKey();
            return;
        }

        /**
        *
        * @name exitYN
        * 
        * This function asks the Y/N question to the screen after pressing exit from the game. Pressing Y exits the game. Pressing N cancels the exit.
        **/
        public void exitYN()
        {
            ConsoleKey pressed;
            Console.SetCursorPosition(9, 8);
            Console.WriteLine("Are you sure you want to exit(Y/N)\n");

            do
            {
                pressed = waitForAnyKey();
            } while (!(pressed == ConsoleKey.Y || pressed == ConsoleKey.N));

            if (pressed == ConsoleKey.Y)
            {
                clrscr(); //clear the console
                Environment.Exit(0);
            }
            return;
        }

        /**
        *
        * @name mainMenu
        * 
        * This function is used to create buttons such as New Game, High Scores, Controls and Exit on the main menu.
        **/
        public int mainMenu()
        {
            int x = 10, y = 5;
            int yStart = y;

            int selected;

            clrscr(); //clear the console
                      //Might be better with arrays of strings???
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("New Game\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("High Scores\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Controls\n");
            Console.SetCursorPosition(x, y++);
            Console.WriteLine("Exit\n");
            Console.SetCursorPosition(x, y++);

            selected = menuSelector(x, y, yStart);

            return (selected);
        }
    }
}
