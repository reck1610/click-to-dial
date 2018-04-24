using System;
using System.Windows.Forms;

namespace FairManager.ClickToDial {
	internal static class Program {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );
		private static CallEventHandler _callEventHandler = new CallEventHandler( );

		[STAThread]
		private static void Main( string[] args ) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );

			log.Info( "============== Application Start ==============" );
			_callEventHandler.CreateCall( args );
		}
	}
}
