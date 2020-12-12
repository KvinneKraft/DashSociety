using System;

namespace DashCore
{
    public class Program
    {
	[STAThread]
	static void Main(string[] args)
	{
	    var cache = DStream.GetProperties("C:\\Windows\\System32\\cmd.exe", false);

	    // I had to code an entire class just because .NET is annoying.
	    
	    foreach (DStream.Properties obj in Enum.GetValues(typeof(DStream.Properties)))
		Console.WriteLine(DStream.GetRawValue(cache, obj));
	    
	    // Not annoying anymore though ;)

	    Console.ReadKey();

	    /*using (var save = new DashCore.SaveFileDialog())
	    {
		save.ShowDialog();
	    };*/
	}
    }
}
