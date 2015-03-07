/*  StartScene.cs
 *  
 *  This game component provides the game's starting scene/menu.
 *  Inherits from GameScene.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
 *      + declare variables, create draw and update methods
 *      + added music, background
 *  Revision History:
 *  2014-11-18
 *      + added menu option for how to play scene
 *  2014-11-30
 *      + added extra option for continue if game is escaped during play
 *      + fixed user ability to control kitty in start scene
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
    /// This class provides the game's starting scene / menu scene
    /// </summary>
    public class StartScene : GameScene
    {
        //StartScene class variables
        private MenuComponent menu;
        public MenuComponent Menu
        {
            get { return menu; }
            set { menu = value; }
        }
        private SpriteBatch spriteBatch;
        private Zombie introZombie;
        private HelloKitty introKitty;
        private CollisionManager cm;
        private List<Zombie> zombies = new List<Zombie>();
        private float elapsedTime = 0f;       
        private string[] menuItems = { "Continue","New Game","Help", "How To Play", "High Score", "About", "Quit" };

        /// <summary>
        /// Constructor for the StartScene class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public StartScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // Construct child components
            this.spriteBatch = spriteBatch;
            menu = new MenuComponent(game, spriteBatch, game.Content.Load<SpriteFont>("Fonts/RegularFont"),
                game.Content.Load<SpriteFont>("Fonts/HighlightFont"), menuItems);

            //Background and menu
            Texture2D backgroundTex = game.Content.Load<Texture2D>("Images/MenuBackground");
            Rectangle src = new Rectangle(0, 0, 800, 480);
            Vector2 pos = new Vector2(0, 0);
            MenuBackground mb = new MenuBackground(game, spriteBatch, backgroundTex, src, pos);
            this.Components.Add(mb);

            //Zombie
            int delay = 30;
            Vector2 introZombiePos = new Vector2(-64, Shared.stage.Y - 64);
            introZombie = new Zombie(game, spriteBatch, introZombiePos, delay, 1, 10);
            zombies.Add(introZombie);

            //Kitty
            introKitty = new HelloKitty(game, spriteBatch, delay);

            //Collision Manager
            cm = new CollisionManager(game, introKitty, zombies, new List<Block>());           

            this.Components.Add(introKitty);
            this.Components.Add(introZombie);
            this.Components.Add(cm);
            introZombie.start();          

            this.Components.Add(menu);            

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here            
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            //Make kitty fall on the screen at the specified time
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if(Math.Round(elapsedTime/1000,0) == 20)
            {
                introKitty.start();
                introKitty.Position = new Vector2(400, -60);
                introKitty.fall();
            }
            Shared.worldSpeed = new Vector2(0, 0);
            base.Update(gameTime);
        }
    }
}
