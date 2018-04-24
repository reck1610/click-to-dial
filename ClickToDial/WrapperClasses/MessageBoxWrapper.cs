using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FakeItEasy;
using FakeItEasy.Configuration;

namespace TelProtocolHandler {
	public class MessageBoxWrapper {

		public static           MessageBoxWrapper MessageBox => UseFake ? FakeMessageBox : RealMessageBox;
		private static readonly MessageBoxWrapper RealMessageBox;
		private static readonly MessageBoxWrapper FakeMessageBox;
		public static           bool              UseFake = false;

		static MessageBoxWrapper( ) {
			RealMessageBox = new MessageBoxWrapper();
			FakeMessageBox = A.Fake<MessageBoxWrapper>();
			A.CallTo( FakeMessageBox )
			 .Where( call => call.Method.Name == nameof( FakeMessageBox.Show ) )
			 .Invokes(
				 call => {
					 int indexOfText = call.Arguments.ArgumentNames.ToList().IndexOf( "text" );
					 Console.WriteLine( $"[MessageBox]: {call.Arguments[indexOfText]}" );
				 } );
		}

		public virtual DialogResult Show(IWin32Window owner, string text) => System.Windows.Forms.MessageBox.Show( owner, text );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption) => System.Windows.Forms.MessageBox.Show( owner, text, caption );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons) => System.Windows.Forms.MessageBox.Show( owner, text, caption, buttons );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) => System.Windows.Forms.MessageBox.Show( owner, text, caption, buttons, icon );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton) => System.Windows.Forms.MessageBox.Show( owner, text, caption, buttons, icon, defaultButton );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options) => System.Windows.Forms.MessageBox.Show( owner, text, caption, buttons, icon, defaultButton, options );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath) => System.Windows.Forms.MessageBox.Show( owner, text, caption, buttons, icon, defaultButton, options, helpFilePath );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword) => System.Windows.Forms.MessageBox.Show( owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator) => System.Windows.Forms.MessageBox.Show( owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator );
		public virtual DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param) => System.Windows.Forms.MessageBox.Show( owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param );
		
		public virtual DialogResult Show(string text) => System.Windows.Forms.MessageBox.Show( text );
		public virtual DialogResult Show(string text, string caption) => System.Windows.Forms.MessageBox.Show( text, caption );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons) => System.Windows.Forms.MessageBox.Show( text, caption, buttons );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon) => System.Windows.Forms.MessageBox.Show( text, caption, buttons, icon );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton) => System.Windows.Forms.MessageBox.Show( text, caption, buttons, icon, defaultButton );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options) => System.Windows.Forms.MessageBox.Show( text, caption, buttons, icon, defaultButton, options );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton) => System.Windows.Forms.MessageBox.Show( text, caption, buttons, icon, defaultButton, options, displayHelpButton );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath) => System.Windows.Forms.MessageBox.Show( text, caption, buttons, icon, defaultButton, options, helpFilePath );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword) => System.Windows.Forms.MessageBox.Show( text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator) => System.Windows.Forms.MessageBox.Show( text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator );
		public virtual DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param) => System.Windows.Forms.MessageBox.Show( text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param );


		public static IAnyCallConfigurationWithNoReturnTypeSpecified GetShowFakeCalls( ) {
			return A.CallTo( FakeMessageBox )
			        .Where( call => call.Method.Name == nameof( FakeMessageBox.Show ) );

		}


	}
}
