using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WorkTracker.Data;
using WorkTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkTracker.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly AddDbContext _context;

        public ObservableCollection<WorkStage> WorkStages { get; } = new();

        [ObservableProperty]
        private double hourlyRate = 50;

        public double TotalEarnings => WorkStages.Sum(ws => ws.Duration.TotalHours * HourlyRate);

        public MainViewModel(AddDbContext context)
        {
            _context = context;
            WorkStages.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalEarnings));
            LoadWorkStages();
        }

        private void LoadWorkStages()
        {
            WorkStages.Clear();
            foreach (var stage in _context.WorkStages)
            {
                WorkStages.Add(stage);
            }
            OnPropertyChanged(nameof(TotalEarnings));  
        }
    }
}
