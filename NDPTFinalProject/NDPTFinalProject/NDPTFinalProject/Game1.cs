/*  Game1.cs
 *  
 *  This class creates the game
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
 *      + declare variables, create draw and update methods
 *      + http://www.officialpsds.com/images/thumbs/Hello-Kitty-psd49763.png icon
 *  2014-11-09
 *      + moved load content for actionScene to actionScene class
 *      + got intro music to stop playing in other scenes
 *  2014-11-23
 *      + created method to transition to level two from level one
 *  2014-11-29
 *      + added music for level two
 *  2014-11-30
 *      + adjusted music transitions
 *      + added methods for level one to load into transition one,
 *          for transition one to load into level two,
 *          and for level two to load into transition two
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NDPTFinalProject
{
    /// <summary>
    /// This is the main class for the game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // class scope variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // declare the scenes
        private GameScene startScene;
        private GameScene levelOne;
        private GameScene levelTwo;
        private GameScene helpScene;
        private GameScene howToPlayScene;
        private GameScene creditScene;
        private GameScene highScoreScene;
        private GameScene transitionOne;
        private GameScene transitionTwo;
        // declare music
        private Song song;
        private Song levelTwoSong;

        /// <summary>
        /// Game1 class constructor
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            song = this.Content.Load<Song>("Sounds/introMusic");
            MediaPlayer.Play(song);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // initialieze shared class and stage size
            Shared.initializeShared();
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        /// <summary>
        /// This method hides all the scenes
        /// </summary>
        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();                    
                }
            }

        }

        /// <summary>
        /// this method hides all scenes and then loads transition one.
        /// called after first level
        /// </summary>
        public void loadTransitionOne()
        {
            hideAllScenes();
            transitionOne.show();
        }

        /// <summary>
        /// this method hides all scenes and then loads transition two.
        /// called after second level
        /// </summary>
        public void loadTransitionTwo()
        {
            hideAllScenes();
            transitionTwo.show();
        }

        /// <summary>
        /// this method hides all scenes and then loads level two.
        /// As well as level two music.  Called after transition one
        /// </summary>
        public void loadLevelTwo()
        {
            hideAllScenes();
            MediaPlayer.Play(levelTwoSong);
            MediaPlayer.IsRepeating = true;
            levelTwo.show();
        }

        /// <summary>
        /// this method hides all scenes and then loads high score scene.
        /// Called on hello kitty death, or game completion
        /// </summary>
        public void loadHighScoreEntry()
        {
            hideAllScenes();
            highScoreScene.show();
            HighScoreScene.setScoreMode(true);
        }

        /// <summary>
        /// this method hides all scenes and then loads the start scene.
        /// called after highscore input, on on esc key press
        /// </summary>
        public void loadStartScene()
        {
            if (highScoreScene.Enabled && HighScoreScene.ScoreInputMode == true)
            {                
                if (Shared.level == 2)
                {
                    MediaPlayer.Play(song);
                }
                LoadContent();

            }
            hideAllScenes();
            startScene.show();
        }
        
        
        /// <summary>
        /// LoadContent will be called once per game and loads
        /// all game content.
        /// </summary>
        protected override void LoadContent()
        {
            //Initialize shared variables
            Shared.initializeShared();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load music
            levelTwoSong = Content.Load<Song>("Sounds/themeSong");

            // load game scenes
            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            howToPlayScene = new HowToPlayScene(this, spriteBatch);
            this.Components.Add(howToPlayScene);
            
            levelOne = new LevelOne(this, spriteBatch, Content.Load<Texture2D>("Images/levelOneBackground"), 
                Content.Load<Texture2D>("Images/levelOneForeground"));
            this.Components.Add(levelOne);

            transitionOne = new TransitionOne(this, spriteBatch);
            this.Components.Add(transitionOne);

            transitionTwo = new TransitionTwo(this, spriteBatch);
            this.Components.Add(transitionTwo);
            
            levelTwo = new LevelTwo(this, spriteBatch, Content.Load<Texture2D>("Images/levelTwoBackground"), 
                Content.Load<Texture2D>("Images/levelTwoForeground"), levelTwoSong);
            this.Components.Add(levelTwo);
            
            creditScene = new CreditScene(this, spriteBatch);
            this.Components.Add(creditScene);

            highScoreScene = new HighScoreScene(this, spriteBatch);
            this.Components.Add(highScoreScene);

            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);
            // initially display startScene.
            startScene.show();
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Unused
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //update logic
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            
            if (startScene.Enabled)
            {
                Shared.isStartScene = true;
                // actions based on menu selections
                selectedIndex = ((StartScene)startScene).Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown (Keys.Enter))
                {
                    hideAllScenes();
                    if(Shared.level == 1)
                    {
                        levelOne.show();
                    }

                    if (Shared.level == 2)
                    {
                        levelTwo.show();
                        MediaPlayer.Play(levelTwoSong);
                    }
                    
                }
                if (selectedIndex == 1 && ks.IsKeyDown (Keys.Enter))
                {
                    LoadContent();
                    hideAllScenes();
                    levelOne.show();
                    Shared.minMenuIndex = 0;
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                if (selectedIndex == 3 && ks.IsKeyDown (Keys.Enter))
                {
                    hideAllScenes();
                    howToPlayScene.show();                    
                }
                if (selectedIndex == 4 && ks.IsKeyDown (Keys.Enter))
                {
                    hideAllScenes();
                    HighScoreScene.setScoreMode(false);
                    highScoreScene.show();                    
                }
                if (selectedIndex == 5 && ks.IsKeyDown (Keys.Enter))
                {
                    hideAllScenes();
                    creditScene.show();
                }

                if (selectedIndex == 6 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    creditScene.show();
                    Exit();
                }
            }

            else
            {
                Shared.isStartScene = false;
            }

            // allows player to exit to start scene at any point in the game
            if (helpScene.Enabled || levelOne.Enabled || levelTwo.Enabled || highScoreScene.Enabled 
                || creditScene.Enabled || howToPlayScene.Enabled || transitionOne.Enabled 
                || transitionTwo.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    if(levelTwo.Enabled)
                    {
                        MediaPlayer.Play(song);
                    }

                    loadStartScene();
                }                
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateBlue);
            base.Draw(gameTime);
        }
    }
}
