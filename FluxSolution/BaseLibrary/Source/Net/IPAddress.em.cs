using System.Linq;

namespace Flux
{
	public static partial class SystemNetEm
	{
		/// <summary>Create a new sequence of the IP address./summary>
		/// <see cref="https://en.wikipedia.org/wiki/IPv6"/>
		public static System.Collections.Generic.IEnumerable<int> GetAddressWords(this System.Net.IPAddress source)
		{
			var bytes = (source ?? throw new System.ArgumentNullException(nameof(source))).GetAddressBytes();

			for (var index = 0; index < bytes.Length; index += 2)
				yield return bytes[index] << 8 | bytes[index + 1];
		}

		/// <summary></summary>
		/// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
		public static System.Net.IPAddress GetBroadcastAddress(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (subnetMask is null) throw new System.ArgumentNullException(nameof(subnetMask));

			var sourceBytes = source.GetAddressBytes();
			var subnetMaskBytes = subnetMask.GetAddressBytes();

			if (sourceBytes.Length != subnetMaskBytes.Length) throw new System.ArgumentException(@"Incompatible source address and subnet mask.");

			var broadcastAddress = new byte[sourceBytes.Length];
			for (var i = 0; i < broadcastAddress.Length; i++)
				broadcastAddress[i] = (byte)(sourceBytes[i] | (subnetMaskBytes[i] ^ 255));
			return new System.Net.IPAddress(broadcastAddress);
		}

		public static System.Numerics.BigInteger GetMaxValue()
			=> System.Numerics.BigInteger.Parse(@"340282366920938463463374607431768211456", System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture);
		public static System.Numerics.BigInteger GetMinValue()
			=> System.Numerics.BigInteger.Zero;

		/// <summary></summary>
		/// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
		public static System.Net.IPAddress GetNetworkAddress(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (subnetMask is null) throw new System.ArgumentNullException(nameof(subnetMask));

			var sourceBytes = source.GetAddressBytes();
			var subnetMaskBytes = subnetMask.GetAddressBytes();

			if (sourceBytes.Length != subnetMaskBytes.Length) throw new System.ArgumentException(@"Incompatible source address and subnet mask.");

			var networkAddress = new byte[sourceBytes.Length];
			for (var i = 0; i < networkAddress.Length; i++)
				networkAddress[i] = (byte)(sourceBytes[i] & (subnetMaskBytes[i]));
			return new System.Net.IPAddress(networkAddress);
		}

		/// <summary>Creates a new sequence of IP addresses between the source and target (inclusive).</summary>
		public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetRange(this System.Net.IPAddress source, System.Net.IPAddress target)
		{
			var sourceBi = ToBigInteger(source);
			var targetBi = ToBigInteger(target);

			for (var ipBi = sourceBi; ipBi < targetBi; ipBi++)
				yield return ToIPAddress(ipBi);

			yield return ToIPAddress(targetBi);
		}

		/// <summary>Returns whether the address is between the specified minimum and maximum (inclusive).</summary>
		public static bool IsBetween(this System.Net.IPAddress source, System.Net.IPAddress min, System.Net.IPAddress max)
			=> source.ToBigInteger() is var bi && bi >= min.ToBigInteger() && bi <= max.ToBigInteger();

		/// <summary>Determintes whether the address is a multicast address (IPv4 or IPv6).</summary>
		public static bool IsMulticast(this System.Net.IPAddress source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			if (source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				return source.GetAddressBytes() is var bytes && (bytes[0] & 0xF0) == 0xE0;

			if (source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
				return source.GetAddressBytes() is var bytes && bytes[0] == 0xFF;

			return false;
		}

		/// <summary>Determines whether two addresses are in the same subnet.</summary>
		/// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
		public static bool InSameSubnet(this System.Net.IPAddress source, System.Net.IPAddress other, System.Net.IPAddress subnetMask)
			=> source.GetNetworkAddress(subnetMask).Equals(other.GetNetworkAddress(subnetMask));

		/// <summary>Convert the IP address to a BigInteger.</summary>
		public static System.Numerics.BigInteger ToBigInteger(this System.Net.IPAddress source)
			=> (source ?? throw new System.ArgumentNullException(nameof(source))).GetAddressBytes().AsEnumerable().Reverse().ToArray().ToBigInteger();

		/// <summary>Convert the BigInteger to an IP address.</summary>
		public static System.Net.IPAddress ToIPAddress(this System.Numerics.BigInteger source)
		{
			if (source < 0 || source > GetMaxValue()) throw new System.ArgumentOutOfRangeException(nameof(source));

			var byteArray = source.ToByteArrayEx(out var _);
			if(byteArray.Length < 4)
				System.Array.Resize(ref byteArray, 4);
			System.Array.Reverse(byteArray);
			return new System.Net.IPAddress(byteArray);
		}
	}

	public static partial class Network
	{
		public static readonly System.Net.IPEndPoint RemoteTest = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(@"4.3.2.1"), 4321);

		public static bool TryGetLocalIPAddressByUdp(out System.Net.IPAddress result)
		{
			try
			{
				using var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0);

				socket.Connect(RemoteTest);

				if (socket.LocalEndPoint is null)
					throw new System.NullReferenceException();

				result = ((System.Net.IPEndPoint)socket.LocalEndPoint).Address;
				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }

			result = default!;
			return false;
		}
	}
}
