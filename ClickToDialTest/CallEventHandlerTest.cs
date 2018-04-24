using System;
using System.Linq;
using JulMar.Tapi3;
using NUnit.Framework;
using FakeItEasy;
using FakeItEasy.Configuration;
using log4net;
using log4net.Config;
using log4net.Core;
using TelProtocolHandler;
using TelProtocolHandler.TestData;

namespace TelProtocolHandler.Tests {
	public static class Program {
		public static void Main( string[] args ) { }

	}
	
	[TestFixture]
	[TestOf( typeof( CallEventHandler ) )]
	[Author( "Matz Reckeweg" )]
	[Description( "Tests " + nameof( CallEventHandler ) )]
	public class CallEventHandlerTest {




		private TTapiWrapper     _fakeTapi;
		private TelHandlerSetup  _fakeSetup;
		private CallEventHandler _fakeCallHandler;
		private Configuration    _fakeConfig;


		[OneTimeSetUp]
		protected void SetUpOnce( ) {
			BasicConfigurator.Configure();
			MessageBoxWrapper.UseFake = true;
		}

		[SetUp]
		protected void SetUp() {
			_fakeTapi        = A.Fake<TTapiWrapper>();
			_fakeCallHandler = A.Fake<CallEventHandler>();
			_fakeSetup       = A.Fake<TelHandlerSetup>();
			_fakeConfig      = A.Fake<Configuration>();

			TTapiWrapper.GlobalTapiWrapper = _fakeTapi;
			TelHandlerSetup.Setup          = _fakeSetup;
			Configuration.Config           = _fakeConfig;
			

			A.CallTo( ( ) => _fakeConfig.Load( ) )
			 .DoesNothing( );

			A.CallTo( () => _fakeCallHandler.CallTapiLineConfiguration() )
			 .DoesNothing();

			A.CallTo( () => _fakeCallHandler.CheckForTapiLineErrors() )
			 .DoesNothing();

			A.CallTo( () => _fakeSetup.Run() )
			 .DoesNothing();

			A.CallTo( _fakeCallHandler )
			 .Where( call => call.Method.Name == nameof( _fakeCallHandler.NumberToCall ) )
			 .CallsBaseMethod( );
			
			A.CallTo( _fakeTapi )
			 .DoesNothing( );

		}
		[TestCaseSource( typeof( Arguments ) )]
		public void CreateCall_Should_OnlyWithoutParameters_RunSetup( params string[] args ) {
			_fakeCallHandler.CreateCall( args );

			if( args != null && args.Length > 0 && args[ 0 ] is string tel ) {
				A.CallTo( () => _fakeSetup.Run() )
				 .MustNotHaveHappened();
			} else {
				A.CallTo( () => _fakeSetup.Run() )
				 .MustHaveHappenedOnceExactly();
			}
		}

		[TestCaseSource( typeof( Arguments ) )]
		public void CreateCall_Should_OnlyWithParameters_LoadConfig( params string[] args ) {
			_fakeCallHandler.CreateCall( args );

			if (args != null && args.Length > 0 && args[0] is string tel) {
				A.CallTo( () => _fakeConfig.Load() )
				 .MustHaveHappenedOnceExactly();
			} else {
				A.CallTo( () => _fakeConfig.Load() )
				 .MustNotHaveHappened();
			}
			
		}

		[TestCaseSource( typeof( Arguments ) )]
		public void CreateCall_Should_OnlyWithParameters_CheckForTapiLineErrors( params string[] args ) {
			_fakeCallHandler.CreateCall( args );

			if (args != null && args.Length > 0 && args[0] is string tel) {
				A.CallTo( () => _fakeCallHandler.CheckForTapiLineErrors() )
				 .MustHaveHappenedOnceExactly();
			} else {
				A.CallTo( () => _fakeCallHandler.CheckForTapiLineErrors() )
				 .MustNotHaveHappened();
			}
		}
	}
}