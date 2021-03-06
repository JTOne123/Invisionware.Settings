﻿// ***********************************************************************
// Assembly         : Invisionware.Settings
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="SettingsSinkConfiguration.cs" company="Invisionware">
//     Copyright (c) Invisionware. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Invisionware.Settings.Sinks;

namespace Invisionware.Settings
{
	/// <summary>
	/// Class SettingsSinkConfiguration.
	/// </summary>
	/// <typeparam name="TSetting">The type of the t setting.</typeparam>
	/// <typeparam name="TSink">The type of the t sink.</typeparam>
	public class SettingsSinkConfiguration<TSink>
	{
		/// <summary>
		/// The settings configuration
		/// </summary>
		private readonly SettingsConfiguration _settingsConfiguration;
		/// <summary>
		/// The add sink
		/// </summary>
		private readonly Action<TSink> _addSink;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsSinkConfiguration{T}"/> class.
		/// </summary>
		/// <param name="settingsConfiguration">The settings configuration.</param>
		/// <param name="addSink">The add sink.</param>
		/// <exception cref="System.ArgumentNullException">
		/// settingsConfiguration
		/// or
		/// addSink
		/// </exception>
		internal SettingsSinkConfiguration(SettingsConfiguration settingsConfiguration, Action<TSink> addSink)	
		{
			_settingsConfiguration = settingsConfiguration ?? throw new ArgumentNullException(nameof(settingsConfiguration));
			_addSink = addSink ?? throw new ArgumentNullException(nameof(addSink));
		}

		/// <summary>
		/// Sinks the specified settings sink.
		/// </summary>
		/// <param name="settingsSink">The settings sink.</param>
		/// <returns>SettingsConfiguration.</returns>
		public SettingsConfiguration Sink(
			TSink settingsSink)
		{
			var sink = settingsSink;

			_addSink(sink);

			return _settingsConfiguration;
		}

		public static SettingsConfiguration Wrap(
			SettingsReaderSinkConfiguration settingsSinkConfiguration,
			Func<ISettingsReaderSink, ISettingsReaderSink> wrapSink,
			Action<SettingsReaderSinkConfiguration> configureWrappedSink)
		{
			if (settingsSinkConfiguration == null) throw new ArgumentNullException(nameof(settingsSinkConfiguration));
			if (wrapSink == null) throw new ArgumentNullException(nameof(wrapSink));
			if (configureWrappedSink == null) throw new ArgumentNullException(nameof(configureWrappedSink));

			void WrapAndAddSink(ISettingsReaderSink sink)
			{
				bool sinkIsDisposable = sink is IDisposable;

				var wrappedSink = wrapSink(sink);

				if (sinkIsDisposable && !(wrappedSink is IDisposable))
				{
					//SelfLog.WriteLine("Wrapping sink {0} does not implement IDisposable, but wrapped sink {1} does.", wrappedSink, sink);
				}

				settingsSinkConfiguration.Sink(wrappedSink);
			}

			var capturingSettingsSinkConfiguration = new SettingsReaderSinkConfiguration(
				settingsSinkConfiguration._settingsConfiguration,
				WrapAndAddSink);

			configureWrappedSink(capturingSettingsSinkConfiguration);

			return settingsSinkConfiguration._settingsConfiguration;
		}
	}
}