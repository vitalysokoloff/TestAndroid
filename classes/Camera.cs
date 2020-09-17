using Microsoft.Xna.Framework;

namespace TestAndroid
{
    public class Camera
    {
        private Vector2 position;
        private Rectangle movingArea;
        private Rectangle drawingArea;


        public Camera(Vector2 position, Vector2 screenSize, int tileSize) 
        {
            this.position = position;
            drawingArea = new Rectangle(0, 0, (int)screenSize.X + tileSize * 2, (int)screenSize.Y + tileSize * 2);
        }

        public void Move(Vector2 position, Vector2 lastPosition, Vector2 cameraDelta, int vectorVelosityY, int gravity) // движение камеры 
        {
            this.position.X = position.X - cameraDelta.X;

            if (position.Y - lastPosition.Y < 0) // когда персонаж прыгает вверх
            {
                this.position.Y += vectorVelosityY / 4;
            }

            if (position.Y - lastPosition.Y > 0 && this.position.Y < position.Y - cameraDelta.Y) // когда персонаж падает вниз
            {
                this.position.Y -= vectorVelosityY - gravity;
            }

            if (position.Y - lastPosition.Y == 0 && vectorVelosityY == 0) // стабилизаия типо
            {
                if (this.position.Y > position.Y - cameraDelta.Y)
                this.position.Y -= 20;
                if (this.position.Y < position.Y - cameraDelta.Y)
                    this.position.Y += 20;
            }           
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Rectangle GetMovingArea()
        {
            return movingArea;
        }

        public Rectangle GetDrawingArea()
        {
            return drawingArea;
        }

        public void ResetDrawArea(int tileSize)
        {
            drawingArea.X = (int)position.X - tileSize;
            drawingArea.Y = (int)position.Y - tileSize;
        }
    }
}