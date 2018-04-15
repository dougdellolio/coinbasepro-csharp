using System.Collections.Generic;
using GDAXSharp.Shared.Types;
using GDAXSharp.WebSocket;
using GDAXSharp.WebSocket.Types;
using Machine.Fakes;
using Machine.Specifications;
using System;

namespace GDAXSharp.Specs.WebSocket
{
    [Subject(typeof(GDAXSharp.WebSocket.WebSocket))]
    class WebSocketSpecs : WithSubject<GDAXSharp.WebSocket.WebSocket>
    {
        static List<ProductType> product_type_inputs;

        static List<ChannelType> channel_type_inputs;

        Establish context = () =>
        {
            The<IWebSocketFeed>().WhenToldTo(p => p.Create(Param.IsAny<string>()))
                .Return((WebSocket4Net.WebSocket)null);

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
                        WasToldTo(p => p.Open(Param.IsAny<WebSocket4Net.WebSocket>()));
            }

            class when_calling_start_and_not_providing_product_types
            {
                static Exception exception;

                Because of = () =>
                    exception = Catch.Exception(() => Subject.Start(new List<ProductType>(), channel_type_inputs));

                It should_have_thrown_an_error = () =>
                    exception.ShouldBeOfExactType<ArgumentException>();
            }
        }
    }
}
