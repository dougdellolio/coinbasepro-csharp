using System.Collections.Generic;
using GDAXSharp.Shared.Types;
using GDAXSharp.WebSocket;
using GDAXSharp.WebSocket.Types;
using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Net.Http;
using GDAXSharp.Network.Authentication;
using Moq;
using It = Machine.Specifications.It;

namespace GDAXSharp.Specs.WebSocket
{
    [Subject(typeof(GDAXSharp.WebSocket.WebSocket))]
    class WebSocketSpecs : WithSubject<GDAXSharp.WebSocket.WebSocket>
    {
        static Exception exception;

        static List<ProductType> product_type_inputs;

        static List<ChannelType> channel_type_inputs;

        Establish context = () =>
        {
            product_type_inputs = new List<ProductType>();
            channel_type_inputs = new List<ChannelType>();
            product_type_inputs.Add(ProductType.BtcUsd);
            channel_type_inputs.Add(ChannelType.Full);
        };

        class when_creating_a_websocket_feed
        {
            class when_calling_start_with_channels_and_product_types
            {
                Because of = () =>
                    Subject.Start(product_type_inputs, channel_type_inputs);

                It should_have_opened_the_feed = () =>
                    The<IWebSocketFeed>().
                        WasToldTo(p => p.Open());
            }

            class when_calling_start_and_not_providing_product_types
            {
                Because of = () =>
                    exception = Catch.Exception(() => Subject.Start(new List<ProductType>(), channel_type_inputs));

                It should_have_thrown_an_error = () =>
                    exception.ShouldBeOfExactType<ArgumentException>();
            }
        }

        class when_events_are_raised
        {
            static Mock<IWebSocketFeed> websocket_feed;

            Establish context = () =>
            {
                websocket_feed = new Mock<IWebSocketFeed>();

                Configure(p => p.For<IWebSocketFeed>().Use(websocket_feed.Object));

                The<IAuthenticator>().WhenToldTo(p => p.ApiKey).Return("key");
                The<IAuthenticator>().WhenToldTo(p => p.Passphrase).Return("passphrase");
                The<IAuthenticator>().WhenToldTo(p => p.UnsignedSignature).Return("test");
                The<IAuthenticator>().
                    WhenToldTo(p => p.ComputeSignature(
                        Param.IsAny<HttpMethod>(),
                        Param.IsAny<string>(),
                        Param.IsAny<double>(),
                        Param.IsAny<string>(),
                        Param.IsAny<string>()));
            };

            class when_opened_is_called
            {
                Because of = () =>
                {
                    Subject.Start(product_type_inputs, channel_type_inputs);

                    websocket_feed.Raise(e => e.Opened += null, EventArgs.Empty);
                };

                It should_have_opened_the_feed = () =>
                    The<IWebSocketFeed>().
                        WasToldTo(p => p.Send(Param.IsAny<string>()));
            }

            class when_closed_is_called
            {
                Because of = () =>
                {
                    Subject.Start(product_type_inputs, channel_type_inputs);

                    websocket_feed.Raise(e => e.Closed += null, EventArgs.Empty);
                };

                It should_have_opened_the_feed = () =>
                    The<IWebSocketFeed>().
                        WasToldTo(p => p.Dispose());
            }
        }
    }
}
