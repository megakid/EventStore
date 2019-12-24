using EventStore.Core.Bus;
using EventStore.Projections.Core.Messages;

namespace EventStore.Projections.Core.Services.Management {
	public sealed class ProjectionCoreResponseWriter
		: IHandle<CoreProjectionStatusMessage.Faulted>,
			IHandle<CoreProjectionStatusMessage.Prepared>,
			IHandle<CoreProjectionStatusMessage.Started>,
			IHandle<CoreProjectionStatusMessage.StatisticsReport>,
			IHandle<CoreProjectionStatusMessage.Stopped>,
			IHandle<CoreProjectionStatusMessage.StateReport>,
			IHandle<CoreProjectionStatusMessage.ResultReport>,
			IHandle<ProjectionManagementMessage.Command.Abort>,
			IHandle<ProjectionManagementMessage.Command.Disable>,
			IHandle<ProjectionManagementMessage.Command.Enable>,
			IHandle<ProjectionManagementMessage.Command.GetQuery>,
			IHandle<ProjectionManagementMessage.Command.GetResult>,
			IHandle<ProjectionManagementMessage.Command.GetState>,
			IHandle<ProjectionManagementMessage.Command.GetStatistics>,
			IHandle<ProjectionManagementMessage.Command.Post>,
			IHandle<ProjectionManagementMessage.Command.PostBatch>,
			IHandle<ProjectionManagementMessage.Command.Reset>,
			IHandle<ProjectionManagementMessage.Command.SetRunAs>,
			IHandle<ProjectionManagementMessage.Command.UpdateQuery>,
			IHandle<ProjectionManagementMessage.Command.Delete>,
			IHandle<ProjectionCoreServiceMessage.StartCore> {
		private readonly IResponseWriter _writer;
		private readonly IPublisher _masterOutputBus;

		public ProjectionCoreResponseWriter(IResponseWriter responseWriter, IPublisher masterOutputBus) {
			_writer = responseWriter;
			_masterOutputBus = masterOutputBus;
		}

		public void Handle(ProjectionCoreServiceMessage.StartCore message) {
			_writer.Reset();
		}

		public void Handle(CoreProjectionStatusMessage.Faulted message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(CoreProjectionStatusMessage.Prepared message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(CoreProjectionStatusMessage.Started message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(CoreProjectionStatusMessage.StatisticsReport message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(CoreProjectionStatusMessage.Stopped message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(CoreProjectionStatusMessage.StateReport message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(CoreProjectionStatusMessage.ResultReport message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.Abort message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.Disable message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.Enable message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.GetQuery message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.GetResult message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.GetState message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.GetStatistics message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.Post message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.PostBatch message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.Reset message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.SetRunAs message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.UpdateQuery message) {
			_masterOutputBus.Publish(message);
		}

		public void Handle(ProjectionManagementMessage.Command.Delete message) {
			_masterOutputBus.Publish(message);
		}
	}
}
