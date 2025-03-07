﻿using System;
using System.IO;
using Xamarin.Forms.OpenTok.Service;

namespace Xamarin.Forms.OpenTok.Sample
{
    public partial class ChatRoomPage : ContentPage
    {
        public ChatRoomPage()
        {
            InitializeComponent();
            CrossOpenTok.Current.MessageReceived += OnMessageReceived;
            CrossOpenTok.Current.SubscriberAdded += OnSubscriberAdded;
        }

        private void OnEndCall(object sender, EventArgs e)
        {
            CrossOpenTok.Current.EndSession();
            CrossOpenTok.Current.MessageReceived -= OnMessageReceived;
            CrossOpenTok.Current.SubscriberAdded -= OnSubscriberAdded;
            Navigation.PopAsync();
        }

        private void OnMessage(object sender, EventArgs e)
        {
            CrossOpenTok.Current.SendMessageAsync($"Path.GetRandomFileName: {Path.GetRandomFileName()}");
        }

        private void OnSwapCamera(object sender, EventArgs e)
        {
            CrossOpenTok.Current.CycleCamera();
        }

        private void OnMessageReceived(string message)
        {
            DisplayAlert("Random message received", message, "OK");
        }

        private void OnSubscriberAdded(string streamId)
        {
            var subscriber = new OpenTokSubscriberView(streamId) {HeightRequest = 200};
            OpenTokSubscribers.Children.Add(subscriber);

        }
    }
}
