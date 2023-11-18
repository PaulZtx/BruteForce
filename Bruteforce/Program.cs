using System.Diagnostics;
using Bruteforce;

const int symbolsVolume = 26;
var targetList = new List<string>
{
    "1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad",
    "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b",
    "74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f",
};

Console.WriteLine("Выберите строку для брутфорса: ");

for (var i = 0; i < targetList.Count; i++)
{
    Console.WriteLine($"{i}: {targetList[i]}");
}

var choice = Console.ReadLine();
if (!int.TryParse(choice, out var row))
{
    Console.WriteLine("Введите корректное число!");
    return;
}

row = Math.Abs(row) % targetList.Count;

Console.WriteLine("Введите количество потоков:");
var count = Console.ReadLine();

if (!int.TryParse(count, out var result))
{
    Console.WriteLine("Введите корректное число!");
    return;
}

result = Math.Abs(result) % symbolsVolume;

var taskList = new List<Task>();
var brutesList = new List<BruteForce>();
var steps = symbolsVolume / result;

var timer = Stopwatch.StartNew();
timer.Start();

for (var i = 0; i < result; ++i)
{
    var bruteForce = new BruteForce();
    var startIndex = i * steps;
    var stopIndex = i == result - 1 ? startIndex + steps + symbolsVolume % result
        : startIndex + steps ;
    
    brutesList.Add(bruteForce);
    taskList.Add(Task.Factory.StartNew(() => bruteForce.ApplyForce(targetList[row], startIndex, stopIndex)));
}

Task.WaitAll(taskList.ToArray());
timer.Stop();

Console.WriteLine(timer.Elapsed);

Console.WriteLine(brutesList.First(x => x.Value is not null).Value);