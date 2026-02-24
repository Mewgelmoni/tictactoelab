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
        private char currentPlayer;
        private bool gameEnded = false;

        // Новые переменные для второй версии
        private char playerXSymbol = 'X';
        private char playerOSymbol = 'O';
        private int scoreX = 0;
        private int scoreO = 0;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
            UpdateScoreDisplay();
        }

        private void NewGame()
        {
            // Очищаем массив состояния
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = '\0';

            currentPlayer = playerXSymbol; // Используем кастомный символ
            gameEnded = false;

            if (StatusLabel != null)
                StatusLabel.Content = $"Ход: {currentPlayer}";

            // Очищаем все кнопки
            ClearAllButtons();
        }

        private void ClearAllButtons()
        {
            Button00.Content = "";
            Button01.Content = "";
            Button02.Content = "";
            Button10.Content = "";
            Button11.Content = "";
            Button12.Content = "";
            Button20.Content = "";
            Button21.Content = "";
            Button22.Content = "";

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
                return;

            string[] coordinates = clickedButton.Tag.ToString().Split(',');
            int row = int.Parse(coordinates[0]);
            int col = int.Parse(coordinates[1]);

            // Делаем ход текущим символом
            board[row, col] = currentPlayer;
            clickedButton.Content = currentPlayer.ToString();

            // Проверка победы
            if (CheckForWin())
            {
                StatusLabel.Content = $"Игрок {currentPlayer} победил!";
                gameEnded = true;
                EnableAllButtons(false);

                // Увеличиваем счет соответствующему игроку
                if (currentPlayer == playerXSymbol)
                    scoreX++;
                else
                    scoreO++;

                UpdateScoreDisplay();
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
            currentPlayer = (currentPlayer == playerXSymbol) ? playerOSymbol : playerXSymbol;
            StatusLabel.Content = $"Ход: {currentPlayer}";
        }

        private bool CheckForWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                    return true;
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                    return true;
            }

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

        // Новый метод для обновления отображения счета
        private void UpdateScoreDisplay()
        {
            ScoreXText.Text = $"{playerXSymbol}: {scoreX}";
            ScoreOText.Text = $"{playerOSymbol}: {scoreO}";
        }

        // Новый метод для применения кастомных символов
        private void ApplySymbolsButton_Click(object sender, RoutedEventArgs e)
        {
            string newX = SymbolXTextBox.Text.Trim();
            string newO = SymbolOTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(newX) && !string.IsNullOrEmpty(newO))
            {
                // Берем первый символ из каждого текстового поля
                playerXSymbol = newX[0];
                playerOSymbol = newO[0];

                // Обновляем отображение счета
                UpdateScoreDisplay();

                // Перезапускаем игру с новыми символами
                NewGame();
            }
            else
            {
                MessageBox.Show("Символы не могут быть пустыми!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
