using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace Configuration
{
    class Model : INotifyPropertyChanged
    {
        #region command pattern implement

        public void WorklistServerAdd(object obj)
        {
            AddNetwork addNetwork = new AddNetwork();
            WorklistServer worklistServer = new WorklistServer();

            if (addNetwork.ShowDialog() == true)
            {
                try
                {
                    worklistServer.Alias = addNetwork.alias.Text;
                    worklistServer.CalledAeTitle = addNetwork.calledAeTitle.Text;
                    worklistServer.CallingAeTitle = addNetwork.callingAeTitle.Text;
                    worklistServer.IPAddress = addNetwork.iPAddress.Text;
                    worklistServer.Port = Convert.ToInt32(addNetwork.port.Text);
                    worklistServer.CharacterSet = addNetwork.characterSet.Text;

                   dataBase.WorklistServerTableProperty.Add(worklistServer);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public void StorageServerAdd(object obj)
        {
            AddNetwork addNetwork = new AddNetwork();
            StorageServer storageServer = new StorageServer();

            if (addNetwork.ShowDialog() == true)
            {
                try
                {
                    storageServer.Alias = addNetwork.alias.Text;
                    storageServer.CalledAeTitle = addNetwork.calledAeTitle.Text;
                    storageServer.CallingAeTitle = addNetwork.callingAeTitle.Text;
                    storageServer.IPAddress = addNetwork.iPAddress.Text;
                    storageServer.Port = Convert.ToInt32(addNetwork.port.Text);
                    storageServer.CharacterSet = addNetwork.characterSet.Text;

                   dataBase.StorageServerTableProperty .Add(storageServer);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public bool CanExecuteMethod(object arg)
        {
            return true;
        }


        public void ExecuteMethodSave(object obj)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(dataBase);
                string filePath = "configuration.json";

                using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    streamWriter.Write(jsonString);
                }
                MessageBox.Show("configuration.json file Saved");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public void ExecuteMethodLoad(object obj)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                // Nullable<bool> result = openFileDialog.ShowDialog();
                bool? result = openFileDialog.ShowDialog();
                string filePath = "";

                if (result == true)
                {
                    filePath = openFileDialog.FileName;
                }

                string jsonString;
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    jsonString = streamReader.ReadToEnd();
                }

                try
                {
                    dataBaseProperty = JsonConvert.DeserializeObject<DataBase>(jsonString);
                    MessageBox.Show("loaded");
                }
                catch(Exception e)
                {
                    string fileName = "DefaultConfiguration.json";
                     filePath = Path.GetFullPath(fileName);
                    using (StreamReader streamReader = new StreamReader(filePath))
                    {
                        jsonString = streamReader.ReadToEnd();
                    }
                    dataBaseProperty = JsonConvert.DeserializeObject<DataBase>(jsonString);
                   
                    MessageBox.Show(e.Message);
                    MessageBox.Show("default configuration loaded");
                }
                
                
                }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        #region link 

        DataBase dataBase = new DataBase();
       
        public DataBase dataBaseProperty
        {
            set
            {
                dataBase = value;
                NotifyPropertyChanged("dataBaseProperty");

            }
            get
            {
                return dataBase;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
             //if (this.PropertyChanged != null)
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }

    class Table
    {
        public ObservableCollection<StorageServer> storageServerTable = new ObservableCollection<StorageServer>();
        public ObservableCollection<WorklistServer> worklistServerTable = new ObservableCollection<WorklistServer>();
    }
    class DataBase
    {

        General general = new General();
        public General GeneralProperty
        {
            set
            {
                general = value;
            }
            get
            {
                return general;
            }
        }


        private StorageServer selectedStorageServer;
        public StorageServer SelectedStorageServerProperty
        {
            set
            {
                selectedStorageServer = value;
            }
            get
            {
                return selectedStorageServer;
            }
        }

        private WorklistServer selectedWorklistServer;
        public WorklistServer SelectedWorklistServerProperty
        {
            set
            {
                selectedWorklistServer = value;
            }
            get
            {
                return selectedWorklistServer;
            }
        }

        //  public ObservableCollection<StorageServer> storageServerTable = new ObservableCollection<StorageServer>();
        Table table = new Table();
        public ObservableCollection<StorageServer> StorageServerTableProperty
        {
            set
            {
                table.storageServerTable = value;
            }
            get
            {
                return table.storageServerTable;
            }
        }

       // public ObservableCollection<WorklistServer> worklistServerTable = new ObservableCollection<WorklistServer>();
        public ObservableCollection<WorklistServer> WorklistServerTableProperty
        {
            set
            {
               table. worklistServerTable = value;
            }
            get
            {
                return table. worklistServerTable;
            }
        }
       
    }

    #endregion

    #region data
    class General 
    {
        public LanguageEnum Language { set; get; }
        public ToothNoNotationEnum ToothNoNotation { get; set; }
        public DoseUnitEnum DoseUnit { set; get; }
        public string LocalStoragePath { set; get; }
        public string InstitutionName { set; get; }
        public string Telephone { get; set; } //= "test";
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Homepage { get; set; }
        public string Logo { get; set; }
        public ImageTypeEnum ImageType 
        {
            set;
            get; 
        }
        public double DefaultBrightness { get; set; }
        public double DefaultContrast { get; set; }
        public int WindowCenter { get; set; }
        public int WindowWidth { get; set; }
        public DeviceTypeEnum DeviceType { get; set; }
        public int ResolutionWidth { get; set; }
        public int ResolutionHeight { get; set; }
        public string CalibrationDataFilePath { get; set; }

    }

    class StorageServer
    {
        public string Alias { get; set; }
        public string CallingAeTitle { get; set; }
        public string CalledAeTitle { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string CharacterSet { get; set; }

    }

    class WorklistServer
    {
        public string Alias { get; set; }
        public string CallingAeTitle { get; set; }
        public string CalledAeTitle { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string CharacterSet { get; set; }
    }


    public enum LanguageEnum
    {
        English,
        Korean,

    }

    public enum ToothNoNotationEnum
    {
        FDI,
        Universal
    }

    public enum DoseUnitEnum
    {
        MGym2,
        Gycm2,
        DGycm2,
        UGym2,
        MGycm2
    }

    public enum ImageTypeEnum
    {
        Raw,
        EightBit
    }

    public enum DeviceTypeEnum
    {
        Fos,
        Gos


    }

    #endregion
}
