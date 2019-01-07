using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSigning
{
	class Program
	{
		static void Main( string[] args )
		{
			string solutionDir = args[0];
			string target = args[1];

			string pfxPassword = File.ReadAllText( Path.Combine( solutionDir, ".certificates\\PFX_PASSWORD" ) ).TrimEnd();
			string pfxPath = Path.Combine( solutionDir, ".certificates/fairmanager-production-codesign.pfx" );

			const string commandSign = @"C:\Program Files (x86)\Windows Kits\10\App Certification Kit\signtool.exe";
			string argsSign = "sign /f \"" + pfxPath + "\" /p \"" + pfxPassword + "\" \"" + target + "\"";
			ProcessStartInfo procSign = new ProcessStartInfo(  ) {
				FileName = commandSign,
				Arguments= argsSign,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false,
				CreateNoWindow=true
			};

			const string commandTimestamp = @"C:\Program Files (x86)\Windows Kits\10\App Certification Kit\signtool.exe";
			string argsTimestamp = "timestamp /t http://timestamp.comodoca.com/authenticode \"" + target + "\"";
			ProcessStartInfo procTimestamp = new ProcessStartInfo() {
				FileName = commandTimestamp,
				Arguments = argsTimestamp,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};

			Process signing = Process.Start( procSign );
			while (!signing.StandardOutput.EndOfStream) {
				string line = signing.StandardOutput.ReadLine();
				Console.WriteLine( line );
			}
			while (!signing.StandardError.EndOfStream) {
				string line = signing.StandardError.ReadLine();
				Console.WriteLine( line );
			}

			Process timestamp = Process.Start( procTimestamp );
			while (!timestamp.StandardOutput.EndOfStream) {
				string line = timestamp.StandardOutput.ReadLine();
				Console.WriteLine( line );
			}
			while (!timestamp.StandardError.EndOfStream) {
				string line = timestamp.StandardError.ReadLine();
				Console.WriteLine( line );
			}


		}
	}
}
