using System;
using System.Collections.Generic;
using System.Threading;

namespace Data
{
    public abstract class DataLayerAPI
    {
        public static DataLayerAPI CreateData()
        {
            return new DataLayer();
        }

        public abstract void CreateBoard(int height, int width, int numberOfBalls, int radiusOfBalls);
        public abstract void AddBall(double x, double y, double radius, double mass);
        public abstract void StartThreads();
        public abstract void StopAnimation();
        public abstract bool isBallInCoordinates(double x, double y, double radius);
        public abstract BoardApi GetBoard();
        public abstract List<BallApi> GetBalls();

        internal class DataLayer : DataLayerAPI
        {
            private BoardApi _board;
            private List<Thread> _thread;
            private bool _moving = false;
            private object _lock = new object();
            private object _barrier = new object();
            private int _countOfBalls = 0;
            private int _counter = 0;

            public override void CreateBoard(int height, int width, int count, int radius)
            {
                _board = BoardApi.CreateBoard(height, width);
                _thread = new();
                this._countOfBalls = count;
                double mass = 5;

                Random r = new();
                for (int i = 0; i < count; i++)
                {
                    double x;
                    double y;
                    do
                    {
                        x = r.NextDouble() * (width - 2 - 2 * radius) + radius;
                        y = r.NextDouble() * (height - 2 - 2 * radius) + radius;

                    } while (!isBallInCoordinates(x, y, radius));
                    AddBall(x, y, radius, mass);
                }
            }

            public override void AddBall(double x, double y, double radius, double mass)
            {
                BallApi newBall = BallApi.CreateBall(x, y, radius, mass);
                _board.AddBall(newBall);

                Thread t = new Thread(() =>
                {
                    while (_moving)
                    {
                        //critical section
                        lock (_lock)
                        {
                            newBall.MoveBall();
                            while (newBall.IsMoving) { }
                        }

                        //barrier
                        if (Interlocked.CompareExchange(ref _counter, 1, 0) == 0)
                        {
                            Monitor.Enter(_barrier);
                            while (_moving == true && _counter != _countOfBalls ) { }
                            Interlocked.Decrement(ref _counter);
                            Monitor.Exit(_barrier);
                        }
                        else
                        {
                            Interlocked.Increment(ref _counter);
                            Monitor.Enter(_barrier);
                            Interlocked.Decrement(ref _counter);
                            Monitor.Exit(_barrier);
                        }

                        Thread.Sleep(1);
                    }
                });
                _thread.Add(t);
            }

            public override void StartThreads()
            {
                _moving = true;
                foreach (Thread thread in _thread)
                {
                    thread.Start();
                }
            }

            public override void StopAnimation()
            {
                _moving = false;
            }

            public override BoardApi GetBoard()
            {
                return _board;
            }

            public override List<BallApi> GetBalls()
            {
                return _board.GetBalls();
            }

            public override bool isBallInCoordinates(double x, double y, double radius)
            {
                foreach (BallApi other in GetBalls())
                {
                    double distance = Math.Sqrt((x - other.X) * (x - other.X) + (y - other.Y) * (y - other.Y));
                    if (distance <= radius + other.Radius + 1)
                        return false;
                }
                return true;
            }
        }
    }
}