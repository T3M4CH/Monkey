using System;
using Channels;
using UnityEngine;

namespace Game.UI.FinishScreen
{
    public class FinishScreen : IDisposable
    {
        private readonly IChannel<bool> _boolChannel;
        private readonly ScreenView _panel;

        public FinishScreen(IChannel<bool> boolChannel, ScreenView view)
        {
            _panel = view;
            _boolChannel = boolChannel;
            _boolChannel.Subscribe(SetWindow);
            _panel.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _boolChannel.Unsubscribe(SetWindow);
        }

        private void SetWindow(bool winner)
        {
            _panel.gameObject.SetActive(true);
            _panel.recordText.text = $"Record {PlayerPrefs.GetInt("scoreKey")}";
            if (winner)
            {
                _panel.text.text = "You're winner!";
                _panel.buttonText.text = "Next Level";
            }
            else
            {
                _panel.text.text = "Shit happens :(";
                _panel.buttonText.text = "Same level";
            }
        }
    }
}