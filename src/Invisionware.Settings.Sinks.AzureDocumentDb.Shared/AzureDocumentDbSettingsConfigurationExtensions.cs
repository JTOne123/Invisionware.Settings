﻿// ***********************************************************************
// Assembly         : Invisionware.Settings.Sinks.Azure
// Author           : Shawn Anderson (sanderson@eye-catcher.com)
// Created          : 04-09-2017
//
// Last Modified By : Shawn Anderson (sanderson@eye-catcher.com)
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="AzureDocumentDbSettingsConfigurationExtensions.cs" company="Invisionware">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Invisionware.Settings.Sinks.Azure;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace Invisionware.Settings.Sinks
{
	/// <summary>
	/// Class AzureDocumentDbSettingsConfigurationExtensions.
	/// </summary>
	public static class AzureDocumentDbSettingsConfigurationExtensions
	{
		/// <summary>
		/// Creates the Azures the file storage Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:AzureFileStorage:EndPointUri
		///		settings:sink:AzureFileStorage:AuthorizationKey
		///		settings:sink:AzureFileStorage:DatabaseName
		///		settings:sink:AzureFileStorage:CollectionName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="appConfigSettingsMgr">The configuration settings MGR.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration<T> AzureDocumentDb<T>(this SettingsWriterSinkConfiguration<T> settingsConfig, IAppConfigSettingsMgr appConfigSettingsMgr = null) where T : class, new()
		{
			if (appConfigSettingsMgr == null) appConfigSettingsMgr = AppConfigSettingsMgr.Current;
			if (appConfigSettingsMgr == null) throw new ArgumentNullException(nameof(appConfigSettingsMgr));

			return AzureDocumentDb(settingsConfig,
				new Uri(appConfigSettingsMgr.GetValue("settings:sink:AzureDocumentDb:EndPointUri", string.Empty)),
				appConfigSettingsMgr.GetValue("settings:sink:AzureDocumentDb:AuthorizationKey", string.Empty),
				appConfigSettingsMgr.GetValue("settings:sink:AzureDocumentDb:DatabaseName", "AppSettings"),
				appConfigSettingsMgr.GetValue("settings:sink:AzureDocumentDb:CollectionName", "Config") 
			);
		}

		/// <summary>
		/// Azures the document database.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="endpointUri">The endpoint URI.</param>
		/// <param name="authorizationKey">The authorization key.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="collectionName">Name of the collection.</param>
		/// <param name="maxVersions">The maximum versions.</param>
		/// <param name="connectionProtocol">The connection protocol.</param>
		/// <param name="jsonSettings">The json settings.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig
		/// or
		/// endpointUri
		/// or
		/// authorizationKey</exception>
		public static SettingsConfiguration<T> AzureDocumentDb<T>(this SettingsWriterSinkConfiguration<T> settingsConfig, Uri endpointUri,
			string authorizationKey, string databaseName = "AppSettings", string collectionName = "Config", int maxVersions = 3, Protocol connectionProtocol = Protocol.Https, JsonSerializerSettings jsonSettings = null) where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (endpointUri == null) throw new ArgumentNullException(nameof(endpointUri));
			if (authorizationKey == null) throw new ArgumentNullException(nameof(authorizationKey));

			return settingsConfig.Sink(
				new AzureDocumentDbSink(
					endpointUri,
					authorizationKey,
					databaseName,
					collectionName,
					maxVersions,
					connectionProtocol, 
					jsonSettings));
		}

		/// <summary>
		/// Creates the Azures the file storage Configuiration object and loads the main settings form the config manager.
		/// Keys:
		///		settings:sink:AzureFileStorage:EndPointUri
		///		settings:sink:AzureFileStorage:AuthorizationKey
		///		settings:sink:AzureFileStorage:DatabaseName
		///		settings:sink:AzureFileStorage:CollectionName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="appConfigSettingsMgr">The configuration settings MGR.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">configSettingsMgr</exception>
		public static SettingsConfiguration<T> AzureDocumentDb<T>(this SettingsReaderSinkConfiguration<T> settingsConfig, IAppConfigSettingsMgr appConfigSettingsMgr = null) where T : class, new()
		{
			if (appConfigSettingsMgr == null) appConfigSettingsMgr = AppConfigSettingsMgr.Current;
			if (appConfigSettingsMgr == null) throw new ArgumentNullException(nameof(appConfigSettingsMgr));

			return AzureDocumentDb(settingsConfig,
				new Uri(appConfigSettingsMgr.GetValue("settings:sink:AzureDocumentDb:EndPointUri", string.Empty)),
				appConfigSettingsMgr.GetValue("settings:sink:AzureDocumentDb:AuthorizationKey", string.Empty),
				appConfigSettingsMgr.GetValue("settings:sink:AzureDocumentDb:DatabaseName", "AppSettings"),
				appConfigSettingsMgr.GetValue("settings:sink:AzureDocumentDb:CollectionName", "Config")
			);
		}

		/// <summary>
		/// Azures the document database.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingsConfig">The settings configuration.</param>
		/// <param name="endpointUri">The endpoint URI.</param>
		/// <param name="authorizationKey">The authorization key.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="collectionName">Name of the collection.</param>
		/// <param name="connectionProtocol">The connection protocol.</param>
		/// <param name="jsonSettings">The json settings.</param>
		/// <returns>SettingsConfiguration.</returns>
		/// <exception cref="System.ArgumentNullException">settingsConfig
		/// or
		/// endpointUri
		/// or
		/// authorizationKey</exception>
		public static SettingsConfiguration<T> AzureDocumentDb<T>(this SettingsReaderSinkConfiguration<T> settingsConfig, Uri endpointUri,
			string authorizationKey, string databaseName = "AppSettings", string collectionName = "Config", Protocol connectionProtocol = Protocol.Https, JsonSerializerSettings jsonSettings = null) where T : class, new()
		{
			if (settingsConfig == null) throw new ArgumentNullException(nameof(settingsConfig));
			if (endpointUri == null) throw new ArgumentNullException(nameof(endpointUri));
			if (authorizationKey == null) throw new ArgumentNullException(nameof(authorizationKey));

			return settingsConfig.Sink(
				new AzureDocumentDbSink(
					endpointUri,
					authorizationKey,
					databaseName,
					collectionName,
					connectionProtocol: connectionProtocol, 
					jsonSettings: jsonSettings));
		}
	}
}