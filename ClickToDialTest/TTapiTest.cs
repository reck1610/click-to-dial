using System;
using System.Linq;
using JulMar.Tapi3;
using NUnit.Framework;
using FakeItEasy;
using FakeItEasy.Configuration;
using log4net;
using log4net.Config;
using log4net.Core;
using TelProtocolHandler.TestData;

namespace TelProtocolHandler.Tests {
	
	[TestFixture]
	[TestOf( typeof( TTapi ) )]
	[Author( "Matz Reckeweg" )]
	[Description( "Tests " + nameof( TTapi ) )]
	public class TTapiTest {

		private TTapiWrapper     _fakeTapi;
		private Configuration    _fakeConfig;
		private TAddressWrapper _fakeAdress;
		private TCallWrapper _fakeCall;
		private IAnyCallConfigurationWithNoReturnTypeSpecified _aCallToFakeCallConnect;
		private readonly CallEventHandler CallHandler = new CallEventHandler( );

		[OneTimeSetUp]
		protected void SetUpOnce( ) {
			BasicConfigurator.Configure();
			MessageBoxWrapper.UseFake = true;
		}

		[SetUp]
		protected void SetUp() {
			_fakeTapi        = A.Fake<TTapiWrapper>();
			_fakeConfig      = A.Fake<Configuration>();
			_fakeAdress      = A.Fake<TAddressWrapper>();
			_fakeCall        = A.Fake<TCallWrapper>();

			TTapiWrapper.GlobalTapiWrapper = _fakeTapi;
			Configuration.Config           = _fakeConfig;
			

			
			_aCallToFakeCallConnect = A.CallTo( _fakeCall )
			                           .Where( call => call.Method.Name == nameof( _fakeCall.Connect ) );

			_aCallToFakeCallConnect.DoesNothing();
			
			A.CallTo( _fakeTapi )
			 .WithReturnType<TAddressWrapper>()
			 .Returns( _fakeAdress );

			A.CallTo( _fakeTapi )
			 .Where( call => call.Method.Name == nameof( _fakeTapi.Call ) )
			 .CallsBaseMethod( );

			A.CallTo( _fakeAdress )
			 .Where( call => call.Method.Name == nameof( _fakeAdress.CreateCall ) )
			 .WithReturnType<TCallWrapper>()
			 .Returns( _fakeCall );

		}

		[TestCaseSource( typeof( Arguments ) )]
		public void TTapiWrapperCall_Should_InitiateCall( params string[] args ) {

			if (args != null && args.Length > 0 ) {
				string number = CallHandler.NumberToCall( args[0] );
				
				_fakeTapi.Call( number );

				if (args.Length > 0 && args[0] is string a && a.StartsWith( "tel:" )) {
					_aCallToFakeCallConnect.MustHaveHappenedOnceExactly();

				} else {
					_aCallToFakeCallConnect.MustNotHaveHappened();
				}
			}
		}

	}
}