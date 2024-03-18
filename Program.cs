using System;
using System.Threading;
using System.Windows.Input;
using SFML.Graphics;
using SFML.Learning;

     class Program :Game
    {
    static uint APPLICATION_WINDOW_WIDTH = 1400;
    static uint APPLICATION_WINDOW_HEIGHT = 900;

    static int INNER_CIRCLE_INITIAL_RADIUS = 10;
    static int OUTER_CIRCLE_INITIAL_RADIUS = 400;

    static int OUTER_CIRCLE_RADIUS = OUTER_CIRCLE_INITIAL_RADIUS;
    static int INNER_CIRCLE_RADIUS = INNER_CIRCLE_INITIAL_RADIUS;
    static int CIRCLE_RADIUS_ITERATION_INCREMENT = 5;
    static int getOuterCircleRadius () {
        return OUTER_CIRCLE_RADIUS;
    }
    static int getInnerCircleRadius() {
        return INNER_CIRCLE_RADIUS;
    }

    static void reportCircleParameters()
    {
        Console.WriteLine("Outer circle : " + getOuterCircleRadius());
        Console.WriteLine("Inner circle : " + getInnerCircleRadius());
    }

    static int iterationCounter = 1;


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

    static void processIteration()
    {
        Timer timer = new Timer(timerExec, null, 0, 1000);

        while (ALLOW_DRAW)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    ALLOW_DRAW = false;
                    timer.Dispose();
                    Console.WriteLine("ITERATION " + iterationCounter + " completed.");
                    iterationCounter++;
                }
            }
        } 
    }

    static void growInnerCircle()
    {
        if(getInnerCircleRadius()>getOuterCircleRadius())
        {
            gameOver=true;
        }
       drawInnerCircle();
       increaseInnerCircleRadius();

    }

    static void setNextPhaseParameters() {
        ClearWindow();
        setOuterCircleRadius(getInnerCircleRadius());
        setInnerCircleRadius(0);
    }

    static Boolean ALLOW_DRAW = false;
    static Boolean gameOver = false;  
 
    static void Main(string[] args)
        {
        Console.WriteLine("Press Spacebar to start iteration..."); 
        InitWindow(APPLICATION_WINDOW_WIDTH, APPLICATION_WINDOW_HEIGHT, "SquizzingCircles 1.0");
        SetFont("comic.ttf");
        SetFillColor(255, 0, 255);
        DrawText(300, 400, "Space - start iteration, LShift - stop iteration", 35);

        while (!gameOver)
        {
            DispatchEvents();
            DisplayWindow();

            if (GetKey(SFML.Window.Keyboard.Key.Space) == true)
            {               
                Console.WriteLine("DRAW STARTED");                
                ALLOW_DRAW=true;                    
            }

           
            if (GetKey(SFML.Window.Keyboard.Key.LShift) == true)
            {                
                Console.WriteLine("DRAW FINISHED");
                ALLOW_DRAW = false;

                Console.WriteLine("Outer circle: " + getOuterCircleRadius());
                Console.WriteLine("Inner circle: " + getInnerCircleRadius());

                ClearWindow();
                drawOuterCircle(Color.Black);
                setOuterCircleRadius(getInnerCircleRadius());
                setInnerCircleRadius(0);              
                 
            }

            if(ALLOW_DRAW)
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
        // PlaySound(endGameSuccess); 
        DisplayWindow();
        Delay(1000);
    }
}
 

