using JulMar.Tapi3;
using System;
using System.Linq;
using System.Windows.Forms;

namespace TelProtocolHandler {
	// ReSharper disable once InconsistentNaming
	public class TCallWrapper {
		private TCall _call;

		public virtual void Connect( ) => _call.Connect( false );

		public static implicit operator TCallWrapper(TCall call) {
			return new TCallWrapper(  ) {
				_call = call
			};
		}
	}
}