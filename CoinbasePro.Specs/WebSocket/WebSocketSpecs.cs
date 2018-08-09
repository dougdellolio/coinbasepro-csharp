using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Net.Http;
using CoinbasePro.Network.Authentication;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Websocket;
using CoinbasePro.WebSocket;
using CoinbasePro.WebSocket.Models.Response;
using CoinbasePro.WebSocket.Types;
using Moq;
using Serilog;
using Serilog.Sinks.TestCorrelator;
using WebSocket4Net;
using It = Machine.Specifications.It;

namespace CoinbasePro.Specs.WebSocket
{
    [Subject(typeof(CoinbasePro.WebSocket.WebSocket))]
    class WebSocketSpecs : WithSubject<CoinbasePro.WebSocket.WebSocket>
    {
        static Exception exception;

        static List<ProductType> product_type_inputs;

        static List<ChannelType> no_channel_type_inputs;
        static List<ChannelType> specified_channel_type_inputs;
        Establish context = () =>
        {
            product_type_inputs = new List<ProductType>();
            no_channel_type_inputs = new List<ChannelType>();
            specified_channel_type_inputs = new List<ChannelType>(new[] { ChannelType.Level2, ChannelType.User });
            product_type_inputs.Add(ProductType.BtcUsd);
        };

        class when_creating_a_websocket_feed
        {
            class when_calling_start_with_product_type_and_not_providing_channels
            {
                Because of = () =>
                    Subject.Start(product_type_inputs, no_channel_type_inputs);

                It should_have_opened_the_feed = () =>
                    The<IWebSocketFeed>().
                        WasToldTo(p => p.Open());
            }
            class when_calling_start_with_product_type_and_providing_channels
            {
                Because of = () =>
                    Subject.Start(product_type_inputs, specified_channel_type_inputs);

                It should_have_opened_the_feed = () =>
                    The<IWebSocketFeed>().
                        WasToldTo(p => p.Open());
            }

            class when_calling_start_and_not_providing_product_types
            {
                Because of = () =>
                    exception = Catch.Exception(() => Subject.Start(new List<ProductType>(), no_channel_type_inputs));

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

                Log.Logger = new LoggerConfiguration().WriteTo.Sink(new TestCorrelatorSink()).
                    CreateLogger();
            };

            class when_opened_is_called
            {
                class with_authentication_with_no_channels
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

                        websocket_feed.Raise(e => e.Opened += null, EventArgs.Empty);
                    };

                    It should_have_called_send_with_authentication_properties = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Send(@"{""type"":""subscribe"",""product_ids"":[""BTC-USD""],""channels"":[{""name"":""full"",""product_ids"":[""BTC-USD""]},{""name"":""heartbeat"",""product_ids"":[""BTC-USD""]},{""name"":""level2"",""product_ids"":[""BTC-USD""]},{""name"":""matches"",""product_ids"":[""BTC-USD""]},{""name"":""ticker"",""product_ids"":[""BTC-USD""]},{""name"":""user"",""product_ids"":[""BTC-USD""]}],""key"":""key"",""passphrase"":""passphrase"",""timestamp"":""-62135596800""}"));
                }

                class with_authentication_with_specified_channels
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, specified_channel_type_inputs);

                        websocket_feed.Raise(e => e.Opened += null, EventArgs.Empty);
                    };

                    It should_have_called_send_with_authentication_properties = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Send(@"{""type"":""subscribe"",""product_ids"":[""BTC-USD""],""channels"":[{""name"":""level2"",""product_ids"":[""BTC-USD""]},{""name"":""user"",""product_ids"":[""BTC-USD""]}],""key"":""key"",""passphrase"":""passphrase"",""timestamp"":""-62135596800""}"));
                }

                class without_authentication
                {
                    Establish context = () =>
                        Configure(x => x.For<IAuthenticator>().Use((Authenticator)null));

                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

                        websocket_feed.Raise(e => e.Opened += null, EventArgs.Empty);
                    };

                    It should_have_called_send_without_authentication_properties = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Send(@"{""type"":""subscribe"",""product_ids"":[""BTC-USD""],""channels"":[{""name"":""full"",""product_ids"":[""BTC-USD""]},{""name"":""heartbeat"",""product_ids"":[""BTC-USD""]},{""name"":""level2"",""product_ids"":[""BTC-USD""]},{""name"":""matches"",""product_ids"":[""BTC-USD""]},{""name"":""ticker"",""product_ids"":[""BTC-USD""]},{""name"":""user"",""product_ids"":[""BTC-USD""]}],""timestamp"":""-62135596800""}"));
                }
            }

            class when_closed_is_called
            {
                Because of = () =>
                {
                    Subject.Start(product_type_inputs, no_channel_type_inputs);

                    websocket_feed.Raise(e => e.Closed += null, EventArgs.Empty);
                };

                It should_have_called_dispose = () =>
                    The<IWebSocketFeed>().
                        WasToldTo(p => p.Dispose());
            }

            class when_message_received_is_called
            {
                class when_response_type_is_snapshot
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

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
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

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
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

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
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

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
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

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
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

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
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

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
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateDoneResponse()));
                    };

                    It should_have_invoked_done_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Done>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Done>>()));
                }

                class when_response_type_is_change
                {
                    class with_size
                    {
                        Because of = () =>
                        {
                            Subject.Start(product_type_inputs, no_channel_type_inputs);

                            websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateChangeResponse(true)));
                        };

                        It should_have_invoked_change_response = () =>
                            The<IWebSocketFeed>().
                                WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Change>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Change>>()));
                    }

                    class with_funds
                    {
                        Because of = () =>
                        {
                            Subject.Start(product_type_inputs, no_channel_type_inputs);

                            websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateChangeResponse(false)));
                        };

                        It should_have_invoked_change_response = () =>
                            The<IWebSocketFeed>().
                                WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Change>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Change>>()));
                    }
                }

                class when_response_type_is_activate
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateActivateResponse()));
                    };

                    It should_have_invoked_activate_response = () =>
                        The<IWebSocketFeed>().
                            WasToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<Activate>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<Activate>>()));
                }

                class when_response_type_doesnt_match_a_type
                {
                    Because of = () =>
                    {
                        Subject.Start(product_type_inputs, no_channel_type_inputs);

                        websocket_feed.Raise(e => e.MessageReceived += null, new MessageReceivedEventArgs(WebSocketTypeResponseFixture.CreateRandomResponse()));
                    };

                    It should_have_logged = () =>
                        TestCorrelator.GetLogEventsFromCurrentContext().
                            ShouldContain(p => p.MessageTemplate.Text.Contains("Unknown ResponseType"));

                    It should_not_have_called_invoke = () =>
                        The<IWebSocketFeed>().
                            WasNotToldTo(p => p.Invoke(Param.IsAny<EventHandler<WebfeedEventArgs<BaseMessage>>>(), Param.IsAny<object>(), Param.IsAny<WebfeedEventArgs<BaseMessage>>()));
                }
            }
        }
    }
}
