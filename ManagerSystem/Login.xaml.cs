using ManagerSystem.Database.Tables;
using ManagerSystem.Properties;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace ManagerSystem
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Inicializa uma nova instância da tela Login
        /// </summary>
        public Login()
        {
            InitializeComponent();
            userEntry.Focus();
        }

        #region Methods

        /// <summary>
        /// Valida os valores digitados nas entradas de controle de acesso.
        /// Se os dados correpoderem com dados cadastrados, libera o acesso ao sistema.
        /// Caso contrário, informa ao usuário que os dados estão incorretos.
        /// </summary>
        private void ValidateAccessControl()
        {
            DataRow aControl = AccessControl.SelectByUserName(userEntry.Text);

            if (aControl != null)
            {
                if (aControl[AccessControl.PasswordColumnName].ToString().Equals(passEntry.Password))
                {
                    ColaboratorRegistration cc = new ColaboratorRegistration();
                    cc.Show();

                    Close();
                }
                else
                {
                    loginMessage.Content = Messages.IncorrectPassword;
                    passEntry.Focus();
                }
            }
            else
            {
                loginMessage.Content = Messages.UserNotFound;
                userEntry.Focus();
            }

            loginMessage.Visibility = Visibility.Visible;
            passEntry.Password = string.Empty;
        }

        #endregion

        #region Events

        /// <summary>
        /// Evento de clique do botão Cancelar.
        /// </summary>
        /// <param name="sender">Objeto que gerou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Evento de leitura de tecla pressionada no cmapo de entrada de usuário.
        /// </summary>
        /// <param name="sender">Objeto que gerou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void userEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter) || e.Key.Equals(Key.Tab))
            {
                if (String.IsNullOrWhiteSpace(passEntry.Password))
                    passEntry.Focus();
                else
                    ValidateAccessControl();
            }
        }

        /// <summary>
        /// Evento de leitura de tecla pressionada no cmapo de entrada de senha.
        /// </summary>
        /// <param name="sender">Objeto que gerou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void passEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter) || e.Key.Equals(Key.Tab))
            {
                if (String.IsNullOrWhiteSpace(userEntry.Text))
                    userEntry.Focus();
                else
                    ValidateAccessControl();
            }
        }

        /// <summary>
        /// Evento de clique do botão OK.
        /// </summary>
        /// <param name="sender">Objeto que gerou o evento.</param>
        /// <param name="e">Argumentos do evento.</param>
        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(userEntry.Text))
            {
                ValidateAccessControl();
            }
            else
            {
                loginMessage.Content = Messages.BlankUserEntry;
                loginMessage.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}