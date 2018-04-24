using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FairManager.ClickToDial.WrapperClasses;

namespace FairManager.ClickToDial {
    public class CallEventHandler {
	    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );
		private static readonly TTapiWrapper Tapi = TTapiWrapper.GetInstance();

        public void CreateCall( params string[] args ) {

            if( args.Length < 1 ) {
	            Log.Info( "Application runs without arguments. Starting setup…" );
				// Set application up as default tel: handler.
				TelHandlerSetup.Setup.Run();
	            CallTapiLineConfiguration();
				return;
            }

            Configuration.Config.Load();
            CheckForTapiLineErrors();

            // Convert input parameters to actual number we want to dial.
            string phoneNumber = NumberToCall( args );
	        Tapi.Call( phoneNumber );
        }

	    public virtual string NumberToCall( params string[] args ) {
            const string protocol = "tel:";

	        if( args.Length < 1 ) {
                Log.Error( "No arguments given." );
		        MessageBoxWrapper.MessageBox.Show( "No arguments given.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return string.Empty;
            }

            string phoneNumber = args[ 0 ];
            if( !phoneNumber.StartsWith( protocol ) ) {
                Log.Error( $"Unexpected input. Expected argument to start with '{protocol}'." );
	            MessageBoxWrapper.MessageBox.Show( $"Unexpected input. Expected argument to start with '{protocol}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return string.Empty;
            }

            phoneNumber = phoneNumber.Substring( protocol.Length );
            // Replace + prefix with a 00
            if( phoneNumber.StartsWith( "+" ) ) {
                phoneNumber = "00" + phoneNumber.Substring( 1 );
            }

            return phoneNumber;
        }

        public virtual void CheckForTapiLineErrors() {
            // If no line is configured, start the configuration.
            if( string.IsNullOrEmpty( Configuration.Config.Container.LineToUse ) ) {
                Log.Warn( "No line configuration value set. Starting settings application…" );
                CallTapiLineConfiguration();
            }

            // If, after configuration, there's still no line, show an error and exit.
            if( string.IsNullOrEmpty( Configuration.Config.Container.LineToUse ) ) {
                Log.Error( "No TAPI line selected!" );
	            MessageBoxWrapper.MessageBox.Show( "No TAPI line configured or none available.", "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                Environment.Exit( 0 );
            }

            // Find the actual TAPI object for the line name.
            string lineToUse = Configuration.Config.Container.LineToUse;
            TAddressWrapper line = Tapi.GetAddress( lineToUse );
            if( null != line ) {
                return;
            }

            Log.Error( $"Unable to find TAPI line with name '{lineToUse}'!" );
            DialogResult reconfigure = MessageBoxWrapper.MessageBox.Show(
                $"Unable to find TAPI line with name '{lineToUse}'!\nDo you wish to select another TAPI line?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error );
            if( reconfigure == DialogResult.Yes ) {
                CallTapiLineConfiguration();
            }
        }

	    public virtual void CallTapiLineConfiguration() {
            SelectTapiForm tapiForm = new SelectTapiForm( Configuration.Config.Container.LineToUse );
            Application.Run( tapiForm );

            // If the form wasn't cancelled, check selected line for errors.
            if( !tapiForm.WasCancelled ) {
                CheckForTapiLineErrors();
            }
        }
    }
}
