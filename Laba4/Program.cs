using Laba4;

Console.WriteLine("Enter count of philosophers: ");
int countOfPhilosophers = Int32.Parse(Console.ReadLine());

object[] forks = new object[countOfPhilosophers];
for (int i = 0; i < countOfPhilosophers; i++)
{
    forks[i] = new object();
}

Philosopher[] philosophers = new Philosopher[countOfPhilosophers];
Thread[] philosophersThreads = new Thread[countOfPhilosophers];
MyTimer timer = new MyTimer();

for (int i = 0; i < countOfPhilosophers - 1; i++)
{
    philosophers[i] = new Philosopher(((char)('A' + i)).ToString(), forks[(i + 1) % countOfPhilosophers], forks[i], timer);
    philosophersThreads[i] = new Thread(philosophers[i].Eat);
}

int lastIndex = countOfPhilosophers - 1;
philosophers[lastIndex] = new Philosopher(((char)('A' + lastIndex)).ToString(), forks[lastIndex], forks[(lastIndex + 1) % countOfPhilosophers], timer);
philosophersThreads[lastIndex] = new Thread(philosophers[lastIndex].Eat);
timer.Start();

for (int i = 0; i < countOfPhilosophers; i++)
{
    philosophersThreads[i].Start();
}

for (int i = 0; i < countOfPhilosophers; i++)
{
    philosophersThreads[i].Join();
}

for (int i = 0; i < countOfPhilosophers; i++)
{
    var curPhilosopher = philosophers[i];
    Console.WriteLine($@"Philosoher {curPhilosopher.Name} ate for {curPhilosopher.TimeOfEating}");
}