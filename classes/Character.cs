using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace TestAndroid
{
	public class Character
	{        
        // control
        private ScreenButtonController controller;
        private ScreenButton[] buttons = new ScreenButton[3];
        private Vector2 screenSize;
        private Vector2 delta; // Для смещения коллайдера
        private Rectangle collider;

        //Any stuff
        private int[] answer = { 0, 0 };
        private bool isAPressed = false;
        private int curFrame = 0;
        private int direction = 0; // 0 - left, 1 - right;

        private int gravity = 15;
        private int vectorVelocityY = 0;
        private int vectorVelocityX = 0;
        Vector2 cameraDelta;

        private Vector2 position;
        private Vector2 lastPosition;

        private Camera camera;

        // Sprites
        private SpriteFont font;
        // Buttons
        private Texture2D[] buttonsSprites = new Texture2D[6];

        // character
        private Texture2D[] characterSprites = new Texture2D[12];
        SoundEffect[] sounds;
        SoundEffectInstance theme;
        SoundEffectInstance coin;
        SoundEffectInstance jump;
        private bool isWin = false;

        // items
        private Texture2D[] itemSprites = new Texture2D[5];

        // Animation
        private int curTime = 0;
        private int period = 96;

        // ForVelocityX
        private int curTime2 = 0;
        private int period2 = 80;

        // ForTalking
        private int curFrame2 = 0;
        private int curTime3 = 0;
        private int period3 = 800;

        // Items
        private int cherry = 0;
        private int icecream = 0;
        private int baloon = 0;

        // Talking
        private bool talking = false;
        private bool maping = false;

        /* characterSprites: 0 - standing, 1 - jumping, 2-5 - running 
         * characterSprites: 6 - standing, 7 - jumping, 8-11 - running 
         * buttonsSprites: 0-1 - left, 2-3 - right,  4-5 - a
         * font:
         * screenSize: размер экрана ля ориентации элементов
         * delta: смещение коллайдера
         */
        public Character(Texture2D [] characterSprites, Texture2D[] buttonsSprites, Texture2D[] itemSprites, SoundEffect[] sounds, SpriteFont font, Vector2 screenSize, Vector2 delta) 
        {
            this.characterSprites = characterSprites;
            this.buttonsSprites = buttonsSprites;
            this.itemSprites = itemSprites;
            this.font = font;
            this.screenSize = screenSize;
            this.delta = delta;
            this.sounds = sounds;

            theme = sounds[0].CreateInstance();
            theme.IsLooped = true;
            theme.Volume = 0.4f;
            coin = sounds[1].CreateInstance();
            jump = sounds[2].CreateInstance();

            position = new Vector2(10030, 6000);
            collider = ResetCollider();
            int deltaTmp = screenSize.Y == 720 ? -60 : screenSize.Y == 1080 ? 80 : 0;
            cameraDelta = new Vector2(screenSize.X / 2 - characterSprites[0].Width / 2, screenSize.Y / 2 + deltaTmp );
            camera = new Camera(position - cameraDelta, screenSize, 120);
            camera.ResetDrawArea(120);

            buttons[0] = new ScreenButton(buttonsSprites[0], buttonsSprites[1], new Vector2(60, screenSize.Y - buttonsSprites[0].Height - 174));
            buttons[1] = new ScreenButton(buttonsSprites[2], buttonsSprites[3], new Vector2(180, screenSize.Y - buttonsSprites[0].Height - 50));
            buttons[2] = new ScreenButton(buttonsSprites[4], buttonsSprites[5], new Vector2(screenSize.X - buttonsSprites[0].Width - 60, screenSize.Y - buttonsSprites[0].Height - 50));
            controller = new ScreenButtonController(buttons);
        }

        public bool IsWin()
        {
            return isWin;
        }

        public void Reset()
        {
            cherry = 0;
            icecream = 0;
            baloon = 0;
            position = new Vector2(10030, 6000);
            isWin = false;
        }

        public void Update(GameTime gameTime, MapTile[] tiles, CreepTile[] creeps)
        {
            if (!isWin)
            theme.Play();

            if (position.Y > 6480)
                position.Y = 240;

            talking = false;maping = false;
            answer = controller.Listener();
            curTime2 += gameTime.ElapsedGameTime.Milliseconds;

            if (answer[0] == 1) // Кнопка влево
            {
                curTime += gameTime.ElapsedGameTime.Milliseconds;

                if (direction != 0) // Если персонаж не повёрнут влево
                {
                    direction = 0;
                }

                if (direction == 0)
                    if (curTime > period) // Анимация
                    {
                        curTime = 0;
                        if (curFrame < 2)
                            curFrame = 3;
                        if (curFrame < 6)
                            curFrame++;
                        if (curFrame > 5)
                            curFrame = 2;
                    }
                vectorVelocityX = -11;
            }
            if (answer[0] == 2) // Кнопка вправо
            {
                curTime += gameTime.ElapsedGameTime.Milliseconds;

                if (direction != 1) // Если персонаж не повёрнут вправо
                {
                    direction = 1;
                }
                if (direction == 1)
                    if (curTime > period) // Анимация
                    {
                        curTime = 0;
                        if (curFrame < 8)
                            curFrame = 9;
                        if (curFrame < 12)
                            curFrame++;
                        if (curFrame > 11)
                            curFrame = 8;
                    }
                vectorVelocityX = 11;
            }
            if (answer[0] == 0 && answer[1] == 0) // Если ни стрелки ни А не нажаты
            {
                if (direction == 0)
                    curFrame = 0;
                else
                    curFrame = 6;
            }
            if (answer[1] == 1  && !isAPressed) // А нажата
            {
                if (vectorVelocityY == 0 && position.Y == lastPosition.Y)
                {
                    jump.Play();
                    vectorVelocityY -= 40;
                    isAPressed = true;
                }
            }
            if (position.Y != lastPosition.Y) // Персонаж падает или прыгает
            {
                if (direction == 0)
                    curFrame = 1;
                else
                    curFrame = 7;

                if (curTime2 > period2)
                {
                    curTime2 = 0;
                    if (vectorVelocityX > 0) // гасит скорость по X
                        vectorVelocityX--;

                    if (vectorVelocityX < 0) // гасит скорость по X
                        vectorVelocityX++;
                }
            }
            else
            {
                if (answer[0] == 0) // Персонаж не падает и не прыгает
                {
                    if (direction == 0)
                        curFrame = 0;
                    else
                        curFrame = 6;

                }
            }
            if (answer[1] == 0 && isAPressed) // А отпущена
            {
                isAPressed = false;
            }

            camera.Move(position, lastPosition, cameraDelta, vectorVelocityY, gravity);
            lastPosition = position;

            if (vectorVelocityY < 0) // гасит скорость по Y
                vectorVelocityY++;            

            position.X += vectorVelocityX;
            position.Y += vectorVelocityY + gravity; // действие грвитации
            collider = ResetCollider();
            CheckCollider(tiles, creeps);
            if (talking)
            {
                curTime3 += gameTime.ElapsedGameTime.Milliseconds;
                if (curTime3 > period3)
                {
                    curTime3 = 0;
                    curFrame2++;

                    if (curFrame2 > 2)
                        curFrame2 = 0;
                }
            }
            camera.ResetDrawArea(120);
        }

        public void DrawInreface(SpriteBatch spriteBatch)
        {
            if (talking)
            {
                spriteBatch.Draw(itemSprites[curFrame2], new Vector2(screenSize.X / 2 - 134, screenSize.Y - 170), Color.White);
            }
            if (maping)
            {
                spriteBatch.Draw(itemSprites[6], new Vector2(screenSize.X / 2 - 163, screenSize.Y - 170), Color.White);
            }
            if (cherry > 0)
            {
                for (int i = 0; i < cherry; i++)
                    spriteBatch.Draw(itemSprites[3], new Vector2(5 + i * 40, 5), Color.White);
            }
            if (icecream > 0)
            {
                for (int i = 0; i < icecream; i++)
                    spriteBatch.Draw(itemSprites[4], new Vector2(165 + i * 40, 5), Color.White);
            }
            if (baloon > 0)
            {
                for (int i = 0; i < baloon; i++)
                    spriteBatch.Draw(itemSprites[5], new Vector2(330 + i * 40, 5), Color.White);
            }

            if (!GamePad.GetState(PlayerIndex.One).IsConnected)
                foreach (ScreenButton b in buttons)
                    b.Draw(spriteBatch);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(characterSprites[curFrame], position - camera.GetPosition(), Color.White);        
            
            //spriteBatch.DrawString(font, vectorVelocityX.ToString(), new Vector2(10, 80), Color.Black);
        }

        public int GetVectorVelocityY()
        {
            return vectorVelocityY;
        }        

        public Vector2 GetDelta() // Возвращает положение камеры, смещение спрайтов
        {
            return camera.GetPosition();
        }

        public bool CanDraw(Rectangle collider)
        {
            return camera.GetDrawingArea().Intersects(collider);
        }
        
        private Rectangle ResetCollider()
        {
            return new Rectangle((int)(position.X + delta.X), (int)(position.Y + characterSprites[0].Height - delta.Y), (int)(characterSprites[0].Width - delta.X * 2), (int)delta.Y);
        }

        private void CheckCollider(MapTile[] tiles, CreepTile[] creeps)
        {
            for (int i = 0; i < creeps.Length; i++)
            {
                if (collider.Intersects(creeps[i].GetCollider()) && creeps[i].IsExist())
                {
                    switch (creeps[i].GetTileType())
                    {
                        case 1:
                            jump.Play();
                            if (direction == 0)
                            {
                                vectorVelocityX = +20;
                                vectorVelocityY = -30;
                            }
                            else
                            {
                                vectorVelocityX = -20;
                                vectorVelocityY = -30;
                            }
                            break;
                        case 2:
                            jump.Play();
                            if (direction == 0)
                            {
                                vectorVelocityX = +20;
                                vectorVelocityY = -40;
                            }
                            else
                            {
                                vectorVelocityX = -20;
                                vectorVelocityY = -40;
                            }
                            break;
                        case 3:
                            jump.Play();
                            if (direction == 0)
                            {
                                vectorVelocityX = +20;
                                vectorVelocityY = -30;
                            }
                            else
                            {
                                vectorVelocityX = -20;
                                vectorVelocityY = -30;
                            }
                            break;
                        case 4:
                            jump.Play();
                            if (direction == 0)
                            {
                                vectorVelocityX = +20;
                                vectorVelocityY = -40;
                            }
                            else
                            {
                                vectorVelocityX = -20;
                                vectorVelocityY = -40;
                            }
                            break;
                        case 5:
                            cherry++;
                            coin.Play();
                            creeps[i].Kill();
                            break;
                        case 6:
                            icecream++;
                            coin.Play();
                            creeps[i].Kill();
                            break;
                        case 7:
                            baloon++;
                            coin.Play();
                            creeps[i].Kill();
                            break;
                        case 8:
                            talking = true;
                            if (cherry == 4 && icecream == 4 && baloon == 4)
                            {
                                theme.Stop();
                                isWin = true;
                            }
                            break;
                        case 9:
                            maping = true;
                            break;
                    }
                }
            }
            for (int i = 0; i < tiles.Length; i++)
            {
                if (collider.Intersects(tiles[i].GetCollider()))
                {
                    switch (tiles[i].GetTileType())
                    {
                        case 0:
                                if (lastPosition.Y < position.Y)
                                {
                                    position.Y = tiles[i].GetCollider().Y - characterSprites[0].Height;

                                    if (vectorVelocityX > 0) // гасит скорость по X
                                    vectorVelocityX--;

                                    if (vectorVelocityX < 0) // гасит скорость по X
                                    vectorVelocityX++;
                                }
                            break;
                        case 1:
                                position.X += 11;
                            break;
                        case 2:
                                position.X -=11;
                            break;
                        case 5:
                            if (lastPosition.Y < position.Y)
                            {
                                position.Y = tiles[i].GetCollider().Y - characterSprites[0].Height;
                                if (curTime2 > period2)
                                {
                                    curTime2 = 0;

                                    if (vectorVelocityX > 0) // гасит скорость по X
                                        vectorVelocityX--;                                    

                                    if (vectorVelocityX < 0) // гасит скорость по X
                                        vectorVelocityX++;
                                }
                            }
                            break;
                        case 6:
                            if (lastPosition.Y < position.Y)
                            {
                                if (tiles[i].GetCollider().Y + 10 >= collider.Y)
                                {
                                    position.Y = tiles[i].GetCollider().Y - characterSprites[0].Height;

                                    if (vectorVelocityX > 0) // гасит скорость по X
                                        vectorVelocityX--;

                                    if (vectorVelocityX < 0) // гасит скорость по X
                                        vectorVelocityX++;
                                }
                            }
                            else if (tiles[i].GetCollider().Y <= collider.Y && tiles[i].GetCollider().X + 90 < position.X)
                            {                                
                                position.X += 11;
                            }
                            if (vectorVelocityX < -11)
                                vectorVelocityX = 11;
                            break;
                        case 7:
                            if (lastPosition.Y < position.Y)
                            {
                                if (tiles[i].GetCollider().Y + 10 >= collider.Y)
                                {
                                    position.Y = tiles[i].GetCollider().Y - characterSprites[0].Height;
                                    if (vectorVelocityX > 0) // гасит скорость по X
                                        vectorVelocityX--;

                                    if (vectorVelocityX < 0) // гасит скорость по X
                                        vectorVelocityX++;
                                }
                            }
                            else if (tiles[i].GetCollider().Y <= collider.Y && tiles[i].GetCollider().X - 30 < position.X)
                            {                                
                                position.X -= 11;
                            }
                            if (vectorVelocityX > 11)
                                vectorVelocityX = -11;
                            break;
                    }
                }
            }
            
        }
	}
}
