using System;
using System.Windows.Forms;
using FairManager.ClickToDial.WrapperClasses;

namespace FairManager.ClickToDial {
    public partial class SelectTapiForm : Form {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        /// <summary>
        /// Was the cancel button clicked?
        /// </summary>
        public bool WasCancelled { get; private set; }

        public SelectTapiForm( string lineToUse = "" ) {
            InitializeComponent();

            WasCancelled = false;

            log.Info( "Searching for TAPI devices…" );
	        TTapiWrapper tapi = TTapiWrapper.GetInstance();
	        int foundDevices = tapi.DeviceCount;

            log.Info( $"{foundDevices} devices found" );
            if( 0 == foundDevices ) {
	            MessageBoxWrapper.MessageBox.Show( "No available TAPI lines found.", "TAPI Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                Close();

            } else {
                foreach( TAddressWrapper addr in tapi.GetAddresses() ) {
                    log.Info( $"\t{addr.AddressName}" );
                    tapiSelectBox.Items.Add( addr.AddressName );
                    // If this is the previously selected line, select it in the UI as well.
                    if( addr.AddressName == lineToUse ) {
                        tapiSelectBox.SelectedItem = addr.AddressName;
                    }
                }
                // Select first item if nothing else is selected.
                if( tapiSelectBox.SelectedIndex < 0 ) {
                    tapiSelectBox.SelectedIndex = 0;
                }
            }
        }

        private void OkButtonClick( object sender, EventArgs e ) {
            Configuration.Config.Container.LineToUse = tapiSelectBox.SelectedItem.ToString();
            Configuration.Config.Save();

            Close();
        }

        private void CancelButtonClick( object sender, EventArgs e ) {
            WasCancelled = true;
            Close();
        }
    }
}
