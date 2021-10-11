﻿using EnduranceJudge.Application.Aggregates.Import;
using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using EnduranceJudge.Gateways.Desktop.Services;
using Prism.Commands;
using Prism.Regions;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Import
{
    public class ImportViewModel : ViewModelBase
    {
        private readonly IApplicationContext context;
        private readonly IStorageInitializer storageInitializer;
        private readonly IImportService importService;
        private readonly IExplorerService explorer;
        private readonly INavigationService navigation;

        public ImportViewModel(
            IApplicationContext context,
            IStorageInitializer storageInitializer,
            IImportService importService,
            IExplorerService explorer,
            INavigationService navigation)
        {
            this.context = context;
            this.storageInitializer = storageInitializer;
            this.importService = importService;
            this.explorer = explorer;
            this.navigation = navigation;
            this.OpenFolderDialog = new DelegateCommand(this.OpenFolderDialogAction);
            this.OpenImportFileDialog = new DelegateCommand(this.OpenImportFileDialogAction);
        }

        public DelegateCommand OpenFolderDialog { get; }
        public DelegateCommand OpenImportFileDialog { get; }

        private string workDirectoryPath;
        private string importFilePath;
        private Visibility workDirectoryVisibility = Visibility.Visible;
        private Visibility importFilePathVisibility = Visibility.Hidden;

        public override void OnNavigatedTo(NavigationContext context)
        {
            base.OnNavigatedTo(context);
            if (this.context.IsInitialized)
            {
                this.WorkDirectoryVisibility = Visibility.Collapsed;
                this.ImportFilePathVisibility = Visibility.Visible;
            }
        }

        public string WorkDirectoryPath
        {
            get => this.workDirectoryPath;
            private set => this.SetProperty(ref this.workDirectoryPath, value);
        }
        public Visibility WorkDirectoryVisibility
        {
            get => this.workDirectoryVisibility;
            set => this.SetProperty(ref this.workDirectoryVisibility, value);
        }
        public string ImportFilePath
        {
            get => this.importFilePath;
            set => this.SetProperty(ref this.importFilePath, value);
        }
        public Visibility ImportFilePathVisibility
        {
            get => this.importFilePathVisibility;
            set => this.SetProperty(ref this.importFilePathVisibility, value);
        }

        private void OpenFolderDialogAction()
        {
            var selectedPath = this.explorer.SelectDirectory();
            if (selectedPath == null)
            {
                return;
            }
            this.WorkDirectoryPath = selectedPath;

            this.WorkDirectoryVisibility = Visibility.Collapsed;
            this.ImportFilePathVisibility = Visibility.Visible;

            var result = this.storageInitializer.Initialize(selectedPath);
            this.context.Initialize();

            if (result.IsExistingFile)
            {
                this.Redirect();
            }
        }

        private void OpenImportFileDialogAction()
        {
            var path = this.explorer.SelectFile();
            if (path == null)
            {
                return;
            }

            this.ImportFilePath = path;
            this.importService.Import(path);

            this.context.Initialize();
            this.Redirect();
        }

        private void Redirect()
        {
            this.navigation.NavigateToEvent();
        }
    }
}
