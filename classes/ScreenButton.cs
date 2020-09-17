using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestAndroid
{
    public class ScreenButton
    {
        private int up;
        private int down;
        private int left;
        private int right;

        private bool isPressed = false;
        private Texture2D sprite;
        private Texture2D upState;
        private Texture2D downState;
        
        public ScreenButton(Texture2D upState, Texture2D downState, Vector2 position)
        {
            this.upState = upState;
            this.downState = downState;
            sprite = upState;

            up = (int)position.Y;
            down = (int)position.Y + downState.Height;
            left = (int)position.X;
            right = (int)position.X + downState.Width;
        } 

        public void SetDown()
        {
            isPressed = true;
            sprite = downState;
        } // Устанавливает состояние кнопки

        public void SetUp()
        {
            isPressed = false;
            sprite = upState;
        } // Устанавливает состояние кнопки

        public bool GetState()
        {
            return isPressed;
        } // Возвращает состояние кнопки

        public bool Crossing(Vector2 position)
        {
            if (position.X > left && position.X < right && position.Y > up && position.Y < down)
                return true;
            else
                return false;
        } // Проеверяет пересечение с кнопкой / для проверки нажатия на кнопку

        public void Draw(SpriteBatch spriteBatch) // Отрисовка кнопки
        {
            spriteBatch.Draw(sprite, new Vector2(left,up), Color.White);
        } 
    } 
}