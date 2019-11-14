using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClusterNode;
using EventStore.Common.Log;
using EventStore.Core;
using NLog;
using NLog.Config;
using NLog.Targets;
using Xunit;

namespace EventStore.ClientAPI.Tests {
	public partial class EventStoreClientAPIFixture : IAsyncLifetime {
		private const string TestEventType = "-";

		private static readonly X509Certificate2 ServerCertificate;
		private static int CurPort = 50000;
		private readonly int _externalPort;
		private readonly int _externalSecurePort;
		private readonly int _unusedPort;
		private readonly int _externalHttpPort;
		private readonly int _internalHttpPort;

		private readonly ClusterVNode _node;

		static EventStoreClientAPIFixture() {
			using var stream = typeof(EventStoreClientAPIFixture)
				.Assembly
				.GetManifestResourceStream(typeof(EventStoreClientAPIFixture), "server.p12");
			using var mem = new MemoryStream();
			stream.CopyTo(mem);
			ServerCertificate = new X509Certificate2(mem.ToArray(), "1111");
			try {
				Directory.CreateDirectory("/tmp/eslogs/");
			} catch (Exception e) {

			}
		}

		public EventStoreClientAPIFixture() {
			using (StreamWriter outputFile = new StreamWriter("/tmp/eslogs/fixture.log", true)) {
				outputFile.WriteLine("EventStoreClientAPIFixture start");
			}

			_externalPort = CurPort;
			_externalSecurePort = CurPort + 1;
			_unusedPort = CurPort + 2;
			_externalHttpPort = CurPort + 3;
			_internalHttpPort = CurPort + 4;
			CurPort += 5;

			InitializeLogger();
			var vNodeBuilder = ClusterVNodeBuilder
				.AsSingleNode()
				.WithExternalTcpOn(new IPEndPoint(IPAddress.Loopback, _externalPort))
				.WithExternalSecureTcpOn(new IPEndPoint(IPAddress.Loopback, _externalSecurePort))
				.WithExternalHttpOn(new IPEndPoint(IPAddress.Loopback, _externalHttpPort))
				.WithInternalHttpOn(new IPEndPoint(IPAddress.Loopback, _internalHttpPort))
				.WithServerCertificate(ServerCertificate)
				.RunInMemory();

			_node = vNodeBuilder.Build();
			using (StreamWriter outputFile = new StreamWriter("/tmp/eslogs/fixture.log", true)) {
				outputFile.WriteLine("EventStoreClientAPIFixture end");
			}
		}

		private void InitializeLogger() {
			var fileTarget = new FileTarget("file_target");
			fileTarget.FileName = "/tmp/eslogs/eventstore.log";
			fileTarget.CreateDirs = true;
			fileTarget.Layout = "[PID:${processid:padCharacter=0:padding=5}:${threadid:padCharacter=0:padding=3} ${date:universalTime=true:format=yyyy\\.MM\\.dd HH\\:mm\\:ss\\.fff} ${level:padding=-5:uppercase=true} ${logger:padding=-20:fixedLength=true}] ${message}${onexception:${newline}${literal:text=EXCEPTION OCCURRED}${newline}${exception:format=tostring:innerFormat=tostring:maxInnerExceptionLevel=20}}";
			var config = new NLog.Config.LoggingConfiguration();
			config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
			NLog.LogManager.Configuration = config;
			EventStore.Common.Log.LogManager.SetLogFactory(x => new NLogger(x));
		}

		public Task InitializeAsync() => _node.StartAndWaitUntilReady().WithTimeout();

		public Task DisposeAsync() => _node.Stop().WithTimeout();

		private ConnectionSettingsBuilder DefaultBuilder {
			get {
				var builder = ConnectionSettings.Create()
					.EnableVerboseLogging()
					.LimitReconnectionsTo(0)
					.LimitRetriesForOperationTo(0)
					.SetTimeoutCheckPeriodTo(TimeSpan.FromMilliseconds(100))
					.SetReconnectionDelayTo(TimeSpan.FromMilliseconds(100))
					.FailOnNoServerResponse();

				// ReSharper disable ConditionIsAlwaysTrueOrFalse
				// ReSharper disable HeuristicUnreachableCode
				#if DEBUG
				if (UseLoggerBridge) {
					builder = builder.UseCustomLogger(ConsoleLoggerBridge.Default);
				}
				// ReSharper restore HeuristicUnreachableCode
				// ReSharper restore ConditionIsAlwaysTrueOrFalse
				#endif
				return builder;
			}
		}

		private static ConnectionSettingsBuilder DefaultConfigureSettings(
			ConnectionSettingsBuilder builder)
			=> builder.EnableVerboseLogging().UseFileLogger("/tmp/eslogs/client.log");

		public IEnumerable<EventData> CreateTestEvents(int count = 1)
			=> Enumerable.Range(0, count).Select(CreateTestEvent);

		protected static EventData CreateTestEvent(int index) =>
			new EventData(Guid.NewGuid(), TestEventType, true, Encoding.UTF8.GetBytes($@"{{""x"":{index}}}"),
				Array.Empty<byte>());
	}
}
