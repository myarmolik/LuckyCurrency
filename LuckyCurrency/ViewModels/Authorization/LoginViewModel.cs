﻿using LuckyCurrency.Data;
using LuckyCurrency.Data.Model;
using LuckyCurrency.Infrastructure;
using LuckyCurrency.Infrastructure.Commands;
using LuckyCurrency.ViewModels.Base;
using LuckyCurrency.Views.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LuckyCurrency.ViewModels.Authorization
{
    class LoginViewModel : ViewModel
    {
        private const string FIELDS_EMPTY = "Fields are not filled";
        private const string INCORRECTLY = "Password or login entered incorrectly";

        private UnitOfWork _dbWorker;

        #region Models

        #region Title
        private string _title = "LuckyCurrency";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region Login
        private string _login;
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }
        #endregion

        #region Password
        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        #endregion

        #region InfoMessage
        private string _infoMessage = "";
        public string InfoMessage
        {
            get => _infoMessage;
            set => Set(ref _infoMessage, value);
        }
        #endregion

        #endregion

        #region Команды

        #region CloseCommand
        public ICommand CloseCommand { get; }
        private bool CanCloseCommandExecute(object p) => true;
        private void OnCloseCommandExecuted(object p)
        {
            CurrentWindow.Close();
        }
        #endregion
        #region MinimizedCommand
        public ICommand MinimizedCommand { get; }
        private bool CanMinimizedCommandExecute(object p) => true;
        private void OnMinimizedCommandExecuted(object p)
        {
            CurrentWindow.WindowState = WindowState.Minimized;
        }
        #endregion

        #region LoginCommand
        public ICommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object p) => true;
        private void OnLoginCommandExecuted(object p)
        {
            var passBox = p as PasswordBox;
            Password = passBox.Password;

            if (IsFieldsNotEmpty())
            {
                var api_key = _dbWorker.Users.GetAPI_Key(Login, PasswordHasher.GetHash(Password));
                if (api_key != null)
                {
                    SwitchTo(GetMainWindow(api_key));
                }
                else
                {
                    InfoMessage = INCORRECTLY;
                }
            }
            else
            {
                InfoMessage = FIELDS_EMPTY;
            }
        }
        #endregion

        #region SwitchToRegistrationCommand
        public ICommand SwitchToRegistrationCommand { get; }
        private bool CanSwitchToRegistrationCommandExecute(object p) => true;
        private void OnSwitchToRegistrationCommandExecuted(object p)
        {
            SwitchTo(new Registration());
        }
        #endregion

        #endregion

        public LoginViewModel()
        {
            #region Команды
            CloseCommand = new LambdaCommand(OnCloseCommandExecuted, CanCloseCommandExecute);
            MinimizedCommand = new LambdaCommand(OnMinimizedCommandExecuted, CanMinimizedCommandExecute);

            LoginCommand = new LambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);

            SwitchToRegistrationCommand = new LambdaCommand(OnSwitchToRegistrationCommandExecuted, CanSwitchToRegistrationCommandExecute);
            #endregion

            _dbWorker = new UnitOfWork();
        }

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Login);
        }

        private MainWindow GetMainWindow(API_Key api_key)
        {
            MainWindow clientMainWindow = new MainWindow(api_key);

            return clientMainWindow;
        }
    }
}
