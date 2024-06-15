using Microsoft.Win32;
using OptimaFarm_TestTask.Commands.Base;
using OptimaFarm_TestTask.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using OptimaFarm_TestTask.Helpers;
using OptimaFarm_TestTask.Models;

namespace OptimaFarm_TestTask.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        
        public ObservableCollection<Emploee> emploees = new ObservableCollection<Emploee>();
        private Emploee _selectedEmploee ;
        private Emploee _tempEmploee = new Emploee() { IsActive=true, EmploymentDate=DateTime.Today};
        private string _searchFilter;

        #region commands

        public ICommand AddItem { get; }
        public ICommand DeleteItem { get; }
        public ICommand UpdateItem { get; }
        public ICommand ItemClicked { get; }
        public ICommand SerialyzeButtonClicked {  get; }
        public ICommand DeserialyzeButtonCliced {  get; }
        public ICommand ShowStatsButtonClicked { get; }




        private void CreateNewEmploee(object e)
        {
            if(!ValidationBeforeSaving())
                return;
            
            _tempEmploee._id =emploees.Count>0? emploees.Max(a => a._id)+1:1;

            emploees.Add(new Emploee(_tempEmploee));
            OnPropertyChanged("Emploees");
            SelectedEmploee = emploees.LastOrDefault();
        }
        private void DeleteSelectedEmploee(object e)
        {
            if (_selectedEmploee == null)
                MessageBox.Show("Select an employee before deleting");
            else if (!emploees.Contains(_selectedEmploee))
                MessageBox.Show("It looks like the user has already been deleted");
            else
            {
                string message = "Confirm deletion of the employee card\n" +
                    $"ID:{SelectedEmploee._id}\n {SelectedEmploee.FirstName} {SelectedEmploee.LastName} {SelectedEmploee.SecondName}";
                var result = MessageBox.Show(message, "Important!", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    emploees.Remove(_selectedEmploee);
                    _selectedEmploee= null;
                    OnPropertyChanged("Emploees");
                }
            }
        }
        private void ApplyChangesToEmploee(object e)
        {
            if (_selectedEmploee == null)
            {
                MessageBox.Show("It is impossible to update the card because no employee has been selected.\nPlease use the Create button to add an employee.");
                return;
            }

            if (!ValidationBeforeSaving())
                return;

            int index = emploees.IndexOf(_selectedEmploee);
            if (index == -1)
            {
                MessageBox.Show("It looks like the employee card has been deleted, so the record cannot be updated.\nPlease use the add button to create a new card.");
                return;
            }            
            emploees[index] = SelectedEmploee = new Emploee(_tempEmploee);
            OnPropertyChanged("Emploees");
        }
        private void SelectItem(object e)
        {
            if(e is Emploee) 
            SelectedEmploee = (Emploee)e;
        }
        private void SerialyzeToXML(object e) 
        {
            if(emploees==null|| emploees.Count()==0)
            {
                MessageBox.Show("There is nothing to save yet :(");
                return;
            }
            
            string fileName = string.Empty;
            if (!FileHelper.GetWhereToSave(ref fileName)) return;

            string xml = CompactXMLSerialyzer.GetXMLFromObject(emploees);

            FileHelper.WriteToFile(xml, fileName);

        }

        

        

        private  void GetFromXML(object e) 
        {
            string fileName = string.Empty;

            if(!FileHelper.GetFromWhereToLoad(ref fileName)) return;

            if (emploees == null)
                emploees = new ObservableCollection<Emploee>();            
            ObservableCollection<Emploee> oc = new ObservableCollection<Emploee>();

            string xml = FileHelper.ReadFromFile(fileName);

            
            if (CompactXMLParser.TryDeSerialyze(xml, ref oc))
            {
                foreach (var emploee in oc)
                    emploees.Add(emploee);
                OnPropertyChanged("Emploees");
            }
            else MessageBox.Show("Sorry! Error occured while try to read");


        }

       

        private void ShowStats(object e) 
        { 
            DefaultStats stats = new DefaultStats(emploees);
            MessageBox.Show(stats.ToString(), "Statistics");
        }

        #endregion

        #region Properties

        public Emploee SelectedEmploee
        {
            get => _tempEmploee;
            set
            {
                _tempEmploee = new Emploee( value);
                SetProperty(ref _selectedEmploee, value);
            }
        }
        public IEnumerable<Emploee> Emploees 
        {
            get
            {
                return emploees.Where(e => e.Contains(_searchFilter));
            }
            
        }
        public string SearchFilter 
        {
            get => _searchFilter; 
            set 
            { 
                SetProperty(ref _searchFilter, value);

                OnPropertyChanged("Emploees");
            } 
        }
        #endregion


        public MainWindowViewModel()
        {
            
            AddItem = new Command(CreateNewEmploee);
            DeleteItem = new Command(DeleteSelectedEmploee);
            UpdateItem = new Command(ApplyChangesToEmploee);
            ItemClicked = new Command(SelectItem);
            SerialyzeButtonClicked = new Command(SerialyzeToXML);
            DeserialyzeButtonCliced = new Command(GetFromXML);
            ShowStatsButtonClicked = new Command(ShowStats);
        }
        
        private bool ValidationBeforeSaving()
        {
            
            bool cv = _tempEmploee.Validate();
            if (!cv)
                MessageBox.Show("Please check fields values before saving. :(");
            return cv;
        }

        

    }
}
