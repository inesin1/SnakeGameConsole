﻿using System;
using System.Collections.Generic;

namespace ConsoleGameAdilet
{
    internal class Game
    {
        //Игровые объекты
        private Map _map; //Игровая карта
        private Player _player; //Игрок
        private Apple _apple; //Яблоко
        private List<PlayerClone> _clones; //Список клонов

        private char[,] _screenBuffer; //Буфер экрана
        private int _screenWidth = 16; //Ширина экрана
        private int _screenHeight = 16; //Высота экрана

        private ConsoleKeyInfo _key; //Состояние о нажатой клавише

        private Random _random = new Random(); //Рандом

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

                    //Обновление
                    UpdateSecond();

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

            _player = new Player();
            _apple = new Apple();
            _clones = new List<PlayerClone>();
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

            _screenBuffer[_apple.Y, _apple.X] = _apple.Sym;

            //Управление
            Input();
        }

        /// <summary>
        /// Обновление раз в секунду
        /// </summary>
        public void UpdateSecond()
        {
            MovePlayer();
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
            //Ввод с клавиатуры и смена направления
            if (Console.KeyAvailable)
            {
                _key = Console.ReadKey();

                //Влево
                if (_key.Key == ConsoleKey.LeftArrow)
                    _player.Dir = Player.Direction.Left;
                //Вправо
                if (_key.Key == ConsoleKey.RightArrow)
                    _player.Dir = Player.Direction.Right;
                //Вверх
                if (_key.Key == ConsoleKey.UpArrow)
                    _player.Dir = Player.Direction.Up;
                //Вниз
                if (_key.Key == ConsoleKey.DownArrow)
                    _player.Dir = Player.Direction.Down;
            }
        }

        /// <summary>
        /// Движение пермонажа
        /// </summary>
        public void MovePlayer()
        {
            switch (_player.Dir)
            {
                case Player.Direction.Left: _player.X--; break;
                case Player.Direction.Right: _player.X++; break;
                case Player.Direction.Up: _player.Y--; break;
                case Player.Direction.Down: _player.Y++; break;
            }
        }

        /// <summary>
        /// Генерация яблока в случайном месте
        /// </summary>
        public void SpawnApple()
        {
            //Вычисляем координаты яблока, используя рандом
            int x = _random.Next(0, _map.Width);
            int y = _random.Next(0, _map.Height);

            //Если попали на стенку запускаем метод еще раз
            if (_map.Matrix[y, x] != '.') {SpawnApple(); return;}

            //Меняем координаты яблока на вычисленные
            _apple.X = x;
            _apple.Y = y;
        }
    }
}
