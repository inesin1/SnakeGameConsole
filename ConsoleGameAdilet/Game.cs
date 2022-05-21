using System;

namespace ConsoleGameAdilet
{
    internal class Game
    {
        //Игровые объекты
        private Map _map; //Игровая карта
        private Player _player; //Игрок
        private Apple _apple; //Яблоко

        private char[,] _screenBuffer; //Буфер экрана
        private int _screenWidth = 16; //Ширина экрана
        private int _screenHeight = 16; //Высота экрана

        private ConsoleKeyInfo _key; //Состояние о нажатой клавише

        public Game()
        {
            Initialize();

            Console.CursorVisible = false; //Отключаем курсор
        }

        //Запуск игры
        public void Run()
        {
            long startTime = DateTime.Now.Ticks; //Время старта таймера
            long elapsedTime = DateTime.Now.Ticks - startTime; //Разница между текущим временем и временем когда запустили таймер
            double elapsedSeconds = new TimeSpan(elapsedTime).TotalSeconds; //Тоже самое, что выше, но в секундах, а не в миллисекундах

            //Игровой цикл
            while (true)
            {
                //Обновляем все объекты
                Update();

                //Если прошла секунда
                if (elapsedSeconds >= 1)
                {
                    startTime = DateTime.Now.Ticks; //Обнуляем таймер

                    //Отрисовываем все объекты игры
                    Draw();
                }

                //Обновляем значения времени
                elapsedTime = DateTime.Now.Ticks - startTime;
                elapsedSeconds = new TimeSpan(elapsedTime).TotalSeconds;
            }
        }

        //Инициализация
        public void Initialize()
        {
            _screenBuffer = new char[_screenHeight, _screenWidth];
            _map = new Map(
                    16, 16,
                    new char[,]{
                        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','.','.','.','.','.','.','.','.','.','.','.','.','.','.','#'},
                        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
                        }
                    );

            _player = new Player(3, 3, 'P');
            _apple = new Apple(5, 5, 'A');
        }

        //Обновление
        public void Update()
        {
            //Обновляем карту на экране
            for (int y = 0; y < _screenHeight; y++)
            {
                for (int x = 0; x < _screenWidth; x++)
                {
                    _screenBuffer[y,x] = _map.Matrix[y,x];
                }
            }

            //Обновляем позицию игрока на экране
            _screenBuffer[_player.Y,_player.X] = _player.Sym;

            //Управление
            Input();
        }

        //Отрисовка
        public void Draw()
        {
            Console.Clear(); //Очистка консоли

            for (int y = 0; y < _screenHeight; y++)
            {
                for (int x = 0; x < _screenWidth; x++)
                {
                    if (_screenBuffer[y, x] == 'A') Console.BackgroundColor = ConsoleColor.Red;
                    else Console.ResetColor();
                    Console.Write(_screenBuffer[y,x]);
                    Console.ResetColor();
                    Console.Write("  ");
                }
                Console.WriteLine();
            }

            SpawnApple();
        }

        /// <summary>
        /// Управление персонажем
        /// </summary>
        public void Input()
        {
            if (Console.KeyAvailable)
            {
                _key = Console.ReadKey();

                //Влево
                if (_key.Key == ConsoleKey.LeftArrow & _screenBuffer[_player.Y, _player.X - 1] == '.')
                    _player.X--;
                //Вправо
                if (_key.Key == ConsoleKey.RightArrow & _screenBuffer[_player.Y, _player.X + 1] == '.')
                    _player.X++;
                //Вверх
                if (_key.Key == ConsoleKey.UpArrow & _screenBuffer[_player.Y - 1, _player.X] == '.')
                    _player.Y--;
                //Вниз
                if (_key.Key == ConsoleKey.DownArrow & _screenBuffer[_player.Y + 1, _player.X] == '.')
                    _player.Y++;
            }
        }

        /// <summary>
        /// Генерация яблока в случайном месте
        /// </summary>
        public void SpawnApple()
        {
            //Вычисляем координаты яблока, используя рандом
            Random random = new Random();
            int x = random.Next(0, _map.Width);
            int y = random.Next(0, _map.Height);

            Console.WriteLine(x);
            Console.WriteLine(y);
        }
    }
}
