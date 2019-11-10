using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Zybooks_Simulator {
    static class Help {
        public static T Pick<T>(this List<T> l, int index) {
            T s = l[index];
            l.RemoveAt(index);
            return s;
        }
    }
    class CodeGenerator {
        public StringBuilder Source = new StringBuilder();

        public Random r = new Random();
        List<string> variableNames = new List<string>() {
            "foo", "bar", "baz"
        };
        public string TakeVariableName() => variableNames.Pick(r.Next(variableNames.Count));

        List<string> vars_string = new List<string>();
        List<string> vars_int = new List<string>();
        public void DeclareInt() {
            string name = TakeVariableName();
            vars_int.Add(name);
            string line = $@"{name} = {r.Next(100) - 100 / 2}";
            Source.AppendLine(line);
        }
        public void AssignInt() {
            string name = vars_int[r.Next(vars_int.Count)];
            string line = $@"{name} = {r.Next(100) - 100 / 2}";
            Source.AppendLine(line);
        }

        public void Generate(int i) {
            switch(i) {
                case 0:
                    Example();
                    break;
                case 1:
                    Example2();
                    break;
                case 2:
                    Example3();
                    break;
                case 3:
                    Example4();
                    break;
                case 4:
                    Example5();
                    break;
                case 5:
                    Example6();
                    break;
            }
        }
        public void Example6() {
            Source.AppendLine(
$@"def indexOf(array, n):
    for i in range(0, len(array)):
        if array[i] == n:
            return i
    return -1
    
myArray = [{r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}]


for n in [{r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}]:
    index = indexOf(myArray, n)
    if index > -1:
        print(""Found "" + str(n) + "" at index "" + str(index))
    else:
        print(""Could not find "" + str(n))
");
        }
        public void Example5() {
            Source.AppendLine(
$@"def contains(array, n):
    for i in array:
        if i == n:
            return True
    return False
myArray1 = [{r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}]
myArray2 = [{r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}, {r.Next(1, 20)}]

for i in range(1, 10):
    if contains(myArray1, i):
        print(""Array 1 contains "" + str(i))
    elif contains(myArray2, i):
        print(""Array 1 does not contain "" + str(i) + "", but Array 2 does!"")
    else:
        print(""Array 1 does not contain "" + str(i) + "" and neither does Array 2"")"
);
        }
        public void Example4() {
            Source.AppendLine(
$@"def change(amount):
    b = int(amount / 100)
    amount = amount % 100
    q = int(amount / 25)
    amount = amount % 25
    d = int(amount/10)
    amount = amount % 10
    n = int(amount / 5)
    amount = amount % 5
    p = int(amount)
    return {{
        ""dollars"": b,
        ""quarters"": q,
        ""dimes"": d,
        ""nickels"": n,
        ""pennies"": p
    }}
def say(amount):
    c = change(amount)
    print(""I have $"" + str(amount / 100) + "". That's equal to:"")
    print(str(c[""dollars""]) + "" dollars"")
    print(str(c[""quarters""]) + "" quarters"")
    print(str(c[""dimes""]) + "" dimes"")
    print(str(c[""nickels""]) + "" nickels"")
    print(str(c[""pennies""]) + "" pennies"")
say({r.Next(100, 500)})
say({r.Next(100, 500)})
");
        }
        public void Example3() {
            Source.AppendLine(
$@"import math

i = {r.Next(1, 50)}
k = {r.Next(40, 80)};
if i {(r.Next(1) == 1 ? "<" : ">")} {r.Next(5, 25)}:
    k = k + {r.Next(5, 25)}
else:
    k = k - {r.Next(5, 25)}
print(k)
i = i - {r.Next(1, 50)}
if abs(i) {(r.Next(1) == 1 ? "<" : ">")} {r.Next(30, 60)}:
    k = k * 2
else:
    k = int(k / 2)
print(k)
i = abs(i * {r.Next(2, 4)})
if math.sqrt(i) {(r.Next(1) == 1 ? "<" : ">")} {r.Next(5, 10)}:
    k = abs(k)
else:
    k = -abs(k)
print(k)
");
        }
        /*
        public void Example2() {
            Source.AppendLine(
$@"def remix(words):
    words.sort(key = lambda s: s.lower())
    for w in words:
        print(w)
remix([""Mary"", ""had"", ""a"", ""little"", ""lamb""])");
        }
        */
        public void Example2() {
            Source.AppendLine(
$@"def BinarySearch(array, n):
    index = int(len(array) / 2)
    jump = int(len(array) / 4)
    print(""Checking index "" + str(index))
    while(jump > 0):
        if array[index] == n:
            print(""Found "" + str(n) + "" at index "" + str(index))
            return
        elif array[index] > n:
            index = index + jump
        else:
            index = index - jump
        jump = int(jump / 2)
    if array[index] == n:
        print(""Found "" + str(n) + "" at index "" + str(index))
    print(""Could not find "" + str(n))
arr = [{r.Next(1, 5)}, {r.Next(1, 5) + 5}, {r.Next(1, 5) + 10}, {r.Next(1, 5) + 15}, {r.Next(1, 5) + 20}, {r.Next(1, 5) + 25}, {r.Next(1, 5) + 30}]
target = {r.Next(1, 150)}
BinarySearch(arr, target)
");
        }
        public void Example() {
            Source.AppendLine(
$@"def FizzBuzz(n):
    if n % 15 == 0:
        print(""FizzBuzz"")
    elif n % 3 == 0:
        print(""Fizz"")
    elif n % 5 == 0:
        print(""Buzz"")");
            for(int i = 0; i < r.Next(4, 8); i++) {
                Source.AppendLine($@"FizzBuzz({r.Next(1, 50)})");
            }
        }
        /*
        public void Example() {
            Source.AppendLine(
$@"for i in range(1, 60, {2 + r.Next(6)}):
    if i % 15 == 0:
        print(""FizzBuzz"")
    elif i % 3 == 0:
        print(""Fizz"")
    elif i % 5 == 0:
        print(""Buzz"")");
        }
        */
        public static string Range(int start, int end, int inc = 1) {
            return $@"
for i in range({start}, {end}, {inc}):
    print(i)
";
        }
    }
    class Program {
        public static void Main(string[] args) {
            
            
            new Program().Run();
        }
        public void Run() {
            /*
            using (var client = new System.Net.WebClient()) {


                for (int i = 0; i < 10; i++) {
                    for (int j = 0; j < 6; j++) {
                        CodeGenerator g = new CodeGenerator();
                        g.Generate(j);
                        Put(g.Source.ToString(), string.Join('\n', Run(g.Source.ToString(), out double runtime)));
                    }
                }

                void Put(string code, string output) {
                    Console.WriteLine(code);
                    //Console.WriteLine(output);
                    code = code.Replace(@"""", @"\""");
                    Console.WriteLine(code);

                    string s = "";
                    code.ToList().ForEach(c => {
                        if (c == '\r')
                            return;

                        if(c != '\n') {
                            s += c;
                        } else {
                            s += "\\n";
                        }
                    });
                    code = s;
                    output = output.Replace(@"""", @"\""").Replace("\n", @"\n");
                    Console.WriteLine(code);
                    Console.WriteLine(output);

                    
                }
            }
            */

            for (int n = 0; n < 20; n++) {
                CodeGenerator g = new CodeGenerator();
                DateTime start = DateTime.Now;

                g.Generate(g.r.Next(6));
                List<string> output = Run(g.Source.ToString(), out double runtime);
                int line = 0;
                bool incorrect = false;
                int errors = 0;
                while (line != output.Count && errors < 3) {
                    Print();
                    incorrect = false;
                    string s = Console.ReadLine();
                    if (output[line] == s) {
                        line++;
                    } else {
                        incorrect = true;
                        errors++;
                    }
                }
                Print();

                if (incorrect) {
                    Console.WriteLine();
                    Console.WriteLine($"Your output has too many errors!");
                    Console.WriteLine($"Correct output:");
                    Console.WriteLine(string.Join('\n', output));

                    Console.WriteLine($"Verdict: Why do humans have to be so buggy?");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                } else {
                    DateTime end = DateTime.Now;
                    double time = (end - start).TotalSeconds;
                    Console.WriteLine("Done!");
                    Console.WriteLine($"You took {time} seconds");
                    Console.WriteLine($"The computer took {runtime} seconds");

                    double speed = (60 / time) * g.Source.ToString().Count(c => c == '\n');
                    Console.WriteLine($"You computed {speed} lines of code per minute");

                    Console.WriteLine();
                    string[] comments = new string[] {
                    "A computer could not possibly be this slow! You pass the Turing Test!",
                    "You humans seem awfully slow. What Operating System are you using?",
                    "Silly human, your runtime complexity went from n^0 to n^100 real fast",
                    "I can do a million floating point operations per second, how about you?",
                    "Looks like it's another victory for the machines.",
                    "Is your code running? Better go catch it!"
                };
                    Console.WriteLine($"Verdict: {comments[(int)time % comments.Length]}");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                }



                void Print() {
                    Console.Clear();
                    Console.WriteLine($@"Zybooks Exercise {n+1}
");
                    Console.Write(
    $@"Here's some code:
-------------------------------------------------------------
{g.Source.ToString()}-------------------------------------------------------------
What is the output ({output.Count} lines)?
{(incorrect ? "Incorrect!" : "")}
{(line == 0 ? "" : "\n" + string.Join('\n', output.Take(line).Select((s, i) => $"Line {i + 1}: {output[i]}")))}
{(line == output.Count ? "" : $"Line {line + 1}: ")}");
                }
            }
        }
        /*
        public void Run2(string python) {
            var proc = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = @"C:\Users\alexm\AppData\Local\Programs\Python\Python38-32\python.exe",
                    Arguments = "",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            proc.OutputDataReceived += new DataReceivedEventHandler((s, e) => Console.WriteLine(e.Data));
            proc.ErrorDataReceived += new DataReceivedEventHandler((s, e) => Console.WriteLine(e.Data));

            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();

            proc.StandardInput.WriteLine(python);
            proc.StandardInput.WriteLine("quit()");
            proc.StandardInput.WriteLine("aqewserdtrfgyuhjiokaewserdtrfgyuhijo");

            proc.WaitForExit();
        }
        */
        public List<string> Run(string python, out double runtime) {
            File.WriteAllText("temp.py", python);
            var proc = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = @"C:\Users\alexm\AppData\Local\Programs\Python\Python38-32\python.exe",
                    Arguments = "temp.py",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            DateTime start = DateTime.Now;
            proc.Start();
            proc.WaitForExit();
            DateTime end = DateTime.Now;
            runtime = (end - start).TotalSeconds;
            List<string> output = new List<string>();
            while (!proc.StandardOutput.EndOfStream) {
                output.Add(proc.StandardOutput.ReadLine());
            }
            bool error = false;
            while (!proc.StandardError.EndOfStream) {
                error = true;
                Console.WriteLine(proc.StandardError.ReadLine());
            }
            if(error) {
                Environment.Exit(0);
            }
            return output;
        }
    }
}
