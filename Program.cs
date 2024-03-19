using System;
using SFML.Graphics;
using SFML.Learning;
using SFML.System;
using SFML.Window;

class Program :Game
    {
    static uint APPLICATION_WINDOW_WIDTH = 1400;
    static uint APPLICATION_WINDOW_HEIGHT = 900;

    static int INNER_CIRCLE_INITIAL_RADIUS = 10;
    static int OUTER_CIRCLE_INITIAL_RADIUS = 400;

    static string startDrawSound = LoadSound("action.wav");
    static string stopDrawSound = LoadSound("stopDraw.wav");
    static string circlesCollisionSound = LoadSound("circlesCrash.wav");

    static int OUTER_CIRCLE_RADIUS = OUTER_CIRCLE_INITIAL_RADIUS;
    static int INNER_CIRCLE_RADIUS = INNER_CIRCLE_INITIAL_RADIUS;
    static int CIRCLE_RADIUS_ITERATION_INCREMENT = 5;
    static int getOuterCircleRadius () {
        return OUTER_CIRCLE_RADIUS;
    }
    static int getInnerCircleRadius() {
        return INNER_CIRCLE_RADIUS;
    }
     
    static int drawOuterCircle(Color color)
    {
        SetFillColor(color);
        FillCircle((APPLICATION_WINDOW_WIDTH / 2), (APPLICATION_WINDOW_HEIGHT / 2), OUTER_CIRCLE_RADIUS);
        return OUTER_CIRCLE_RADIUS;
    }

    static int drawInnerCircle() 
    {       
        SetFillColor(255,255,0);
        // Console.WriteLine("inner circle " + INNER_CIRCLE_RADIUS);
        FillCircle((APPLICATION_WINDOW_WIDTH / 2), (APPLICATION_WINDOW_HEIGHT / 2), INNER_CIRCLE_RADIUS);
        return INNER_CIRCLE_RADIUS;
    }

    static void increaseInnerCircleRadius() 
    {
        INNER_CIRCLE_RADIUS+=CIRCLE_RADIUS_ITERATION_INCREMENT;       
    }
    static void setOuterCircleRadius(int radius)
    {
        OUTER_CIRCLE_RADIUS = radius;
    }
    static void setInnerCircleRadius(int radius)
    {
        INNER_CIRCLE_RADIUS = radius;
    }

    static void timerExec(object state)
    {
        // Console.WriteLine("Draw...");
        growInnerCircle();        
    }

    static void growInnerCircle()
    {
        if(getInnerCircleRadius()>getOuterCircleRadius())
        {
            gameOver=true;
            PlaySound(circlesCollisionSound);
        }
       drawInnerCircle();
       increaseInnerCircleRadius();

    }
     static string formScoreString (int score)
    {
        return ("Current score: "+score.ToString());
    }

    static Boolean ALLOW_DRAW = false;
    static Boolean gameOver = false;
    static Boolean quitGame = false;
    static Boolean spaceKeyHitAllowed = true;
    static Boolean leftShiftKeyHitAllowed = false;
    static int totalScore = 0;
    static int currentScore = 0;

    static void increaseScore(int scoreAddition) {
        totalScore += scoreAddition;
    }

    static int getScore() {
    return totalScore;
    }


    static void Main(string[] args)
        {
        Console.WriteLine("Press Spacebar to start iteration..."); 
        InitWindow(APPLICATION_WINDOW_WIDTH, APPLICATION_WINDOW_HEIGHT, "SquizzingCircles 1.0");
        SetFont("comic.ttf");
        SetFillColor(255, 0, 255);
        DrawText(300, 400, "Space - start draw, Left Shift - Stop draw", 35);

         
            while (!gameOver)
            {
                DispatchEvents();
                DisplayWindow();

                if (spaceKeyHitAllowed)
                {
                    if (GetKey(SFML.Window.Keyboard.Key.Space) == true)
                    {
                        Console.WriteLine("DRAW STARTED");
                        ALLOW_DRAW = true;
                        PlaySound(startDrawSound);
                        SetFont("comic.ttf");
                        SetFillColor(255, 0, 255);
                        DrawText(0, 8, "Left shift - stop draw", 35);
                        spaceKeyHitAllowed = false;
                        leftShiftKeyHitAllowed = true;
                    }
                }

                if (leftShiftKeyHitAllowed)
                {
                    if (GetKey(SFML.Window.Keyboard.Key.LShift) == true)
                    {
                        Console.WriteLine("DRAW FINISHED");
                        PlaySound(stopDrawSound);
                        ALLOW_DRAW = false;
                        spaceKeyHitAllowed = true;
                        leftShiftKeyHitAllowed = false;

                        Console.WriteLine("Outer circle: " + getOuterCircleRadius());
                        Console.WriteLine("Inner circle: " + getInnerCircleRadius());

                        ClearWindow();
                        drawOuterCircle(Color.Black);
                        currentScore += (getOuterCircleRadius() - getInnerCircleRadius());
                        increaseScore(currentScore);
                        Console.WriteLine("CURRENT SCORE : " + currentScore);
                        Console.WriteLine("TOTAL SCORE : " + getScore());
                        setOuterCircleRadius(getInnerCircleRadius());
                        setInnerCircleRadius(0);
                    }
                }

                if (ALLOW_DRAW)
                {
                    ClearWindow();
                    drawOuterCircle(Color.White);
                    growInnerCircle();
                }

                Delay(80);

            }           

            ClearWindow();
            SetFont("comic.ttf");
            SetFillColor(255, 0, 0);
            DrawText(500, 400, "GAME OVER", 65);
            DrawText(500, 600, formScoreString(getScore()), 65);
            // PlaySound(endGameSuccess); 
            DisplayWindow();
            Delay(1000);         
    }
}
 

