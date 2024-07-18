using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppYahtzee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int _players = 0;
        int _rollcounter = 0;
        int _playerturn = 1;
        int _turns = 0;
        int[] _numbers = { 0, 0, 0, 0, 0 };

        Random _myrandom = new Random();

        private void UpdatePlayerAmount(bool addorsubtract)
        {
            GetNumberOfPlayers();
            if (addorsubtract == true)
            {
                if (_players == 8)
                {
                    MessageBox.Show("The maximum amount of players is 8!");
                    return;
                }
                _players++;
            }
            else
            {
                if (_players == 2)
                {
                    MessageBox.Show("The minimum amount of players is 2!");
                    return;
                }
                _players--;
            }
            tbPlayerAmount.Text = _players.ToString();
        }

        private void GetNumberOfPlayers()
        {
            _players = Convert.ToInt32(tbPlayerAmount.Text);
        }

        private void CreateNameInserters()
        {
            StackPanel nameInserters = new StackPanel();
            nameInserters.HorizontalAlignment = HorizontalAlignment.Center;
            nameInserters.VerticalAlignment = VerticalAlignment.Center;
            myGrid.Children.Add(nameInserters);

            int counter = 0;
            while (counter < _players)
            {
                counter++;

                StackPanel player = new StackPanel();
                player.Orientation = Orientation.Horizontal;
                nameInserters.Children.Add(player);

                TextBlock playerName = new TextBlock();
                playerName.Width = 300;
                playerName.TextAlignment = TextAlignment.Center;
                playerName.Background = Brushes.White;
                playerName.FontSize = 25;
                playerName.FontWeight = FontWeights.Bold;
                playerName.Foreground = Brushes.Brown;
                playerName.Text = "Player " + counter + "'s name:";
                player.Children.Add(playerName);

                TextBox realName = new TextBox();
                realName.Name = "Name" + counter;
                realName.Width = 200;
                realName.TextAlignment = TextAlignment.Center;
                realName.FontSize = 20;

                player.Children.Add(realName);
            }
            Button insertNames = new Button();
            insertNames.Height = 50;
            insertNames.FontSize = 22;
            insertNames.FontWeight = FontWeights.Black;
            insertNames.Background = Brushes.Gold;
            insertNames.Content = "Insert names";
            insertNames.Click += CreateScoreSheet_Click;

            nameInserters.Children.Add(insertNames);
        }

        private void ResetDices()
        {
            _rollcounter = 0;
            tbRolls.Text = "0";
            btRoll.Visibility = Visibility.Visible;

            int counter = 0;
            while (counter < _numbers.Length)
            {
                _numbers[counter] = 0;
                counter++;
            }

            foreach (Image myImage in spDobbelstenen.Children)
            {
                myImage.Source = new BitmapImage(new Uri("C:/School/Funprojecten nieuw/WpfAppYahtzee/Assets/Dobbelsteendef.jpg"));
            }

            foreach (CheckBox mybox in cbDobbelstenen.Children)
            {
                mybox.IsChecked = false;
            }
        }

        private TextBlock createTitleBlocks(int kleur, bool lichtofvet, int width)
        {
            TextBlock myblock = new TextBlock();
            if (kleur == 0)
            {
                myblock.Background = Brushes.Cyan;
            }
            else if (kleur == 1)
            {
                myblock.Background = Brushes.LightCoral;
            }
            else if (kleur == 2)
            {
                myblock.Background = Brushes.Beige;
            }
            else
            {
                myblock.Background = Brushes.LightGreen;
            }
            myblock.Width = width;
            myblock.Height = 30;
            myblock.FontSize = 20;
            if (lichtofvet == true)
            {
                myblock.FontWeight = FontWeights.Bold;
            }
            myblock.TextAlignment = TextAlignment.Center;
            return myblock;
        }

        private Button createScoreField()
        {
            Button mybutton = new Button();
            mybutton.Background = Brushes.White;
            mybutton.Width = 122;
            mybutton.Height = 30;
            mybutton.FontSize = 18;

            return mybutton;
        }

        private void btUp_Click(object sender, RoutedEventArgs e)
        {
            bool addorsubtract = true;
            UpdatePlayerAmount(addorsubtract);
        }

        private void btDown_Click(object sender, RoutedEventArgs e)
        {
            bool addorsubtract = false;
            UpdatePlayerAmount(addorsubtract);
        }

        private void btInsertPlayers_Click(object sender, RoutedEventArgs e)
        {
            GetNumberOfPlayers();
            spStartInformation.Visibility = Visibility.Hidden;
            CreateNameInserters();
            _turns = _players * 13;
        }

        private void CreateScoreSheet_Click(object sender, RoutedEventArgs e)
        {
            spDobbelstenen.Visibility = Visibility.Visible;
            cbDobbelstenen.Visibility = Visibility.Visible;
            btRoll.Visibility = Visibility.Visible;
            tbRolls.Visibility = Visibility.Visible;

            Button pressed = e.Source as Button;
            StackPanel grandparent = (StackPanel)pressed.Parent;
            int counter = 0;

            StackPanel wholeScoreSheet = new StackPanel();
            wholeScoreSheet.Name = "Wholesheet";
            wholeScoreSheet.Orientation = Orientation.Horizontal;
            wholeScoreSheet.Opacity = 1;
            wholeScoreSheet.HorizontalAlignment = HorizontalAlignment.Center;
            wholeScoreSheet.VerticalAlignment = VerticalAlignment.Center;

            StackPanel titlesScoreSheet = new StackPanel();
            wholeScoreSheet.Children.Add(titlesScoreSheet);

            TextBlock nothing = createTitleBlocks(0, true, 200);
            nothing.Text = "###";
            titlesScoreSheet.Children.Add(nothing);

            TextBlock ones = createTitleBlocks(0, false, 200);
            ones.Text = "Ones";
            titlesScoreSheet.Children.Add(ones);

            TextBlock twos = createTitleBlocks(0, false, 200);
            twos.Text = "Twos";
            titlesScoreSheet.Children.Add(twos);

            TextBlock threes = createTitleBlocks(0, false, 200);
            threes.Text = "Threes";
            titlesScoreSheet.Children.Add(threes);

            TextBlock fours = createTitleBlocks(0, false, 200);
            fours.Text = "Fours";
            titlesScoreSheet.Children.Add(fours);

            TextBlock fives = createTitleBlocks(0, false, 200);
            fives.Text = "Fives";
            titlesScoreSheet.Children.Add(fives);

            TextBlock sixes = createTitleBlocks(0, false, 200);
            sixes.Text = "Sixes";
            titlesScoreSheet.Children.Add(sixes);

            TextBlock totalHalf1 = createTitleBlocks(0, true, 200);
            totalHalf1.Text = "Total score";
            titlesScoreSheet.Children.Add(totalHalf1);

            TextBlock add35 = createTitleBlocks(0, true, 200);
            add35.Text = "Bonus";
            titlesScoreSheet.Children.Add(add35);

            TextBlock totalScoreH1 = createTitleBlocks(0, true, 200);
            totalScoreH1.Text = "Total half 1";
            titlesScoreSheet.Children.Add(totalScoreH1);

            TextBlock threeOfaKind = createTitleBlocks(1, false, 200);
            threeOfaKind.Text = "Three of a kind";
            titlesScoreSheet.Children.Add(threeOfaKind);

            TextBlock fourOfaKind = createTitleBlocks(1, false, 200);
            fourOfaKind.Text = "Four of a kind";
            titlesScoreSheet.Children.Add(fourOfaKind);

            TextBlock fullHouse = createTitleBlocks(1, false, 200);
            fullHouse.Text = "Full house";
            titlesScoreSheet.Children.Add(fullHouse);

            TextBlock smallStraight = createTitleBlocks(1, false, 200);
            smallStraight.Text = "Small straight";
            titlesScoreSheet.Children.Add(smallStraight);

            TextBlock largeStraight = createTitleBlocks(1, false, 200);
            largeStraight.Text = "Large straight";
            titlesScoreSheet.Children.Add(largeStraight);

            TextBlock yahtzee = createTitleBlocks(1, false, 200);
            yahtzee.Text = "Yahtzee";
            titlesScoreSheet.Children.Add(yahtzee);

            TextBlock chance = createTitleBlocks(1, false, 200);
            chance.Text = "Chance";
            titlesScoreSheet.Children.Add(chance);

            TextBlock totalHalf2 = createTitleBlocks(1, true, 200);
            totalHalf2.Text = "Total half 2";
            titlesScoreSheet.Children.Add(totalHalf2);

            TextBlock totalHalf1reload = createTitleBlocks(1, true, 200);
            totalHalf1reload.Text = "Total half 1";
            titlesScoreSheet.Children.Add(totalHalf1reload);

            TextBlock grandtotal = createTitleBlocks(3, true, 200);
            grandtotal.Text = "Grand total";
            titlesScoreSheet.Children.Add(grandtotal);

            foreach (StackPanel parent in grandparent.Children.OfType<StackPanel>())
            {
                foreach (TextBox playername in parent.Children.OfType<TextBox>())
                {
                    counter++;
                    StackPanel playerScoreSheet = new StackPanel();
                    if (counter == 1)
                    {
                        playerScoreSheet.Opacity = 1;
                    }
                    else
                    {
                        playerScoreSheet.Opacity = 0.5;
                    }

                    TextBlock playersname = createTitleBlocks(2, true, 120);
                    playersname.Width = 120;
                    playersname.Text = playername.Text;
                    playerScoreSheet.Children.Add(playersname);

                    Button enen = createScoreField();
                    enen.Name = "Ones";
                    enen.Click += Enen_Click;
                    playerScoreSheet.Children.Add(enen);

                    Button tweeen = createScoreField();
                    tweeen.Name = "Twos";
                    tweeen.Click += Tweeen_Click;
                    playerScoreSheet.Children.Add(tweeen);

                    Button drieen = createScoreField();
                    drieen.Name = "Threes";
                    drieen.Click += Drieen_Click;
                    playerScoreSheet.Children.Add(drieen);

                    Button vieren = createScoreField();
                    vieren.Name = "Fours";
                    vieren.Click += Vieren_Click;
                    playerScoreSheet.Children.Add(vieren);

                    Button vijven = createScoreField();
                    vijven.Name = "Fives";
                    vijven.Click += Vijven_Click;
                    playerScoreSheet.Children.Add(vijven);

                    Button zessen = createScoreField();
                    zessen.Name = "Sixes";
                    zessen.Click += Zessen_Click;
                    playerScoreSheet.Children.Add(zessen);

                    TextBlock scoreHelft1 = createTitleBlocks(0, true, 120);
                    scoreHelft1.Name = "TotalHalf1";
                    scoreHelft1.Text = "0";
                    playerScoreSheet.Children.Add(scoreHelft1);

                    TextBlock bonus = createTitleBlocks(0, true, 120);
                    bonus.Name = "add35";
                    playerScoreSheet.Children.Add(bonus);

                    TextBlock totaalscoreHelft1 = createTitleBlocks(0, true, 120);
                    totaalscoreHelft1.Name = "GrandTotalHalf1";
                    totaalscoreHelft1.Text = "0";
                    playerScoreSheet.Children.Add(totaalscoreHelft1);

                    Button driegelijke = createScoreField();
                    driegelijke.Name = "Threeofakind";
                    driegelijke.Click += Driegelijke_Click;
                    playerScoreSheet.Children.Add(driegelijke);

                    Button viergelijke = createScoreField();
                    viergelijke.Name = "Fourofakind";
                    viergelijke.Click += Viergelijke_Click;
                    playerScoreSheet.Children.Add(viergelijke);

                    Button volhuis = createScoreField();
                    volhuis.Name = "Fullhouse";
                    volhuis.Click += Volhuis_Click;
                    playerScoreSheet.Children.Add(volhuis);

                    Button kleinstraat = createScoreField();
                    kleinstraat.Name = "Smallstraight";
                    kleinstraat.Click += Kleinstraat_Click;
                    playerScoreSheet.Children.Add(kleinstraat);

                    Button grootstraat = createScoreField();
                    grootstraat.Name = "Largestraight";
                    grootstraat.Click += Grootstraat_Click;
                    playerScoreSheet.Children.Add(grootstraat);

                    Button YATZEE = createScoreField();
                    YATZEE.Name = "Yahtzee";
                    YATZEE.Click += YATZEE_Click;
                    playerScoreSheet.Children.Add(YATZEE);

                    Button vrijekeus = createScoreField();
                    vrijekeus.Name = "Chance";
                    vrijekeus.Click += Vrijekeus_Click;
                    playerScoreSheet.Children.Add(vrijekeus);

                    TextBlock totaalscoreHelft2 = createTitleBlocks(1, true, 120);
                    totaalscoreHelft2.Name = "GrandTotalHalf2";
                    totaalscoreHelft2.Text = "0";
                    playerScoreSheet.Children.Add(totaalscoreHelft2);

                    TextBlock totaalscoreHelft1herh = createTitleBlocks(1, true, 120);
                    totaalscoreHelft1herh.Name = "GrandTotalHalf1repeat";
                    totaalscoreHelft1herh.Text = "0";
                    playerScoreSheet.Children.Add(totaalscoreHelft1herh);

                    TextBlock grandtotaal = createTitleBlocks(3, true, 120);
                    grandtotaal.Name = "Grandtotal";
                    grandtotaal.Text = "0";
                    playerScoreSheet.Children.Add(grandtotaal);

                    wholeScoreSheet.Children.Add(playerScoreSheet);
                }
            }

            myGrid.Children.Add(wholeScoreSheet);
            grandparent.Visibility = Visibility.Hidden;
        }

        private void NextTurn()
        {
            foreach (StackPanel stackPanel in myGrid.Children.OfType<StackPanel>())
            {
                if (stackPanel.Name == "Wholesheet")
                {
                    int counter = 0;
                    bool resettedActive = false;
                    bool fromlasttofirst = false;

                    foreach (StackPanel playerPanel in stackPanel.Children.OfType<StackPanel>())
                    {
                        if (counter > 0 && _playerturn != _players && fromlasttofirst == false)
                        {
                            if (resettedActive == true)
                            {
                                playerPanel.Opacity = 1;
                                _playerturn++;
                                return;
                            }

                            if (playerPanel.Opacity == 1)
                            {
                                playerPanel.Opacity = 0.5;
                                resettedActive = true;
                            }
                        }
                        else if (_playerturn == _players)
                        {
                            fromlasttofirst = true;

                            if (playerPanel.Opacity == 1 && counter != 0)
                            {
                                playerPanel.Opacity = 0.5;
                            }

                            if (counter == 1)
                            {
                                playerPanel.Opacity = 1;
                            }
                        }
                        counter++;
                    }
                    if (fromlasttofirst == true)
                    {
                        _playerturn = 1;
                    }
                }
            }
        }

        private void rolldice_Click(object sender, RoutedEventArgs e)
        {
            _rollcounter++;
            tbRolls.Text = _rollcounter.ToString();

            bool[] roll = new bool[5];
            int counter = 0;

            foreach (CheckBox mybox in cbDobbelstenen.Children)
            {
                if (mybox.IsChecked == true)
                {
                    roll[counter] = true;
                }
                else
                {
                    roll[counter] = false;
                }
                counter++;
            }

            counter = 0;

            foreach (Image myimage in spDobbelstenen.Children)
            {
                int getal = _myrandom.Next(1, 7);

                if (roll[counter] == false)
                {
                    if (getal == 1)
                    {
                        myimage.Source = new BitmapImage(new Uri("C:/School/Funprojecten nieuw/WpfAppYahtzee/Assets/Dobbelsteen1.jpg"));
                    }
                    else if (getal == 2)
                    {
                        myimage.Source = new BitmapImage(new Uri("C:/School/Funprojecten nieuw/WpfAppYahtzee/Assets/Dobbelsteen2.jpg"));
                    }
                    else if (getal == 3)
                    {
                        myimage.Source = new BitmapImage(new Uri("C:/School/Funprojecten nieuw/WpfAppYahtzee/Assets/Dobbelsteen3.jpg"));
                    }
                    else if (getal == 4)
                    {
                        myimage.Source = new BitmapImage(new Uri("C:/School/Funprojecten nieuw/WpfAppYahtzee/Assets/Dobbelsteen4.jpg"));
                    }
                    else if (getal == 5)
                    {
                        myimage.Source = new BitmapImage(new Uri("C:/School/Funprojecten nieuw/WpfAppYahtzee/Assets/Dobbelsteen5.jpg"));
                    }
                    else if (getal == 6)
                    {
                        myimage.Source = new BitmapImage(new Uri("C:/School/Funprojecten nieuw/WpfAppYahtzee/Assets/Dobbelsteen6.jpg"));
                    }
                    _numbers[counter] = getal;
                }
                counter++;
            }

            if (_rollcounter >= 3)
            {
                btRoll.Visibility = Visibility.Hidden;
            }
        }

        private void Enen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int punten = TelPuntenHelft1(1);
            myButton.Content = punten.ToString();

            UpdateScore(punten, true, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Tweeen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int punten = TelPuntenHelft1(2);
            myButton.Content = punten.ToString();

            UpdateScore(punten, true, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Drieen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int punten = TelPuntenHelft1(3);
            myButton.Content = punten.ToString();

            UpdateScore(punten, true, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Vieren_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int punten = TelPuntenHelft1(4);
            myButton.Content = punten.ToString();

            UpdateScore(punten, true, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Vijven_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int punten = TelPuntenHelft1(5);
            myButton.Content = punten.ToString();

            UpdateScore(punten, true, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Zessen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int punten = TelPuntenHelft1(6);
            myButton.Content = punten.ToString();

            UpdateScore(punten, true, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Driegelijke_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Array.Sort(_numbers);

            int punten = 0;

            if ((_numbers[0] == _numbers[1] && _numbers[1] == _numbers[2]) || (_numbers[1] == _numbers[2] && _numbers[2] == _numbers[3]) || (_numbers[2] == _numbers[3] && _numbers[3] == _numbers[4]))
            {
                int counter = 0;
                while (counter < _numbers.Length)
                {
                    punten += _numbers[counter];
                    counter++;
                }
            }
            else
            {
                MessageBoxResult myResult = MessageBox.Show("Do you want to insert 0?", "Insert 0", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (myResult == MessageBoxResult.No)
                {
                    return;
                }
            }

            myButton.Content = punten.ToString();
            UpdateScore(punten, false, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Viergelijke_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Array.Sort(_numbers);
            int punten = 0;

            if ((_numbers[0] == _numbers[1] && _numbers[1] == _numbers[2] && _numbers[2] == _numbers[3]) || (_numbers[1] == _numbers[2] && _numbers[2] == _numbers[3] && _numbers[3] == _numbers[4]))
            {
                int counter = 0;

                while (counter < _numbers.Length)
                {
                    punten += _numbers[counter];
                    counter++;
                }
            }
            else
            {
                MessageBoxResult myResult = MessageBox.Show("Do you want to insert 0?", "Insert 0", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (myResult == MessageBoxResult.No)
                {
                    return;
                }
            }

            myButton.Content = punten.ToString();
            UpdateScore(punten, false, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Volhuis_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Array.Sort(_numbers);

            if ((_numbers[0] == _numbers[1] && _numbers[1] == _numbers[2] && _numbers[2] != _numbers[3] && _numbers[3] == _numbers[4]) || 
                (_numbers[0] == _numbers[1] && _numbers[1] != _numbers[2] && _numbers[2] == _numbers[3] && _numbers[3] == _numbers[4]))
            {
                myButton.Content = "25";
                UpdateScore(25, false, parent);
            }
            else
            {
                MessageBoxResult myResult = MessageBox.Show("Do you want to insert 0?", "Insert 0", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (myResult == MessageBoxResult.No)
                {
                    return;
                }
                myButton.Content = "0";
                UpdateScore(0, false, parent);
            }
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Kleinstraat_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool correctstraight = false;
            int counter = 0;
            int current = 1;
            int vorige = 0;
            int goedegetallen = 1;

            Array.Sort(_numbers);
            while (counter < 2)
            {
                try
                {
                    while (_numbers[vorige] + 1 == _numbers[current] && current < 5)
                    {
                        current++;
                        vorige++;
                        goedegetallen++;
                    }
                }
                catch (Exception)
                {
                    //It's ok!
                }

                current++;
                vorige++;
                counter++;
            }

            if (goedegetallen >= 4)
            {
                correctstraight = true;
            }
            else
            {
                correctstraight = false;
            }

            if (correctstraight == true)
            {
                myButton.Content = "30";
                UpdateScore(30, false, parent);
            }
            else
            {
                MessageBoxResult myResult = MessageBox.Show("Do you want to insert 0?", "Insert 0", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (myResult == MessageBoxResult.No)
                {
                    return;
                }
                myButton.Content = "0";
                UpdateScore(0, false, parent);
            }
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Grootstraat_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Array.Sort(_numbers);
            if (_numbers[0] + 1 == _numbers[1] && _numbers[1] + 1 == _numbers[2] && _numbers[2] + 1 == _numbers[3] && _numbers[3] + 1 == _numbers[4])
            {
                myButton.Content = "40";
                UpdateScore(40, false, parent);
            }
            else
            {
                MessageBoxResult myResult = MessageBox.Show("Do you want to insert 0?", "Insert 0", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (myResult == MessageBoxResult.No)
                {
                    return;
                }
                myButton.Content = "0";
                UpdateScore(0, false, parent);
            }
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void YATZEE_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_numbers[0] == _numbers[1] && _numbers[1] == _numbers[2] && _numbers[2] == _numbers[3] && _numbers[3] == _numbers[4])
            {
                myButton.Content = "50";
                UpdateScore(50, false, parent);
            }
            else
            {
                MessageBoxResult myResult = MessageBox.Show("Do you want to insert 0?", "Insert 0", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (myResult == MessageBoxResult.No)
                {
                    return;
                }
                myButton.Content = "0";
                UpdateScore(0, false, parent);
            }
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private void Vrijekeus_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDiceCounter() == false)
            {
                return;
            }
            Button myButton = e.Source as Button;
            StackPanel parent = (StackPanel)myButton.Parent;
            if (myButton.Content != null)
            {
                MessageBox.Show("This field already has a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (parent.Opacity != 1)
            {
                MessageBox.Show("It's not this player's turn!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int counter = 0;
            int punten = 0;
            while (counter < _numbers.Length)
            {
                punten += _numbers[counter];
                counter++;
            }
            myButton.Content = punten.ToString();

            UpdateScore(punten, false, parent);
            ResetDices();
            NextTurn();
            UpdateTurns();
        }

        private bool CheckDiceCounter()
        {
            bool mybool = true;
            if (_rollcounter == 0)
            {
                MessageBox.Show("Please roll the dice at least once!");
                mybool = false;
            }
            return mybool;
        }

        private int TelPuntenHelft1(int getal)
        {
            int punten = 0;
            int counter = 0;

            while (counter < _numbers.Length)
            {
                if (_numbers[counter] == getal)
                {
                    punten += getal;
                }
                counter++;
            }
            return punten;
        }

        private void UpdateScore(int punten, bool helft, StackPanel player)
        {
            int currentpoints = 0;
            int grandhalf1 = 0;
            int grandhalf2 = 0;
            int grandtotal = 0;
            bool add35 = false;

            foreach (TextBlock myBlock in player.Children.OfType<TextBlock>())
            {
                if (helft == true)
                {
                    if (myBlock.Name == "TotalHalf1")
                    {
                        currentpoints = Int32.Parse(myBlock.Text);
                        currentpoints += punten;
                        myBlock.Text = currentpoints.ToString();

                        if (currentpoints >= 63)
                        {
                            add35 = true;
                        }
                    }

                    if (myBlock.Name == "add35" && add35 == true)
                    {
                        myBlock.Text = "35";
                    }

                    if (myBlock.Name == "GrandTotalHalf1")
                    {
                        if (add35 == true)
                        {
                            currentpoints += 35;
                        }
                        myBlock.Text = currentpoints.ToString();
                    }

                    if (myBlock.Name == "GrandTotalHalf1repeat")
                    {
                        myBlock.Text = currentpoints.ToString();
                    }
                }
                else
                {
                    if (myBlock.Name == "GrandTotalHalf2")
                    {
                        currentpoints = Int32.Parse(myBlock.Text);
                        currentpoints += punten;
                        myBlock.Text = currentpoints.ToString();
                    }
                }

                if (myBlock.Name == "GrandTotalHalf1repeat")
                {
                    grandhalf1 = Int32.Parse(myBlock.Text);
                }

                if (myBlock.Name == "GrandTotalHalf2")
                {
                    grandhalf2 = Int32.Parse(myBlock.Text);
                }

                if (myBlock.Name == "Grandtotal")
                {
                    grandtotal = grandhalf1 + grandhalf2;
                    myBlock.Text = grandtotal.ToString();
                }
            }
        }

        private void UpdateTurns()
        {
            _turns--;
            if (_turns == 0)
            {
                Ending();
            }
        }

        private void Ending()
        {
            MessageBox.Show("Game over");
        }
    }
}
