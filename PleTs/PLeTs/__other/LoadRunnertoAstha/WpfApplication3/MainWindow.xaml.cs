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
using Microsoft.Win32;
using System.Xml;
using Coc.Modeling.Uml;
using Coc.Data.Xmi;
using Coc.Data.ControlAndConversionStructures;

//using LoadRunnertoAstha;
namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String path1;
        private String path2;
        private LRToXMI lr;
        private static ListBox textBlockLog = null;

        public MainWindow()
        {
            InitializeComponent();
            MainWindow.SetInstance(this); //cuidar a ordem dos métodos
            lr = new LRToXMI();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Open .C project files (*.c) |*.c;";

            if (dialog.ShowDialog() != true)
            {
                listBox1.Items.Add("[ERROR] The file .c has not been processed");
                path1 = null;
                return;
            }
            else
            {
                path1 = dialog.FileName;
                listBox1.Items.Add(dialog.FileName);
                listBox1.Items.Add(".C file was selected correctly");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Open .PRM project files (*.prm) |*.prm;";

            if (dialog.ShowDialog() != true)
            {
                listBox1.Items.Add("[ERROR] The file .prm has not been processed");
                path2 = null;
                return;
            }
            else
            {
                path2 = dialog.FileName;
                listBox1.Items.Add(dialog.FileName);
                listBox1.Items.Add(" .PRM file was selected correctly");
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            GeneralUseStructure model1 = new UmlModel("");
            List<GeneralUseStructure> listModel1 = new List<GeneralUseStructure>();

            if (path1 != null)
            {
                if (path2 != null)
                {
                    model1 = lr.Converter(path1, path2);
                    listModel1.Add(model1);
                    listBox1.Items.Add("Parsing...");
                    listBox1.Items.Add("Parser finished successfully");
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "XML file (*.xml) | *.xml";

                    if (dialog.ShowDialog() != true)
                    {
                        return;
                    }

                    XmlWriterSettings settings = new XmlWriterSettings();
                    XmiExporter export1 = new XmiExporter();
                    settings.Encoding = new UTF8Encoding(false);
                    settings.Indent = true;
                    settings.CheckCharacters = true;
                    XmlDocument document = export1.ToXmi(listModel1);
                    XmlWriter writer = XmlWriter.Create(dialog.FileName, settings);
                    document.Save(writer);
                    listBox1.Items.Add("File destination: " + dialog.FileName);
                    path1 = null;
                    path2 = null;
                }
                else
                {
                    GeneralUseStructure model2 = new UmlModel("");
                    List<GeneralUseStructure> listModel2 = new List<GeneralUseStructure>();
                    model2 = lr.Converter(path1);
                    listModel2.Add(model2);
                    listBox1.Items.Add("Parsing...");
                    listBox1.Items.Add("Parser finished successfully");
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "XML file (*.xml) | *.xml";

                    if (dialog.ShowDialog() != true)
                    {
                        return;
                    }

                    XmlWriterSettings settings = new XmlWriterSettings();
                    XmiExporter export2 = new XmiExporter();
                    settings.Encoding = new UTF8Encoding(false);
                    settings.Indent = true;
                    settings.CheckCharacters = true;
                    XmlDocument document = export2.ToXmi(listModel2);
                    XmlWriter writer = XmlWriter.Create(dialog.FileName, settings);
                    document.Save(writer);
                    listBox1.Items.Add("File destination: " + dialog.FileName);
                    path1 = null;
                    path2 = null;
                }
            }
            else
            {
                listBox1.Items.Add("[ERROR]the files were not loaded correctly");
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {

        }

        private static void SetInstance(MainWindow mainWindow)
        {
            MainWindow.textBlockLog = mainWindow.listBox1;    //só mudar para o nome da tela
        }


    }




}

