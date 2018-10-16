using System.Windows;

namespace FilmLibrary.Core.Helpers
{
    public static class ConfirmationHelper
    {
        /// <summary>
        /// Ouvre une fenêtre pour demander confirmation à l'utilisateur sur l'action qu'il souhaite mener
        /// </summary>
        /// <param name="confirmationMessage">Message de confirmation à afficher</param>
        /// <param name="windowTitle">Titre de la fenêtre à afficher</param>
        /// <returns>Vrai si l'utilisateur à cliqué sur "OUI".</returns>
        public static bool ConfirmationYesNo(string confirmationMessage, string windowTitle)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(confirmationMessage, windowTitle, MessageBoxButton.YesNo);
            return messageBoxResult == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Ouvre une fenêtre pour afficher un message à l'utilisateur
        /// </summary>
        /// <param name="messageToDisplay">Message à afficher</param>
        /// <param name="windowTitle">Titre de la fenêtre à afficher</param>
        public static void ShowMessagePopup(string messageToDisplay, string windowTitle)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(messageToDisplay, windowTitle, MessageBoxButton.OK);
        }
    }
}
