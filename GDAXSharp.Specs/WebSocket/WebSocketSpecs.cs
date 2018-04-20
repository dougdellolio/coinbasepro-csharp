using System.Collections.Generic;
using GDAXSharp.Shared.Types;
using GDAXSharp.WebSocket;
using GDAXSharp.WebSocket.Types;
using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Net.Http;
using GDAXSharp.Network.Authentication;
using GDAXSharp.Specs.JsonFixtures.Websocket;
using GDAXSharp.WebSocket.Models.Response;
using Moq;
using WebSocket4Net;
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

                It should_have_called_send = () =>
                    The<IWebSocketFeed>().
                        WasToldTo(p => p.Send(Param.IsAny<string>()));
            }

            class when_closed_is_called
            {
                class with_websocket_state_open
                {
                    Establish context = () =>
                        The<IWebSocketFeed>().WhenToldTo(p => p.State).Return(WebSocketState.Open);

                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.Closed += null, EventArgs.Empty);
                    };

                    It should_not_have_called_dispose = () =>
                        The<IWebSocketFeed>().
                            WasNotToldTo(p => p.Dispose());
                }

                class with_websocket_state_in_non_open_state
                {
                    Establish context = () =>
                        The<IWebSocketFeed>().WhenToldTo(p => p.State).Return(WebSocketState.Closed);

                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.Closed += null, EventArgs.Empty);
                    };

                    It should_have_called_dispose = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Dispose());
                }
            }

            class when_message_received_is_called
            {
                class when_response_type_is_snapshot
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateSnapshotResponse()));
                    };

                    It should_have_invoked_snapshot_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Snapshot>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Snapshot>>()));
                }

                class when_response_type_is_subscription
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateSubscriptionResponse()));
                    };

                    It should_have_invoked_subscription_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Subscription>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Subscription>>()));
                }

                class when_response_type_is_ticker
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateTickerResponse()));
                    };

                    It should_have_invoked_ticker_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Ticker>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Ticker>>()));
                }

                class when_response_type_is_l2update
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateLevel2Response()));
                    };

                    It should_have_invoked_l2_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Level2>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Level2>>()));
                }

                class when_response_type_is_heartbeat
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateHeartbeatResponse()));
                    };

                    It should_have_invoked_heartbeat_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Heartbeat>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Heartbeat>>()));

                }

                class when_response_type_is_received
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateReceivedResponse()));
                    };

                    It should_have_invoked_received_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Received>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Received>>()));
                }

                class when_response_type_is_open
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateOpenResponse()));
                    };

                    It should_have_invoked_open_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Open>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Open>>()));
                }

                class when_response_type_is_done
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateDoneResponse()));
                    };

                    It should_have_invoked_done_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Done>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Done>>()));
                }
            }
        }
    }
}
