/*
 * Created by SharpDevelop.
 * User: Gerald
 * Date: 2015/5/2
 * Time: 19:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace URLFetcher
{
	class Program
	{
		public static void Main(string[] args)
		{
			Fetcher fetcher = new Fetcher();
			Result result = fetcher.fetch("http://www.baidu.com");
			Console.WriteLine(result.status);
			Console.WriteLine("encoding: " + result.encoding);
			Console.WriteLine(result.text);
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}