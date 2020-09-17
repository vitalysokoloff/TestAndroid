using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace TestAndroid
{
    public class StartButtonController
    {
        ScreenButton button;

        public StartButtonController(ScreenButton button) 
        {
            this.button = button;
        }

        public int Listener()
        {
            int answer = 0;

            if (GamePad.GetState(PlayerIndex.One).IsConnected) // для XBox геймпада
            {
                answer = GamePad.GetState(PlayerIndex.One).Buttons.Start != ButtonState.Pressed ? 0 : 1;
            }
            else
            {
                TouchCollection touches = TouchPanel.GetState(); // Массив косаний к экрану /  *далее используем первые два
                if (touches.Count > 0) // Проверка что массив заполнен значениями
                {
                    if (button.Crossing(touches[0].Position))
                        answer = 1;
                }
            }

            if (answer == 0)
                button.SetUp();
            else
                button.SetDown();
                return answer;
        }
    }
}