//#define PL_FUNCTIONAL_TESTING
//#define PL_PERFORMANCE_TESTING -- Não foi coberta por nenhuma configuração de solution
//#define PL_DFS
//#define PL_HSI
//#define PL_WP -- Não foi coberta por nenhuma configuração de solution
//#define PL_GRAPH
//#define PL_FSM
//#define PL_LR -- Não foi coberta por nenhuma configuração de solution
//#define PL_OATS
//#define PL_MTM
//#define PL_XMI

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml;
using Lesse.Core.ControlStructure;
using Lesse.Core.ControlUnit;
using Lesse.Core.ProductControlUnit;
using Lesse.OATS.OATSProductControlUnit;
using Microsoft.Win32;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using RichTextBox = System.Windows.Controls.RichTextBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Lesse.App.PLeTs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region CLASS ATTRIBUTES
        public enum ErrorLevel
        {
            Critical,
            Warning,
            Message,
            Green
        }

        private ControlUnit control;
        private Boolean fatalError = false;
        private String value = "";
        private String parserType = "";
        private StructureType type;

        private static TextBlock textBlockStatus;
        private static String CurrentPath;
        private static RichTextBox textBlockLog;
        #endregion

        private Dictionary<String, ProductControlUnit> productControllers;
        private ProductControlUnit selectedProduct;



        public MainWindow()
        {
            productControllers = new Dictionary<string, ProductControlUnit>();
#if PL_OATS
            productControllers.Add("OATS", new OATSControlUnit());
#endif

            InitializeComponent();
            MainWindow.SetInstance(this);
            MainWindow.CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            MainWindow.LogAppend("Ready!", ErrorLevel.Green);
            ButtonsInitialization();

            SetProductOption();
        }


        private void SetProductOption()
        {
            foreach (var item in productControllers)
            {
                Product.Items.Add(item);
            }

            Product.Items.Refresh();

            if (productControllers.Count == 1)
            {
                Product.SelectedIndex = 0;
                Product.IsEnabled = false;
            }
        }

        private void SetParserOptions()
        {
            if (Parser != null)
            {
                PopulateCombobox(Parser, selectedProduct.GetParserOptions());
            }
        }

        private void SetSequenceGeneratorOptions()
        {
            Dictionary<Enum, String> options = selectedProduct.GetSequenceGeneratorOptions();

            if (options == null || options.Count == 0)
            {
                SequenceGeneratorType.IsEnabled = false;
                buttonGenerateTestCases.IsEnabled = false;
                GenerateFile.IsEnabled = true;

                SetExportOptions();
            }
            else
            {
                SequenceGeneratorType.IsEnabled = true;
                PopulateCombobox(SequenceGeneratorType, options);
            }
        }

        private void SetExportOptions()
        {
            if (GenerateFile != null)
            {
                PopulateCombobox(GenerateFile, selectedProduct.GetExportOptions(GetSelectedParseType()));
            }
        }

        private void ButtonsInitialization()
        {
            buttonLoadData.IsEnabled = false;
            buttonGenerateTestCases.IsEnabled = false;
            GenerateFile.IsEnabled = false;
            buttonXmiExport.IsEnabled = false;
        }

        private void SetTitle()
        {
#if PL_FUNCTIONAL_TESTING
            this.Title = "Functional Testing Tool for ";
#elif PL_PERFORMANCE_TESTING
            this.Title = "Performance Testing Tool for ";
#endif

            //adds build number to title bar
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            this.Title += (" - v" + v.Major + "." + v.Minor);//+ " " + v.Build + "." + v.Revision);
            this.buttonClearLog_Click(null, null);
        }

        #region View related methods
        /// <summary>
        /// Singleton implementation.
        /// </summary>
        private static void SetInstance(MainWindow mainWindow)
        {
            MainWindow.textBlockLog = mainWindow.textBlockLogContainer;
            MainWindow.textBlockStatus = mainWindow.textBlockStatusContainer;
        }

        /// <summary>
        /// Update status bar information.
        /// </summary>
        private static void SetStatus(String p)
        {
            MainWindow.textBlockStatus.Text = p;
        }


        /// <summary>
        /// Appends a message to log window.
        /// </summary>
        public static void LogAppend(String s)
        {
            MainWindow.LogAppend(s, ErrorLevel.Message);
        }

        /// <summary>
        /// Appends a message to log window.
        /// </summary>
        public static void LogAppend(String s, MainWindow.ErrorLevel level)
        {
            //as users are unforeseen beings, we need to have sure that the pointer is at the log's end
            MainWindow.textBlockLog.CaretPosition = MainWindow.textBlockLog.Document.ContentEnd;
            MainWindow.textBlockLog.ScrollToEnd();
            TextRange range = new TextRange(textBlockLog.CaretPosition, textBlockLog.Document.ContentEnd);

            //adjust coloring according to log level.
            switch (level)
            {
                case ErrorLevel.Critical:
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.OrangeRed);
                    break;
                case ErrorLevel.Warning:
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Brown);
                    break;
                case ErrorLevel.Green:
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green);
                    break;
                default:
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
                    break;
            }

            //appends a new line to current document.
            MainWindow.textBlockLog.AppendText("[" + DateTime.Now.ToShortTimeString() + "] " + s + Environment.NewLine);
            MainWindow.textBlockLog.CaretPosition = MainWindow.textBlockLog.Document.ContentEnd;
            MainWindow.textBlockLog.ScrollToEnd();
        }

        /// <summary>
        /// Save log to file.
        /// </summary>
        private void buttonSaveLog_Click(Object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text file (*.txt) | *.txt";

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            TextRange textRange = new TextRange(
               this.textBlockLogContainer.Document.ContentStart,//TextPointer to the start of content in the RichTextBox.
               this.textBlockLogContainer.Document.ContentEnd   //TextPointer to the end of content in the RichTextBox.
            );
            File.WriteAllText(dialog.FileName, textRange.Text);
        }

        /// <summary>
        /// Clears log window.
        /// </summary> 
        private void buttonClearLog_Click(Object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(
               this.textBlockLogContainer.Document.ContentStart,//TextPointer to the start of content in the RichTextBox.
               this.textBlockLogContainer.Document.ContentEnd   //TextPointer to the end of content in the RichTextBox.
            );

            this.textBlockLogContainer.Document.Blocks.Clear();
            MainWindow.LogAppend("Waiting for file containing test data.\n");
        }

        /// <summary>
        /// Opens configuration file.
        /// </summary>
        private void buttonConfigure_Click(Object sender, RoutedEventArgs e)
        {
            Process p = new Process();

            ProcessStartInfo i = new ProcessStartInfo();
            i.Arguments = System.IO.Path.Combine(MainWindow.CurrentPath + "\\Configuration.cfg");
            i.FileName = "notepad.exe";

            p.StartInfo = i;
            p.Start();
        }

        /// <summary>
        /// Quits.
        /// </summary>
        private void buttonClose_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Selection Change methods
        private void ProductType_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if ((Product.SelectedItem != null || GetSelectedProduct() != null))
            {
                productControllers.TryGetValue(GetSelectedProduct(), out selectedProduct);
                Parser.IsEnabled = true;
                SequenceGeneratorType.IsEnabled = false;
                GenerateFile.IsEnabled = false;
                buttonLoadData.IsEnabled = false;
                buttonGenerateTestCases.IsEnabled = false;


                SetParserOptions();

                MainWindow.LogAppend("Select an import file to continue.", ErrorLevel.Green);
            }
        }

        private void Parser_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (Parser != null && Parser.SelectedItem != null)
            {
                Parser.IsEnabled = true;
                SequenceGeneratorType.IsEnabled = false;
                GenerateFile.IsEnabled = false;
                buttonLoadData.IsEnabled = true;
                buttonGenerateTestCases.IsEnabled = false;

                SetSequenceGeneratorOptions();

                MainWindow.LogAppend("Select the file you want to parse.", ErrorLevel.Green);
            }
        }

        private void SequenceGeneratorType_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (SequenceGeneratorType != null && SequenceGeneratorType.SelectedItem != null)
            {
                Parser.IsEnabled = true;
                GenerateFile.IsEnabled = false;
                buttonLoadData.IsEnabled = true;
                buttonGenerateTestCases.IsEnabled = true;
            }
        }



        private void GenerateFile_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            String path = "";

            if (selectedProduct == null || GenerateFile.SelectedItem == null)
            {
                return;
            }

            if (selectedProduct.ExportIsFile(GetSelectedExportType()))
            {
                path = GetExportPathForFile();
            }
            else
            {
                path = GetExportPathForFolder();
            }

            try
            {
                if (path == null || path == "")
                {
                    return;
                }

                selectedProduct.SaveResult(GetSelectedExportType(), path);

                MainWindow.LogAppend("Result exported successfully to " + path);

                GenerateFile.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MainWindow.LogAppend("Couldn't export result: " + ex.Message, ErrorLevel.Critical);
                //TODO: block buttons
                return;
            }
        }
        #endregion

        #region Button click methods
        private void buttonLoadData_Click(Object sender, RoutedEventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = selectedProduct.GetImportFileFilter(GetSelectedParseType());

            if (dialog.ShowDialog() != true)
            {
                return;
            }

            MainWindow.LogAppend("Selected file: "+dialog.FileName);
            MainWindow.LogAppend("Initializing file parsing...", ErrorLevel.Green);

            try
            {
                selectedProduct.ParseFile(GetSelectedParseType(), dialog.FileName);
                //TODO: call validate

                SetSequenceGeneratorOptions();

                MainWindow.LogAppend("File successfully parsed!", ErrorLevel.Green);
                MainWindow.LogAppend("Generate test cases if necessary or select an export option.", ErrorLevel.Green);
            }
            catch (IOException ioe)
            {
                SequenceGeneratorType.IsEnabled = false;
                GenerateFile.IsEnabled = false;
                buttonXmiExport.IsEnabled = true;
                MainWindow.LogAppend(ioe.Message, ErrorLevel.Critical);
                MainWindow.LogAppend("Correlation file is being used by another process. Please close it and reload the XMI file.", ErrorLevel.Critical);
            }
            catch (Exception ex)
            {
                MainWindow.LogAppend("[ERROR] " + ex.Message, ErrorLevel.Critical);
                SequenceGeneratorType.IsEnabled = false;
                GenerateFile.IsEnabled = false;
                buttonXmiExport.IsEnabled = true;
            }
        }

        private void buttonGenerateTestCases_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                selectedProduct.GenerateSequence(GetSelectedParseType());

                GenerateFile.IsEnabled = true;
                buttonGenerateTestCases.IsEnabled = false;
                buttonXmiExport.IsEnabled = false;

                MainWindow.LogAppend(control.TestCaseCount + " test cases have been generated.", ErrorLevel.Message);
                MainWindow.LogAppend("There are test cases ready to be generated. Press {Export...} to proceed.", ErrorLevel.Green);
            }
            catch (Exception ex)
            {
                MainWindow.LogAppend("Error generating test plans: " + ex.Message, ErrorLevel.Critical);

                buttonGenerateTestCases.IsEnabled = false;
                buttonXmiExport.IsEnabled = false;
            }
        }
        #endregion


        private Enum GetSelectedParseType()
        {
            try
            {

                return ((KeyValuePair<Enum, string>)Parser.SelectedItem).Key;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Enum GetSelectedExportType()
        {
            try
            {

                return ((KeyValuePair<Enum, string>)GenerateFile.SelectedItem).Key;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private String GetSelectedProduct()
        {
            try
            {
                return ((KeyValuePair<String, ProductControlUnit>)Product.SelectedItem).Key;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void PopulateCombobox(ComboBox comboBox, Dictionary<Enum, String> values)
        {
            comboBox.Items.Clear();

            foreach (var item in values)
            {
                comboBox.Items.Add(item);
            }

            comboBox.Items.Refresh();
        }

        private String GetExportPathForFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = selectedProduct.GetExportFileFilter(GetSelectedExportType());

            if (dialog.ShowDialog() != true)
            {
                return null;
            }

            return dialog.FileName;
        }

        private String GetExportPathForFolder()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Choose destination folder";
            dlg.ShowNewFolderButton = true;
            dlg.Reset();
            dlg.SelectedPath = Environment.CurrentDirectory;

            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                if (new DirectoryInfo(dlg.SelectedPath).GetFiles().Length > 0)
                {
                    if (MessageBox.Show("The selected folder is not empty! Some files may be overrited! Do you want to continue?",
                        "Confirm action", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return null;
                    }
                }
                return dlg.SelectedPath;
            }

            return null;
        }

        private void Validate(String filename, Boolean parsedSuccessfully)
        {
            if (parsedSuccessfully)
            {
                MainWindow.LogAppend("Validating...");

                List<KeyValuePair<String, Int32>> errors = control.ValidateModel(filename);

                int mess = 0, warn = 0;
                foreach (KeyValuePair<String, Int32> error in errors)
                {
                    if (error.Value == 3) //error
                    {
                        MainWindow.LogAppend(error.Key, ErrorLevel.Critical);
                        mess++;
                    }
                    else //warning
                    {
                        MainWindow.LogAppend(error.Key, ErrorLevel.Warning);
                        warn++;
                    }
                }
                if (mess > 0)
                {
                    buttonGenerateTestCases.IsEnabled = false;
                    GenerateFile.IsEnabled = false;
                    buttonXmiExport.IsEnabled = true;
                    //buttonParseXMItoXLS.IsEnabled = false;
                    //buttonMTM.IsEnabled = false;
                    //buttonLoadRunner.IsEnabled = false;   
                    fatalError = true;
                }
                else if (warn > 0)
                {
                    //MainWindow.LogAppend("The next steps may not generate the expected results.", ErrorLevel.Warning);
                    buttonGenerateTestCases.IsEnabled = true;
                    GenerateFile.IsEnabled = false;
#if PL_OATS
                    GenerateFile.IsEnabled = true;
#endif
                    buttonXmiExport.IsEnabled = true;
                    //buttonParseXMItoXLS.IsEnabled = true;
                    //buttonMTM.IsEnabled = false;
                    //buttonLoadRunner.IsEnabled = false;
                    fatalError = false;
                }
                else
                {
                    MainWindow.LogAppend("Parsing finished without issues.", ErrorLevel.Green);
                    buttonGenerateTestCases.IsEnabled = false;

                    switch (parserType)
                    {
#if PL_OATS
                        case "Script JAVA":
                            buttonGenerateTestCases.IsEnabled = false;
                            GenerateFile.IsEnabled = true;
                            break;
                        case "Astah XML":
                            buttonGenerateTestCases.IsEnabled = false;
                            GenerateFile.IsEnabled = true;
                            break;
#elif PL_XMI
#if PL_JUNIT
                        case "Astah SeqDiag XML":
                            buttonGenerateTestCases.IsEnabled = true;
                            GenerateFile.IsEnabled = false;
                            break;
#else
                        case "Astah XML":
                            buttonGenerateTestCases.IsEnabled = true;
                            GenerateFile.IsEnabled = false;
                            break;
                        case "Argo XML":
                            buttonGenerateTestCases.IsEnabled = true;
                            GenerateFile.IsEnabled = false;
                            break;
#endif
#if PL_LR
                        case "LoadRunnerToXMI":
                            buttonGenerateTestCases.IsEnabled = false;
                            break;
#endif
#endif
                    }
                    buttonXmiExport.IsEnabled = true;
                    //buttonParseXMItoXLS.IsEnabled = true;
                    //buttonMTM.IsEnabled = false;
                    //buttonLoadRunner.IsEnabled = false;
                    fatalError = false;
                }
            }
            else
            {
                MainWindow.LogAppend("[ERROR] Some critical error was found while parsing XMI file.", ErrorLevel.Critical);
                buttonGenerateTestCases.IsEnabled = false;
                GenerateFile.IsEnabled = false;
                //buttonParseXMItoXLS.IsEnabled = false;
                //buttonMTM.IsEnabled = false;
                //buttonLoadRunner.IsEnabled = false;
                buttonXmiExport.IsEnabled = false;
                return;
            }
        }
    }
}