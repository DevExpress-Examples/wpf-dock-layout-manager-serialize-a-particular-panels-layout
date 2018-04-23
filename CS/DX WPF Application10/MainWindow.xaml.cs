using System;
using System.IO;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;

namespace DXWpfApplication {
    public partial class MainWindow : DXWindow {
        const string LayoutPath = "layout.xml";
        const string AppName = "DXWpfApplication";
        //
        public MainWindow() {
            InitializeComponent();
            targetPanel.SetValue(DXSerializer.SerializationProviderProperty, new PanelSerializationProvider());
        }
        void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e) {
            DXSerializer.SerializeSingleObject(targetPanel, LayoutPath, AppName);
        }
        void ButtonRestore_Click(object sender, System.Windows.RoutedEventArgs e) {
            DXSerializer.DeserializeSingleObject(targetPanel, LayoutPath, AppName);
        }
    }
    class PanelSerializationProvider : SerializationProvider {
        protected override string GetSerializationID(DependencyObject dObj) {
            return "customPanelProperties";
        }
        protected override void OnStartSerializing(DependencyObject dObj) {
            LayoutPanel panel = (LayoutPanel)dObj;
            using(MemoryStream ms = new MemoryStream()) {
                DXSerializer.Serialize(panel.Control, ms, "customLayout", new DXOptionsLayout());
                ms.Seek(0, SeekOrigin.Begin);
                panel.Tag = Convert.ToBase64String(ms.ToArray());
            }
            base.OnStartSerializing(dObj);
        }
        protected override void OnEndSerializing(DependencyObject dObj) {
            base.OnEndSerializing(dObj);
            ((LayoutPanel)dObj).Tag = null;
        }
        protected override void OnEndDeserializing(DependencyObject dObj, string restoredVersion) {
            base.OnEndDeserializing(dObj, restoredVersion);
            LayoutPanel panel = (LayoutPanel)dObj;
            string layout = panel.Tag as string;
            if(!string.IsNullOrEmpty(layout)) {
                using(MemoryStream ms = new MemoryStream(Convert.FromBase64String(layout))) {
                    DXSerializer.Deserialize(panel.Control, ms, "customLayout", new DXOptionsLayout());
                }
            }
            panel.Tag = null;
        }
        protected override void OnCustomGetSerializableProperties(DependencyObject dObj, CustomGetSerializablePropertiesEventArgs e) {
            e.SetPropertySerializable("Tag", new DXSerializable());
        }
    }
    class TestData {
        public string Text { get; set; }
        public int Number { get; set; }
    }
}