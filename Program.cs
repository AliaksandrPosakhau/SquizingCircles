using System;
using System.Threading;
using System.Windows.Input;
using SFML.Learning;

     class Program :Game
    {
    static uint APPLICATION_WINDOW_WIDTH = 1400;
    static uint APPLICATION_WINDOW_HEIGHT = 900;

    static int INNER_CIRCLE_INITIAL_RADIUS = 10;
    static int OUTER_CIRCLE_INITIAL_RADIUS = 400;

    static int OUTER_CIRCLE_RADIUS = OUTER_CIRCLE_INITIAL_RADIUS;
    static int INNER_CIRCLE_RADIUS = INNER_CIRCLE_INITIAL_RADIUS;

    /*
     * if true - state available for start iteration. // START DRAW
     * if false - state is not available to start iteration // HOLD DRAW
     */
    static Boolean ALLOW_DRAW = false;

    static int iterationCounter = 1;

    static int drawOuterCircle(int radius)
    {
        SetFillColor(255, 255, 255);
        FillCircle((APPLICATION_WINDOW_WIDTH / 2), (APPLICATION_WINDOW_HEIGHT / 2), OUTER_CIRCLE_RADIUS);
        return OUTER_CIRCLE_RADIUS;
    }

    static int drawInnerCircle() 
    {       
        SetFillColor(255,255,0);
        Console.WriteLine("inner circle " + INNER_CIRCLE_RADIUS);
        // FillCircle((APPLICATION_WINDOW_WIDTH / 2), (APPLICATION_WINDOW_HEIGHT / 2), INNER_CIRCLE_RADIUS);
        return INNER_CIRCLE_RADIUS;
    }

    static void increaseInnerCircleRadius() 
    {
        INNER_CIRCLE_RADIUS+=50;
    }
    static void setOuterCircleRadius(int radius)
    {
        OUTER_CIRCLE_RADIUS = radius;
    }
  
    static void timerExec(object state)
    {
        Console.WriteLine("Draw...");
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
            drawInnerCircle();            
            increaseInnerCircleRadius();         
    }

    static int FIRST_CIRCLE_INITIAL_RADIUS = 350;

 
    static void Main(string[] args)
        {

        Console.WriteLine("Press Spacebar to start iteration..."); 

        //InitWindow(APPLICATION_WINDOW_WIDTH, APPLICATION_WINDOW_HEIGHT, "SquizzingCircles 1.0");

        Boolean isGameProcessAvailable = OUTER_CIRCLE_RADIUS > INNER_CIRCLE_RADIUS;

        while (isGameProcessAvailable)
        {
            //DispatchEvents();            
            
            if (ALLOW_DRAW == false)
            {
                ConsoleKeyInfo startCycleKeyInfo;
                startCycleKeyInfo = Console.ReadKey();
                if (startCycleKeyInfo.Key == ConsoleKey.Spacebar && ALLOW_DRAW == false)
                {
                    ALLOW_DRAW = true;
                    Console.WriteLine("ITERATION " + iterationCounter + " started.");                   
                }
            }           
            processIteration();             
 
            if (!isGameProcessAvailable)
            {
                break;
            }
            
        
        //DisplayWindow();
        Delay(1);
        }
       
    }
}
 

