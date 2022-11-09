using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage0
{
	partial class Program
	{
		static partial void Welcome0999()
		{
			string name;
			Console.Write("what is your name?");
            name = Console.ReadLine();
			Console.WriteLine($"Your name is{name}");
		}
	}
}
