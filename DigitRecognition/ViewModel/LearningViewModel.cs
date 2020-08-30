using System.Windows.Input;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DigitRecognition.ViewModel
{
    using DigitRecognition.ViewModel.Base;

    public class LearningViewModel : ViewModelBase
    {
        #region Public Properties
        public ICommand NetworkLearning { get; set; }
        public ICommand StopLearning { get; set; }
        public CancellationToken Token { get; set; }
        public CancellationTokenSource TokenSource { get; set; }
        #endregion
        public LearningViewModel()
        {

            NetworkLearning = new RelayCommand(async () => 
            {
                TokenSource = new CancellationTokenSource();
                Token = TokenSource.Token;
                await Task.Run(() => StartLearning(Token),Token);
            });

            StopLearning = new RelayCommand(() =>
            {
                TokenSource.Cancel();
                Console.WriteLine("STOP");
            });

        }

        private void StartLearning(CancellationToken cancellationToken)
        {
            int i = 0;
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                Console.WriteLine(i);
                i++;
                Thread.Sleep(1000);
            }
        }

    }
}
