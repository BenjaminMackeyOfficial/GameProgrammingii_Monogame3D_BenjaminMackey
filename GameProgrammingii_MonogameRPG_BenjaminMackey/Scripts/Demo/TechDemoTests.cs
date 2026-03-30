using Microsoft.Xna.Framework;

namespace GameProgrammingii_MonogameRPG_BenjaminMackey
{
    public class TechDemoTests : Game
    {
        private static TechDemoTests _instance;
        public static TechDemoTests Instance { get 
            { 
                if(_instance == null) _instance = new TechDemoTests();
                return _instance;
            } 
        }
        public void testMethod()
        {
            GameObject obj = new GameObject();
            Camera cam = new Camera(90f, 10000);

            obj.AddComponent(cam);

            Vector2InputMap move = new Vector2InputMap();

            ButtonAction w = new ButtonAction(System.ConsoleKey.W);
            ButtonAction a = new ButtonAction(System.ConsoleKey.A);
            ButtonAction s = new ButtonAction(System.ConsoleKey.S);
            ButtonAction d = new ButtonAction(System.ConsoleKey.D);

            move._up = w;
            move._down = s;
            move._left = a;
            move._right = d;

            Vector2InputMap look = new Vector2InputMap();

            ButtonAction up = new ButtonAction(System.ConsoleKey.UpArrow);
            ButtonAction left = new ButtonAction (System.ConsoleKey.LeftArrow);
            ButtonAction right = new ButtonAction(System.ConsoleKey.RightArrow);
            ButtonAction down = new ButtonAction(System.ConsoleKey.DownArrow);

            look._up = up;
            look._right = right;  
            look._up = up;
            look._right = left;

            TransformController controller = new TransformController(move, look, 10f);

            obj.AddComponent(controller);




            
        }


    }
}
