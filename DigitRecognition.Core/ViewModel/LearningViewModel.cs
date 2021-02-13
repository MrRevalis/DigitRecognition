using System.Windows.Input;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitRecognition.Core
{
    public class LearningViewModel : ViewModelBase
    {
        #region Private
        private int epoch = 1;
        #endregion

        #region Public Properties
        public ICommand NetworkLearning { get; set; }
        public ICommand StopLearning { get; set; }
        public ICommand LoadLearningSource { get; set; }
        public ICommand LoadNetworkSource { get; set; }
        public ICommand CreateNewNetwork { get; set; }
        public ICommand ExportNetwork { get; set; }
        public CancellationToken Token { get; set; }
        public CancellationTokenSource TokenSource { get; set; }
        public string LearningProgress { get; set; } = "";
        public string NetworkLayers { get; set; }
        public bool IsRunning { get; set; } = false;
        public List<DataSet> LearningData { get; set; }
        #endregion

        #region Constructor
        public LearningViewModel()
        {
            NetworkLearning = new RelayCommand(async () =>
            {
                if(LearningData != null)
                {
                    if (IsRunning == false)
                    {
                        IsRunning = true;
                        Console.WriteLine("START");
                        TokenSource = new CancellationTokenSource();
                        Token = TokenSource.Token;
                        await Task.Run(() => StartLearning(Token));
                    }
                }
            });

            StopLearning = new RelayCommand(() =>
            {
                if(IsRunning == true)
                {
                    TokenSource.Cancel();
                    IsRunning = false;
                    Console.WriteLine("STOP");
                }
            });

            LoadLearningSource = new RelayCommand(() => LoadMaterial());
            LoadNetworkSource = new RelayCommand(() => LoadNetwork());
            CreateNewNetwork = new RelayCommand(() => NewNetwork());

        }
        #endregion

        #region Methods

        private void StartLearning(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                string message = $"Proba {epoch} " +IoC.Get<Network>().Learn(LearningData, cancellationToken);


                if(LearningProgress.Length >= 1)
                    LearningProgress += "\n" + message;
                else
                    LearningProgress += message;

                epoch++;

                Thread.Sleep(1000);
            }
        } 

        private void LoadMaterial()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            openFileDialog.Multiselect = true;

            if(openFileDialog.ShowDialog() == true)
            {
                FileInfo[] files = openFileDialog.FileNames.Select(y => new FileInfo(y)).ToArray();
                LearningData = new List<DataSet>();

                foreach(var x in files)
                {
                    LearningData.Add(new DataSet(x));
                }
                MessageBox.Show("Wczytano Dane");
            }
        }

        //Do zrobienia
        private void LoadNetwork()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.JSON)|*.JSON";

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
            }
        }
        #endregion

        private void NewNetwork()
        {
            epoch = 0;
            StopLearning.Execute(null);
            LearningProgress = String.Empty;

            if(NetworkLayers.Length > 1)
            {
                IoC.Get<Network>().ChangeNetworkSettings(NetworkLayers);
                MessageBox.Show("Utworzono");
                Console.WriteLine(IoC.Get<Network>().LearningRate);
            }
        }
    }
}