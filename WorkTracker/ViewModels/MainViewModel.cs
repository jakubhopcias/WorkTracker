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
using System.ComponentModel;

namespace WorkTracker.ViewModels
{
    public partial class MainViewModel : ObservableObject, INotifyPropertyChanged
    {

        private readonly AddDbContext _context;

        public ObservableCollection<WorkStage> WorkStages { get; } = new();

        
        private double _hourlyRate;
        public double HourlyRate { get => _hourlyRate; set { _hourlyRate = value; OnPropertyChanged(); OnPropertyChanged(nameof(TotalEarnings)); } }

        public double TotalEarnings => WorkStages.Sum(ws => ws.Duration.TotalHours * _hourlyRate);

        public MainViewModel(AddDbContext context)
        {
            _context = context;
            WorkStages.CollectionChanged += (s, e) => OnPropertyChanged(nameof(TotalEarnings));
            LoadWorkStages();
        }

        public void LoadWorkStages()
        {
            WorkStages.Clear();
            foreach (var stage in _context.WorkStages)
            {
                WorkStages.Add(stage);
            }
            OnPropertyChanged(nameof(TotalEarnings));
        }
        public void AddWorkStage(WorkStage workStage)
        {
            _context.WorkStages.Add(workStage);
            _context.SaveChanges();
            WorkStages.Add(workStage);
            OnPropertyChanged(nameof(TotalEarnings));
        }
        public void RemoveWorkStage(WorkStage workStage)
        {
            _context.WorkStages.Remove(workStage);
            _context.SaveChanges();
            WorkStages.Remove(workStage);
            OnPropertyChanged(nameof(TotalEarnings));
        }
        public void UpdateWorkStage(WorkStage updatedWorkStage)
        {
            var existingStage = _context.WorkStages.Find(updatedWorkStage.Id);
            if (existingStage == null) return;

            existingStage.Name = updatedWorkStage.Name;
            existingStage.StartTime = updatedWorkStage.StartTime;
            existingStage.EndTime = updatedWorkStage.EndTime;

            _context.SaveChanges();
            OnPropertyChanged(nameof(TotalEarnings));
        }

    }
}
