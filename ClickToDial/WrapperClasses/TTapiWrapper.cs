using JulMar.Tapi3;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FairManager.ClickToDial.WrapperClasses {
	public class TTapiWrapper {
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

		private readonly TTapi _tapi = new TTapi();
		public static TTapiWrapper GlobalTapiWrapper = new TTapiWrapper(  );
		public int DeviceCount { get; private set; }

		public virtual TAddressWrapper[] GetAddresses() => _tapi.Addresses.Cast<TAddressWrapper>( ).ToArray();

		public virtual TAddressWrapper GetAddress( string addressName ) => GetAddresses().SingleOrDefault( a => a.AddressName == addressName );

		public virtual TAddressWrapper GetAddress( ) {
			string lineToUse = Configuration.Config.Container.LineToUse;
			TAddressWrapper line = GetAddress( lineToUse );
			if (line is null) {
				Log.Error( $"unable to find line '{lineToUse}'" );
				return null;
			}

			return line;
		}

		public int Initialize( ) {
			DeviceCount = _tapi.Initialize();
			return DeviceCount;
		}

		public static TTapiWrapper GetInstance( ) {
			return GlobalTapiWrapper ?? ( GlobalTapiWrapper = new TTapiWrapper( ) );
		}

		public virtual void Call( string phoneNumber ) {
			if (string.IsNullOrEmpty( phoneNumber )) {
				return;
			}

			TAddressWrapper line = GetInstance().GetAddress( );
			Log.Info( $"Creating call via line '{line.AddressName}'." );

			// Always assumes 0 prefix is needed to dial out.
			TCallWrapper call = line.CreateCall( "0"+phoneNumber );
			try {
				call.Connect( );
			} catch (TapiException ex) {
				Log.Error( "TapiException: ", ex );
				return;
			}

			Log.Info( $"Calling '{phoneNumber}'..." );
		}

	}
}
