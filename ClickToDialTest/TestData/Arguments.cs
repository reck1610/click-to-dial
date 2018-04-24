using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FairManager.ClickToDial.Test.TestData {
	public class Arguments : IEnumerable {

		public IEnumerator GetEnumerator() {
			
			string[][] parametersArray = new[] {
				new string[]{},
				new[] { "tel:123123123123" },
				new[] { "235235223524" },
				new[] { "" },
				new[] { "tel:23412312323422" }
			};

			foreach( string[] parameters in parametersArray) {

				string arguments = string.Join( ", ", parameters.Select( x => $"'{x}'" ) );

				yield return new TestCaseData( new object[]{ parameters} ).SetName( $"{{m}} :   \t [ {arguments} ]" );
			}
		}
	}
}
