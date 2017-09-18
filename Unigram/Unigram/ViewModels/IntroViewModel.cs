﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Api.Aggregator;
using Telegram.Api.Services;
using Telegram.Api.Services.Cache;
using Unigram.Common;
using Unigram.Views.SignIn;
using Windows.UI.Xaml;

namespace Unigram.ViewModels
{
    public class IntroViewModel : UnigramViewModelBase
    {
        public IntroViewModel(IMTProtoService protoService, ICacheService cacheService, ITelegramEventAggregator aggregator) 
            : base(protoService, cacheService, aggregator)
        {
            Items = new ObservableCollection<IntroPage>();

            // TODO: put them in a separate file?
            // TODO: localization
            Items.Add(new IntroPage { Title = "Unigram", Text = "**Unigram** is an open-source Telegram client for the Universal Windows Platform, developed by the community, for the community." });
            Items.Add(new IntroPage { Title = "Fast", Text = "**Unigram** delivers messages faster\nthan any other application." });
            Items.Add(new IntroPage { Title = "Free", Text = "**Unigram** is free forever. No ads.\nNo subscription fees." });
            Items.Add(new IntroPage { Title = "Powerful", Text = "**Unigram** has no limits on\nthe size of your media and chats." });
            Items.Add(new IntroPage { Title = "Secure", Text = "**Unigram** keeps your messages\nsafe from hacker attacks." });
            Items.Add(new IntroPage { Title = "Cloud-Based", Text = "**Unigram** lets you access your\nmessages from multiple devices." });
            SelectedItem = Items[0];
        }

        private RelayCommand _continueCommand;
        public RelayCommand ContinueCommand => _continueCommand = (_continueCommand ?? new RelayCommand(ContinueExecute /*, () => SelectedItem == Items.Last()*/));
        private void ContinueExecute()
        {
            NavigationService.Navigate(typeof(SignInPage));
        }

        private IntroPage _selectedItem;
        public IntroPage SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                Set(ref _selectedItem, value);
                ContinueCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<IntroPage> Items { get; private set; }

        public class IntroPage
        {
            public string Title { get; set; }

            public string Text { get; set; }
        }
    }
}
