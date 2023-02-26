using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
namespace learning;
public class Fff
{
    int d;
    public static int y;
  
    public Fff()
    {
        y = 6;
    }
    public void sen() { }
    public int ffeef() { return 2; }

}

class MyClass<T1, T2>where T2:IEnumerable<T2>,new() where T1 : new()
{
    T1[] a;
    T2 b;
    public MyClass()
    {
        foreach (var item in a)
        {
            T1 ta = new T1();
            Console.WriteLine(ta);
        }
        foreach (var item in b)
        {
            T2 y = new();
            Console.WriteLine(item);
        }
    }
}
static internal class Tttt
{
    static  void Hh(this int t, int x)
    {
        t = 8;
    }
}

public static class Example
{
    public static string NewMethod( this int  obj)
    {
        return obj.ToString();
    }
}

internal static class Program
{
    public struct MyClass
    {

        public int A;
        public int B;
        public int C;
        public MyClass dd()
        {
            var dd = new MyClass { A = 1 };
            return dd;
        }
    }



    public struct A { public int a; }
    public class B { public int h<T>(T g) { return 3; } public char h(int g) { return 't'; } }

    class C
    {
        static void foo(string g) {

        }
        Action<string> dd = foo;
        public new string ToString() { return "!!!"; }
    }
    public interface iff
    {
        public void sen() { }
    }
    public class Ab
    {
      public  string rrr;
        public  void MyFunc(string s, ref int len)
        {
          
            len= 0; 
        }
        public IEnumerable<int> rr()
        {
            yield return 1;
        }
    }
    class rect
    {
        public rect() { Console.WriteLine("pp"); }
        public rect(rect t) { Console.WriteLine("pp"); }
        public static rect getRect { get { return new rect(); } }
    }
    public class A<T,U> where T :new(){ }

    private static void Main(string[] args)
    {
        var arr = new[]
{
new {name="name1",type=4,cost=50},
new {name="name2",type=3,cost=82},
new {name="name3",type=4,cost=60},
new {name="nem4",type=2,cost=12},
};
        Console.WriteLine(arr.Where(v => v.type == 4).Average(v => v.cost));
        rect r = new rect();
        rect r2 =new rect( r);
        rect r3 = rect.getRect;
        rect r4 = r3;
        double a = 12, b = 6;
        double c = a / b;
        double d = (double)a / (b);
        Console.WriteLine("c = {1}, d = {0}", c, d);
        Console.WriteLine("hekk");
        Console.WriteLine("{1,5}", "123", "2", "333333");
        Console.WriteLine("{1}{0,5}{2,5}", "123", "2", "333333");
        Console.WriteLine("{1}{0,-5}{2,-5}", "123", "2", "333333");
        Console.WriteLine("{1}{0,-5}{0,-1}", "123", "2", "333333");
        Ab ss = new() { rrr = "" };
        Ab bb = ss;
        Console.WriteLine(ss);

        ss.rrr = "";
;        //    double[] eere = { 1, 2 };
        //    int[] xdd = { 2, 33 };
        //    xdd.min();
        //    B yyy=new B();
        //   var r= yyy.h(1);
        //    Console.WriteLine(r);
        //    List<int> list = new List<int>() { 1,2};
        //    List<int> list2 = list.Where(x => x==2).ToList();

        //    list[0] = 2;
        //    foreach (int x in list2)
        //        Console.WriteLine("ff");
    }
   
}