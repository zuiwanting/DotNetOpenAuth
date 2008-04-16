﻿using System;
using System.Collections.Generic;
using System.Text;
using DotNetOpenId.RelyingParty;
using System.Globalization;

namespace DotNetOpenId.Extensions {
	/// <summary>
	/// The Attribute Exchange Store message, response leg.
	/// </summary>
	public class AttributeExchangeStoreResponse : IExtensionResponse {
		readonly string Mode = "store_response";
		
		/// <summary>
		/// Reads a Provider's response for Attribute Exchange values and returns
		/// an instance of this struct with the values.
		/// </summary>
		public static AttributeExchangeStoreResponse ReadFromResponse(IAuthenticationResponse response) {
			var obj = new AttributeExchangeStoreResponse();
			return ((IExtensionResponse)obj).ReadFromResponse(response) ? obj : null;
		}

		#region IExtensionResponse Members
		string IExtensionResponse.TypeUri { get { return Constants.ae.ns; } }

		public void AddToResponse(Provider.IRequest authenticationRequest) {
			var fields = new Dictionary<string, string> {
				{ "mode", Mode },
			};
			authenticationRequest.AddExtensionArguments(Constants.ae.ns, fields);
		}

		bool IExtensionResponse.ReadFromResponse(IAuthenticationResponse response) {
			var fields = response.GetExtensionArguments(Constants.ae.ns);
			if (fields == null) return false;
			string mode;
			fields.TryGetValue("mode", out mode);
			if (mode != Mode) return false;

			return true;
		}

		#endregion
	}
}
