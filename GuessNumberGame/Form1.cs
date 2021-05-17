using System;
using System.Drawing;
using System.Windows.Forms;

namespace GuessNumberGame
{
    public partial class Form1 : Form
    {
        Random randomNumbers = new Random(); // random-number generator
        int _numberPlayed;
        int _numberGuessed;
        int _guessDifferenceNew;
        int _guessDifferenceOld;

        public Form1()
        {
            InitializeComponent();
            StartGame();                    // starts game on launch
        }

        private void StartGame()
        {
            this.BackColor = SystemColors.Control;      // reset background color
            textInput.Enabled = true;                   // enable guess input
            _numberPlayed = randomNumbers.Next(1, 1000); // pick a number to play
            textOutput.Text = "I have a number between 1 and 1000--can you guess my number?";
            textOutput.AppendText("\r\nPlease enter your first guess.");
            //textOutput.AppendText($" Hint for testing: {numberPlayed}.");
            this.ActiveControl = textInput;
            _guessDifferenceOld = 0;
        }

        private void buttonGuess_Click(object sender, EventArgs e)
        {
            _numberGuessed = int.Parse(textInput.Text);

            // tell the player how the guess compares
            if (_numberGuessed < _numberPlayed)
            {
                textOutput.AppendText($"\r\n{_numberGuessed} is Too Low");
                // determine guess (positive) difference
                _guessDifferenceNew = _numberPlayed - _numberGuessed;
            }
            else if (_numberGuessed > _numberPlayed)
            {
                textOutput.AppendText($"\r\n{_numberGuessed} is Too High");
                // determine guess (positive) difference
                _guessDifferenceNew = _numberGuessed - _numberPlayed;
            }
            else if (_numberGuessed == _numberPlayed)
            {
                this.BackColor = Color.Green;
                textInput.Enabled = false;
                DialogResult result = MessageBox.Show("Play again?", 
                    "Correct!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation, 
                    MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                { StartGame(); }           // start new game
                else
                { Application.Exit(); }    // exit game
            }

            // check if we're getting warmer (ignore if 1st try)
            if (_guessDifferenceNew < _guessDifferenceOld)
            { this.BackColor = Color.Red; }       // warmer
            else if (_guessDifferenceOld > 0 && _guessDifferenceNew > _guessDifferenceOld)
            { this.BackColor = Color.Blue; }      // colder

            _guessDifferenceOld = _guessDifferenceNew;    // remember last guess difference
            textInput.Clear();                          // get ready for new input
        }
    }
}
