using System;
using System.Collections.Generic;
using EventStore.Common.Log;
using EventStore.Core.Bus;
using EventStore.Projections.Core.Messages;

namespace EventStore.Projections.Core.Services.Management {
	public sealed class ProjectionManagerCommandWriter
		: IHandle<CoreProjectionManagementMessage.CreatePrepared>,
			IHandle<CoreProjectionManagementMessage.CreateAndPrepare>,
			IHandle<CoreProjectionManagementMessage.LoadStopped>,
			IHandle<CoreProjectionManagementMessage.Start>,
			IHandle<CoreProjectionManagementMessage.Stop>,
			IHandle<CoreProjectionManagementMessage.Kill>,
			IHandle<CoreProjectionManagementMessage.Dispose>,
			IHandle<CoreProjectionManagementMessage.GetState>,
			IHandle<CoreProjectionManagementMessage.GetResult> {
		private readonly IDictionary<Guid, IPublisher> _queues;
		private readonly ILogger _logger = LogManager.GetLoggerFor<ProjectionManagerCommandWriter>();

		public ProjectionManagerCommandWriter(IDictionary<Guid, IPublisher> queues) {
			_queues = queues;
		}

		private IPublisher GetQueueFor(Guid workerId, string messageName) {
			if (!_queues.TryGetValue(workerId, out var queue)) {
				_logger.Warn($"[PROJECTIONS] Could not find worker id {workerId} for {messageName} message.");
			}
			return queue;
		}
		
		public void Handle(CoreProjectionManagementMessage.CreatePrepared message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.CreatePrepared));
			queue?.Publish(message);
		}

		public void Handle(CoreProjectionManagementMessage.CreateAndPrepare message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.CreateAndPrepare));
			queue?.Publish(message);
		}

		public void Handle(CoreProjectionManagementMessage.LoadStopped message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.LoadStopped));
			queue?.Publish(message);
		}

		public void Handle(CoreProjectionManagementMessage.Start message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.Start));
			queue?.Publish(message);
		}

		public void Handle(CoreProjectionManagementMessage.Stop message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.Stop));
			queue?.Publish(message);
		}

		public void Handle(CoreProjectionManagementMessage.Kill message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.Kill));
			queue?.Publish(message);
		}

		public void Handle(CoreProjectionManagementMessage.Dispose message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.Dispose));
			queue?.Publish(message);
		}

		public void Handle(CoreProjectionManagementMessage.GetState message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.GetState));
			queue?.Publish(message);
		}

		public void Handle(CoreProjectionManagementMessage.GetResult message) {
			var queue = GetQueueFor(message.WorkerId, nameof(CoreProjectionManagementMessage.GetResult));
			queue?.Publish(message);
		}
	}
}
