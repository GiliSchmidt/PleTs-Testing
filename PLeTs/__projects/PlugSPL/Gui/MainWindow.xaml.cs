using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlugSpl.Gui;
using PlugSpl.Modules;
using System.Reflection;
using System.IO;

namespace PlugSpl {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window {

        /// <summary>
        /// Stores error message for unloaded components.
        /// </summary>
        private string messageError = string.Empty;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow() {

            InitializeComponent();            

            //loads configuration file.
            ConfigurationSet c = ConfigurationSet.GetInstance();
            c.LoadConfiguration();

            //try to load configured modules.
            string message = String.Empty;
            foreach(Module m in c.Modules){
                if(!this.LoadModule(m)){
                    message += "- " + m.Name + "\n";
                }
            }

            //display modules which cannot be loaded.
            if(message != String.Empty)
                this.messageError = "The following modules cannot be loaded by PlugSPL:\n" + message;

            //displays the splashscreen by 1 seconds.
            System.Threading.Thread.Sleep(1000); 
        }

        /// <summary>
        /// Loads a module into main application.
        /// </summary>
        private bool LoadModule(Module m) {

            if(!File.Exists(m.Assembly)){
                return false;
            }

            Assembly asm = Assembly.LoadFrom(m.Assembly);
            Type t = asm.GetType(m.Type);

            if(t == null){
                return false;
            }
            
            Control control = (Control)Activator.CreateInstance(t);
            switch(m.Interface){
                case "IFeatureModelEditor":
                    this.gridFeatureModelContainer.Children.Add(control);
                    ((IFeatureModelEditor)control).SetApplicationWindow(this);
                    break;
                case "IUmlComponentDiagramEditor":
                    this.gridComponentDiagramContainer.Children.Add(control);
                    ((IUmlComponentDiagramEditor)control).SetApplicationWindow(this);
                    break;
                case "IComponentPoolManager":
                    this.gridComponentPoolContainer.Children.Add(control);
                    ((IComponentPoolManager)control).SetApplicationWindow(this);
                    break;
                case "IProductConfigurator":
                    this.gridProductconfiguratorContainer.Children.Add(control);
                    ((IProductConfigurator)control).SetApplicationWindow(this);
                    break;
                case "IProductInstantiator":
                    this.gridProductInstantiatorContainer.Children.Add(control);
                    ((IProductInstantiator)control).SetApplicationWindow(this);
                    break;
            }
            
            return true;
        }

        /// <summary>
        /// Triggers when tabControlModules tab changes (switch through modules)
        /// </summary>
        private void tabControlModules_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            
            //colors the tab
            (tabItemDesign.Header as TextBlock).Background = (tabItemDesign.IsSelected) ? new SolidColorBrush(Color.FromRgb(0x73, 0xA3, 0xC4)) : tabItemWelcome.Background;
            (tabItemConfiguration.Header as TextBlock).Background = (tabItemConfiguration.IsSelected) ? new SolidColorBrush(Color.FromRgb(0x00, 0xAA, 0x66)) : tabItemWelcome.Background;
            (tabItemInstantiation.Header as TextBlock).Background = (tabItemInstantiation.IsSelected) ? new SolidColorBrush(Colors.Orange) : tabItemWelcome.Background;
            (tabItemComponentPool.Header as TextBlock).Background = (tabItemComponentPool.IsSelected) ? new SolidColorBrush(Colors.Brown) : tabItemWelcome.Background;
            //colors tab caption
            (tabItemDesign.Header as TextBlock).Foreground = (tabItemDesign.IsSelected) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Blue);
            (tabItemConfiguration.Header as TextBlock).Foreground = (tabItemConfiguration.IsSelected) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Green);
            (tabItemInstantiation.Header as TextBlock).Foreground = (tabItemInstantiation.IsSelected) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.DarkOrange);
            (tabItemComponentPool.Header as TextBlock).Foreground = (tabItemComponentPool.IsSelected) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Brown);

            //change margins
            (tabItemDesign.Header as TextBlock).Margin = (tabItemDesign.IsSelected) ? new Thickness(-8,-4,-8,-4) : new Thickness(0);
            (tabItemConfiguration.Header as TextBlock).Margin = (tabItemConfiguration.IsSelected) ? new Thickness(-8,-4,-8,-4) : new Thickness(0);
            (tabItemInstantiation.Header as TextBlock).Margin = (tabItemInstantiation.IsSelected) ? new Thickness(-8,-4,-8,-4) : new Thickness(0);
            (tabItemComponentPool.Header as TextBlock).Margin = (tabItemComponentPool.IsSelected) ? new Thickness(-8,-4,-8,-4) : new Thickness(0);

            //change margins
            (tabItemDesign.Header as TextBlock).Padding = (tabItemDesign.IsSelected) ? new Thickness(5) : new Thickness(0);
            (tabItemConfiguration.Header as TextBlock).Padding = (tabItemConfiguration.IsSelected) ? new Thickness(5) : new Thickness(0);
            (tabItemInstantiation.Header as TextBlock).Padding = (tabItemInstantiation.IsSelected) ? new Thickness(5) : new Thickness(0);
            (tabItemComponentPool.Header as TextBlock).Padding = (tabItemComponentPool.IsSelected) ? new Thickness(5) : new Thickness(0);

            //changes visibility of diagram selection controls when tab swapping occurs

            if(this.buttonChangeDiagram != null)
                this.buttonChangeDiagram.Visibility = (tabItemDesign.IsSelected)
                     ? System.Windows.Visibility.Visible
                     : System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Display error message (if exist)
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            if(this.messageError != String.Empty)
                MessageBox.Show(this.messageError, "PlugSPL Warning: Unable to load modules", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        /// <summary>
        /// Switches between diagrams
        /// </summary>
        private void buttonChangeDiagram_Click(object sender, RoutedEventArgs e) {
            
            if(gridFeatureModelContainer.Visibility == System.Windows.Visibility.Visible){
                gridFeatureModelContainer.Visibility = System.Windows.Visibility.Collapsed;
                gridComponentDiagramContainer.Visibility = System.Windows.Visibility.Visible;
                buttonChangeDiagram.Text = "Click (or press TAB) to switch to FODA Feature Model editor";
            }else{
                gridFeatureModelContainer.Visibility = System.Windows.Visibility.Visible;
                gridComponentDiagramContainer.Visibility = System.Windows.Visibility.Collapsed;
                buttonChangeDiagram.Text = "Click (or press TAB) to switch to UML Component Diagram editor";
            }
        }

        
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Tab)
                this.buttonChangeDiagram_Click(null, null);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {

        }
    }
}
