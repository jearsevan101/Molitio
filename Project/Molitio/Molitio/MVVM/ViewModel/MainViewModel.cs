using Molitio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molitio.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand NotebookViewCommand { get; set; }

        public RelayCommand ProductivityViewCommand { get; set; }

        public NotebookViewModel NoteVM { get; set; }

        public ProductivityViewModel ProdVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            NoteVM = new NotebookViewModel();
            ProdVM = new ProductivityViewModel();
            CurrentView = ProdVM;

            ProductivityViewCommand = new RelayCommand(o => {

                CurrentView = ProdVM;

            });

            NotebookViewCommand = new RelayCommand(o => { 

                CurrentView =NoteVM;
            
            });

            
        }

    }
}
