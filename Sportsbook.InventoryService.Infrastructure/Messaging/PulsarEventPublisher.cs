using CloudNative.CloudEvents;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Sportsbook.InventoryService.Application.Seedwork.Interfaces;
using System.Buffers;
using System.Globalization;
using System.Net.Mime;

namespace Sportsbook.InventoryService.Infrastructure.Messaging
{
    internal sealed class PulsarEventPublisher(
        IProducer<ReadOnlySequence<byte>> producer,
        CloudEventFormatter cloudEventFormatter) : IEventPublisher
    {
        private const string source = "https://training-cqrs";

        public async Task PublishAsync<TEvent>(
            TEvent eventToPublish,
            CancellationToken cancellationToken = default)
        {

            var cloudEvent = new CloudEvent
            {
                Id = Guid.NewGuid().ToString(),
                Type = eventToPublish.GetType().Name,
                Source = new Uri(source),
                Time = DateTimeOffset.UtcNow,
                DataContentType = MediaTypeNames.Application.Json,
                Data = eventToPublish
            };
            ReadOnlyMemory<byte> encodedPayload = cloudEventFormatter.EncodeStructuredModeMessage(cloudEvent, out ContentType contentType);

            await producer.Send(BuildMessageMetadata(cloudEvent, contentType), encodedPayload, cancellationToken);
        }

        private static MessageMetadata BuildMessageMetadata(CloudEvent cloudEvent, ContentType contentType)
        {
            MessageMetadata messageMetadata = new();

            foreach (KeyValuePair<CloudEventAttribute, object> populatedAttribute in cloudEvent.GetPopulatedAttributes())
            {
                string key = "ce-" + populatedAttribute.Key.Name;
                object value = populatedAttribute.Value;
                messageMetadata[key] = ((!IsTimeAttribute(populatedAttribute.Key.Name)) ? value.ToString() : FormatTimeAsIso8601(value));
            }

            if (contentType != null)
            {
                messageMetadata["contenttype"] = contentType.MediaType;
            }

            return messageMetadata;
        }

        private static bool IsTimeAttribute(string attributeName)
        {
            return string.Equals(attributeName, "time", StringComparison.OrdinalIgnoreCase);
        }

        private static string FormatTimeAsIso8601(object timeAttributeValue)
        {
            if (timeAttributeValue is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.ToString("o");
            }

            if (DateTimeOffset.TryParse(timeAttributeValue.ToString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result))
            {
                return result.ToString("o");
            }

            return timeAttributeValue.ToString();
        }
    }
}
