using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ManagerSystem.Properties;
using System.Windows.Forms;
using ManagerSystem.Database.Tables;
using System.Windows.Documents;
using System.Data;
using System.Text;

namespace ManagerSystem
{
    /// <summary>
    /// Interaction logic for CadastroColaborador.xaml
    /// </summary>
    public partial class ColaboratorRegistration : Window
    {
        #region Fields

        /// <summary>
        /// Holds whether the colaborator that is been shown, was just added.
        /// </summary>
        private bool justAdded = false;

        /// <summary>
        /// Hols whether the colaborator taht is been shown, was just loaded.
        /// </summary>
        private bool justLoaded = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes anew instance of CadastroColaborador
        /// </summary>
        public ColaboratorRegistration()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new colaborator registry.
        /// </summary>
        private void AddRegistry()
        {
            bool hasAccessControl = !string.IsNullOrWhiteSpace(userEntry.Text) && !string.IsNullOrWhiteSpace(passwordEntry.Password);
            bool atLeastOneModule = Convert.ToBoolean(moduleCb_1.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_2.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_3.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_4.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_5.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_6.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_7.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_8.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_9.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_10.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_11.IsChecked) ||
                                    Convert.ToBoolean(moduleCb_12.IsChecked);

            DataRow dt = AccessControl.SelectByUserName(userEntry.Text);

            if (dt == null)
            {
                dt = Colaborator.SelectColaborator(Colaborator.RgColumnName, Convert.ToInt64(rgEntry.Text));

                if (dt == null)
                {
                    dt = Colaborator.SelectColaborator(Colaborator.CpfColumnName, Convert.ToInt64(RemoveSpecialCharacteres(cpfEntry.Text)));

                    if (dt == null)
                    {
                        dt = Colaborator.SelectColaborator(Colaborator.CtpsColumnName, Convert.ToInt64(ctpsEntry.Text));

                        if (dt == null)
                        {
                            if (!string.IsNullOrEmpty(cnhEntry.Text))
                                dt = Colaborator.SelectColaborator(Colaborator.CnhColumnName, Convert.ToInt64(cnhEntry.Text));

                            if (dt == null)
                            {
                                if (!hasAccessControl || !atLeastOneModule || AccessControl.Insert(userEntry.Text, passwordEntry.Password, GetAllowedModules()))
                                {
                                    if (dt == null)
                                    {
                                        dt = AccessControl.SelectByUserName(userEntry.Text);

                                        string address = null;

                                        if (!string.IsNullOrWhiteSpace(addressEntry.Text))
                                        {
                                            StringBuilder sbAddress = new StringBuilder(string.Format("{0}, {1}", addressEntry.Text, numberEntry.Text));

                                            if (!string.IsNullOrWhiteSpace(complementEntry.Text))
                                                sbAddress.Append(string.Format(", {0}", complementEntry.Text));

                                            sbAddress.Append(string.Format(", {0}", RemoveSpecialCharacteres(cepEntry.Text)));
                                            sbAddress.Append(string.Format(", {0}", neighborhoodEntry.Text));
                                            sbAddress.Append(string.Format(", {0}", cityEntry.Text));
                                            sbAddress.Append(string.Format(", {0}", stateEntry.Text));
                                            sbAddress.Append(string.Format(", {0}", countryEntry.Text));

                                            address = sbAddress.ToString();
                                        }

                                        string sFormation = new TextRange(formationEntry.Document.ContentStart, formationEntry.Document.ContentEnd).Text;
                                        string sExperience = new TextRange(experienceEntry.Document.ContentStart, experienceEntry.Document.ContentEnd).Text;

                                        string fkAccessControl = dt != null ? dt[AccessControl.PrimaryKeyColumnName].ToString() : null;

                                        long pkCode = Colaborator.Insert(nameEntry.Text, sexCbox.Text, birthDateEntry.Text, rgEntry.Text,
                                                                         RemoveSpecialCharacteres(cpfEntry.Text), cnhEntry.Text, cnhCategoryCb.Text,
                                                                         address, RemoveSpecialCharacteres(privatePhoneEntry.Text),
                                                                         RemoveSpecialCharacteres(residencialPhoneEntry.Text), emailEntry.Text,
                                                                         nationalityEntry.Text, ctpsEntry.Text, positionEntry.Text,
                                                                         admissionDateEntry.Text.Replace("_", string.Empty),
                                                                         validityEntry.Text.Replace("_", string.Empty).Replace("/", string.Empty),
                                                                         sFormation, sExperience, fkAccessControl);

                                        if (pkCode > 0)
                                        {
                                            System.Windows.MessageBox.Show(
                                                            Messages.ColaboratorCorrectlyAdded,
                                                            Messages.ColaboratorRegistry,
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Information);

                                            codeEntry.Text = pkCode.ToString("D6");
                                            addBtn.IsEnabled = false;
                                            justAdded = true;

                                            return;
                                        }

                                        System.Windows.MessageBox.Show(
                                                            Messages.ColaboratorNotInserted,
                                                            Messages.ColaboratorRegistry,
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Error);

                                        return;
                                    }

                                    if (dt[Colaborator.RgColumnName].ToString().Equals(rgEntry.Text))
                                        System.Windows.MessageBox.Show(
                                                            Messages.HasAlreadyExistsColaboratorWithRg,
                                                            Messages.ColaboratorRegistry,
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Error);
                                    else if (dt[Colaborator.CpfColumnName].ToString().Equals(cpfEntry.Text))
                                        System.Windows.MessageBox.Show(
                                                            Messages.HasAlreadyExistsColaboratorWithCpf,
                                                            Messages.ColaboratorRegistry,
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Error);
                                    else if (dt[Colaborator.CnhColumnName].ToString().Equals(cnhEntry.Text))
                                        System.Windows.MessageBox.Show(
                                                            Messages.HasAlreadyExistsColaboratorWithCnh,
                                                            Messages.ColaboratorRegistry,
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Error);
                                    else if (dt[Colaborator.CtpsColumnName].ToString().Equals(ctpsEntry.Text))
                                        System.Windows.MessageBox.Show(
                                                            Messages.HasAlreadyExistsColaboratorWithCtps,
                                                            Messages.ColaboratorRegistry,
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Error);

                                    return;
                                }

                                System.Windows.MessageBox.Show(
                                    Messages.AccessControlNotInserted,
                                    Messages.ColaboratorRegistry,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                                return;
                            }

                            System.Windows.MessageBox.Show(
                                    Messages.HasAlreadyExistsColaboratorWithCnh,
                                    Messages.ColaboratorRegistry,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                            return;
                        }

                        System.Windows.MessageBox.Show(
                                    Messages.HasAlreadyExistsColaboratorWithCtps,
                                    Messages.ColaboratorRegistry,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                        return;
                    }

                    System.Windows.MessageBox.Show(
                                    Messages.HasAlreadyExistsColaboratorWithCpf,
                                    Messages.ColaboratorRegistry,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    return;
                }

                System.Windows.MessageBox.Show(
                                    Messages.HasAlreadyExistsColaboratorWithRg,
                                    Messages.ColaboratorRegistry,
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                return;
            }

            System.Windows.MessageBox.Show(
                                Messages.UserAlreadyExists,
                                Messages.ColaboratorRegistry,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
        }

        /// <summary>
        /// Saves a colaborator registry.
        /// </summary>
        private void SaveRegistry()
        {
            addBtn.IsEnabled = false;
        }

        /// <summary>
        /// Gets what allowed modules check boxes were marked in user interface.
        /// </summary>
        /// <returns>Returns the value identifying the allowed modules.</returns>
        private ushort GetAllowedModules()
        {
            ushort allowedModules = 0;

            if (Convert.ToBoolean(moduleCb_1.IsChecked))
                allowedModules |= 0x01;

            if (Convert.ToBoolean(moduleCb_2.IsChecked))
                allowedModules |= (0x01 << 1);

            if (Convert.ToBoolean(moduleCb_3.IsChecked))
                allowedModules |= (0x01 << 2);

            if (Convert.ToBoolean(moduleCb_4.IsChecked))
                allowedModules |= (0x01 << 3);

            if (Convert.ToBoolean(moduleCb_5.IsChecked))
                allowedModules |= (0x01 << 4);

            if (Convert.ToBoolean(moduleCb_6.IsChecked))
                allowedModules |= (0x01 << 5);

            if (Convert.ToBoolean(moduleCb_7.IsChecked))
                allowedModules |= (0x01 << 6);

            if (Convert.ToBoolean(moduleCb_8.IsChecked))
                allowedModules |= (0x01 << 7);

            if (Convert.ToBoolean(moduleCb_9.IsChecked))
                allowedModules |= (0x01 << 8);

            if (Convert.ToBoolean(moduleCb_10.IsChecked))
                allowedModules |= (0x01 << 9);

            if (Convert.ToBoolean(moduleCb_11.IsChecked))
                allowedModules |= (0x01 << 10);

            if (Convert.ToBoolean(moduleCb_12.IsChecked))
                allowedModules |= (0x01 << 11);

            return allowedModules;
        }

        /// <summary>
        /// Sets the allowed modules on user interface marking the repective check boxes.
        /// </summary>
        /// <param name="allowedModules">The value that indicates what allowed modules checkboxes was checked.</param>
        private void SetAllowedModules(ushort allowedModules)
        {
            if ((allowedModules & 0x01) > 0)
                moduleCb_1.IsChecked = true;

            if ((allowedModules & 0x02) > 0)
                moduleCb_2.IsChecked = true;

            if ((allowedModules & 0x04) > 0)
                moduleCb_3.IsChecked = true;

            if ((allowedModules & 0x08) > 0)
                moduleCb_4.IsChecked = true;

            if ((allowedModules & 0x10) > 0)
                moduleCb_5.IsChecked = true;

            if ((allowedModules & 0x20) > 0)
                moduleCb_6.IsChecked = true;

            if ((allowedModules & 0x40) > 0)
                moduleCb_7.IsChecked = true;

            if ((allowedModules & 0x80) > 0)
                moduleCb_8.IsChecked = true;

            if ((allowedModules & 0x100) > 0)
                moduleCb_9.IsChecked = true;

            if ((allowedModules & 0x200) > 0)
                moduleCb_10.IsChecked = true;

            if ((allowedModules & 0x400) > 0)
                moduleCb_11.IsChecked = true;

            if ((allowedModules & 0x800) > 0)
                moduleCb_12.IsChecked = true;
        }

        /// <summary>
        /// Removes special characters from an expression.
        /// </summary>
        /// <param name="expression">Expression which will be removed the characteres.</param>
        /// <returns>Returns the expression without the special characteres.</returns>
        private string RemoveSpecialCharacteres(string expression)
        {
            string charsToRemove = "-.()/_ ";

            foreach (char c in charsToRemove)
                expression = expression.Replace(c.ToString(), string.Empty);

            return expression;
        }

        /// <summary>
        /// Selects the colaborator photo
        /// </summary>
        private void SelectPhoto()
        {
            using (OpenFileDialog openImage = new OpenFileDialog())
            {
                openImage.Title = "Selecione a foto do colaborador:";
                openImage.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp";

                if (openImage.ShowDialog().Equals(System.Windows.Forms.DialogResult.OK))
                {
                    photoText.Visibility = Visibility.Collapsed;
                    photoField.Visibility = Visibility.Visible;
                    photoField.Source = new BitmapImage(new Uri(openImage.FileName));
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Name, RG and CPF textblocks' text changed event.
        /// </summary>
        /// <param name="sender">Object sender of event</param>
        /// <param name="e">Arguments of event</param>
        private void infoEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (findBtn != null)
                findBtn.IsEnabled = !string.IsNullOrWhiteSpace(nameEntry.Text) || 
                                    !string.IsNullOrWhiteSpace(rgEntry.Text) ||
                                    !string.IsNullOrWhiteSpace(cpfEntry.Text.Replace(".", string.Empty).Replace("-", string.Empty).Replace("_", string.Empty));

            if (addBtn != null)
            {
                addBtn.IsEnabled = !string.IsNullOrWhiteSpace(nameEntry.Text);

                if (addBtn.IsEnabled)
                {
                    if (justAdded || justLoaded)
                        addBtn.Content = Strings.SaveButton;
                }
            }
        }

        /// <summary>
        /// addBtn Click event.
        /// </summary>
        /// <param name="sender">Object sender of event.</param>
        /// <param name="e">Arguments of event.</param>
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            bool invalidCPF = false;
            bool invalidFormat = false;
            bool notAllowedBlankField = false;            
            MessageBoxResult msgBoxResult = MessageBoxResult.Yes;

            name.Foreground = new SolidColorBrush(Colors.Black);
            birthDate.Foreground = new SolidColorBrush(Colors.Black);
            rg.Foreground = new SolidColorBrush(Colors.Black);
            cpf.Foreground = new SolidColorBrush(Colors.Black);
            cnh.Foreground = new SolidColorBrush(Colors.Black);
            cnhCategory.Foreground = new SolidColorBrush(Colors.Black);
            ctps.Foreground = new SolidColorBrush(Colors.Black);
            position.Foreground = new SolidColorBrush(Colors.Black);
            admissionDate.Foreground = new SolidColorBrush(Colors.Black);
            number.Foreground = new SolidColorBrush(Colors.Black);
            cep.Foreground = new SolidColorBrush(Colors.Black);
            neighborhood.Foreground = new SolidColorBrush(Colors.Black);
            city.Foreground = new SolidColorBrush(Colors.Black);
            state.Foreground = new SolidColorBrush(Colors.Black);
            country.Foreground = new SolidColorBrush(Colors.Black);
            terminationDate.Foreground = new SolidColorBrush(Colors.Black);
            validity.Foreground = new SolidColorBrush(Colors.Black);

            if (string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                notAllowedBlankField = true;
                name.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(birthDateEntry.Text.Replace("/", string.Empty).Replace("_", string.Empty)))
            {
                notAllowedBlankField = true;
                birthDate.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(rgEntry.Text))
            {
                notAllowedBlankField = true;
                rg.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(cpfEntry.Text.Replace(".", string.Empty).Replace("-", string.Empty).Replace("_", string.Empty)))
            {
                notAllowedBlankField = true;
                cpf.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (!string.IsNullOrWhiteSpace(cnhEntry.Text) && cnhCategoryCb.SelectedValue == null)
            {
                notAllowedBlankField = true;
                cnhCategory.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (!string.IsNullOrWhiteSpace(addressEntry.Text))
            {
                if (string.IsNullOrWhiteSpace(numberEntry.Text))
                {
                    notAllowedBlankField = true;
                    number.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (string.IsNullOrWhiteSpace(cepEntry.Text.Replace("-", string.Empty).Replace("_", string.Empty)))
                {
                    notAllowedBlankField = true;
                    cep.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (string.IsNullOrWhiteSpace(neighborhoodEntry.Text))
                {
                    notAllowedBlankField = true;
                    neighborhood.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (string.IsNullOrWhiteSpace(cityEntry.Text))
                {
                    notAllowedBlankField = true;
                    city.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (string.IsNullOrWhiteSpace(stateEntry.Text))
                {
                    notAllowedBlankField = true;
                    state.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (string.IsNullOrWhiteSpace(countryEntry.Text))
                {
                    notAllowedBlankField = true;
                    country.Foreground = new SolidColorBrush(Colors.Red);
                }
            }

            if (string.IsNullOrWhiteSpace(ctpsEntry.Text))
            {
                notAllowedBlankField = true;
                ctps.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(positionEntry.Text))
            {
                notAllowedBlankField = true;
                position.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (string.IsNullOrWhiteSpace(admissionDateEntry.Text.Replace("/", string.Empty).Replace("_", string.Empty)))
            {
                notAllowedBlankField = true;
                admissionDate.Foreground = new SolidColorBrush(Colors.Red);
            }            

            if (!notAllowedBlankField)
            {
                DateTime birthDateTemp = new DateTime();
                if (!DateTime.TryParse(birthDateEntry.Text, out birthDateTemp))
                {
                    invalidFormat = true;
                    birthDate.Foreground = new SolidColorBrush(Colors.Red);
                }

                long rgNumberTemp = new long();
                if (!long.TryParse(rgEntry.Text , out rgNumberTemp))
                {
                    invalidFormat = true;
                    rg.Foreground = new SolidColorBrush(Colors.Red);
                }

                long cnhNumberTemp = new long();
                if (!string.IsNullOrWhiteSpace(cnhEntry.Text) && !long.TryParse(cnhEntry.Text, out cnhNumberTemp))
                {
                    invalidFormat = true;
                    cnh.Foreground = new SolidColorBrush(Colors.Red);
                }

                long ctpsNumberTemp = new long();
                if (!long.TryParse(ctpsEntry.Text, out ctpsNumberTemp))
                {
                    invalidFormat = true;
                    ctps.Foreground = new SolidColorBrush(Colors.Red);
                }

                DateTime admissionDateTemp = new DateTime();
                if (!DateTime.TryParse(admissionDateEntry.Text, out admissionDateTemp))
                {
                    invalidFormat = true;
                    admissionDate.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (!string.IsNullOrWhiteSpace(terminationDateEntry.Text.Replace("/", string.Empty).Replace("_", string.Empty)))
                {
                    DateTime terminationDateTemp = new DateTime();
                    if (!DateTime.TryParse(terminationDateEntry.Text, out terminationDateTemp))
                    {
                        invalidFormat = true;
                        terminationDate.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }

                if (!string.IsNullOrWhiteSpace(validityEntry.Text.Replace("/", string.Empty).Replace("_", string.Empty)))
                {
                    DateTime validityTemp = new DateTime();
                    if (!DateTime.TryParse(validityEntry.Text, out validityTemp))
                    {
                        invalidFormat = true;
                        validity.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }

                if (!invalidFormat)
                {
                    string cpfNumberStr = cpfEntry.Text.Replace(".", string.Empty).Replace("-", string.Empty).Replace("_", string.Empty);

                    invalidCPF = true;
                    if (cpfNumberStr.Length.Equals(11))
                    {
                        if (cpfNumberStr.Distinct().Count() > 0)
                        {
                            int firstDigit = (((Convert.ToInt32(cpfNumberStr[0].ToString()) * 10) +
                                               (Convert.ToInt32(cpfNumberStr[1].ToString()) * 9) +
                                               (Convert.ToInt32(cpfNumberStr[2].ToString()) * 8) +
                                               (Convert.ToInt32(cpfNumberStr[3].ToString()) * 7) +
                                               (Convert.ToInt32(cpfNumberStr[4].ToString()) * 6) +
                                               (Convert.ToInt32(cpfNumberStr[5].ToString()) * 5) +
                                               (Convert.ToInt32(cpfNumberStr[6].ToString()) * 4) +
                                               (Convert.ToInt32(cpfNumberStr[7].ToString()) * 3) +
                                               (Convert.ToInt32(cpfNumberStr[8].ToString()) * 2)) * 10) % 11;

                            int secondDigit = (((Convert.ToInt32(cpfNumberStr[0].ToString()) * 11) +
                                                (Convert.ToInt32(cpfNumberStr[1].ToString()) * 10) +
                                                (Convert.ToInt32(cpfNumberStr[2].ToString()) * 9) +
                                                (Convert.ToInt32(cpfNumberStr[3].ToString()) * 8) +
                                                (Convert.ToInt32(cpfNumberStr[4].ToString()) * 7) +
                                                (Convert.ToInt32(cpfNumberStr[5].ToString()) * 6) +
                                                (Convert.ToInt32(cpfNumberStr[6].ToString()) * 5) +
                                                (Convert.ToInt32(cpfNumberStr[7].ToString()) * 4) +
                                                (Convert.ToInt32(cpfNumberStr[8].ToString()) * 3) +
                                                (Convert.ToInt32(cpfNumberStr[9].ToString()) * 2)) * 10) % 11;

                            if (Convert.ToInt32(cpfNumberStr[9].ToString()).Equals(firstDigit) && Convert.ToInt32(cpfNumberStr[10].ToString()).Equals(secondDigit))
                                invalidCPF = false;
                        }
                    }

                    if (invalidCPF)
                    {
                        System.Windows.MessageBox.Show(
                                            Messages.CpfInvalidFormat,
                                            Messages.IncorrectInformation,
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error);

                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(addressEntry.Text) ||
                             string.IsNullOrWhiteSpace(numberEntry.Text) ||
                             string.IsNullOrWhiteSpace(cepEntry.Text.Replace("-", string.Empty).Replace("_", string.Empty)) ||
                             string.IsNullOrWhiteSpace(neighborhoodEntry.Text) ||
                             string.IsNullOrWhiteSpace(cityEntry.Text) ||
                             string.IsNullOrWhiteSpace(stateEntry.Text) ||
                             string.IsNullOrWhiteSpace(countryEntry.Text))
                    {
                        msgBoxResult = System.Windows.MessageBox.Show(
                                                            Messages.AddressBlankFileds,
                                                            string.Empty,
                                                            MessageBoxButton.YesNo,
                                                            MessageBoxImage.Exclamation);
                        
                    }
                    else if (string.IsNullOrWhiteSpace(userEntry.Text) ||
                             string.IsNullOrWhiteSpace(passwordEntry.Password))
                    {
                        msgBoxResult = System.Windows.MessageBox.Show(
                                                            Messages.ControlAccessBlankFields,
                                                            string.Empty,
                                                            MessageBoxButton.YesNo,
                                                            MessageBoxImage.Exclamation);
                    }
                    else if (moduleCb_1.IsChecked.Equals(false) && moduleCb_2.IsChecked.Equals(false) && moduleCb_3.IsChecked.Equals(false) &&
                             moduleCb_4.IsChecked.Equals(false) && moduleCb_5.IsChecked.Equals(false) && moduleCb_6.IsChecked.Equals(false) &&
                             moduleCb_7.IsChecked.Equals(false) && moduleCb_8.IsChecked.Equals(false) && moduleCb_9.IsChecked.Equals(false) &&
                             moduleCb_10.IsChecked.Equals(false) && moduleCb_11.IsChecked.Equals(false) && moduleCb_12.IsChecked.Equals(false))
                    {
                        msgBoxResult = System.Windows.MessageBox.Show(
                                                            Messages.NotChosenAllowedModules,
                                                            string.Empty,
                                                            MessageBoxButton.YesNo,
                                                            MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(
                                        Messages.InvalidFormatInformation,
                                        Messages.IncorrectInformation,
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show(
                                        Messages.NotAllowedBlankFileds,
                                        Messages.IncorrectInformation,
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);

                return;
            }

            if (msgBoxResult.Equals(MessageBoxResult.Yes))
            {
                if (addBtn.Content.Equals(Strings.AddButton))
                    AddRegistry();
                else if (addBtn.Content.Equals(Strings.SaveButton))
                    SaveRegistry();
            }
        }

        /// <summary>
        /// photoText textblock's double click event.
        /// </summary>
        /// <param name="sender">Object sender of event.</param>
        /// <param name="e">Arguments of event.</param>
        private void photoText_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectPhoto();
        }

        /// <summary>
        /// photoField textblock's mouse down event.
        /// </summary>
        /// <param name="sender">Object sender of event.</param>
        /// <param name="e">Arguments of event.</param>
        private void photoField_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Check if is double click
            if (e.ClickCount.Equals(2) && e.ChangedButton.Equals(MouseButton.Left))
            {
                SelectPhoto();
            }
        }

        /// <summary>
        /// findBtn button's click event.
        /// </summary>
        /// <param name="sender">Object sender of event.</param>
        /// <param name="e">Arguments of event.</param>
        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion
    }
}
