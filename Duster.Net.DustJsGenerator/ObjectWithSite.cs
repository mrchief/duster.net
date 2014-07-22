using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;

namespace Duster.Net
{
	[ComVisible(true)]
	public class ObjectWithSite : IObjectWithSite
	{
		private object site;

		/// <param name="pUnkSite"></param>
		public void SetSite(object pUnkSite)
		{
			// Save away the site object for later use
			site = pUnkSite;
		}

		/// <param name="riid"></param><param name="ppvSite"></param>
		public void GetSite(ref Guid riid, out IntPtr ppvSite)
		{
			if (site == null)
				Marshal.ThrowExceptionForHR(VSConstants.E_NOINTERFACE);

			// Query for the interface using the site object initially passed to the generator
			var punk = Marshal.GetIUnknownForObject(site);
			var hr = Marshal.QueryInterface(punk, ref riid, out ppvSite);
			Marshal.Release(punk);
			ErrorHandler.ThrowOnFailure(hr);
		}
	}
}