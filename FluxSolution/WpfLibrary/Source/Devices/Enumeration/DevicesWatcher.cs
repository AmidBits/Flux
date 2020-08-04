#if WINDOWS_UWP
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Flux
{
	public class DeviceWatcher
	{
		private string _deviceSelector;
		private Windows.Devices.Enumeration.DeviceWatcher _deviceWatcher;
		private bool _enumerationCompleted;

		public Windows.Devices.Enumeration.DeviceInformationCollection DeviceInformationCollection { get; private set; }

		public DeviceWatcher(string deviceSelector)
		{
			_deviceSelector = deviceSelector;

			_deviceWatcher = Windows.Devices.Enumeration.DeviceInformation.CreateWatcher(deviceSelector);

			_deviceWatcher.Added += DeviceWatcher_Added;
			_deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
			_deviceWatcher.Removed += DeviceWatcher_Removed;
			_deviceWatcher.Stopped += DeviceWatcher_Stopped;
			_deviceWatcher.Updated += DeviceWatcher_Updated;

			Start();
		}
		~DeviceWatcher()
		{
			Stop();

			_deviceWatcher.Added -= DeviceWatcher_Added;
			_deviceWatcher.EnumerationCompleted -= DeviceWatcher_EnumerationCompleted;
			_deviceWatcher.Removed -= DeviceWatcher_Removed;
			_deviceWatcher.Stopped += DeviceWatcher_Stopped;
			_deviceWatcher.Updated -= DeviceWatcher_Updated;
		}

		public void Start()
		{
			if (_deviceWatcher.Status != Windows.Devices.Enumeration.DeviceWatcherStatus.Started)
				_deviceWatcher.Start();
		}
		public void Stop()
		{
			if (_deviceWatcher.Status != Windows.Devices.Enumeration.DeviceWatcherStatus.Stopped)
				_deviceWatcher.Stop();
		}

		private async void UpdateDevices()
		{
			DeviceInformationCollection = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(_deviceSelector);
		}

		private void DeviceWatcher_Added(Windows.Devices.Enumeration.DeviceWatcher sender, Windows.Devices.Enumeration.DeviceInformation args)
		{
			if (_enumerationCompleted)
				UpdateDevices();
		}
		private void DeviceWatcher_EnumerationCompleted(Windows.Devices.Enumeration.DeviceWatcher sender, object args)
		{
			_enumerationCompleted = true;

			UpdateDevices();
		}
		private void DeviceWatcher_Removed(Windows.Devices.Enumeration.DeviceWatcher sender, Windows.Devices.Enumeration.DeviceInformationUpdate args)
		{
			if (_enumerationCompleted)
				UpdateDevices();
		}
		private void DeviceWatcher_Stopped(Windows.Devices.Enumeration.DeviceWatcher sender, object args)
		{
		}
		private void DeviceWatcher_Updated(Windows.Devices.Enumeration.DeviceWatcher sender, Windows.Devices.Enumeration.DeviceInformationUpdate args)
		{
			if (_enumerationCompleted)
				UpdateDevices();
		}
	}
}
#endif
