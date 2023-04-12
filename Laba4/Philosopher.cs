namespace Laba4
{
    internal sealed class Philosopher
    {
        private object firstFork;
        private object secondFork;
        Random rand = new ();
        private MyTimer timer;

        public Philosopher(string name, object fork1, object fork2, MyTimer timer)
        {
            firstFork = fork1;
            secondFork = fork2;
            Name = name;
            TimeOfEating = 0;
            this.timer = timer;
        }

        public string Name { get; init; }
        public long TimeOfEating { get; private set; }

        public void Eat()
        {
            while (timer.CanEat())
            {
                if (Monitor.TryEnter(firstFork, ConstantsTime.MaxWaitingTime))
                {
                    Console.WriteLine($@"Philosopher {Name} is getting right fork");
                    if (Monitor.TryEnter(secondFork, ConstantsTime.MaxWaitingTime))
                    {
                        Console.WriteLine($@"Philosopher {Name} is getting left fork and eating");
                        var eatTime = rand.Next(ConstantsTime.MaxEatingTime);
                        Thread.Sleep(eatTime);
                        TimeOfEating += eatTime;
                        Console.WriteLine($@"Philosopher {Name} is putting left fork down");
                        Monitor.Exit(secondFork);
                    }
                    else
                    {
                        Console.WriteLine($@"Philosopher {Name} can't get left fork");
                    }

                    Console.WriteLine($@"Philosopher {Name} is putting right fork down");
                    Monitor.Exit(firstFork);
                }
                else
                {
                    Think();
                }
            }
        }

        public void Think()
        {
            Console.WriteLine($@"Philosopher {Name} is thinking");
            Thread.Sleep(rand.Next(ConstantsTime.MaxThinkingTime));
        }
    }
}
