using GameProgrammingii_MonogameRPG_BenjaminMackey.Scripts.Adjustable;
using GameProgrammingii_MonogameRPG_BenjaminMackey.Scripts.Backend;
using System;
using System.Diagnostics;

namespace GameProgrammingii_MonogameRPG_BenjaminMackey.Scripts.Demo
{


    public class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.WriteLine("aa");
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        private GameObject car;
        private CarController carController;
        private GameObject mapObj;


        private GameManager()
        {
            InitializeValues();
            startScreen = new GameObject();
            startScreen._transform._scale = new Vector3(2, 2, 2);
            startScreen._transform._position = new Vector3(500, 500, 0);
            SpriteRenderer rend = new SpriteRenderer(SpriteBin.GetSprite("StartMenu"),
                new Vector2(1, 1), SpriteRenderer.RenderFrom.Centre);
            rend.UI = true;
            startScreen._active = false;
            startScreen.AddComponent(rend);

            winScreen = new GameObject();
            winScreen._transform._scale = new Vector3(2, 2, 2);
            winScreen._transform._position = new Vector3(500, 500, 0);
            SpriteRenderer rend1 = new SpriteRenderer(SpriteBin.GetSprite("Win"),
                new Vector2(1, 1), SpriteRenderer.RenderFrom.Centre);
            rend1.UI = true;
            winScreen._active = false;
            winScreen.AddComponent(rend1);

            
            spacePress.ButtonPressed += StartGame;
            rPress.ButtonPressed += StartGame;
        }

        private void InitializeValues()
        {
            startScreen = new GameObject();
            startScreen._transform._scale = new Vector3(2, 2, 2);
            startScreen._transform._position = new Vector3(500, 500, 0);
            SpriteRenderer rend = new SpriteRenderer(SpriteBin.GetSprite("StartMenu"),
                new Vector2(1, 1), SpriteRenderer.RenderFrom.Centre);
            rend.UI = true;
            startScreen._active = false;
            startScreen.AddComponent(rend);

            winScreen = new GameObject();
            winScreen._transform._scale = new Vector3(2, 2, 2);
            winScreen._transform._position = new Vector3(500, 500, 0);
            SpriteRenderer rend1 = new SpriteRenderer(SpriteBin.GetSprite("Win"),
                new Vector2(1, 1), SpriteRenderer.RenderFrom.Centre);
            rend1.UI = true;
            winScreen._active = false;
            winScreen.AddComponent(rend1);

            loseScreen = new GameObject();
            loseScreen._transform._scale = new Vector3(2, 2, 2);
            loseScreen._transform._position = new Vector3(500, 500, 0);
            SpriteRenderer rend2 = new SpriteRenderer(SpriteBin.GetSprite("Lose"),
                new Vector2(1, 1), SpriteRenderer.RenderFrom.Centre);
            rend2.UI = true;
            loseScreen._active = false;
            loseScreen.AddComponent(rend2);

            ObjectManager._gameObjects.Clear();
            Debug.WriteLine("weeeeeeoooooaaaaaaaaaaaaa");
            car = new GameObject();
            car._transform._scale = new Vector3(100, 100, 100);
            car._name = "Car";
            player = car;

            mapObj = new GameObject();
            mapObj.AddTag("map");

            Map map = new Map("Map", 10000);
            mapObj.AddComponent(map);

            carController = new CarController();
            car.AddComponent(carController);

            checkpoints = map.checkPoints.ToArray();
            checkpoints[0]._active = true;
            EnemyInfoBin._player = car;
            checkPointVal = 0;

        }

        //game data
        public GameObject player;
        public bool playing { get; private set; }
        public double timer { get; private set; }
        public int checkPointVal = 0;
        public GameObject[] checkpoints;
        //

        //
        private GameObject startScreen;
        private GameObject loseScreen;
        private GameObject winScreen;
        //
        public void RestartGame()
        {
            checkPointVal = 0;
            car.Destroy();
            carController = null;
            mapObj.Destroy();
            timer = 0;

            InitializeValues();

            playing = true;
        }

        private double lastTime = 0;
        public void UpdateTime(TimeSpan num)
        {
            if (playing)
            {
                timer += num.TotalSeconds - lastTime;
            }
            lastTime = num.TotalSeconds;
        }
        public void StartGame(Object s, InputArgs a)
        {
            spacePress.ButtonPressed -= StartGame;
            playing = true;
            startScreen._active = false;
            loseScreen._active = false;
            winScreen._active = false;
            RestartGame();
        }
        public void LoadStartScreen()
        {
            
            startScreen._active = true;
            playing = false;
        }
        public void LoadLoseScreen()
        {
            loseScreen = new GameObject();
            loseScreen._transform._scale = new Vector3(2, 2, 2);
            loseScreen._transform._position = new Vector3(500, 500, 0);
            SpriteRenderer rend2 = new SpriteRenderer(SpriteBin.GetSprite("Lose"),
                new Vector2(1, 1), SpriteRenderer.RenderFrom.Centre);
            rend2.UI = true;
            loseScreen._active = true;
            loseScreen.AddComponent(rend2);
            playing = false;
        }
        public void LoadWinScreen()
        {

            winScreen = new GameObject();
            winScreen._transform._scale = new Vector3(2, 2, 2);
            winScreen._transform._position = new Vector3(500, 500, 0);
            SpriteRenderer rend1 = new SpriteRenderer(SpriteBin.GetSprite("Win"),
                new Vector2(1, 1), SpriteRenderer.RenderFrom.Centre);
            rend1.UI = true;
            winScreen._active = true;
            winScreen.AddComponent(rend1);
            playing = false;
        }
        private ButtonAction rPress = new ButtonAction(ConsoleKey.R);
        private ButtonAction spacePress = new ButtonAction(ConsoleKey.Spacebar);
        public void HitNext(GameObject chkPoint)
        {
            if (checkpoints == null) return;
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i] == chkPoint)
                {
                    if (i == checkPointVal)
                    {
                        checkPointVal++;
                        chkPoint.Destroy();
                        Debug.WriteLine("checky point");
                        if (i == checkpoints.Length - 1)
                        {
                            LoadWinScreen();
                            return;
                        }
                        checkpoints[i + 1]._active = true;
                    }
                }
            }
        }
    }
}
