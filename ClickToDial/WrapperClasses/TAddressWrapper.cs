using JulMar.Tapi3;
using System;
using System.Linq;
using System.Windows.Forms;

namespace TelProtocolHandler {
	// ReSharper disable once InconsistentNaming
	public class TAddressWrapper {
		private TAddress _tAddress;

		public virtual string AddressName => _tAddress.AddressName;

		public virtual TCallWrapper CreateCall(string phoneNumber) {
			
			return _tAddress.CreateCall( phoneNumber, LINEADDRESSTYPES.PhoneNumber, TAPIMEDIATYPES.AUDIO );
		}

		public static implicit operator TAddressWrapper(TAddress address) {
			return new TAddressWrapper {
				_tAddress = address
			};
		}

	}
}