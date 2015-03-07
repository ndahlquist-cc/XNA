/*  Zombie.cs
 *  
 *  This class is used to draw and create the enemies of the game
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
 *      + declare variables, create draw and update methods
 *      + https://wiki.themanaworld.org/images/b/b2/Monster-zombie.png zombie sprite sheet
 *  
 *  2014-11-11
 *      + Added parameters to set min and max pathing position and pathing speed
 *      + Added scrolling so that zombies move when the user presses left or right keys (helloKitty movement)
 *      + Added source rectangles for use with collision manager
 *      
 *  2014-11-12
 *      +Added squish animation
 *      +Created method to start squish animation and added code to update to kill zombie after it has been squished
 *      + http://soundbible.com/1040-Zombie-Gets-Attacked.html death sound effect
 *  2014-11-29
 *      + added second zombie
 *      http://www.castlevaniacrypt.com/games/hc/images/sprites/zombie.gif second zombie
 *  2014-11-30
 *      + fixed bug with zombie's not displaying
 * 
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
    /// Class to create and maintain drawable zombie enemies in the game
    /// </summary>
    public class Zombie : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //Zombie Class Variables
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Texture2D secondTex;
        private Vector2 position;
        private Rectangle srcRect;

        public Rectangle SrcRect
        {
            get { return srcRect; }
            set { srcRect = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 dimension;
        private Vector2 secondDimension;
        private List<Rectangle> framesRight;
        private List<Rectangle> framesLeft;
        private List<Rectangle> framesSquish;
        private List<Rectangle> framesSecondZombieRight;
        private List<Rectangle> framesSecondZombieLeft;
        private List<Rectangle> framesSecondZombieSquishedLeft;
        private List<Rectangle> framesSecondZombieSquishedRight;
        private List<Rectangle> framesSecondZombieStart;
        private SoundEffect squishSound;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private int positionChange;

        public int PositionChange
        {
            get { return positionChange; }
            set { positionChange = value; }
        }
        private KeyboardState oldState;
        private Vector2 speed;
        private bool isAlive;

        public bool IsAlive
        {
            get { return isAlive; }
        }

        private int zombieNumber;
        private int hitPoints;

        /// <summary>
        /// Paramaterized constructor for the Zombie class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">Main spritebatch</param>
        /// <param name="position">Starting position of the zombie</param>
        /// <param name="delay">Delay between frame changes and zombie movement</param>
        /// <param name="zombieNumber">Number used to specify different types of zombies</param>
        /// <param name="positionChange">Direction and speed the zombie will passively move at when it is created</param>
        public Zombie(Game game, SpriteBatch spriteBatch, Vector2 position, int delay, int zombieNumber, int positionChange = -10)
            : base(game)
        {
            // Construct child components
            this.spriteBatch = spriteBatch;
            this.tex = game.Content.Load<Texture2D>("Images/zombieSpriteSheet");
            this.secondTex = game.Content.Load<Texture2D>("Images/zombieLarge");
            this.position = position;
            if (zombieNumber == 1)
            {
                this.position.Y = this.position.Y + 3;
            }
            this.delay = delay;
            this.dimension = new Vector2(30, 57);
            this.secondDimension = new Vector2(40, 60);
            this.positionChange = positionChange;
            this.speed = Shared.worldSpeed;
            this.squishSound = game.Content.Load<SoundEffect>("Sounds/zombieDeath");
            this.isAlive = true;
            this.zombieNumber = zombieNumber;
            this.hitPoints = zombieNumber;

            // create frames here
            createFramesRight();
            createFramesLeft();
            createFramesSquished();
            createFramesSecondZombieRight();
            createFramesSecondZombieLeft();
            createFramesSecondZombieSquishedLeft();
            createFramesSecondZombieSquishedRight();
            createFramesSecondZombieStart();

            // stop the animation initially
            stop();

            //Set zombie number to default if number provided is out of bounds
            if(zombieNumber != 1 || zombieNumber !=2)
            {
                zombieNumber = 1;
            }
        }
        #region Second zombie frames
        /// <summary>
        /// Method to create frames for second Zombie animation moving right
        /// </summary>
        private void createFramesSecondZombieRight()
        {
            framesSecondZombieRight = new List<Rectangle>();
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int x = j * (int)secondDimension.X;
                    int y = i * (int)secondDimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)secondDimension.X, (int)secondDimension.Y);
                    framesSecondZombieRight.Add(r);
                }
            }
        }
        /// <summary>
        /// Method to create frames for second Zombie animation moving left
        /// </summary>
        private void createFramesSecondZombieLeft()
        {
            framesSecondZombieLeft = new List<Rectangle>();
            for (int i = 2; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int x = j * (int)secondDimension.X;
                    int y = i * (int)secondDimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)secondDimension.X, (int)secondDimension.Y);
                    framesSecondZombieLeft.Add(r);
                }
            }
        }
        /// <summary>
        /// Method to create frames for second Zombie animation squished left
        /// </summary>
        private void createFramesSecondZombieSquishedLeft()
        {
            framesSecondZombieSquishedLeft = new List<Rectangle>();
            for (int i = 3; i < 4; i++)
            {
                for (int j = 6; j < 11; j++)
                {
                    int x = j * (int)secondDimension.X;
                    int y = i * (int)secondDimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)secondDimension.X, (int)secondDimension.Y);
                    framesSecondZombieSquishedLeft.Add(r);
                }
            }
        }
        /// <summary>
        /// Method to create frames for second Zombie animation squished right
        /// </summary>
        private void createFramesSecondZombieSquishedRight()
        {
            framesSecondZombieSquishedRight = new List<Rectangle>();
            for (int i = 1; i < 2; i++)
            {
                for (int j = 4; j > -1; j--)
                {
                    int x = j * (int)secondDimension.X;
                    int y = i * (int)secondDimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)secondDimension.X, (int)secondDimension.Y);
                    framesSecondZombieSquishedRight.Add(r);
                }
            }
        }
        /// <summary>
        /// Method to create frames for second Zombie animation starting
        /// </summary>
        private void createFramesSecondZombieStart()
        {
            framesSecondZombieStart = new List<Rectangle>();
            for (int i = 3; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    int x = j * (int)secondDimension.X;
                    int y = i * (int)secondDimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)secondDimension.X, (int)secondDimension.Y);
                    framesSecondZombieStart.Add(r);
                }
            }
        }
        #endregion
        #region zombie frames
        /// <summary>
        /// Method to create frames for Zombie animation moving right
        /// </summary>
        private void createFramesRight()
        {
            framesRight = new List<Rectangle>();
            for (int i = 0; i < 1; i++)
			{
			    for (int j = 0; j < 7; j++)
			    {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    framesRight.Add(r);
			    }
			}
        }

        /// <summary>
        /// Method to create frames for Zombie animation moving left
        /// </summary>
        private void createFramesLeft()
        {
            framesLeft = new List<Rectangle>();
            for (int i = 1; i < 2; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle l = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    framesLeft.Add(l);
                }
            }
        }

        /// <summary>
        /// Method to create frames for Zombie animation when a zombie is squished
        /// </summary>
        private void createFramesSquished()
        {
            framesSquish = new List<Rectangle>();
            for (int i = 2; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle l = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    framesSquish.Add(l);
                }
            }
        }
        #endregion
        /// <summary>
        /// Method to employ the squish animation
        /// </summary>
        public void squish()
        {
            hitPoints--;
            this.squishSound.Play();
            if(hitPoints <=0)
            {
                this.isAlive = false;
                Shared.score += 100 * zombieNumber;
                this.positionChange = 0;
                this.frameIndex = 0;
            }                                    
        }


        /// <summary>
        /// method to stop zombie animation
        /// </summary>
        public void stop()
        {
            
            this.Enabled = false;
            this.Visible = false;
        }        

        /// <summary>
        /// method to start zombie animation
        /// </summary>
        public void start()
        {
            this.Enabled = true;
            this.Visible = true;
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
            // update code
            delayCounter++;
            if(delayCounter > delay)
            {
                delayCounter = 0;
                //Movement and set movement frames for zombie type 1
                if (zombieNumber == 1)
                {
                    position.X += positionChange;

                    frameIndex++;
                    if (frameIndex > 6)
                    {
                        frameIndex = 0;
                        //stop();
                    }

                    delayCounter = 0;                    
                }

                //Movement and set movement frames for zombie type 2
                else if (zombieNumber == 2)
                {
                    if (positionChange != 0)
                    {
                        position.X += positionChange;

                        frameIndex++;
                        if (frameIndex > 7)
                        {
                            frameIndex = 0;
                        }

                        delayCounter = 0;
                    }
                    else if (positionChange == 0)
                    {
                        position.X += positionChange;
                        frameIndex++;
                        if (frameIndex > 4)
                        {
                            stop();
                        }
                        delayCounter = 0;
                    }
                }
            }            

            //Move zombie as part of world movement
            if (!Shared.isKittyDead)
            {
                KeyboardState ks = Keyboard.GetState();              

                //Right movement
                if ((ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)) && !(oldState.IsKeyDown(Keys.Left) || oldState.IsKeyDown(Keys.A)))
                {
                    position.X -= Shared.worldSpeed.X;
                }

                //Left movement
                if ((ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A)) && !(ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)))
                {
                    position.X += Shared.worldSpeed.X;
                }

                oldState = ks;
            }

            //Zombies walk off screen when kitty is dead
            if (delayCounter > delay)
            {
                if (Shared.isKittyDead)
                {
                    position.X -= Math.Abs(positionChange);
                    if (position.X + tex.Width < Shared.stage.X)
                    {
                        stop();
                    }
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            //Create bounds for zombie type 1
            if (zombieNumber == 1)
            {
                srcRect = new Rectangle((int)position.X, (int)position.Y, 30, 57);
            }

            //Create bounds for zombie type 2
            else if (zombieNumber == 2)
            {
                srcRect = new Rectangle((int)position.X, (int)position.Y, 40, 60);
            }

            //Draw zombies depending on frameIndex and zombie type
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                if (positionChange > 0)
                {
                    if (zombieNumber == 1)
                    {
                        spriteBatch.Draw(tex, position, framesRight.ElementAt<Rectangle>(frameIndex), Color.White);
                    }
                    else if (zombieNumber == 2)
                    {
                        spriteBatch.Draw(secondTex, position, framesSecondZombieRight.ElementAt<Rectangle>(frameIndex), Color.White);
                    }
                }
                if (positionChange < 0)
                {
                    if (zombieNumber == 1)
                    {
                        spriteBatch.Draw(tex, position, framesLeft.ElementAt<Rectangle>(frameIndex), Color.White);
                    }
                    else if (zombieNumber == 2)
                    {
                        spriteBatch.Draw(secondTex, position, framesSecondZombieLeft.ElementAt<Rectangle>(frameIndex), Color.White);
                    }
                }

                //Zombie is in squish animation
                if (positionChange == 0)
                {
                    //Zombie type 1 squish
                    if (zombieNumber == 1)
                    {
                        //Squish animation final frame, stop zombie animation
                        if (frameIndex == 6)
                        {
                            this.stop();
                        }
                        spriteBatch.Draw(tex, position, framesSquish.ElementAt<Rectangle>(frameIndex), Color.White);
                    }

                    //Zombie type 2 squish
                    if (zombieNumber == 2)
                    {
                        //Squish animation final frame, stop zombie animation
                        if (frameIndex == 5)
                        {
                            this.stop();
                        }
                        spriteBatch.Draw(secondTex, position, framesSecondZombieSquishedLeft.ElementAt<Rectangle>(frameIndex), Color.White);
                    }

                }
                
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
