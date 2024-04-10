using System.Diagnostics;
using System.Text.Json;

int taskCount = 100000;
var watch = new Stopwatch();
double[] time = [0.0, 0.0, 0.0];
List<Task> tasks = [];

watch.Start();
Parallel.For(0, taskCount, (i) =>
{
    ;
    //Console.WriteLine($"{i:000000} {Environment.CurrentManagedThreadId:000}");
});
watch.Stop();
time[0] = watch.ElapsedMilliseconds / 1000.0;


for (int i = 0; i < taskCount; i++)
{
    tasks.Add(Task.Run(() =>/* Console.WriteLine($"{i:000000} {Environment.CurrentManagedThreadId:000}")*/
    {
        ;
    }));
}
watch.Reset();
watch.Start();
Task.WaitAll([.. tasks]);
watch.Stop();

time[1] = watch.ElapsedMilliseconds / 1000.0;

watch.Reset();
watch.Start();
for (int i = 0; i < taskCount; i++)
{
    ;   //Console.WriteLine($"{i:000000} {Environment.CurrentManagedThreadId:000}");
}
watch.Stop();
time[2] = watch.ElapsedMilliseconds / 1000.0;


Console.WriteLine(JsonSerializer.Serialize(time));
