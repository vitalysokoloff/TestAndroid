using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace TestAndroid
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 screenSize;

        Character character;

        // Content
        SpriteFont font;
        // Buttons
        Texture2D [] buttonsSprites = new Texture2D[8];

        // Character
        Texture2D [] characterSprites = new Texture2D[12];
        SoundEffect[] sounds = new SoundEffect[3];

        // Tiles
        Texture2D [] tilesSprites = new Texture2D[21];
        MapTile[] tiles;
        Texture2D[] decorSprites = new Texture2D[16];
        DecorTile[] decors;
        DecorTile[] frontDecors;
        Texture2D[] creepSprites = new Texture2D[14];
        CreepTile[] creeps;
        Texture2D[] itemSprites = new Texture2D[7];

        //Background
        Texture2D background1;
        Texture2D background2;

        // Maps
        Texture2D map1;  // ландшафт
        Texture2D map2;  // декор задник
        Texture2D map3;  // крипы
        Texture2D map4;  // декор передник=)

        // Game
        Texture2D logo;
        Texture2D logo2;
        int mode = 0;
        ScreenButton startButton;
        StartButtonController startButtonController;
        bool startPressed = false;
        SoundEffect sound;
        SoundEffectInstance intro;

        int count;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            var metric = new Android.Util.DisplayMetrics();
            Activity.WindowManager.DefaultDisplay.GetMetrics(metric);
           
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            screenSize.X = graphics.PreferredBackBufferWidth = metric.WidthPixels;
            screenSize.Y = graphics.PreferredBackBufferHeight = metric.HeightPixels;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            graphics.ApplyChanges();
        }


        protected override void Initialize()
        {
            base.Initialize();
            tiles = ReadMap.MapTiles(map1,tilesSprites);
            decors = ReadMap.DecorTiles(map2, decorSprites);
            frontDecors = ReadMap.DecorTiles(map4, decorSprites);
            creeps = ReadMap.CreepTiles(map3, creepSprites);            
            character = new Character(characterSprites, buttonsSprites, itemSprites, sounds, font, screenSize, new Vector2(120, 10));
            startButton = new ScreenButton(buttonsSprites[6], buttonsSprites[7], new Vector2(screenSize.X / 2 - 123, screenSize.Y - 144));
            startButtonController = new StartButtonController(startButton);
            intro = sound.CreateInstance();
            intro.Volume = 0.4f;

        }
        protected override void LoadContent()
        {           
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");
            
            buttonsSprites[0] = Content.Load<Texture2D>("i_button_left_up");
            buttonsSprites[1] = Content.Load<Texture2D>("i_button_left_down");
            buttonsSprites[2] = Content.Load<Texture2D>("i_button_right_up");
            buttonsSprites[3] = Content.Load<Texture2D>("i_button_right_down");
            buttonsSprites[4] = Content.Load<Texture2D>("i_button_a_up");
            buttonsSprites[5] = Content.Load<Texture2D>("i_button_a_down");
            buttonsSprites[6] = Content.Load<Texture2D>("i_button_start_up");
            buttonsSprites[7] = Content.Load<Texture2D>("i_button_start_down");

            characterSprites[0] = Content.Load<Texture2D>("c_standing");
            characterSprites[1] = Content.Load<Texture2D>("c_jumping");
            characterSprites[2] = Content.Load<Texture2D>("c_running_1");
            characterSprites[3] = Content.Load<Texture2D>("c_running_2");
            characterSprites[4] = Content.Load<Texture2D>("c_running_3");
            characterSprites[5] = Content.Load<Texture2D>("c_running_4");
            characterSprites[6] = Content.Load<Texture2D>("c_standing_2");
            characterSprites[7] = Content.Load<Texture2D>("c_jumping_2");
            characterSprites[8] = Content.Load<Texture2D>("c_running_1_2");
            characterSprites[9] = Content.Load<Texture2D>("c_running_2_2");
            characterSprites[10] = Content.Load<Texture2D>("c_running_3_2");
            characterSprites[11] = Content.Load<Texture2D>("c_running_4_2");

            tilesSprites[0] = Content.Load<Texture2D>("m_t_ground_1");
            tilesSprites[1] = Content.Load<Texture2D>("m_t_ground_2");
            tilesSprites[2] = Content.Load<Texture2D>("m_t_ground_3");
            tilesSprites[3] = Content.Load<Texture2D>("m_t_ground_2_2");
            tilesSprites[4] = Content.Load<Texture2D>("m_t_ground_3_2");
            tilesSprites[5] = Content.Load<Texture2D>("m_t_ground_4");

            tilesSprites[6] = Content.Load<Texture2D>("m_l_corner");
            tilesSprites[7] = Content.Load<Texture2D>("m_l_border_1");
            tilesSprites[8] = Content.Load<Texture2D>("m_l_border_2");
            tilesSprites[9] = Content.Load<Texture2D>("m_r_corner");
            tilesSprites[10] = Content.Load<Texture2D>("m_r_border_1");
            tilesSprites[11] = Content.Load<Texture2D>("m_r_border_2");
            tilesSprites[12] = Content.Load<Texture2D>("m_t_gridge_1");
            tilesSprites[13] = Content.Load<Texture2D>("m_t_gridge_2");
            tilesSprites[14] = Content.Load<Texture2D>("m_t_gridge_3");
            tilesSprites[15] = Content.Load<Texture2D>("m_water");
            tilesSprites[16] = Content.Load<Texture2D>("m_h_ground_1");
            tilesSprites[17] = Content.Load<Texture2D>("m_h_ground_2");
            tilesSprites[18] = Content.Load<Texture2D>("m_h_ground_3");
            tilesSprites[19] = Content.Load<Texture2D>("m_l_corner_2");
            tilesSprites[20] = Content.Load<Texture2D>("m_r_corner_2");

            decorSprites[0] = Content.Load<Texture2D>("d_bush_1");
            decorSprites[1] = Content.Load<Texture2D>("d_bush_2");
            decorSprites[2] = Content.Load<Texture2D>("d_bush_1_1");
            decorSprites[3] = Content.Load<Texture2D>("d_bush_1_2");
            decorSprites[4] = Content.Load<Texture2D>("d_bush_2_1");
            decorSprites[5] = Content.Load<Texture2D>("d_splash_1");
            decorSprites[6] = Content.Load<Texture2D>("d_splash_2");
            decorSprites[7] = Content.Load<Texture2D>("d_splash_3");
            decorSprites[8] = Content.Load<Texture2D>("d_splash_4");
            decorSprites[9] = Content.Load<Texture2D>("d_house");
            decorSprites[10] = Content.Load<Texture2D>("d_backborder");
            decorSprites[11] = Content.Load<Texture2D>("d_well");
            decorSprites[12] = Content.Load<Texture2D>("d_backtree");
            decorSprites[13] = Content.Load<Texture2D>("d_frontborder");
            decorSprites[14] = Content.Load<Texture2D>("d_cake");
            decorSprites[15] = Content.Load<Texture2D>("d_fronttree");

            creepSprites[0] = Content.Load<Texture2D>("e_1_l_1");
            creepSprites[1] = Content.Load<Texture2D>("e_1_l_2");
            creepSprites[2] = Content.Load<Texture2D>("e_1_r_1");
            creepSprites[3] = Content.Load<Texture2D>("e_1_r_2");
            creepSprites[4] = Content.Load<Texture2D>("e_2_1");
            creepSprites[5] = Content.Load<Texture2D>("e_2_2");
            creepSprites[6] = Content.Load<Texture2D>("e_2_3");
            creepSprites[7] = Content.Load<Texture2D>("e_2_4");
            creepSprites[8] = Content.Load<Texture2D>("i_1");
            creepSprites[9] = Content.Load<Texture2D>("i_2");
            creepSprites[10] = Content.Load<Texture2D>("i_3");
            creepSprites[11] = Content.Load<Texture2D>("e_3");
            creepSprites[12] = Content.Load<Texture2D>("i_4");
            creepSprites[13] = Content.Load<Texture2D>("e_4");

            itemSprites[0] = Content.Load<Texture2D>("i_4");
            itemSprites[1] = Content.Load<Texture2D>("i_5");
            itemSprites[2] = Content.Load<Texture2D>("i_6");
            itemSprites[3] = Content.Load<Texture2D>("i_1_1");
            itemSprites[4] = Content.Load<Texture2D>("i_2_1");
            itemSprites[5] = Content.Load<Texture2D>("i_3_1");
            itemSprites[6] = Content.Load<Texture2D>("i_7");

            background1 = Content.Load<Texture2D>("b_1");
            background2 = Content.Load<Texture2D>("b_2");
            logo = Content.Load<Texture2D>("logo");
            logo2 = Content.Load<Texture2D>("logo2");

            map1 = Content.Load<Texture2D>("map_1");
            map2 = Content.Load<Texture2D>("map_2");
            map3 = Content.Load<Texture2D>("map_3");
            map4 = Content.Load<Texture2D>("map_4");

            sounds[0] = Content.Load<SoundEffect>("theme");
            sounds[1] = Content.Load<SoundEffect>("coin");
            sounds[2] = Content.Load<SoundEffect>("jump");
            sound = Content.Load<SoundEffect>("intro");
        }

        protected override void UnloadContent()
        {
          
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.BigButton == ButtonState.Pressed)
                Exit();

            switch (mode)
            {
                case 0:
                    intro.Play();
                    if (startButtonController.Listener() == 1)
                    {
                        startPressed = true;                        
                    }
                    else if (startPressed)
                    {
                        intro.Stop();
                        startPressed = false;
                        mode = 1;
                    }

                    break;
                case 1:
                    intro.Play();
                    if (startButtonController.Listener() == 1)
                    {
                        startPressed = true;
                        
                    }
                    else if (startPressed)
                    {
                        intro.Stop();
                        startPressed = false;
                        mode = 2;
                    }
                    break;
                case 2:

                    if (character.IsWin())
                    {
                        mode = 3;
                    }

                    character.Update(gameTime, tiles, creeps);

                    foreach (DecorTile d in decors)
                        if (character.CanDraw(d.GetCollider()))
                        {
                            d.Update(gameTime);
                        }

                    foreach (CreepTile c in creeps)
                        if (character.CanDraw(c.GetCollider()) && c.IsExist())
                        {
                            c.Update(gameTime);
                        }

                    foreach (DecorTile f in frontDecors)
                        if (character.CanDraw(f.GetCollider()))
                        {
                            f.Update(gameTime);
                        }

                    base.Update(gameTime);
                    break;
                case 3:
                    intro.Play();
                    if (startButtonController.Listener() == 1)
                    {
                        startPressed = true;

                    }
                    else if (startPressed)
                    {
                        intro.Stop();
                        startPressed = false;
                        mode = 0;
                        character.Reset();
                        foreach (CreepTile c in creeps)
                            if (!c.IsExist())
                                c.WakeUp();
                    }
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (mode)
            {
                case 0:
                    GraphicsDevice.Clear(new Color(30, 30, 30));
                    spriteBatch.Begin();
                    spriteBatch.Draw(logo, new Vector2(screenSize.X / 2 - 177, screenSize.Y / 2 - 350), Color.White);
                    spriteBatch.DrawString(font,"разработчик: vitaly sokoloff", new Vector2(screenSize.X / 2 - 210 , screenSize.Y / 2 + 96), Color.White);
                    spriteBatch.DrawString(font, "с любовью для моей племянницы Сони", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 + 136), Color.White);
                    startButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case 1:
                    GraphicsDevice.Clear(new Color(30, 30, 30));
                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, "Маленькие лошадки, жители деревни", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 - 200), Color.White);
                    spriteBatch.DrawString(font, "\"Пинкберри\", собираются праздновать", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 - 160), Color.White);
                    spriteBatch.DrawString(font, "день рождение жеребенка по имени", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 - 120), Color.White);
                    spriteBatch.DrawString(font, "желудь. ", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 - 80), Color.White);
                    spriteBatch.DrawString(font, "Лошадки уже испекли торт, но", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 - 40), Color.White);
                    spriteBatch.DrawString(font, "для празднования нужно еще найти", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2), Color.White);
                    spriteBatch.DrawString(font, "шарики, мороженое и вишню.", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 + 40), Color.White);
                    spriteBatch.DrawString(font, "Лошадка по именни ягодка", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 + 80), Color.White);
                    spriteBatch.DrawString(font, "вызвалась найти все необходимое.", new Vector2(screenSize.X / 2 - 280, screenSize.Y / 2 + 120), Color.White);
                    startButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case 2:

                    GraphicsDevice.Clear(new Color(60, 190, 250));

                    spriteBatch.Begin();
                    spriteBatch.Draw(background2, new Vector2(0, screenSize.Y - 410 + character.GetVectorVelocityY() / 2), Color.White);
                    spriteBatch.Draw(background1, new Vector2(screenSize.X / 2 - 707, -character.GetVectorVelocityY() / 4), Color.White);

                    count = 0;
                    foreach (MapTile t in tiles)
                    {
                        if (character.CanDraw(t.GetCollider()))
                        {
                            t.Draw(spriteBatch, character.GetDelta());
                            count++;
                        }
                    }

                    foreach (DecorTile d in decors)
                    {
                        if (character.CanDraw(d.GetCollider()))
                        {
                            d.Draw(spriteBatch, character.GetDelta());
                            count++;
                        }
                    }                   

                    foreach (CreepTile c in creeps)
                    {
                        if (character.CanDraw(c.GetCollider()) && c.IsExist())
                        {
                            c.Draw(spriteBatch, character.GetDelta());
                            count++;
                        }
                    }

                    character.Draw(spriteBatch);

                    foreach (DecorTile f in frontDecors)
                    {
                        if (character.CanDraw(f.GetCollider()))
                        {
                            f.Draw(spriteBatch, character.GetDelta());
                            count++;
                        }
                    }

                    character.DrawInreface(spriteBatch);

                    //spriteBatch.DrawString(font, screenSize.X + "x" + screenSize.Y, new Vector2(10, 40), Color.Black);

                    spriteBatch.End();
                    break;
                case 3:
                    GraphicsDevice.Clear(new Color(30, 30, 30));
                    spriteBatch.Begin();
                    spriteBatch.Draw(logo2, new Vector2(screenSize.X / 2 - 345, screenSize.Y / 2 - 340), Color.White);
                    startButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
