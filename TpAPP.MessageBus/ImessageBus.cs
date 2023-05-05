using System;
using System.Threading.Tasks;

namespace TpAPP.MessageBus
{
    public interface ImessageBus
    {
        Task PublishMessage(BaseMessage message, string topicName);
    }
}
