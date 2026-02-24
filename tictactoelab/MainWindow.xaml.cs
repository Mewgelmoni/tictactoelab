using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tictactoelab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char[,] board = new char[3, 3];
        private char currentPlayer = 'X';
        private bool gameEnded = false;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            // Очищаем массив состояния
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = '\0';

            currentPlayer = 'X';
            gameEnded = false;

            // Проверяем, что StatusLabel не равен null
            if (StatusLabel != null)
                StatusLabel.Content = "Ход: X";

            // Очищаем текст на всех кнопках
            ClearAllButtons();
        }

        private void ClearAllButtons()
        {
            // Очищаем каждую кнопку по имени
            Button00.Content = "";
            Button01.Content = "";
            Button02.Content = "";
            Button10.Content = "";
            Button11.Content = "";
            Button12.Content = "";
            Button20.Content = "";
            Button21.Content = "";
            Button22.Content = "";

            // Включаем все кнопки
            EnableAllButtons(true);
        }

        private void EnableAllButtons(bool enable)
        {
            Button00.IsEnabled = enable;
            Button01.IsEnabled = enable;
            Button02.IsEnabled = enable;
            Button10.IsEnabled = enable;
            Button11.IsEnabled = enable;
            Button12.IsEnabled = enable;
            Button20.IsEnabled = enable;
            Button21.IsEnabled = enable;
            Button22.IsEnabled = enable;
        }

        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded) return;

            Button clickedButton = sender as Button;
            if (clickedButton.Content != null && clickedButton.Content.ToString() != "")
                return; // Клетка уже занята

            // Получаем координаты из Tag
            string[] coordinates = clickedButton.Tag.ToString().Split(',');
            int row = int.Parse(coordinates[0]);
            int col = int.Parse(coordinates[1]);

            // Делаем ход
            board[row, col] = currentPlayer;
            clickedButton.Content = currentPlayer.ToString();

            // Проверка победы
            if (CheckForWin())
            {
                StatusLabel.Content = $"Игрок {currentPlayer} победил!";
                gameEnded = true;
                EnableAllButtons(false);
                return;
            }

            // Проверка на ничью
            if (IsBoardFull())
            {
                StatusLabel.Content = "Ничья!";
                gameEnded = true;
                return;
            }

            // Смена игрока
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            StatusLabel.Content = $"Ход: {currentPlayer}";
        }

        private bool CheckForWin()
        {
            // Проверка строк и столбцов
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                    return true;
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                    return true;
            }

            // Проверка диагоналей
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                return true;
            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == '\0')
                        return false;
            return true;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
