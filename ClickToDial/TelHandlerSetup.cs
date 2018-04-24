using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairManager.ClickToDial {
	public class TelHandlerSetup {
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );
		public static TelHandlerSetup Setup = new TelHandlerSetup();

		public virtual void Run() {
			
			// Start setup.exe from the same folder we're running from.
			System.Diagnostics.Process process = new System.Diagnostics.Process {
				StartInfo = {
					FileName         = "setup.exe",
					Verb             = "runas",
					WindowStyle      = System.Diagnostics.ProcessWindowStyle.Hidden,
					WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
				}
			};
			process.Start();
			Log.Info( "Setup completed." );
		}
	}
}
